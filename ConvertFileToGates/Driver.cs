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
            /* 
            Console.Write("What is the gate file name? ");
            var gateFile = Console.ReadLine();
            */
            var gateFile = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/extracted_terms.json";
            if (File.Exists(gateFile))
            {
                // Do stuff if the file exists
                string raw_JSON = System.IO.File.ReadAllText(gateFile);
                var output = JObject.Parse(raw_JSON);
                var constants = output["constants"];
                var nBitsPrecision = 7;

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

                // also use the quantum computer 
                using (var qsim = new QuantumSimulator())
                {
                    for (int i = 0; i < 100; i++)
                    {
                        var (phaseEst, energyEst) = GetEnergyByTrotterization.Run(qsim, totalHamiltonian, nBitsPrecision).Result; 
                        Console.WriteLine($"Predicted energy: {energyEst}");
                    }
                    // AcceptArray<CompressedHamiltonian>.Run(qsim, new QArray<CompressedHamiltonian>(out_values)).Wait();
                    // ApplyFromFile.Run(qsim, 4, listOfGates).Wait();
                }
            }
            else
            {
                Console.WriteLine("ERROR: File not found.");
            }
        }
    }
}