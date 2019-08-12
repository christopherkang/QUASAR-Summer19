﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Quantum.Chemistry.Paulis;

namespace Microsoft.Quantum.Chemistry.Fermion
{
    using static Microsoft.Quantum.Chemistry.Extensions;
    /// <summary>
    /// Extensions for converting fermion terms to Pauli terms.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Method for constructing a Pauli Hamiltonian from a fermion Hamiltonina.
        /// </summary>
        /// <param name="sourceHamiltonian">Input orbital integral Hamiltonian.</param>
        /// <param name="encoding">Identifies how the terms should be encoded on the qubits.</param>
        /// <returns>Fermion Hamiltonian constructed from orbital integrals.</returns>
        public static PauliHamiltonian ToPauliHamiltonian(
            this FermionHamiltonian sourceHamiltonian,
            QubitEncoding encoding = QubitEncoding.JordanWigner)
        {
            var nFermions = sourceHamiltonian.SystemIndices.Max() + 1;
            var hamiltonian = new PauliHamiltonian();
            Func<FermionTerm, TermType.Fermion, double, IEnumerable<(PauliTerm, PauliTermValue)>> conversion =
                (fermionTerm, termType, coeff)
                => (fermionTerm.ToJordanWignerPauliTerms(termType, coeff));

            foreach (var termType in sourceHamiltonian.Terms)
            {
                foreach (var term in termType.Value)
                {
                    hamiltonian.AddRange(conversion(term.Key, termType.Key, term.Value.Value));
                }
            }
            // Create a copy of fermion indices hash set.
            hamiltonian.SystemIndices = new HashSet<int>(sourceHamiltonian.SystemIndices.ToList());
            return hamiltonian;
        }

        #region Create Jordan–Wigner Pauli terms from fermion terms.

        /// <summary>
        /// Creates all fermion terms generated by all symmetries of an orbital integral.
        /// </summary>
        /// <returns>List of fermion terms generated by all symmetries of an orbital integral.</returns>
        public static List<(PauliTerm, PauliTermValue)> ToJordanWignerPauliTerms(
            this FermionTerm fermionTerm,
            TermType.Fermion termType,
            double coeff)
        {
            var pauliTerms = new List<(PauliTerm, PauliTermValue)>();
            // Make a copy of the list of indices.
            var seq = fermionTerm.Sequence.Select(o => o.Index).ToArray();
            var string_output = $"{termType.ToString()} | {String.Join(",", seq.Select(p=>p.ToString()).ToArray())} | {coeff.ToString()}";
            Console.WriteLine(string_output);

            switch (termType)
            {
                case TermType.Fermion.Identity:
                    pauliTerms.Add((new PauliTerm(new int[] { }, TermType.PauliTerm.Identity), coeff.ToPauliTTermValue()));
                    break;

                case TermType.Fermion.PP:
                    pauliTerms.Add((new PauliTerm(new int[] { }, TermType.PauliTerm.Identity), (0.5 * coeff).ToPauliTTermValue()));
                    pauliTerms.Add((new PauliTerm(seq.Take(1), TermType.PauliTerm.Z), (-0.5 * coeff).ToPauliTTermValue()));
                    break;

                case TermType.Fermion.PQ:
                    pauliTerms.Add((new PauliTerm(seq, TermType.PauliTerm.PQ), (0.25 * coeff).ToPauliTTermValue()));
                    break;

                case TermType.Fermion.PQQP:
                    pauliTerms.Add((new PauliTerm(new int[] { }, TermType.PauliTerm.Identity), (0.25 * coeff).ToPauliTTermValue()));
                    pauliTerms.Add((new PauliTerm(seq.Take(2), TermType.PauliTerm.ZZ), (0.25 * coeff).ToPauliTTermValue()));
                    pauliTerms.Add((new PauliTerm(seq.Take(1), TermType.PauliTerm.Z), (-0.25 * coeff).ToPauliTTermValue()));
                    pauliTerms.Add((new PauliTerm(seq.Skip(1).Take(1), TermType.PauliTerm.Z), (-0.25 * coeff).ToPauliTTermValue()));
                    break;

                case TermType.Fermion.PQQR:
                    var multiplier = 1.0;
                    if (seq.First() == seq.Last())
                    {
                        // This means terms are ordered like QPRQ. So we reorder to PQQR
                        seq[0] = seq[1];
                        seq[1] = seq[3];
                        seq[3] = seq[2];
                        seq[2] = seq[1];
                    }
                    else if (seq[1] == seq[3])
                    {
                        // This means terms are ordered like PQRQ. So we reorder to PQQR
                        seq[3] = seq[2];
                        seq[2] = seq[1];
                        multiplier = -1.0;
                    }
                    pauliTerms.Add((new PauliTerm(seq, TermType.PauliTerm.PQQR), (-0.125 * multiplier * coeff).ToPauliTTermValue()));
                    // PQ term
                    pauliTerms.Add((new PauliTerm(new int[] { seq[0], seq[3] }, TermType.PauliTerm.PQ), (0.125 * multiplier * coeff).ToPauliTTermValue()));
                    break;
                case TermType.Fermion.PQRS:
                    var pqrsSorted = seq.ToList();
                    pqrsSorted.Sort();
                    var (newTerm, newCoeff) = IdentifyHpqrsPermutation((pqrsSorted, seq, coeff));
                    pauliTerms.Add((new PauliTerm(newTerm, TermType.PauliTerm.v01234), newCoeff.ToPauliTTermValue()));
                    break;
                default:
                    throw new System.NotImplementedException();
            }
            return pauliTerms;
        }


