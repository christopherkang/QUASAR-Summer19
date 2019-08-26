using System;
using System.IO;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;

namespace ImportOptimizedFermions
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
                // Extract important command line arguments
                string JSONPath = args[0];
                int numberOfSamples = Int16.Parse(args[1]);
                var nBitsPrecision = Int64.Parse(args[2]);

                if (File.Exists(JSONPath))
                {
                    #region Extract Fermion Terms
                    string raw_JSON = System.IO.File.ReadAllText(JSONPath);
                    var output = JObject.Parse(raw_JSON);

                    // get the constants specifically
                    var constants = output["constants"];

                    // get important constants
                    float trotterStepSize = (float)constants["trotterStep"];
                    double rescaleFactor = 1.0 / trotterStepSize;
                    float energyOffset = (float)constants["energyOffset"];
                    int nSpinOrbitals = (int)constants["nSpinOrbitals"];
                    int trotterOrder = (int)constants["trotterOrder"];
                    #endregion

                    #region Convert to Q# Format
                    // var test = Auxiliary.ProduceLowLevelTerms(output);
                    // Console.WriteLine(test);
                    var data = Auxiliary.ProduceCompleteHamiltonian(output);
                    #endregion

                    #region Simulate Optimized Fermion Terms
                    using (var qsim = new QuantumSimulator(randomNumberGeneratorSeed: 42))
                    {
                        // keep track of the running total of the energy to produce the average energy amount
                        var runningSum = 0.0;

                        // iterate over the sample size
                        for (int i = 0; i < numberOfSamples; i++)
                        {
                            var (phaseEst, energyEst) = EstimateEnergyLevel.Run(qsim, data, nBitsPrecision).Result;
                            runningSum += energyEst;
                            Console.WriteLine($"Predicted energy: {energyEst}");
                        }

                        // Output to stdout
                        Console.WriteLine($"Average predicted energy: {runningSum / (float)numberOfSamples}");
                    }
                    #endregion

                    #region Produce Cost Estimates
                    ResourcesEstimator estimator = new ResourcesEstimator();
                    ApplyTrotterOracleOnce.Run(estimator, data).Wait();
                    System.IO.Directory.CreateDirectory("_temp");
                    System.IO.File.WriteAllLines("./_temp/_costEstimateOptimized.txt", new[] { estimator.ToTSV() });
                    #endregion
                }
            }

        }
    }
}