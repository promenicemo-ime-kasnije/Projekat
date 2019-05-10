using ClassLibrary;
using ClassLibrary.DataProvider;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using Microsoft.Win32;
using Projekat.Pomocne_klase;
using System.Threading.Tasks;

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

        private int projectID;
        private IList<Dokumentacija> svaDokumenta;

        public DokumentaPage(int projectID)
        {
            this.projectID = projectID;
            InitializeComponent();
            DataContext = this;
            Loaded += DokumentaPage_Loaded;
        }

        private async void DokumentaPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await UcitajListe(projectID);
            DataGrid.ItemsSource = OblastLokacijskiUslovi;
        }

        private async Task UcitajListe(int projectID)
        {
            OblastInformacijaOLokaciji = new ObservableCollection<Dokumentacija>();
            OblastLokacijskiUslovi = new ObservableCollection<Dokumentacija>();
            OblastGradjevinskaDozvola= new ObservableCollection<Dokumentacija>();
            OblastIzgradnja= new ObservableCollection<Dokumentacija>();

            svaDokumenta = await new EFCoreDataProvider().GetDokumentaProjekta(projectID);
            // Ako za dati projekat nema dokumenata
            if (svaDokumenta.Count == 0)
                return;

            ProveriKojiDokumentiImajuUslov();
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

        private void ProveriKojiDokumentiImajuUslov()
        {
            svaDokumenta[0].IsEnable = true; // informacija o lokaciji je uvek ukljucena
            for (int i = 1; i < svaDokumenta.Count; i++)
                svaDokumenta[i].IsEnable = IspunjenUslov(svaDokumenta, i);
        }

        private bool IspunjenUslov(IList<Dokumentacija> svaDokumenta, int i)
        {
            for (int j = 0; j < i; j++)
                if (svaDokumenta[j].StatusDokumenta == false && svaDokumenta[j].Redosled < svaDokumenta[i].Redosled)
                    return false;
            return true;
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

            if ( await dataProvider.DokumentImaPDFFajl(doc.IDDokumenta)) // Otvori taj fajl, da li je StatusDokumenta isto kao i to Da li ima pdf fajl??? za sad jeste
            {
                string filename = "temp.pdf";
                File.WriteAllBytes(filename, (await dataProvider.GetPDFAsync(doc.IDDokumenta)).PDFFajl); //ovo kreira lokalni pdf fajl od bajtova 
                System.Diagnostics.Process.Start(filename); // Otvara ga u default pdf vieweru
            }
            else // Prilozi pdf fajl
            {
                byte[] a;
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "PDF dokument | *.pdf";

                if (dlg.ShowDialog() == true)
                {
                    // Kreira se i prikazu progressWindows sa porukom dok se vrsi slanje dokumenta na server
                    var progressWindow = new ProgressWindow("Upload PDF dokumenta");
                    progressWindow.Show();

                    string path = dlg.FileName.ToString();
                    a = File.ReadAllBytes(path); //ovo pretvara izabrani fajl u bajtove
                    // Dodavanje novog PDF u tabelu 
                    await dataProvider.AddPDFAsync( new PDF
                    {
                        IDDokumenta = doc.IDDokumenta,
                        PDFFajl = a
                    });

                    //Update dokumenta
                    doc.StatusDokumenta = true;
                    await dataProvider.UpdateDokumentAsync(doc);

                    // ProgressWindow moze da se zatvori
                    progressWindow.Close();

                    // Update podataka koji se prikazuju u dataGridu
                    ProveriKojiDokumentiImajuUslov();
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
