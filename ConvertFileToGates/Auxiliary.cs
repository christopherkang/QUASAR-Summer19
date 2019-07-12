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
        public static List<CompressedHamiltonian> ProduceTermInfo(
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

            return out_file;
        }

        public static JWOptimizedHTerms ConvertCompressedToQSharp(
            JObject hamiltonianData
            )
        {
            // seperate the terms into their types
            var _Zterms = new List<CompressedHamiltonian>();
            var _ZZterms = new List<CompressedHamiltonian>();
            var _PQandPQQRterms = new List<CompressedHamiltonian>();
            var _0123terms = new List<CompressedHamiltonian>();

            // extract data 
            var term_list = hamiltonianData["terms"];
            var trotterStep = hamiltonianData["constants"]["trotterStep"];

            foreach (var term in term_list) 
            {
                // rearrange the data inside the terms

                // replace the angles with their values divided by the trotter step size
            }
            return new JWOptimizedHTerms();
        }

        public static JordanWignerEncodingData ProduceJWData(
            JObject hamiltonianData
            )
        {
            // (int, JordanWignerInputState[]) StatePrepData, 
            // int nSpinOrbitals, 
            // float energyOffset 

            // Extract critical values from the data
            var convertedFermionTerms = Auxiliary.ConvertCompressedToQSharp(hamiltonianData);
            var nSpinOrbitals = (int)hamiltonianData["constants"]["nSpinOrbitals"];
            var energyOffset = (float)hamiltonianData["constants"]["energyOffset"];

            // go t
            return new JordanWignerEncodingData((
                nSpinOrbitals, 
                convertedFermionTerms, 
                statePrepData, 
                energyOffset
                ));
        }
    }
}