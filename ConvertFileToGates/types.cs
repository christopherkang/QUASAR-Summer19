using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Quantum.Simulation.Core;

namespace ConvertFileToGates
{
    public static class Auxiliary
    {
        public static List<CompressedHamiltonian> ProduceTermInfo(
                JObject data
            )
        {
            var out_file = new List<CompressedHamiltonian>();
            var term_data = data["terms"];

            foreach (var term in term_data)
            {
                string term_type = (string) term["type"];
                double angle = (double) term["angle"];
                var controls = new QArray<Int64>(term["controls"].ToObject<long[]>());
                var ops = new QArray<Int64>(term["ops"].ToObject<long[]>());
                var targets = new QArray<Int64>(term["targets"].ToObject<long[]>());
                out_file.Add(new CompressedHamiltonian((term_type, angle, (controls, targets), ops)));
            }
            return out_file;
        }
    }
}