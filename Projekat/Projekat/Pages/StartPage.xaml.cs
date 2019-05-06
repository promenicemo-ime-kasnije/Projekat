using ClassLibrary.DataProvider;
using Projekat.Pages;
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

namespace Projekat
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// TODO: arhirivani projekti da mogu da se scroluju
    /// </summary>
    public partial class StartPage : Page
    {
        public ObservableCollection<ClassLibrary.Projekat> ListaAktivnihProjekata { get; set; }
        public ObservableCollection<ClassLibrary.Projekat> ListaArhiviranihProjekata { get; set; }

        public StartPage()
        {
            ListaAktivnihProjekata = new ObservableCollection<ClassLibrary.Projekat>();
            ListaArhiviranihProjekata = new ObservableCollection<ClassLibrary.Projekat>();
            UcitajProjekte();

            InitializeComponent();
            DataContext = this;
        }

        private async Task UcitajProjekte()
        {
            var projekti = await new EFCoreDataProvider().GetProjekteAsync() as List<ClassLibrary.Projekat>;

            ListaAktivnihProjekata.Clear();
            ListaArhiviranihProjekata.Clear();

            foreach (ClassLibrary.Projekat p in projekti)
                if (p.StanjeProjekta.Contains("Aktivan"))
                    ListaAktivnihProjekata.Add(p);
                else if (p.StanjeProjekta.Contains("Arhiviran"))
                    ListaArhiviranihProjekata.Add(p);
        }

        private void OdjaviSe_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Window).Content = new LoginPage();
        }

        private void Arhiva_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Window).Content = new ArhivaPage();
        }

        private void NoviProjekat_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Window).Content = new NoviProjekatPage();
        }

        private void OtvoriProjekat_Click(object sender, MouseButtonEventArgs e)
        {
            var p = (sender as ListView).SelectedItem as ClassLibrary.Projekat;
            (Parent as MainWindow).TrenutniProjekat = p;
            (Parent as Window).Content = new ProjectPage();
        }
    }
}
