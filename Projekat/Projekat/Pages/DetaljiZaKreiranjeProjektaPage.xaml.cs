using ClassLibrary;
using ClassLibrary.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekat.Pages
{
    /// <summary>
    /// Interaction logic for DetaljiZaKreiranjeProjektaPage.xaml
    /// </summary>
    public partial class DetaljiZaKreiranjeProjektaPage : Page
    {
        public ClassLibrary.Projekat ZapocetiProjekat { get; set; }

        public DetaljiZaKreiranjeProjektaPage(ClassLibrary.Projekat projekat)
        {
            ZapocetiProjekat = projekat;
            InitializeComponent();
            DataContext = this;
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Window).Content = new StartPage();
        }

        private async void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            // Dodavanje podataka za trenutni projekat
            ZapocetiProjekat.DatumPocetka = dpDatumPocetkaProjekta.DisplayDate.ToShortDateString();
            ZapocetiProjekat.StanjeProjekta = "Aktivan";

            // Kreiranje i prikazivanje progress windowa
            var progressWindow = new Window { Title = "Kreiranje projekta u toku", Height = 200, Width = 600 , Content = new ProgressPage("Kreiranje projekta u toku")};
            progressWindow.Show();

            // U bazi se kreira novi projekat i za njega se kreiraju odgovarajuca dokumenta
            await new EFCoreDataProvider().KreirajProjekatIDodajDokumenta(ZapocetiProjekat, GetListuDokumenata(ZapocetiProjekat.VrstaProjekta));

            // Zatvaranja ProgressWindow-a
            progressWindow.Close();

            // Prelazak na ClanoviProjektaPage
            (Parent as Window).Content = new ClanoviProjektaPage();
        }

        private Dokumentacija[] GetListuDokumenata(string vrstaProjekta)
        {
            // U zavisnosti od vrsta projekta vracaju se razlicita dokumenta, ako tako uopste treba da buce
            // mozda sve vrsta imaju istu listu?

            return new Dokumentacija[]
            {
                new Dokumentacija{ Naziv = "Dokaz o uplati administrativne takse", Redosled = 1 },
                new Dokumentacija{ Naziv = "Idejno resenje"                      , Redosled = 2 },
                new Dokumentacija{ Naziv = "Kopija plana za kat. parcelu"        , Redosled = 2 },
                new Dokumentacija{ Naziv = "Izvod i katastra vodova"             , Redosled = 2 },
                new Dokumentacija{ Naziv = "Podatak o povrsini parcele"          , Redosled = 3 },
                new Dokumentacija{ Naziv = "Dokaz o uplati administrativne takse", Redosled = 3 },
                new Dokumentacija{ Naziv = "Idejno resenje"                      , Redosled = 3 },
                new Dokumentacija{ Naziv = "Kopija plana za kat. parcelu"        , Redosled = 3 },
                new Dokumentacija{ Naziv = "Izvod i katastra vodova"             , Redosled = 3 },
                new Dokumentacija{ Naziv = "Podatak o povrsini parcele"          , Redosled = 4 },
                new Dokumentacija{ Naziv = "Dokaz o uplati administrativne takse", Redosled = 5 },
                new Dokumentacija{ Naziv = "Idejno resenje"                      , Redosled = 6 },
                new Dokumentacija{ Naziv = "Kopija plana za kat. parcelu"        , Redosled = 6 },
                new Dokumentacija{ Naziv = "Izvod i katastra vodova"             , Redosled = 6 },
                new Dokumentacija{ Naziv = "Podatak o povrsini parcele"          , Redosled = 7 }
            };
        }
    }
}

