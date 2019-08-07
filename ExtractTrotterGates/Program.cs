// This file is a modified version of the example Trotterization file.
// The purpose is to identify the specific gates being used in the Trotter implementatino
// There are prerequisites to run this file; please check the README

#region Using Statements

using Microsoft.Quantum.Chemistry;
using Microsoft.Quantum.Chemistry.Broombridge;
using Microsoft.Quantum.Chemistry.OrbitalIntegrals;
using Microsoft.Quantum.Chemistry.Fermion;
using Microsoft.Quantum.Chemistry.QSharpFormat;
using Microsoft.Quantum.Simulation.Simulators;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Collections.Generic;
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

                # region Obtaining information from command line
                string YAMLPath = args[0];
                string inputState = $"|{args[1]}>";
                int nBitsPrecision = Int16.Parse(args[2]);
                float trotterStepSize = float.Parse(args[3]);
                int trotterOrder = Int16.Parse(args[4]);

                Console.WriteLine($"Extracting the YAML from {YAMLPath}");
                System.IO.Directory.CreateDirectory("./_temp");

                #endregion

                #region Convert the YAML into Q# format
                // convert the YAML file into a JWED
                var broombridge = Deserializers.DeserializeBroombridge(YAMLPath);
                var problem = broombridge.ProblemDescriptions.Single();

                Console.WriteLine("Preparing Q# data format");

                var qSharpData = problem.ToQSharpFormat(inputState);

                // get the spin data and state data and dump to an auxiliary file
                var (nSpinOrbitals, trash2, stateData, energyOffset) = qSharpData;
                var (intinfo, datalist) = stateData;

                #endregion

                #region Extract relevant gates
                // output the Fermion interaction list
                Console.WriteLine("----- BEGIN ORACLE WRITE -----");
                Auxiliary.ToQSharpFormat(problem, inputState);
                Console.WriteLine("----- END ORACLE WRITE -----");

                // Begin resource estimation (extract gate information)
                ResourcesEstimator estimator = new ResourcesEstimator();
                TargetedGateExtraction.Run(estimator, qSharpData, trotterStepSize, trotterOrder).Wait();
                #endregion

                #region Output constant data
                var constants = new List<String>();
                constants.Add($"trotterStep:float:{trotterStepSize}");
                constants.Add($"trotterOrder:int:{trotterOrder}");
                constants.Add($"nSpinOrbitals:int:{nSpinOrbitals}");
                constants.Add($"energyOffset:float:{energyOffset}");
                System.IO.File.WriteAllLines("./_temp/_constants.txt", constants);
                #endregion

                #region Obtain state information
                var lines = new List<String>();
                lines.Add(intinfo.ToString("N0"));
                foreach (var term in datalist)
                {
                    lines.Add(term.ToString());
                }

                System.IO.File.WriteAllLines("./_temp/_stateData.txt", lines);
                #endregion
            }
        }
    }
}
