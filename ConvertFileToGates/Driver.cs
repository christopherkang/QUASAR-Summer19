using System;
using System.IO;
using System.Collections.Generic;

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
            var listOfGates = new List<CompressedGateForm>();
            if (File.Exists(gateFile)) 
            {
                // Do stuff if the file exists
                using (StreamReader file = new StreamReader(gateFile))
                {
                    bool lineHasInfo = false;
                    string ln;
                    
                    while ((ln = file.ReadLine()) != null)
                    {
                        if (lineHasInfo)  
                        {
                            if (ln == "-----Finished oracle implementation-----") 
                            {
                                lineHasInfo = false;
                            }
                            else 
                            {
                                // do processing;
                                string[] infoTypes = ln.Split(" | ");
                                infoTypes[1] = infoTypes[1].Replace(",", string.Empty);
                                string[] qubitInfo = infoTypes[1].Split(" ");
                                var targetInfo = (-1, -1, -1, -1);

                                if (infoTypes[0] == "Zterm") 
                                {
                                    targetInfo = (Int32.Parse(qubitInfo[1]), -1, -1, -1);
                                }
                                else if (infoTypes[0] == "ZZterm")
                                {
                                    targetInfo = (Int32.Parse(qubitInfo[1]), Int32.Parse(qubitInfo[2]), -1, -1);
                                }
                                else if (infoTypes[0] == "PQterm") 
                                {
                                    // FLAG NEEDS TO BE FIXED
                                    string[] PQ = qubitInfo[1].Split("-");
                                    targetInfo = (Int32.Parse(PQ[0]), Int32.Parse(PQ[1]), -1, -1);
                                }
                                else if (infoTypes[0] == "PQQRterm") 
                                {
                                    targetInfo = (Int32.Parse(qubitInfo[1]), -1, -1, -1);
                                }
                                else if (infoTypes[0].Contains("0123term")) 
                                {
                                    string[] PQ = qubitInfo[1].Split("-");
                                    string[] RS = qubitInfo[2].Split("-");
                                    targetInfo = (Int32.Parse(PQ[0]), Int32.Parse(PQ[1]), Int32.Parse(RS[0]), Int32.Parse(RS[1]));
                                }
                                else 
                                {
                                    Console.WriteLine("ERROR: Unknown term type");
                                }

                                var totalInfo = (infoTypes[0], targetInfo, float.Parse(infoTypes[2]));
                                listOfGates.Add(totalInfo);
                            }
                        }
                        else if (ln == "----- BEGIN ORACLE WRITE -----") 
                        {
                            lineHasInfo = true;
                        }
                    }
                    file.Close();
                }

                // also use the quantum computer 
                using (var qsim = new QuantumSimulator())
                {
                    // AcceptArray<(string, (int, int, int, int), float)>.Run(qsim, new QArray<(string, (int, int, int, int), float)>(listOfGates)).Wait();
                    ApplyFromFile.Run(qsim, 4, listOfGates).Wait();
                }
            }
            else 
            {
                Console.WriteLine("ERROR: File not found.");
            }
        }
    }
}