﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json.Linq;

using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using Microsoft.Quantum.Simulation.Simulators.QCTraceSimulators;

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
                    // var data = Auxiliary.ProduceCompleteHamiltonian(output);
                    // var qSharpData = Auxiliary.CreateQSharpFormat(output);
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
                    var config = new QCTraceSimulatorConfiguration();
                    config.usePrimitiveOperationsCounter = true;
                    QCTraceSimulator estimator = new QCTraceSimulator(config);
                    ApplyTrotterOracleOnce.Run(estimator, data).Wait();
                    System.IO.Directory.CreateDirectory("_temp");
                    // System.IO.File.WriteAllLines("./_temp/_costEstimateOptimized.csv",estimator.ToCSV().Select(x => x.Key + " " + x.Value).ToArray());
                    foreach (var collectedData in estimator.ToCSV())
                    {
                        File.WriteAllText(
                            Path.Combine("./_temp/", $"CCNOTCircuitsMetrics.{collectedData.Key}.csv"),
                            collectedData.Value);
                    }
                    #endregion
                }
            }

        }
    }
}