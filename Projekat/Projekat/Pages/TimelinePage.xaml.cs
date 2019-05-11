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
        public ObservableCollection<Aktivnost> Aktivnosti { get; set; }
        private bool zaSveProjekte; // Da li treba prikazati aktivnosti za sve projekte
        private Dictionary<int, string> NaziviProjekata; // U Aktivnosti imam id projekta a treba mi njegovo ime, pa to resavam preko Dictionary

        public TimelinePage(bool zaSveProjekte = true)
        {
            this.zaSveProjekte = zaSveProjekte;

            Aktivnosti = new ObservableCollection<Aktivnost>();
            NaziviProjekata = new Dictionary<int, string>();

            Loaded += TimelinePage_Loaded;
            InitializeComponent();
            DataContext = this;
        }

        private async void TimelinePage_Loaded(object sender, RoutedEventArgs e)
        {
            string vrstaKorisnika = Helper.GetTrenutniKorisnik(this).VrstaKorisnika;
            IList<Aktivnost> temp; // treba mi da privremeno pamti aktivnosti
            if (zaSveProjekte)
                temp = await new EFCoreDataProvider().GetAktivnostiAsync(vrstaKorisnika);
            else
            {
                int idProjekta = Helper.GetTrenutniProjekat(this).IDProjekta;
                temp = await new EFCoreDataProvider().GetAktivnostiProjektaAsync(idProjekta, vrstaKorisnika);
            }

            await UzmiNaziveProjekata();

            foreach (var a in temp)
            {
                a.NazivProjekta = NaziviProjekata[a.IDProjekta];
                Aktivnosti.Add(a);
            }

        }

        private async Task UzmiNaziveProjekata()
        {
            var projekti = await new EFCoreDataProvider().GetProjekteAsync();
            foreach (var p in projekti)
                NaziviProjekata.Add(p.IDProjekta, "Projekat: " + Regex.Replace(p.NazivProjekta, @"\s+", " ")); // Da izbacim razmak iz naziva projekta jer nije u bazi varchar i vraca mi gomilu " "
        }
    }
}
