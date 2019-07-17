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