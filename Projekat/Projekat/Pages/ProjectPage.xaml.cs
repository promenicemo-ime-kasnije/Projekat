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
        public List<TimelineElement> TimelineElements { get; set; }

        public ProjectPage()
        {
            ListaProjekata = new List<string>() { "Fabrika Cigla", "Supermarket Migros", "Ferizova prodavnica", "Hotel Hibis" };
            TimelineElements = TimelineElement.GetTimelineElements();
            InitializeComponent();
            DataContext = this;
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Window).Content = new StartPage();
        }
    }
}
