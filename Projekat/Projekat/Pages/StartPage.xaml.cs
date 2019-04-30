using Projekat.Pages;
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

namespace Projekat
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public List<string> ListaProjekata { get; set; }
        public List<TimelineElement> TimelineElements { get; set; }

        public StartPage()
        {
            ListaProjekata = new List<string>() { "Fabrika Cigla", "Supermarket Migros", "Ferizova prodavnica", "Hotel Hibis" };
            TimelineElements = TimelineElement.GetTimelineElements();

            InitializeComponent();
            DataContext = this;
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
            (Parent as Window).Content = new ProjectPage();
        }
    }
}
