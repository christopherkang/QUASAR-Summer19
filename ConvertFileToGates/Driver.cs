using System;
using System.IO;

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
            if (File.Exists(gateFile)) 
            {
                // Do stuff if the file exists
                using (StreamReader file = new StreamReader(gateFile))
                {
                    bool lineHasInfo = false;
                    string ln;
                    
                    while ((ln = file.ReadLine()) != null)
                    {
                        if (lineHasInfo || ln == "----- BEGIN ORACLE WRITE -----") 
                        {
                            Console.WriteLine("why is alex so negative :(");
                        }
                        counter++;
                    }
                    file.Close();
                }

                // also use the quantum computer 
                using (var qsim = new QuantumSimulator())
                {
                    HelloQ.Run(qsim).Wait();
                }
            }
            else 
            {
                Console.WriteLine("ERROR: File not found.");
            }
        }
    }
}