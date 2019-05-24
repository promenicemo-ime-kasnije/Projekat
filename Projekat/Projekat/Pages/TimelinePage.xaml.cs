using ClassLibrary;
using ClassLibrary.DataProvider;
using Projekat.Pomocne_klase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for TimelinePage.xaml
    /// </summary>
    public partial class TimelinePage : Page
    {
        public ObservableCollection<Aktivnost> Aktivnosti { get; set; } // kolekcija svih aktivnosti koje se ucitaju
        public ObservableCollection<Korisnik> Korisnici { get; set; } // kolekcija svih korisnika koji rade na projektu koji se prikazuju u listi za izbor korisnika
        private List<string> IzabraniKorisnici = new List<string>(); // izabrani korsnici, kad se chekira combobox onda se ovde dodaju, kad se uncheck onda se uklone

        private bool zaSveProjekte; // Da li treba prikazati aktivnosti za sve projekte
        private Dictionary<int, string> NaziviProjekata; // U Aktivnosti imam id projekta a treba mi njegovo ime, pa to resavam preko Dictionary

        /// <summary>
        /// Konsturktor
        /// </summary>
        /// <param name="zaSveProjekte">Da li treba ucitati i prikazati aktivnosti za sve projekte</param>
        public TimelinePage(bool zaSveProjekte = true)
        {
            this.zaSveProjekte = zaSveProjekte;

            Aktivnosti = new ObservableCollection<Aktivnost>();
            Korisnici = new ObservableCollection<Korisnik>();
            NaziviProjekata = new Dictionary<int, string>();

            Loaded += TimelinePage_Loaded;
            InitializeComponent();
            DataContext = this;
        }

        #region Ucitavanje podataka

        private async void TimelinePage_Loaded(object sender, RoutedEventArgs e)
        {
            await UcitajAktivnosti();
            await UcitajKorisnike();
        }

        private async Task UcitajKorisnike()
        {
            var dataProvider = new EFCoreDataProvider();
            IList<Korisnik> temp;
            if (zaSveProjekte)
                temp = await dataProvider.GetKorisniciAsync();
            else
                temp = await dataProvider.GetKorisnikeProjektaAsync(Helper.TrenutniProjekat.IDProjekta);

            foreach (Korisnik k in temp)
                Korisnici.Add(k);   
        }

        private async Task UcitajAktivnosti()
        {
            string vrstaKorisnika = Helper.TrenutniKorisnik.VrstaKorisnika;
            IList<Aktivnost> temp; // treba mi da privremeno pamti aktivnosti
            if (zaSveProjekte)
                temp = await new EFCoreDataProvider().GetAktivnostiAsync(vrstaKorisnika);
            else
            {
                int idProjekta = Helper.TrenutniProjekat.IDProjekta;
                temp = await new EFCoreDataProvider().GetAktivnostiProjektaAsync(idProjekta, vrstaKorisnika);
            }

            await UzmiNaziveProjekata();

            Aktivnosti.Clear();
            foreach (var a in temp)
            {
                a.NazivProjekta = NaziviProjekata[a.IDProjekta];
                Aktivnosti.Add(a);
            }

        }

        private async Task UzmiNaziveProjekata()
        {
            var projekti = await new EFCoreDataProvider().GetProjekteAsync();

            NaziviProjekata.Clear();
            foreach (var p in projekti)
                NaziviProjekata.Add(p.IDProjekta, "Projekat: " + Regex.Replace(p.NazivProjekta, @"\s+", " ")); // Da izbacim razmak iz naziva projekta jer nije u bazi varchar i vraca mi gomilu " "
        }

        #endregion

        #region Filtiranje

        // Filtiranje ne pristupa bazi za infomacije, vec nad originalnom kolekcijom Aktivnosti izvrsi neke operacije i izdovji trazene
        // aktivnosti u novu (privremenu) listu i to postavlja kao ItemsSource lvAktivnostis

        private void PrimeniFiltere_Click(object sender, RoutedEventArgs e)
        {

            // Filtiranje korisnika
            var rez = Aktivnosti.Where(ak => IzabraniKorisnici.Any(k => ak.Poruka.Contains(k)));

            // Filtiranje po tipu aktivnosti
            string trazenaRec = string.Empty;
            switch (cbVrstaAktivnosti.Text)
            {
                case "Dodavanje dokumenta":
                    trazenaRec = "dokument";
                    break;
                case "Prilaganje zahteva":
                    trazenaRec = "zahtev";
                    break;
                case "Troskovi":
                    trazenaRec = "trosak";
                    break;
                case "Upravljanje clanovima":
                    trazenaRec = "jen "; // ne znam koliko ce ovo dobro da radi, imaju dva slucaja "je dodeljen projektu" i "je uklonjen sa projekta" i oba sadrze "jen "
                    break;
            }


            rez = rez.Where(ak => ak.Poruka.Contains(trazenaRec));

            // Filtriranje po datumu aktivnosti
            DateTime pocetak = dpPocetniDatum.SelectedDate ?? new DateTime(1, 1, 1); // ako nije unet pocetni datum onda se postavlja 1/1/1 tj. prikazuje aktivnosti od pocekta
            DateTime kraj = dpKrajnjiDatum.SelectedDate ?? new DateTime(9999, 1, 1); // ako nije unet kranji datum onda se postavlja 1/1/9999 tj. prikazuje aktivnosti do kraja

            // moram ovako jer se ak.Datum u bazi pamti kao string pa ga konvertujem iz stringa u DateTime
            rez = rez.Where(ak => Convert.ToDateTime(ak.Datum).Date >= pocetak && Convert.ToDateTime(ak.Datum).Date <= kraj);


            // Postavi pomocnu listu rez kao itemssource lvAktivnosti
            lvAktivnosti.ItemsSource = rez;
        }

        private void ObrisiFiltere_Click(object sender, RoutedEventArgs e)
        {
            // resetuj listu korisnika, samo mu switcham itemsource i to bi trebalo da radi
            var temp = lvKorisnici.ItemsSource;
            lvKorisnici.ItemsSource = null;
            lvKorisnici.ItemsSource = temp;

            // ocisti listu izabranih korisnika
            IzabraniKorisnici.Clear();

            // resetuj sva polja
            cbVrstaAktivnosti.SelectedIndex = -1;
            dpPocetniDatum.SelectedDate = dpKrajnjiDatum.SelectedDate = null;

            // vrati itemssource na originalu listu "Aktivnosti"
            lvAktivnosti.ItemsSource = Aktivnosti;
        }

        // Izabrani koricnici se pamte u listi, kad se chekira combobox onda se oni dodaju u listu, kad se uncheck onda se uklone
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            IzabraniKorisnici.Add(cb.Content as string);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            IzabraniKorisnici.Remove(cb.Content as string);
        }


        #endregion
    }
}
