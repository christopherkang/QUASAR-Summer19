using System;

using Microsoft.Quantum.Chemistry;
using Microsoft.Quantum.Chemistry.OrbitalIntegrals;
using Microsoft.Quantum.Chemistry.Broombridge;

using Microsoft.Extensions.Logging;
using System.Linq;

using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using Microsoft.Quantum.Simulation.Simulators.QCTraceSimulators;

namespace ProduceSampleEnergy
{
    class Driver
    {
        static void Main(string[] args)
        {
            // This is the name of the file we want to load
            if (args.Length < 6)
            {
                Console.WriteLine("Too few parameters provided!");
                Console.WriteLine("Must provide the path to the YAML, input state label, precision, step size, trotter order, and sample size");
            }
            else
            {
                string YAMLPath = args[0];
                string inputState = $"|{args[1]}>";
                int nBitsPrecision = Int16.Parse(args[2]);
                float trotterStepSize = float.Parse(args[3]);
                int trotterOrder = Int16.Parse(args[4]);
                var numberOfSamples = Int16.Parse(args[5]);

                Console.WriteLine($"Extracting the YAML from {YAMLPath}");
                Console.WriteLine($"Input state: {inputState}");
                Console.WriteLine($"Precision: {nBitsPrecision}");
                Console.WriteLine($"Trotter step size: {trotterStepSize}");
                Console.WriteLine($"Trotter order: {trotterOrder}");
                Console.WriteLine($"Number of samples: {numberOfSamples}");

                // This deserializes a Broombridge file, given its filename.
                var broombridge = Deserializers.DeserializeBroombridge(YAMLPath);

                // Note that the deserializer returns a list of `ProblemDescription` instances 
                // as the file might describe multiple Hamiltonians. In this example, there is 
                // only one Hamiltonian. So we use `.Single()`, which selects the only element of the list.
                var problem = broombridge.ProblemDescriptions.Single();

                // This is a data structure representing the Jordan-Wigner encoding 
                // of the Hamiltonian that we may pass to a Q# algorithm.
                // If no state is specified, the Hartree-Fock state is used by default.
                Console.WriteLine("Preparing Q# data format");
                var qSharpData = problem.ToQSharpFormat(inputState);

                Console.WriteLine("----- Begin simulation -----");
                using (var qsim = new QuantumSimulator(randomNumberGeneratorSeed: 42))
                {
                    // HelloQ.Run(qsim).Wait();
                    var runningSum = 0.0;
                    for (int i = 0; i < numberOfSamples; i++)
                    {
                        var (phaseEst, energyEst) = GetEnergyByTrotterization.Run(qsim, qSharpData, nBitsPrecision, trotterStepSize, trotterOrder).Result;
                        Console.WriteLine(energyEst);
                        runningSum += energyEst;
                    }
                    Console.WriteLine("----- End simulation -----");
                    Console.WriteLine($"Average energy estimate: {runningSum / (float)numberOfSamples}");
                }

                ResourcesEstimator estimator = new ResourcesEstimator();
                GetEnergyByTrotterization.Run(estimator, qSharpData, nBitsPrecision, trotterStepSize, trotterOrder).Wait();
                System.IO.Directory.CreateDirectory("_temp");
                System.IO.File.WriteAllLines("./_temp/_costEstimateReference.txt", new []{estimator.ToTSV()});
            }
        }
    }
}