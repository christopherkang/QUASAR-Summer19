// Code reused from various parts of the Microsoft Quantum Chemistry Library
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation;
using Microsoft.Quantum.Chemistry.JordanWigner;

namespace ImportOptimizedFermions
{
    public static class Auxiliary
    {
        // Produce the completed Hamiltonian given JSON
        // Input: JObject containing JSON
        // Output: Packaged Hamiltonian data
        public static PackagedHamiltonian ProduceCompleteHamiltonian(
            JObject OptimizedHamiltonian
            )
        {
            var statePrepData = PrepareStatePrepData(OptimizedHamiltonian);
            var fermionTerms = ProduceLowLevelTerms(OptimizedHamiltonian);
            var constantValues = PrepareConstantValues(OptimizedHamiltonian);
            return new PackagedHamiltonian((constantValues, fermionTerms, statePrepData));
        }
        
        // Convert the JSON fermion terms into GeneratorIndex[] that can be interpreted in Q#
        // Input: JObject containing JSON
        // Output: QArray<GeneratorIndex>
        // Note: Much of this code has repurposed from Chemistry/src/DataModel/Fermion/JordanWignerEncoding.cs
        public static QArray<GeneratorIndex> ProduceLowLevelTerms(
            JObject OptimizedHamiltonian
            )
        {
            var termList = OptimizedHamiltonian["terms"];
            var outData = new List<GeneratorIndex>();
            foreach (var term in termList)
            {
                var targets = term["targets"].ToObject<long[]>();
                var termType = term["type"].ToObject<string>();
                var angle = term["angle"].ToObject<double>();
                if (termType == "Identity")
                {
                    outData.Add(new GeneratorIndex(((new QArray<Int64>(new long[] { -1 }), new QArray<Double>(angle)), new QArray<Int64>(targets))));

                }
                else if (termType == "SWAP")
                {
                    outData.Add(new GeneratorIndex(((new QArray<Int64>(new long[] { -2 }), new QArray<Double>(-1.0)), new QArray<Int64>(targets))));
                }

                else if (termType == "PP")
                {
                    // add an Identity, Z
                    outData.Add(new GeneratorIndex(((new QArray<Int64>(new long[] { -1 }), new QArray<Double>(0.5 * angle)), new QArray<Int64>(targets))));

                    var PTarget = new QArray<Int64>(targets.Take(1));
                    outData.Add(new GeneratorIndex(((new QArray<Int64>(new long[] { 0 }), new QArray<Double>(-0.5 * angle)), PTarget)));
                }
                else if (termType == "PQ")
                {
                    // add a PQ
                    outData.Add(new GeneratorIndex(((new QArray<Int64>(new long[] { 2 }), new QArray<Double>(0.25 * angle)), new QArray<Int64>(targets))));
                }
                else if (termType == "PQQP")
                {

                    // add an Identity, ZZ, Z, Z
                    outData.Add(new GeneratorIndex(((new QArray<Int64>(new long[] { -1 }), new QArray<Double>(0.25 * angle)), new QArray<Int64>(targets))));

                    var ZZTarget = new QArray<Int64>(targets.Take(2));
                    outData.Add(new GeneratorIndex(((new QArray<Int64>(new long[] { 1 }), new QArray<Double>(0.25 * angle)), ZZTarget)));

                    var ZTarget1 = new QArray<Int64>(targets.Take(1));
                    outData.Add(new GeneratorIndex(((new QArray<Int64>(new long[] { 0 }), new QArray<Double>(-0.25 * angle)), ZTarget1)));

                    var ZTarget2 = new QArray<Int64>(targets.Skip(1).Take(1));
                    outData.Add(new GeneratorIndex(((new QArray<Int64>(new long[] { 0 }), new QArray<Double>(-0.25 * angle)), ZTarget2)));
                }
                else if (termType == "PQQR")
                {
                    // Console.WriteLine("[{0}]", string.Join(", ", targets));
                    var multiplier = 1.0;
                    if (targets.First() == targets.Last())
                    {
                        // QPRQ -> PQQR
                        targets[0] = targets[1];
                        targets[1] = targets[3];
                        targets[3] = targets[2];
                        targets[2] = targets[1];
                    }
                    else if (targets[1] == targets[3])
                    {
                        // PQRQ -> PQQR
                        targets[3] = targets[2];
                        targets[2] = targets[1];
                        multiplier = -1.0;
                    }
                    // FLAG - SHOULD THIS DO THIS?????????
                    if (targets[0] > targets[3])
                    {
                        var lowerBound = targets[3];
                        targets[3] = targets[0];
                        targets[0] = lowerBound;
                    }
                    // Console.WriteLine("[{0}]", string.Join(", ", targets));

                    // add a PQQR and PQ term
                    outData.Add(new GeneratorIndex(((new QArray<Int64>(new long[] { 2 }), new QArray<Double>(-0.125 * multiplier * angle)), new QArray<Int64>(targets))));

                    var PQTargets = new QArray<Int64>(new long[] { targets[0], targets[3] });
                    outData.Add(new GeneratorIndex(((new QArray<Int64>(new long[] { 2 }), new QArray<Double>(0.125 * multiplier * angle)), PQTargets)));

                }
                else if (termType == "PQRS")
                {

                    var pqrsSorted = targets.ToList();
                    pqrsSorted.Sort();
                    var (newTerm, newCoeff) = IdentifyHpqrsPermutation((pqrsSorted, targets, angle));
                    outData.Add(new GeneratorIndex(((new QArray<Int64>(new long[] {3}), new QArray<Double>(newCoeff)), new QArray<Int64>(newTerm))));
                }
                else
                {

                    throw new System.NotImplementedException();
                }
            }
            return new QArray<GeneratorIndex>(outData.ToArray());
        }

