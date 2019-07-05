using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;


#region Using statements

using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;

#endregion
namespace ReadGatesFile
{
    class Driver
    {
        // PURPOSE
        // In the following file, we'd like to read from a gate file and give this data directly to a Q# file
        static void Main(string[] args)
        {
            #region User Interaction
            // what 
            Console.Write("What is the input file name? ");
            var testString = Console.ReadLine();
            Console.WriteLine($"wow, I think {testString} is interesting too!");
            #endregion
            using (var qsim = new QuantumSimulator())
            {
                HelloQ.Run(qsim).Wait();
            }
        }
    }
}