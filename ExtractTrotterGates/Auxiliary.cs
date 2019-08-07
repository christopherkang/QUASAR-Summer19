// modified code from the Chemistry/src/DataModel/Fermion/JordanWignerEncoding.cs
// to print out term information directly without requiring overwriting QuantumLibraries

using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Quantum.Simulation.Core;

using Microsoft.Quantum.Chemistry.Broombridge;
using Microsoft.Quantum.Chemistry.OrbitalIntegrals;
using Microsoft.Quantum.Chemistry.Fermion;
using Microsoft.Quantum.Chemistry.Paulis;
using QSharpFormat = Microsoft.Quantum.Chemistry.QSharpFormat;
using Microsoft.Quantum.Chemistry.JordanWigner;
using Microsoft.Quantum.Chemistry.Generic;
using Microsoft.Quantum.Chemistry;

namespace ExtractTrotterGates
{
    public static class Auxiliary
    {
        public static void ToQSharpFormat(
            ProblemDescription problem,
            string state = "",
            IndexConvention indexConvention = IndexConvention.UpDown,
            QubitEncoding qubitEncoding = QubitEncoding.JordanWigner
            )
        {
            var fermionHamiltonian = problem
                .OrbitalIntegralHamiltonian
                .ToFermionHamiltonian(indexConvention);

            Auxiliary.ToPauliHamiltonian(fermionHamiltonian, qubitEncoding);
        }

        public static void ToPauliHamiltonian(
            FermionHamiltonian sourceHamiltonian,
            QubitEncoding encoding = QubitEncoding.JordanWigner)
        {
            foreach (var termType in sourceHamiltonian.Terms)
            {
                foreach (var term in termType.Value)
                {
                    Auxiliary.ToJordanWignerPauliTerms(term.Key, termType.Key, term.Value.Value);
                }
            }
        }

        public static void ToJordanWignerPauliTerms(
            FermionTerm fermionTerm,
            TermType.Fermion termType,
            double coeff)
        {
            var seq = fermionTerm.Sequence.Select(o => o.Index).ToArray();
            var string_output = $"{termType.ToString()} | {String.Join(",", seq.Select(p=>p.ToString()).ToArray())} | {coeff.ToString()}";
            Console.WriteLine(string_output);
        }
    }
}