        // Helper method for ProduceLowLevelTerms
        // Identifies Hpqrs permutation and gives appropriate coefficients
        public static (List<long>, double[]) IdentifyHpqrsPermutation((List<long>, long[], double) term)
        {
            //We only consider permutations pqrs || psqr || prsq || qprs || spqr || prqs
            var (pqrsSorted, pqrsPermuted, coeff) = term;
            coeff = coeff * 0.0625;
            var h123 = new double[] { .0, .0, .0 };
            var v0123 = new double[] { .0, .0, .0, .0 };

            //Console.WriteLine($"{pqrsSorted}, {pqrsPermuted}");

            var prsq = new long[] { pqrsSorted[0], pqrsSorted[2], pqrsSorted[3], pqrsSorted[1] };
            var pqsr = new long[] { pqrsSorted[0], pqrsSorted[1], pqrsSorted[3], pqrsSorted[2] };
            var psrq = new long[] { pqrsSorted[0], pqrsSorted[3], pqrsSorted[2], pqrsSorted[1] };

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

        // Extracts the constant values
        // Input: JObject containing JSON
        // Output: Tuple of constants (HamiltonianConstants)
        public static HamiltonianConstants PrepareConstantValues(
            JObject OptimizedHamiltonian
            )
        {
            var constants = OptimizedHamiltonian["constants"];
            var nSpinOrbitals = constants["nSpinOrbitals"].ToObject<long>();
            var energyOffset = constants["energyOffset"].ToObject<double>();
            var trotterStep = constants["trotterStep"].ToObject<double>();
            var trotterOrder = constants["trotterOrder"].ToObject<long>();
            return new HamiltonianConstants((nSpinOrbitals, energyOffset, trotterStep, trotterOrder));
        }

        // Extracts state prep data
        // Input: JObject containing JSON
        // Output: StatePrepData format that can be used with PrepareTrialState
        public static StatePrepData PrepareStatePrepData(
            JObject OptimizedHamiltonian
            )
        {
            var stateData = OptimizedHamiltonian["statePrepData"];
            var intConst = stateData["int"].ToObject<long>();
            var termList = new List<JordanWignerInputState>();
            foreach (var term in stateData["terms"]) {
                var doublePair = term["tuple"].ToObject<double[]>();
                var intArray = term["array"].ToObject<long[]>();
                termList.Add(new JordanWignerInputState(((doublePair[0], doublePair[1]), new QArray<Int64>(intArray.ToArray()))));
            }
            var convertedTerms = new QArray<JordanWignerInputState>(termList.ToArray());
            return new StatePrepData((intConst, convertedTerms));
        }
    }
}