using ClassLibrary;
using ClassLibrary.DataProvider;
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
    /// Interaction logic for ClanoviProjektaPage.xaml
    /// </summary>
    public partial class ClanoviProjektaPage : Page
    {
        public ObservableCollection<Korisnik> SviKorisnici { get; set; }
        public ObservableCollection<Korisnik> KorisniciKojiRadeNaProjektu { get; set; }
        private int projectID;

        public ClanoviProjektaPage()
        {
            SviKorisnici = new ObservableCollection<Korisnik>();
            KorisniciKojiRadeNaProjektu = new ObservableCollection<Korisnik>();

            InitializeComponent();
            Loaded += ClanoviProjektaPage_Loaded;
            DataContext = this;
        }

        private async void ClanoviProjektaPage_Loaded(object sender, RoutedEventArgs e)
        {
            await UcitajPodatke();
        }

        private async Task UcitajPodatke()
        {
            projectID = (Window.GetWindow(this) as MainWindow).TrenutniProjekat.IDProjekta;
            var sviKorisnici = await new EFCoreDataProvider().GetKorisniciAsync();
            var korisniciProjekta = await new EFCoreDataProvider().GetKorisnikeProjektaAsync(projectID);

            foreach (Korisnik k in sviKorisnici)
                if (korisniciProjekta.Contains(k))
                    KorisniciKojiRadeNaProjektu.Add(k);
                else
                    SviKorisnici.Add(k);
        }

        private void OtvoriProjectPage_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as Window).Content = new ProjectPage();
        }

        private void PrebaciUDruguListu_Click(object sender, MouseButtonEventArgs e)
        {
            var lv = sender as ListView;
            var temp = lv.SelectedItem as Korisnik;
            if (temp == null)
                return;

            if (lv.Tag.ToString() == "1")
            {
                KorisniciKojiRadeNaProjektu.Add(temp);
                SviKorisnici.Remove(temp);
            }
            else
            {
                SviKorisnici.Add(temp);
                KorisniciKojiRadeNaProjektu.Remove(temp);
            }

        }

        private async void ZapamtiIzmene_Click(object sender, RoutedEventArgs e)
        {
            var dataProvider = new EFCoreDataProvider();
            var korisniciUBazi = await dataProvider.GetKorisnikeProjektaAsync(projectID) as List<Korisnik>;
            var trenutniProjekat = await dataProvider.GetProjekatAsync(projectID);

            // Izbaci iz baze one koji vise nisu tu
            foreach (Korisnik k in korisniciUBazi)
                if (!KorisniciKojiRadeNaProjektu.Contains(k))
                    await dataProvider.DeleteInterakcijaAsync(k, trenutniProjekat);

            // Dodaj one koji vec nisu bili u bazi
            foreach (Korisnik k in KorisniciKojiRadeNaProjektu)
                if (!korisniciUBazi.Contains(k))
                    await dataProvider.AddInterakcijaAsync(k, trenutniProjekat);

        }
    }
}
