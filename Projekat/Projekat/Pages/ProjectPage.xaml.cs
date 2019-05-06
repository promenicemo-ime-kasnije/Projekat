using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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

        private void PrikaziTabelu_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new TabelaPage();
        }

        private void PrikaziTimeline_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new TimelinePage();
        }

        private void ComingSoon_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coming soon!");
        }
    }
}
