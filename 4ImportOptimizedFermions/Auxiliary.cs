// Code reused from various parts of the Microsoft Quantum Chemistry Library
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation;
using Microsoft.Quantum.Chemistry;
using Microsoft.Quantum.Chemistry.Generic;
using Microsoft.Quantum.Chemistry.JordanWigner;
using Microsoft.Quantum.Chemistry.Paulis;
using Microsoft.Quantum.Chemistry.Fermion;
using Microsoft.Quantum.Chemistry.LadderOperators;
using QSharpFormat = Microsoft.Quantum.Chemistry.QSharpFormat;

namespace ImportOptimizedFermions
{
    using static Microsoft.Quantum.Chemistry.Extensions;
    using static Microsoft.Quantum.Chemistry.QSharpFormat.Convert;

    public static class Auxiliary
    {
        public static CompleteHamiltonian ProduceCompleteHamiltonian(
            JObject OptimizedHamiltonian
        )
        {
            // if this breaks see other method with the same name
            var stateData = new StatePrepData(CreateJWInputState(OptimizedHamiltonian));
            var (swaps, JWEDData, newEnergyOffset) = ProduceTotalRounds(OptimizedHamiltonian);

            // the new energy offset is factored in here
            var constants = PrepareConstantValues(OptimizedHamiltonian, newEnergyOffset);
            return new CompleteHamiltonian((constants, stateData, swaps, JWEDData));
        }
        // Produces all information necessary for the Q# code
        // Input: JSON file
        // Output: SWAPs and JWED to be used. JWED goes to the TrotterStepOracle, whereas SWAPs
        // have a custom implementation
        public static (QArray<SWAPRound>, QArray<JordanWignerEncodingData>, Double) ProduceTotalRounds(
            JObject OptimizedHamiltonian
        )
        {
            var swaps = ProduceAllSWAPRounds(OptimizedHamiltonian);
            var (interactions, newEnergyOffset) = ProduceAllJWED(OptimizedHamiltonian);

            return (swaps, interactions, newEnergyOffset);
        }

        // Produces all the necessary SWAP rounds
        // Input: JSON containing Hamiltonian
        // Output: Array of SWAPRounds
        public static QArray<SWAPRound> ProduceAllSWAPRounds(
            JObject OptimizedHamiltonian
        )
        {
            var swapData = OptimizedHamiltonian["terms"]["swaps"];
            List<SWAPRound> swaps = new List<SWAPRound>();
            for (int swapIndex = 0; swapIndex < swapData.Count(); swapIndex++)
            {
                swaps.Add(ProduceSWAPRound(OptimizedHamiltonian, swapIndex));
            }
            return new QArray<SWAPRound>(swaps);
        }

        // Produce a SWAPRound describing the FermionicSWAPs to be applied
        // Input: JSON with the Hamiltonian, index of SWAP to be used
        // Output: SWAPRound describing that iteration of the Hamiltonian
        public static SWAPRound ProduceSWAPRound(
            JObject OptimizedHamiltonian,
            int index
        )
        {
            var swapData = OptimizedHamiltonian["terms"]["swaps"][index];
            List<SWAPSeries> series = new List<SWAPSeries>();
            foreach (var swapIter in swapData)
            {
                // we'll have data in the form: [{swap info...}, ...]
                // we need to take this array of dicts and turn it into a SWAPSeries
                List<long> qubitLeft = new List<long>();
                List<long> qubitRight = new List<long>();
                foreach (var swap in swapIter)
                {
                    // now, we have a single {swap}
                    // let's add it to our list
                    qubitLeft.Add((long)swap["targets"][0]);
                    qubitRight.Add((long)swap["targets"][1]);
                }
                series.Add(new SWAPSeries((new QArray<long>(qubitLeft), new QArray<long>(qubitRight))));
            }
            return new SWAPRound((new QArray<SWAPSeries>(series)));
        }
        public static (QArray<JordanWignerEncodingData>, Double) ProduceAllJWED(
            JObject OptimizedHamiltonian
        )
        {
            // pull the number of interaction rounds we have
            int numberOfInteractionRounds = OptimizedHamiltonian["terms"]["interactions"].Count();
            List<JordanWignerEncodingData> allRounds = new List<JordanWignerEncodingData>();

            // We also need to calculate a new energy offset
            double newEnergyOffset = 0;

            for (int interactionIndex = 0; interactionIndex < numberOfInteractionRounds; interactionIndex++)
            {
                var converted = ConvertOneToJWED(OptimizedHamiltonian, interactionIndex);
                allRounds.Add(converted);
                var (trash1, trash2, trash3, energyOffset) = converted;
                newEnergyOffset += energyOffset;
            }

            Console.WriteLine(newEnergyOffset);

            return (new QArray<JordanWignerEncodingData>(allRounds), newEnergyOffset);
        }


