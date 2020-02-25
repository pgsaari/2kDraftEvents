using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace _2KDraftEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            var players = new List<Player>();
            using (var reader = new StreamReader("..\\players.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                players = csv.GetRecords<Player>().ToList();
            }

            foreach (var p in players)
            {
                Console.WriteLine(p.DraftPosition + " " + p.Name);
            }
        }
    }
}