        /// <summary>
        /// Function for classifying PQRS terms that have the same set of
        /// spin-orbital indices, modulo the standard symmetries
        /// arrising from
        /// <list type="bullet">
        /// <description>
        /// <item>
        /// Orbitals are real.
        /// </item>
        /// <item>
        /// Electrons are indistinguishable.
        /// </item>
        /// </description>
        /// </list>
        /// </summary>
        static (List<int>, double[]) IdentifyHpqrsPermutation((List<int>, int[], double) term)
        {
            //We only consider permutations pqrs || psqr || prsq || qprs || spqr || prqs
            var (pqrsSorted, pqrsPermuted, coeff) = term;
            coeff = coeff * 0.0625;
            var h123 = new double[] { .0, .0, .0 };
            var v0123 = new double[] { .0, .0, .0, .0 };

            //Console.WriteLine($"{pqrsSorted}, {pqrsPermuted}");

            var prsq = new int[] { pqrsSorted[0], pqrsSorted[2], pqrsSorted[3], pqrsSorted[1] };
            var pqsr = new int[] { pqrsSorted[0], pqrsSorted[1], pqrsSorted[3], pqrsSorted[2] };
            var psrq = new int[] { pqrsSorted[0], pqrsSorted[3], pqrsSorted[2], pqrsSorted[1] };

            //Console.WriteLine($"{pqrs}, {psqr}, {prsq}, {qprs}, {spqr}, {prqs}");

            if (Enumerable.SequenceEqual(pqrsPermuted, prsq))
            {
                h123 = new double[] { 0.0, 0.0, coeff };
            }
            else if (Enumerable.SequenceEqual(pqrsPermuted, pqsr))
            {
                h123 = new double[] { -coeff, 0.0, 0.0 };
            }
            else if (Enumerable.SequenceEqual(pqrsPermuted, psrq))
            {
                h123 = new double[] { 0.0, -coeff, 0.0 };
            }
            else
            {
                h123 = new double[] { 0.0, 0.0, 0.0 };
            }

            v0123 = new double[] { -h123[0] - h123[1] + h123[2],
                                        h123[0] - h123[1] + h123[2],
                                        -h123[0] - h123[1] - h123[2],
                                        -h123[0] + h123[1] + h123[2] };

            // DEBUG for output h123
            //v0123 = h123;
            //v0123.Add(0.0);

            return (pqrsSorted, v0123);
        }
        #endregion
    }
}


