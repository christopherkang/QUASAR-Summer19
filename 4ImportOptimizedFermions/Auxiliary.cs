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

            // Put single-body terms at the beginning
            // var converted = ConvertOneToJWED(OptimizedHamiltonian, numberOfInteractionRounds - 1);
            // allRounds.Add(converted);
            // var (trash1, trash2, trash3, lastEnergyOffset) = converted;
            // newEnergyOffset += lastEnergyOffset;

            for (int interactionIndex = 0; interactionIndex < numberOfInteractionRounds; interactionIndex++)
            {
                var converted = ConvertOneToJWED(OptimizedHamiltonian, interactionIndex);
                allRounds.Add(converted);
                var (trash11, trash22, trash33, energyOffset) = converted;
                newEnergyOffset += energyOffset;
            }

            // Console.WriteLine(newEnergyOffset);

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
                var (term, type, coeffData) = ConvertToFermionFormat(interaction);
                hamiltonian.AddRange(conversion(term, type, coeffData));
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
            var angle = (double)interaction["angle"];
            var p = -1;
            var q = -1;
            var r = -1;
            var s = -1;
            TermType.Fermion termType;

            // create the new ladder sequence
            var ladderSpins = interaction["targets"].ToObject<int[]>();
            var pqrsSorted = ladderSpins.ToList();
            pqrsSorted.Sort();

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
                    p = pqrsSorted[0];
                    q = pqrsSorted[2];
                    ladderSpins[0] = p;
                    ladderSpins[1] = q;
                    ladderSpins[2] = q;
                    ladderSpins[3] = p;
                    break;

                case "PQQR":
                    termType = TermType.Fermion.PQQR;
                    // Console.WriteLine("[{0}]", string.Join(", ", ladderSpins));
                    // Console.WriteLine($"{angle}");

                    // Console.WriteLine("-------------------");
                    // Console.WriteLine($"PQQR values: {ladderSpins[0]}, {ladderSpins[1]}, {ladderSpins[2]}, {ladderSpins[3]} | {angle}");

                    p = -1;
                    q = -1;
                    r = -1;

                    // PROBLEM: if you swap the ladderspins, the internal data format will try to move it
                    // into a way it understands. HOWEVER, this causes two problems:
                    // - the term has been reordered without your knowledge, into a form you're not sure of
                    // - the coefficient induced by this swap is not recorded BY THE ACTUAL TRANSFORMATION
                    // so, I avoided the more rigorous transformation by simply avoiding our main problem,
                    // where p > q. This code checks for that and swaps p,q if so.
                    // it also assumes we're in some combo of qprq or pqrq
                    if (ladderSpins[0] == ladderSpins[3]) {
                        // QPRQ
                        if (ladderSpins[1] > ladderSpins[2]) {
                            var temp = ladderSpins[1];
                            ladderSpins[1] = ladderSpins[2];
                            ladderSpins[2] = temp;
                        }
                    } else if (ladderSpins[1] == ladderSpins[3]) {
                        // PQRQ
                        if (ladderSpins[0] > ladderSpins[2]) {
                            var temp = ladderSpins[2];
                            ladderSpins[2] = ladderSpins[0];
                            ladderSpins[0] = temp;
                        }
                    } else if (ladderSpins[1] == ladderSpins[2]) {
                        // PQQR
                        if (ladderSpins[0] > ladderSpins[3]) {
                            var temp = ladderSpins[3];
                            ladderSpins[3] = ladderSpins[0];
                            ladderSpins[0] = temp;
                        }
                    }

                    // if (ladderSpins[0] == ladderSpins[1])
                    // {
                    //     // we are in QQPR, need to see if P, R should be switched
                    //     p = ladderSpins[2];
                    //     q = ladderSpins[0];
                    //     r = ladderSpins[3];
                    //     Console.WriteLine("---------QQPR");
                    //     ladderSpins[0] = p;
                    //     ladderSpins[1] = q;
                    //     ladderSpins[2] = q;
                    //     ladderSpins[3] = r;
                    // } else if (ladderSpins[0] == ladderSpins[2])
                    // {
                    //     // we are in QPQR
                    //     p = ladderSpins[1];
                    //     q = ladderSpins[0];
                    //     r = ladderSpins[3];
                    //     angle = angle * -1.0; // FLAG
                    //     Console.WriteLine("---------QPQR");
                    // } else if (ladderSpins[0] == ladderSpins[3])
                    // {
                    //     // we are in QPRQ
                    //     p = ladderSpins[1];
                    //     q = ladderSpins[0];
                    //     r = ladderSpins[2];
                    //     Console.WriteLine("---------QPRQ");
                    // }
                    // else 
                    // if (ladderSpins[1] == ladderSpins[2])
                    // {
                    //     // we are in PQQR
                    //     p = ladderSpins[0];
                    //     q = ladderSpins[1];
                    //     r = ladderSpins[3];
                    //     Console.WriteLine("---------PQQR");
                    // }
                    // else 
                    // if (ladderSpins[1] == ladderSpins[3])
                    // {
                    //     // we are in pqrq
                    //     p = ladderSpins[0];
                    //     q = ladderSpins[1];
                    //     r = ladderSpins[2];
                    //     angle = angle * -1.0;
                    //     Console.WriteLine("---------PQRQ");
                    //     ladderSpins[0] = p;
                    //     ladderSpins[1] = q;
                    //     ladderSpins[2] = q;
                    //     ladderSpins[3] = r;
                    // }
                    // else if (ladderSpins[2] == ladderSpins[3])
                    // {
                    //     // we are in prqq
                    //     p = ladderSpins[0];
                    //     q = ladderSpins[2];
                    //     r = ladderSpins[1];
                    //     Console.WriteLine("---------PRQQ");
                    //     ladderSpins[0] = p;
                    //     ladderSpins[1] = q;
                    //     ladderSpins[2] = q;
                    //     ladderSpins[3] = r;
                    // }
                    // else
                    // {
                    //     Console.WriteLine("Unknown PQQR Permutation");
                    // }

                    // // rename the terms now 
                    // if (p < r)
                    // {
                    //     ladderSpins[0] = p;
                    //     ladderSpins[3] = r;
                    // }
                    // else
                    // {
                    //     ladderSpins[0] = r;
                    //     ladderSpins[3] = p;
                    //     angle = angle * -1.0;
                    // }

                    // // Console.WriteLine(angle);

                    // ladderSpins[1] = q;
                    // ladderSpins[2] = q;
                    // Console.WriteLine($"PQQR values: {ladderSpins[0]}, {ladderSpins[1]}, {ladderSpins[2]}, {ladderSpins[3]} | {angle}");
                    break;

                case "PQRS":
                    termType = TermType.Fermion.PQRS;

                    // find the new PQRS (p'q'r's') and their corresponding original indices
                    p = pqrsSorted[0];
                    q = pqrsSorted[1];
                    r = pqrsSorted[2];
                    s = pqrsSorted[3];

                    var pIndex = Array.IndexOf(ladderSpins, p);
                    var qIndex = Array.IndexOf(ladderSpins, q);
                    var rIndex = Array.IndexOf(ladderSpins, r);
                    var sIndex = Array.IndexOf(ladderSpins, s);

                    // // if the index is 0, 1, then that coefficient is creation
                    // // if the index is 2, 3, then that coefficient is annihilation

                    // Console.WriteLine($"{pIndex}, {qIndex}, {rIndex}, {sIndex}");
                    // Console.WriteLine($"values: {p}, {q}, {r}, {s}");
                    // Console.WriteLine($"values: {ladderSpins[0]}, {ladderSpins[1]}, {ladderSpins[2]}, {ladderSpins[3]} | {angle}");

                    // set the leading sign coefficients
                    var zSign = 0.0;
                    if ((qIndex + sIndex == 1) || (qIndex + sIndex == 5))
                    {
                        // we have two annihilation / creation
                        zSign = -1.0;
                    }
                    else
                    {
                        // we could have pr (2) to qs (4)
                        zSign = 1.0;
                    }
                    int[] orderingArray = {pIndex, qIndex, rIndex, sIndex};
                    var swapCoeff = RecursivelyIdentifyDeterminant(orderingArray.ToArray());

                    // identify which Q# pipeline to use
                    if ((pIndex + qIndex == 1) || (pIndex + qIndex == 5))
                    {
                        // XXYY / YYXX are negative; pqsr pipeline
                        ladderSpins[0] = p;
                        ladderSpins[1] = q;
                        ladderSpins[2] = s;
                        ladderSpins[3] = r;
                    }
                    else if ((qIndex + sIndex == 1) || (qIndex + sIndex == 5))
                    {
                        // XYXY / YXYX are negative; prsq pipeline
                        ladderSpins[0] = p;
                        ladderSpins[1] = r;
                        ladderSpins[2] = s;
                        ladderSpins[3] = q;
                    }
                    else if ((qIndex + rIndex == 1) || (qIndex + rIndex == 5))
                    {
                        // XYYX / YXXY are negative; psrq pipeline
                        ladderSpins[0] = p;
                        ladderSpins[1] = s;
                        ladderSpins[2] = r;
                        ladderSpins[3] = q;
                    }
                    else
                    {
                        throw new System.NotImplementedException();
                    }

                    // apply final corrections
                    // Console.WriteLine($"z: {zSign}, {swapCoeff}, {angle}");
                    angle = -1.0 * zSign * swapCoeff * angle;

                    // Console.WriteLine($"values: {ladderSpins[0]}, {ladderSpins[1]}, {ladderSpins[2]}, {ladderSpins[3]} | {angle}");
                    break;

                default:
                    throw new System.NotImplementedException();
            }

            var ladderSeq = ladderSpins.ToLadderSequence();
            FermionTerm convertedTerm = new FermionTerm(ladderSeq);
            // Console.WriteLine($"{termType} | {convertedTerm} | {angle}");
            // // Console.WriteLine($"{convertedTerm.Sequence}");
            // Console.WriteLine("[{0}]", string.Join(", ", convertedTerm.Sequence));
            // Console.WriteLine(convertedTerm.Coefficient);

            // IMPORTANT: MAKE SURE TO KEEP THE convertedTerm.Coefficient, as it corrects for internal manipulation
            return (convertedTerm, termType, angle * convertedTerm.Coefficient);
        }

        // Identify the determinant of a permutation array
        // Input: array with unique elements from 0..n -1
        // Output: determinant of the array who had the elements of the permutation matrix
        public static int RecursivelyIdentifyDeterminant(
            int[] indexList
        )
        {
            // e.g. [0, 3, 1, 2]
            // move 3, count the number of swaps, then move on until there is nothing
            if (indexList.Length == 1)
            {
                return 1;
            }
            else
            {
                var mover = indexList.Max();
                int[] moverArr = { mover };
                var moverIndex = indexList.ToList().IndexOf(mover);

                // we have 0, 3, 1 ,2 at 0, 1, 2, 3
                // 3 will need to move from index 1 to 3, which is 2 swaps
                var swapNum = mover - moverIndex;
                if (swapNum % 2 == 0)
                {
                    return RecursivelyIdentifyDeterminant(indexList.Except(moverArr).ToArray());
                }
                else
                {
                    return -1 * RecursivelyIdentifyDeterminant(indexList.Except(moverArr).ToArray());
                }
            }
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