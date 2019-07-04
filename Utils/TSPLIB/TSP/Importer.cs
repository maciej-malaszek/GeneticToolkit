using GeneticToolkit.Utils.Data;
using GeneticToolkit.Utils.DataStructs;

using System;
using System.IO;

namespace GeneticToolkit.Utils.TSPLIB.TSP
{
    public static class Importer
    {
        public static Vector2F64[] ImportFile(string path)
        {
            var data = new List<Vector2F64>();
            string[] rawData = File.ReadAllLines(path);

            var index = Array.FindIndex(rawData, x => x.StartsWith("NODE_COORD_SECTION")) + 1;

            for (var i = index; i < rawData.Length; i++)
            {
                if (char.IsLetter(rawData[i], 0))
                    continue;

                string[] row = rawData[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (row.Length == 3)
                    data.Add(new Vector2F64
                    {
                        Identifier = Convert.ToInt32(row[0]),
                        X = Convert.ToDouble(row[1], System.Globalization.CultureInfo.InvariantCulture),
                        Y = Convert.ToDouble(row[2], System.Globalization.CultureInfo.InvariantCulture)
                    });
            }

            return data.ToArray();
        }
    }
}