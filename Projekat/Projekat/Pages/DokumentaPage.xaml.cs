using ClassLibrary;
using ClassLibrary.DataProvider;
using Projekat.Pages.DokumentDetails;
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
    /// Interaction logic for DokumentaPage.xaml
    /// </summary>
    public partial class DokumentaPage : Page
    {
        public ObservableCollection<Dokumentacija> OblastLokacijskiUslovi { get; set; }
        public ObservableCollection<Dokumentacija> OblastGradjevinskaDozvola { get; set; }
        public ObservableCollection<Dokumentacija> OblastIzgradnja { get; set; }
        public DokumentaPage(int projectID)
        {
            InitializeComponent();
            DataContext = this;
            UcitajListe(projectID);
        }

        private async void UcitajListe(int projectID)
        {
            IList<Dokumentacija> svaDokumenta = await new EFCoreDataProvider().GetDokumentaProjekta(projectID);
            for (int i = 0; i < svaDokumenta.Count; i++)
            {
                if (i >= 1 && i <= 10)
                    OblastLokacijskiUslovi.Add(svaDokumenta[i]);
                else if (i > 10 && i <= 27)
                    OblastGradjevinskaDozvola.Add(svaDokumenta[i]);
                else if (i > 27 && i <= 37)
                    OblastIzgradnja.Add(svaDokumenta[i]);
            }
        }
             
    }
}
