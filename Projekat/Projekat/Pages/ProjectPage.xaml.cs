using Projekat.Pomocne_klase;
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
    /// Interaction logic for ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        public List<string> ListaProjekata { get; set; }
        

        public ProjectPage()
        {
            ListaProjekata = new List<string>() { "Fabrika Cigla", "Supermarket Migros", "Ferizova prodavnica", "Hotel Hibis" };
            InitializeComponent();
            DataContext = this;
            Frame.Content = new DokumentaPage();
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Window).Content = new StartPage();
        }

        private void PrikaziDokumenta_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new DokumentaPage();
        }

        private void PrikaziTroskove_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new TroskoviPage();
        }

        private void PrikazuTabelu_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new TabelaPage();
        }

        private void PrikaziTimeline_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new TimelinePage();
        }

        private void BackToStartPage_Click(object sender, RoutedEventArgs e)
        {
            string poruka = "Nisam ovo jos uradio kako treba, za sad kad se klikne ovde moze samo da se ide nazad na pocetnu stranu. Da li to hocete?";
            var result = MessageBox.Show(poruka, "Pitanje", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                (Parent as Window).Content = new StartPage();
        }
    }
}