        // Creates the typical JordanWignerEncodingData given the JSON input + the index of the interaction to use
        // Input: full JSON, index of interaction to use
        // Output: JordanWignerEncodingData to use with Q#
        public static JordanWignerEncodingData ConvertOneToJWED(
            JObject OptimizedHamiltonian,
            int index
        )
        {
            // begin initial converstion to Q# files
            PauliHamiltonian hamiltonianQSharpFormat = CallQSharpPipeline(OptimizedHamiltonian, index);
            var inputState = CreateJWInputState(OptimizedHamiltonian);

            // convert to refined Q# format
            var pauliHamiltonianQSharpFormat = hamiltonianQSharpFormat.ToQSharpFormat();
            var (energyOffset, trash, trash2) = pauliHamiltonianQSharpFormat;

            return QSharpFormat.Convert.ToQSharpFormat(pauliHamiltonianQSharpFormat, inputState);
        }

        // Create the JW Input State from the JSON
        // Input: Hamiltonian JSON
        // Output: JWInputState for use in the typical Q# pipeline
        public static (Int64, QArray<JordanWignerInputState>) CreateJWInputState(
            JObject OptimizedHamiltonian
        )
        {
            // extract the data
            var stateData = OptimizedHamiltonian["statePrepData"]["terms"];
            long intVal = (long)OptimizedHamiltonian["statePrepData"]["int"];
            List<JordanWignerInputState> stateList = new List<JordanWignerInputState>();
            foreach (var stateTerm in stateData)
            {
                var tuple = ((double)stateTerm["tuple"][0], (double)stateTerm["tuple"][1]);
                var intArray = stateTerm["array"].ToObject<long[]>();
                stateList.Add(new JordanWignerInputState((tuple, new QArray<long>(intArray))));
            }

            return (intVal, new QArray<JordanWignerInputState>(stateList));
        }



        // Converts JSON to data format with existing Q# pipeline
        // Input: JSON file
        // Output: PauliHamiltonian for later use in Q# pipeline
        public static PauliHamiltonian CallQSharpPipeline(
            JObject OptimizedHamiltonian,
            int index
        )
        {
            // make Hamiltonian to be output
            var hamiltonian = new PauliHamiltonian();

            // Pull the interactions from a specific round
            var interactionList = OptimizedHamiltonian["terms"]["interactions"][index];

            // make the conversion function
            Func<FermionTerm, TermType.Fermion, double, IEnumerable<(PauliTerm, PauliTermValue)>> conversion =
                (fermionTerm, termType, coeff)
                => (fermionTerm.ToJordanWignerPauliTerms(termType, coeff));

            // pull the interactions and parse them into the PauliHamiltonian format
            foreach (var interaction in interactionList)
            {
                var (term, type, coeff) = ConvertToFermionFormat(interaction);
                hamiltonian.AddRange(conversion(term, type, coeff));
                // if ((string)interaction["type"] == "Identity")
                // {
                //     // We need to still add the term if it's an identity term
                //     hamiltonian.
                // } else {
                //     var (term, type, coeff) = ConvertToFermionFormat(interaction);
                //     hamiltonian.AddRange(conversion(term, type, coeff));
                // }
            }

            // assume that the indices are simply an ascending 0-indexed list
            List<int> indices = new List<int>();
            for (int i = 0; i < interactionList.Count(); i++)
            {
                indices.Add(i);
            }

            hamiltonian.SystemIndices = new HashSet<int>(indices);

            // hamiltonian.SystemIndices = new HashSet<int>(sourceHamiltonian.SystemIndices.ToList());
            return hamiltonian;
        }

