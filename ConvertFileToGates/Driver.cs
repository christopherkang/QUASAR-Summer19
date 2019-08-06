// This project enables the conversion of the JSON format back into gates
// and then produces the expected energy level associated with running the 
// optimized Trotter gate (phase estimation + rescale code kept the same)

// Auxiliary.cs describes many of the secondary methods used to pass info
// to the Q# code. Types.qs describes the new types needed to be created
// in order to ingest the JSON file into Q#.

// This project takes command line arguments to run; see below.

using System;
using System.IO;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;

namespace ConvertFileToGates
{
    class Driver
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("You did not provide the gatefile path, number of samples, and precision.");
            }
            else
            {
                string JSONPath = args[0];
                int numberOfSamples = Int16.Parse(args[1]);
                var nBitsPrecision = Int64.Parse(args[2]);
                if (File.Exists(JSONPath))
                {
                    #region Extract JSON information
                    string raw_JSON = System.IO.File.ReadAllText(JSONPath);
                    var output = JObject.Parse(raw_JSON);
                    var constants = output["constants"];

                    float trotterStepSize = (float)constants["trotterStep"];
                    double rescaleFactor = 1.0 / trotterStepSize;
                    float energyOffset = (float)constants["energyOffset"];
                    int nSpinOrbitals = (int)constants["nSpinOrbitals"];
                    int trotterOrder = (int)constants["trotterOrder"];
                    #endregion

                    #region Convert to Q# format
                    var hamiltonianTermData = Auxiliary.ProduceTermInfo(output);
                    var inputState = Auxiliary.ProduceStateData(output);

                    var totalHamiltonian = new CompleteHamiltonian(
                        (
                            new CompressedConstants((nSpinOrbitals, energyOffset, trotterStepSize, trotterOrder)),
                            inputState,
                            hamiltonianTermData
                        )
                    );

                    Console.WriteLine($"Extracting the JSON from {JSONPath}");
                    Console.WriteLine($"Precision: {nBitsPrecision}");
                    Console.WriteLine($"Trotter step size: {trotterStepSize}");
                    Console.WriteLine($"Trotter order: {trotterOrder}");
                    Console.WriteLine($"Number of samples: {numberOfSamples}");
                    #endregion

                    #region Obtain energy level estimates
                    using (var qsim = new QuantumSimulator())
                    {
                        var runningSum = 0.0;
                        for (int i = 0; i < numberOfSamples; i++)
                        {
                            var (phaseEst, energyEst) = GetEnergyByTrotterization.Run(qsim, totalHamiltonian, nBitsPrecision).Result;
                            Console.WriteLine($"Predicted energy: {energyEst}");
                            runningSum += energyEst;
                        }
                        Console.WriteLine($"Average predicted energy: {runningSum / (float)numberOfSamples}");
                    }
                    #endregion
                }
                else
                {
                    Console.WriteLine("ERROR: File not found.");
                }
            }
        }
    }
}