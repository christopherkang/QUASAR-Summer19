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
                // var gateFile = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/extracted_terms.json";
                string gateFile = args[0];
                int numberOfSamples = Int16.Parse(args[1]);
                var nBitsPrecision = Int64.Parse(args[2]);
                if (File.Exists(gateFile))
                {
                    // Do stuff if the file exists
                    string raw_JSON = System.IO.File.ReadAllText(gateFile);
                    var output = JObject.Parse(raw_JSON);
                    var constants = output["constants"];

                    float trotterStepSize = (float)constants["trotterStep"];
                    double rescaleFactor = 1.0 / trotterStepSize;
                    float energyOffset = (float)constants["energyOffset"];
                    int nSpinOrbitals = (int)constants["nSpinOrbitals"];
                    int trotterOrder = (int)constants["trotterOrder"];

                    var hamiltonianTermData = Auxiliary.ProduceTermInfo(output);
                    var inputState = Auxiliary.ProduceStateData(output);

                    var totalHamiltonian = new CompleteHamiltonian(
                        (
                            new CompressedConstants((nSpinOrbitals, energyOffset, trotterStepSize, trotterOrder)),
                            inputState,
                            hamiltonianTermData
                        )
                    );

                    Console.WriteLine($"Running a Hamiltonian of order {trotterOrder}");
                    Console.WriteLine($"Step size: {trotterStepSize}");

                    // Send the gates to the quantum computer
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
                }
                else
                {
                    Console.WriteLine("ERROR: File not found.");
                }
            }
        }
    }
}