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
            Console.Write("What is the gate file name? ");
            var gateFile = Console.ReadLine();
            // var listOfGates = new List<CompressedGateForm>();
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
                List<CompressedHamiltonian> out_values = Auxiliary.ProduceTermInfo(output);

                Console.WriteLine($"Running a Hamiltonian of order {trotterOrder}");
                Console.WriteLine($"Step size: {trotterStepSize}");

                // foreach (var term in out_values) 
                // {
                //     Console.WriteLine(term);
                // }

                // also use the quantum computer 
                using (var qsim = new QuantumSimulator())
                {
                    AcceptArray<CompressedHamiltonian>.Run(qsim, new QArray<CompressedHamiltonian>(out_values)).Wait();
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