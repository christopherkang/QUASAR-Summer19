// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#region Using Statements
// We will need several different libraries in this sample.
// Here, we expose these libraries to our program using the
// C# "using" statement, similar to the Q# "open" statement.

// We will use the data model implemented by the Quantum Development Kit chemistry
// libraries. This model defines what a fermionic Hamiltonian is, and how to
// represent Hamiltonians on disk.
using Microsoft.Quantum.Chemistry;
using Microsoft.Quantum.Chemistry.Broombridge;
using Microsoft.Quantum.Chemistry.OrbitalIntegrals;
using Microsoft.Quantum.Chemistry.Fermion;
using Microsoft.Quantum.Chemistry.QSharpFormat;

// To count gates, we'll use the trace simulator provided with
// the Quantum Development Kit.
using Microsoft.Quantum.Simulation.Simulators;

// The System namespace provides a number of useful built-in
// types and methods that we'll use throughout this sample.
using System;

// The System.Diagnostics namespace provides us with the
// Stopwatch class, which is quite useful for measuring
// how long each gate counting run takes.
using System.Diagnostics;

// The System.Collections.Generic library provides many different
// utilities for working with collections such as lists and dictionaries.
using System.Collections.Generic;

// We use the logging library provided with .NET Core to handle output
// in a robust way that makes it easy to turn on and off different messages.
using Microsoft.Extensions.Logging;

// We use this for convenience methods for manipulating arrays.
using System.Linq;
#endregion

namespace ExtractTrotterGates
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 5) 
            {
                Console.WriteLine("Too few parameters provided!");
                Console.WriteLine("Must provide the path to the YAML, input state label, precision, step size, and trotter order.");
            }
            else 
            {
                // get the core variables from command line
                string YAMLPath = args[0];
                string inputState = $"|{args[1]}>";
                int nBitsPrecision = Int16.Parse(args[2]);
                float trotterStepSize = float.Parse(args[3]);
                int trotterOrder = Int16.Parse(args[4]);

                Console.WriteLine($"Extracting the YAML from {YAMLPath}");

                // convert the YAML file into a JWED
                var broombridge = Deserializers.DeserializeBroombridge(YAMLPath);
                var problem = broombridge.ProblemDescriptions.Single();

                Console.WriteLine("Preparing Q# data format");
                var qSharpData = problem.ToQSharpFormat(inputState);

                // get the spin data and state data and dump to an auxiliary file
                var (nElectrons, trash2, stateData, trash3) = qSharpData;
                var (intinfo, datalist) = stateData;

                // Begin resource estimation (extract gate information)
                ResourcesEstimator estimator = new ResourcesEstimator();
                TargetedGateExtraction.Run(estimator, qSharpData, trotterStepSize, trotterOrder).Wait();
                Console.WriteLine($"trotterStep:float:{trotterStepSize}");
                Console.WriteLine($"trotterOrder:int:{trotterOrder}");
                Console.WriteLine($"nElectrons:int:{nElectrons}");
                Console.WriteLine($"----- END FILE -----");

                // also write the state information
                var lines = new List<String>();
                lines.Add(intinfo.ToString("N0"));
                foreach (var term in datalist) {
                    lines.Add(term.ToString());
                }

                System.IO.Directory.CreateDirectory("./temp");
                System.IO.File.WriteAllLines("./temp/_tempState.txt", lines);
            }
        }
    }
}