        // Produce a format of fermions to be used in the typical Q# pathway
        // Input: JObject containing the JSON
        // Output: Fermion data to be used immediately in the pipeline
        public static (FermionTerm, TermType.Fermion, double) ConvertToFermionFormat(
            JToken interaction
        )
        {
            // extract only the fermionic interactions
            string type = (string)interaction["type"];
            double angle = (double)interaction["angle"];
            TermType.Fermion termType;

            // create the new ladder sequence
            var ladderSpins = interaction["targets"].ToObject<int[]>();

            switch (type)
            {
                case "Identity":
                    termType = TermType.Fermion.Identity;
                    ladderSpins = new int[0];
                    break;

                case "PP":
                    termType = TermType.Fermion.PP;
                    break;

                case "PQ":
                    termType = TermType.Fermion.PQ;
                    break;

                case "PQQP":
                    termType = TermType.Fermion.PQQP;
                    break;

                case "PQQR":
                    termType = TermType.Fermion.PQQR;
                    // Console.WriteLine("[{0}]", string.Join(", ", ladderSpins));

                    var p = -1;
                    var q = -1;
                    var r = -1;

                    if (ladderSpins[0] == ladderSpins[1]) {
                        // we are in QQPR, need to see if P, R should be switched
                        p = ladderSpins[2];
                        q = ladderSpins[0];
                        r = ladderSpins[3];

                    } else if (ladderSpins[0] == ladderSpins[2]) {
                        // we are in QPQR

                        p = ladderSpins[1];
                        q = ladderSpins[0];
                        r = ladderSpins[3];
                        angle = angle * -1.0;

                    } else if (ladderSpins[0] == ladderSpins[3]) {
                        // we are in QPRQ
                        p = ladderSpins[1];
                        q = ladderSpins[0];
                        r = ladderSpins[2];

                    } else if (ladderSpins[1] == ladderSpins[2]) {
                        // we are in pqqr

                        p = ladderSpins[0];
                        q = ladderSpins[1];
                        r = ladderSpins[3];

                    } else if (ladderSpins[1] == ladderSpins[3]) {
                        // we are in pqrq

                        p = ladderSpins[0];
                        q = ladderSpins[1];
                        r = ladderSpins[2];
                        angle = angle * -1.0;

                    } else if (ladderSpins[2] == ladderSpins[3]) {
                        // we are in prqq

                        p = ladderSpins[0];
                        q = ladderSpins[2];
                        r = ladderSpins[1];

                    } else {
                        Console.WriteLine("wtf!!!");
                    }

                    // rename the terms now 
                    if (p < r) {
                        ladderSpins[0] = p;
                        ladderSpins[3] = r;
                    } else {
                        ladderSpins[0] = r;
                        ladderSpins[3] = p;
                        angle = angle * -1.0;
                    }

                    ladderSpins[1] = q;
                    ladderSpins[2] = q;

                    // Console.WriteLine("[{0}]", string.Join(", ", ladderSpins));

                    break;

                case "PQRS":
                    termType = TermType.Fermion.PQRS;
                    break;

                default:
                    throw new System.NotImplementedException();
            }

            var ladderSeq = ladderSpins.ToLadderSequence();
            FermionTerm convertedTerm = new FermionTerm(ladderSeq);

            return (convertedTerm, termType, angle);
        }

        // Produce the completed Hamiltonian given JSON
        // Input: JObject containing JSON
        // Output: Packaged Hamiltonian data
        // public static PackagedHamiltonian ProduceCompleteHamiltonian(
        //     JObject OptimizedHamiltonian
        //     )
        // {
        //     var statePrepData = PrepareStatePrepData(OptimizedHamiltonian);
        //     var fermionTerms = ProduceLowLevelTerms(OptimizedHamiltonian);
        //     var constantValues = PrepareConstantValues(OptimizedHamiltonian);
        //     return new PackagedHamiltonian((constantValues, fermionTerms, statePrepData));
        // }

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
                // extract relevant information
                var targets = term["targets"].ToObject<long[]>();
                var termType = term["type"].ToObject<string>();
                var angle = term["angle"].ToObject<double>();

                // decide on how to split the term based on its type
                if (termType == "Identity")
                {
                    // add an Identity
                    outData.Add(new GeneratorIndex(((new QArray<Int64>(new long[] { -1 }), new QArray<Double>(angle)), new QArray<Int64>(targets))));

                }
                else if (termType == "SWAP")
                {
                    // add a SWAP
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
                    outData.Add(new GeneratorIndex(((new QArray<Int64>(new long[] { 3 }), new QArray<Double>(newCoeff)), new QArray<Int64>(newTerm))));
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
            JObject OptimizedHamiltonian, Double newEnergyOffset
            )
        {
            var constants = OptimizedHamiltonian["constants"];
            var nSpinOrbitals = constants["nSpinOrbitals"].ToObject<long>();
            // var energyOffset = constants["energyOffset"].ToObject<double>();
            var energyOffset = newEnergyOffset;
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
            foreach (var term in stateData["terms"])
            {
                var doublePair = term["tuple"].ToObject<double[]>();
                var intArray = term["array"].ToObject<long[]>();
                termList.Add(new JordanWignerInputState(((doublePair[0], doublePair[1]), new QArray<Int64>(intArray.ToArray()))));
            }
            var convertedTerms = new QArray<JordanWignerInputState>(termList.ToArray());
            return new StatePrepData((intConst, convertedTerms));
        }
    }
}