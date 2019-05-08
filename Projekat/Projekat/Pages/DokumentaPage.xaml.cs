using ClassLibrary;
using ClassLibrary.DataProvider;
using Projekat.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Projekat.Pages
{
    /// <summary>
    /// Interaction logic for DokumentaPage.xaml
    /// </summary>
    public partial class DokumentaPage : Page
    {
        public ObservableCollection<Dokumentacija> OblastInformacijaOLokaciji { get; set; }
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
            OblastInformacijaOLokaciji = new ObservableCollection<Dokumentacija>();
            OblastLokacijskiUslovi = new ObservableCollection<Dokumentacija>();
            OblastGradjevinskaDozvola= new ObservableCollection<Dokumentacija>();
            OblastIzgradnja= new ObservableCollection<Dokumentacija>();

            IList<Dokumentacija> svaDokumenta = await new EFCoreDataProvider().GetDokumentaProjekta(projectID);
            for (int i = 0; i < svaDokumenta.Count; i++)
            {
                if (i == 0)
                    OblastInformacijaOLokaciji.Add(svaDokumenta[i]);
                else if (i >= 1 && i <= 10)
                    OblastLokacijskiUslovi.Add(svaDokumenta[i]);
                else if (i > 10 && i <= 27)
                    OblastGradjevinskaDozvola.Add(svaDokumenta[i]);
                else if (i > 27 && i <= 37)
                    OblastIzgradnja.Add(svaDokumenta[i]);
            }
        }

        private void OdaberiKategoriju_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var rb = sender as RadioButton;

            switch (rb.Content)
            {
                case "Informacija o lokaciji":
                    DataGrid.ItemsSource = OblastInformacijaOLokaciji;
                    break;
                case "Lokacijski uslovi":
                    DataGrid.ItemsSource = OblastLokacijskiUslovi;
                    break;
                case "Gradjevinska dozvola":
                    DataGrid.ItemsSource = OblastGradjevinskaDozvola;
                    break;
                case "Izgradnja":
                    DataGrid.ItemsSource = OblastIzgradnja;
                    break;
            }
        }

        private async void ShowHideDetails(object sender, System.Windows.RoutedEventArgs e)
        {
            Dokumentacija doc = DataGrid.SelectedItem as Dokumentacija;
            var dataProvider = new EFCoreDataProvider();

            if ((DataGrid.SelectedItem as Dokumentacija).PDFFajl != null)
            {
                string filename = "temp.pdf";
                File.WriteAllBytes(filename, doc.PDFFajl); //ovo kreira lokalni pdf fajl od bajtova 
                System.Diagnostics.Process.Start(filename); // Otvara ga u default pdf vieweru
            }
            else
            {
                byte[] a;
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "PDF dokument | *.pdf";
                if (dlg.ShowDialog() == true)
                {
                    string path = dlg.FileName.ToString();
                    a = File.ReadAllBytes(path); //ovo pretvara izabrani fajl u bajtove 
                    doc.PDFFajl = a;
                    await dataProvider.UpdateDokumentAsync(doc); // pamti fajl u bazi

                    UpdateDataGrid();
                }
            }
        }

        // Nece da prikaze da je doslo do promene, pa cu da mu promenim itemsource
        private void UpdateDataGrid()
        {
            var temp = DataGrid.ItemsSource;
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = temp;
        }
    }
}
