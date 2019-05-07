﻿using ClassLibrary;
using ClassLibrary.DataProvider;
using System.Windows;
using System.Windows.Controls;

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
            var parent = (Parent as MainWindow);
            parent.TrenutniProjekat = ZapocetiProjekat;
            parent.Content = new ClanoviProjektaPage();
        }

        private Dokumentacija[] GetListuDokumenata(string vrstaProjekta)
        {
            // U zavisnosti od vrsta projekta vracaju se razlicita dokumenta, ako tako uopste treba da buce
            // mozda sve vrsta imaju istu listu?

            return new Dokumentacija[]
            {
                new Dokumentacija{ Naziv = "Informacija o lokaciji"                                      , Redosled = 0, StatusDokumenta = "Ispunjeni uslovi" },
                // LOKACIJSKI USLOVI                                                                     
                new Dokumentacija{ Naziv = "Dokaz o uplati administrativne takse 1"                      , Redosled = 1, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Idejno resenje"                                              , Redosled = 1, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Kopija plana za katastarsku parcelu"                         , Redosled = 2, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Izvod i katastra vodova"                                     , Redosled = 2, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Podatak o povrsini parcele"                                  , Redosled = 2, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Uslovi za projektovanje i prikljucivanje"                    , Redosled = 3, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Obavestenje da ne mogu da se izdaju uslovi"                  , Redosled = 3, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Obavestenje o visini stvarnih troskova"                      , Redosled = 3, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Dokaz o uplati troskova"                                     , Redosled = 4, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Lokacijski uslovi"                                           , Redosled = 5, StatusDokumenta = "Na cekanju" },
                // GRADJEVINSKA DOZVOLA
                new Dokumentacija{ Naziv = "Izvod iz projekta za gradjevinsku dozvolu"                   , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Projekat za gradjevinsku dozvolu"                            , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Dokaz o uplati administrativne takse 2"                      , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Dokaz o pravu na zeljistu ili objektu"                       , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Dokaz o pravu na zemljistu ili objektu 2"                    , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Ugovor izmedju investitora i finansijera"                    , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Uslovi za prikljucenje"                                      , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Energetska dozvola"                                          , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Sredstva obezbedjenja"                                       , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Saglasnost preostalih suvlasnika"                            , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Geodetski snimak postojeceg stanja"                          , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Izjasnjenje o nacinu placanja doprinosa"                     , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Ugovor izmedju investitora i imaoca javnih ovlascenja"       , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Dokaz o placenoj naknadi za promene namene zemljista"        , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Dokaz o uredjenju medusobnih odnosa sa vlasnikom"            , Redosled = 6, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Izvoda iz lista nepokretnosti"                               , Redosled = 7, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Gradjevinska dozvola"                                        , Redosled = 8, StatusDokumenta = "Na cekanju" },
                // IZGRADNJA
                new Dokumentacija{ Naziv = "Dokaz o uplati administrativne takse 3"                      , Redosled = 9, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Projekat za izvodenje"                                       , Redosled = 9, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Ugovor o pravu sluzbenosti"                                  , Redosled = 9, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Dokaz o izmirenju doprinosa za uredenje gradj. zemljista"    , Redosled = 9, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Saglasnost na studiju o proceni uticaja na zivostnu sredinu" , Redosled = 9, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Izjava o datumu pocetka izvodjenja radova"                   , Redosled = 9, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Izjava o roku zavrsetka izvodjenja radova"                   , Redosled = 9, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Dokaz o uplati administrativne takse 4"                      , Redosled = 10, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Geodetski snimak izgradjenih temelja"                        , Redosled = 10, StatusDokumenta = "Na cekanju" },
                new Dokumentacija{ Naziv = "Dokaz o uplati administrativne takse 5"                      , Redosled = 11, StatusDokumenta = "Na cekanju" }
            };
        }
    }
}

