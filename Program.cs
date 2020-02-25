using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace _2KDraftEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            var players = GetPlayers();
            var events = GetDraftEvents();
            foreach (var p in players)
            {
                Console.WriteLine(p.DraftPosition + " " + p.Name);
            }
            foreach (var e in events)
            {
                Console.WriteLine("Prob: {0} Picks {1} - {2} ", e.Probability, e.PickRange[0], e.PickRange[1]);
                foreach (var ef in e.DraftEvents)
                {
                    Console.WriteLine("{0} ({1})", ef.Description, ef.Effect);
                }
            }
        }

        private static List<DraftEventGroup> GetDraftEvents()
        {
            using (var reader = new StreamReader(".\\draftEvents.json"))
            {
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<DraftEventGroup>>(json);
            }
        }

        private static List<Player> GetPlayers()
        {
            var players = new List<Player>();
            using (var reader = new StreamReader(".\\players.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Player>().ToList();
            }
        }
    }
}
