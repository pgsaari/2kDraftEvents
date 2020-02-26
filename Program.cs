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
            var random = new Random();
            
            for (int i = 0; i < 60; i++)
            {
                var p = players[i];
                var eg = events.First(x => x.MinPick <= (i+1) && x.MaxPick >= (i+1));
                
                Console.WriteLine("Drafted at pick " + p.DraftPosition + ": " + p.Name);
                if(random.NextDouble() < eg.Probability) {
                    int randomInt = random.Next(eg.DraftEvents.Count -1);
                    var e = eg.DraftEvents[randomInt];
                    Console.WriteLine("{0} ({1})\n", e.Description, e.Effect);
                }
                else {
                    Console.WriteLine("No Draft Event\n");
                }
            }
            Console.ReadKey();
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
