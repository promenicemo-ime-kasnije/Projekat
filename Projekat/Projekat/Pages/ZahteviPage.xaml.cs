using ClassLibrary;
using ClassLibrary.DataProvider;
using Projekat.Pomocne_klase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ZahteviPage.xaml
    /// </summary>
    public partial class ZahteviPage : Page
    {
        public ObservableCollection<Zahtev> Zahtevi { get; set; } = new ObservableCollection<Zahtev>();

        public ZahteviPage()
        {
            InitializeComponent();
            Loaded += ZahteviPage_Loaded;
            DataContext = this;
        }

        private async void ZahteviPage_Loaded(object sender, RoutedEventArgs e)
        {
            await UcitajZahteve();
            await UcitajKorisnikeUComboBox();
        }

        private async Task UcitajZahteve()
        {
            // ovo mora malo da se preradi, kasnije
            var lista = await new EFCoreDataProvider().GetZahteveProjektaAsync(Helper.TrenutniProjekat.IDProjekta);

            if (Helper.TrenutniKorisnik.VrstaKorisnika != "admin") // ako nije admin onda mu prikazi samo one zahteve sa kojima on ima veze
                lista = lista.Where(z => z.KorisnickoImePosiljaoca == Helper.TrenutniKorisnik.KorisnickoIme || z.KorisnickoImePrimaoca == Helper.TrenutniKorisnik.KorisnickoIme)
                             .OrderByDescending(z => z.IDZahteva).ToList(); // da pocinje od najskorijeg zahteva

            Zahtevi.Clear();
            foreach (var item in lista)
                Zahtevi.Add(item);
        }

        private async Task UcitajKorisnikeUComboBox()
        {
            var korisnici = await new EFCoreDataProvider().GetKorisnikeProjektaAsync(Helper.TrenutniProjekat.IDProjekta);
            korisnici.Remove(Helper.TrenutniKorisnik); // izbacujemo trenutnog korisnika, ne moze da salje samom sebi
            cbPrijemnik.ItemsSource = korisnici;
        }

        #region Dodavanje zahteva
        private async void DodajZahtev_Click(object sender, RoutedEventArgs e)
        {
            Zahtev zahtev = new Zahtev
            {
                IDProjekta = Helper.TrenutniProjekat.IDProjekta,
                KorisnickoImePosiljaoca = Helper.TrenutniKorisnik.KorisnickoIme,
                KorisnickoImePrimaoca = cbPrijemnik.SelectedValue as string,
                Naslov = txtKategorija.Text,
                Poruka = txtOpis.Text
            };
            await new EFCoreDataProvider().AddZahtevAsync(zahtev);
            OcistiPolja();

            // refreshuj zahteve
            await UcitajZahteve();
        }

        private void OcistiPolja()
        {
            cbPrijemnik.SelectedIndex = -1;
            txtKategorija.Text = txtOpis.Text = string.Empty;
        }
        #endregion

        private async void OdgovoriNaZahtev_Click(object sender, RoutedEventArgs e)
        {
            var zahtev = (sender as Button).DataContext as Zahtev;
            if (zahtev.Odgovor == null) // znaci zahtev je na cekanju
            {
                if (zahtev.KorisnickoImePrimaoca == Helper.TrenutniKorisnik.KorisnickoIme)
                {
                    var result = MessageBox.Show("Da li zelite da odobrite ovaj zahtev? Vas odgovor je konacan i ne moze se kasnije zameniti!", "Zahtev", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes || result == MessageBoxResult.No)
                    {
                        zahtev.Odgovor = (result == MessageBoxResult.Yes);
                        await new EFCoreDataProvider().UpdateZahtevAsync(zahtev);
                        await DodajAktivnost(zahtev);
                        await UcitajZahteve(); // refreshuj zahteve
                    }
                }
                else
                    MessageBox.Show($"Na ovaj zahtev moze da odgovori samo: {zahtev.KorisnickoImePrimaoca}!");
            }
        }

        private async Task DodajAktivnost(Zahtev zahtev)
        {
            var aktivnost = new Aktivnost
            {
                IDProjekta = Helper.TrenutniProjekat.IDProjekta,
                NazivProjekta = Helper.TrenutniProjekat.NazivProjekta,
                Poruka = $"{Helper.TrenutniKorisnik.PunoIme} je odgovorio na zahtev od {zahtev.KorisnickoImePosiljaoca} ({zahtev}) sa {((bool)zahtev.Odgovor ? "DA" : "NE")}"
        };
            await new EFCoreDataProvider().AddAktivnostAsync(aktivnost);
        }
    }
}
