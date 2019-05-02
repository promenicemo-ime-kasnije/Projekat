using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Pomocne_klase
{
    public class FakeDokument
    {
        public string Naziv { get; set; }
        public bool Zavrsen { get; set; }
        public Type DetailsPage { get; set; }
        public int Redosled { get; set; }

        public FakeDokument(string naziv, bool zavrsen, Type detailsPage, int redosled = 0)
        {
            Naziv = naziv;
            Zavrsen = zavrsen;
            DetailsPage = detailsPage;
            Redosled = redosled;
        }
    }
}
