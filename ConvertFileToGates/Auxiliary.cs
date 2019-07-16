using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Chemistry.JordanWigner;

namespace ConvertFileToGates
{
    public static class Auxiliary
    {
        public static QArray<CompressedHamiltonian> ProduceTermInfo(
            JObject hamiltonianData
            )
        {
            // extract the "terms" data
            var out_file = new List<CompressedHamiltonian>();
            var term_data = hamiltonianData["terms"];

            // extract individual term information and produce a CompressedHamiltonian
            foreach (var term in term_data)
            {
                string term_type = (string)term["type"];
                double angle = (double)term["angle"];
                var controls = new QArray<Int64>(term["controls"].ToObject<long[]>());
                var ops = new QArray<Int64>(term["ops"].ToObject<long[]>());
                var targets = new QArray<Int64>(term["targets"].ToObject<long[]>());
                out_file.Add(new CompressedHamiltonian((term_type, angle, (controls, targets), ops)));
            }

            return new QArray<CompressedHamiltonian>(out_file.ToArray());
        }

        // Takes in the JSON file and produces the fermion terms in the Q# format
        // This will require creating generator indices
        // public static JWOptimizedHTerms ConvertCompressedToQSharp(
        //     JObject hamiltonianData
        //     )
        // {
        //     // seperate the terms into their types
        //     var _Zterms = new List<HTerm>();
        //     var _ZZterms = new List<HTerm>();
        //     var _PQandPQQRterms = new List<HTerm>();
        //     var _0123terms = new List<HTerm>();

        //     // extract data 
        //     var termList = hamiltonianData["terms"];
        //     var trotterStep = hamiltonianData["constants"]["trotterStep"];

        //     foreach (var term in termList) 
        //     {
        //         // rearrange the data inside the terms
        //         var (termType, angle, (controls, targets), ops) = term;
        //         HTerm termToBeAdded;
        //         if (termType.contains("Zterm"))
        //         {
        //             // Z term type:
        //             // HTerm(([target], [angle]))
        //             termToBeAdded = (targets, new Double[](angle / trotterStep));
        //             _Zterms.Add(termToBeAdded);
        //         }
        //         else if (termType.contains("ZZterm"))
        //         {
        //             // ZZ term type
        //             // HTerm(([]))

        //             _ZZterms.Add(termToBeAdded);
        //         }
        //         else if (termType.contains("PQ"))
        //         {
        //             // PQ, PQQR term type

        //             _PQandPQQRterms.Add(termToBeAdded);
        //         }
        //         else if (termType.contains("0123term"))
        //         {
        //             // 0123 term type

        //             _0123terms.Add(termToBeAdded);
        //         }
        //         else 
        //         {
        //             Console.WriteLine($"INFO: {termType} Term not implemented yet");
        //         }
        //         // replace the angles with their values divided by the trotter step size
        //     }
        //     return new JWOptimizedHTerms((
        //         new QArray<HTerm>(_Zterms),
        //         new QArray<HTerm>(_ZZterms),
        //         new QArray<HTerm>(_PQandPQQRterms),
        //         new QArray<HTerm>(_0123terms)));
        // }

        // public static JordanWignerEncodingData ProduceJWData(
        //     JObject hamiltonianData
        //     )
        // {
        //     // (int, JordanWignerInputState[]) StatePrepData, 
        //     // int nSpinOrbitals, 
        //     // float energyOffset 

        //     // Extract critical values from the data
        //     var convertedFermionTerms = Auxiliary.ConvertCompressedToQSharp(hamiltonianData);
        //     var nSpinOrbitals = (long)hamiltonianData["constants"]["nSpinOrbitals"];
        //     var energyOffset = (double)hamiltonianData["constants"]["energyOffset"];
        //     var statePrepData = ProduceStateData(hamiltonianData);

        //     // go t
        //     return new JordanWignerEncodingData((
        //         nSpinOrbitals, 
        //         convertedFermionTerms, 
        //         statePrepData, 
        //         energyOffset
        //         ));
        // }

        public static (long, QArray<JordanWignerInputState>) ProduceStateData(
            JObject stateData
            )
        {
            var raw_data = stateData["statePrepData"];
            var int_data = (long)raw_data["int"];
            var JWData = new List<JordanWignerInputState>();

            foreach (var term_data in raw_data["terms"])
            {
                var int_array = new QArray<Int64>(term_data["array"].ToObject<long[]>());
                var compressed_term = (((double)term_data["tuple"][0], (double)term_data["tuple"][0]), int_array);
                JWData.Add(new JordanWignerInputState((compressed_term)));
            }

            var JW_array = new QArray<JordanWignerInputState>(JWData.ToArray());
            return (int_data, JW_array);
        }
    }
}