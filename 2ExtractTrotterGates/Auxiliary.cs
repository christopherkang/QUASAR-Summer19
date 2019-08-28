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
using System.Reflection;
using System.Reflection.Emit;

namespace ExtractTrotterGates
{
    public static class Auxiliary
    {
        // Takes in a standard Q# format and outputs interpretable data to text
        // Input: Problem, state, indexConvention (default), qubitEncoding (default)
        // Output: No output, see ToPauliHamiltonian for generated filed
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

        // Creates the file with fermion terms
        // Input: FermionHamiltonian, encoding type (default)
        // Output: Writes the fermion terms to a file
        public static void ToPauliHamiltonian(
            FermionHamiltonian sourceHamiltonian,
            QubitEncoding encoding = QubitEncoding.JordanWigner)
        {
            var lines = new List<String>();
            foreach (var termType in sourceHamiltonian.Terms)
            {
                foreach (var term in termType.Value)
                {
                    lines.Add(Auxiliary.ToJordanWignerPauliTerms(term.Key, termType.Key, term.Value.Value));
                }
            }
            System.IO.File.WriteAllLines("./_temp/_FermionTerms.txt", lines);
        }

        // Extracts the JW terms from the Hamiltonian
        // Input: Individual fermionTerm, type of the term, coefficient
        // Output: String describing the term information

        public static String ToJordanWignerPauliTerms(
            FermionTerm fermionTerm,
            TermType.Fermion termType,
            double coeff)
        {
            // Console.WriteLine(fermionTerm.ToString());
            var seq = fermionTerm.Sequence.Select(o => o.Index).ToArray();
            var string_output = $"{termType.ToString()} | {String.Join(",", seq.Select(p => p.ToString()).ToArray())} | {string.Join(",", fermionTerm.Sequence)} | {coeff.ToString()}";
            // Console.WriteLine(string_output);
            return string_output;
        }
    }
}