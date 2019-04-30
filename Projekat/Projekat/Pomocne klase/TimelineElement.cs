using System;
using System.Collections.Generic;
using System.Linq;

namespace Projekat.Pomocne_klase
{
    public class TimelineElement
    {
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

        public TimelineElement(string text, DateTime dateTime)
        {
            Text = text;
            DateTime = dateTime;
        }

        public static List<TimelineElement> GetTimelineElements()
        {
            var lista = new List<TimelineElement>();
            string[] korisnici = { "Samed", "Ahmet", "Armin", "Milan" };
            string[] projekti = { "Fabrika Cigla", "Supermarket Migros", "Ferizova prodavnica", "Hotel Hibis" };

            Random rng = new Random();

            foreach (var k in korisnici)
                foreach (var p in projekti)
                    lista.Add(new TimelineElement($"{k} je dodao dokument XY za projekat {p}", new DateTime(2019, rng.Next(12) + 1, rng.Next(25) + 1)));

            lista.OrderBy(e => e.DateTime);

            return lista;
        }
    }
}
