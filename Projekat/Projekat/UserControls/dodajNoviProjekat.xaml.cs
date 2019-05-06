using ClassLibrary.DataProvider;
using ClassLibrary;
using MaterialDesignThemes.Wpf;
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
using Projekat.Pages;

namespace Projekat.UserControls
{
    /// <summary>
    /// Interaction logic for dodajNoviProjekat.xaml
    /// </summary>
    public partial class dodajNoviProjekat : UserControl
    {
        public List<TipProjekta> TipoviProjekta { get; set; }

        public dodajNoviProjekat()
        {
            TipoviProjekta = new List<TipProjekta>()
            {
                new TipProjekta("Stambena zgrada", PackIconKind.House),
                new TipProjekta("Poslovna zgrada", PackIconKind.OfficeBuilding),
                new TipProjekta("Fabrika", PackIconKind.Factory)
            };
            InitializeComponent();
            DataContext = this;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            pocetnaStranaUC psuc = new pocetnaStranaUC();
            Grid parentform = (this.Parent as Grid);
            parentform.Children.Remove(this);
            parentform.Children.Add(psuc);
        }

        private void DodajNoviProjekat_Click(object sender, RoutedEventArgs e)
        {
            if (SvaPoljaPopunjena())
            {
                OtvoriDetaljeZaKreiranjeProjekta();
            }
            else
                txtGreska.Text = "Niste popunili sva polja!";
        }

        private void OtvoriDetaljeZaKreiranjeProjekta()
        {
            ClassLibrary.Projekat zapocetiProjekat = new ClassLibrary.Projekat
            {
                NazivProjekta = tbImeProjekta.Text,
                VrstaProjekta = (lvTipoviProjekta.SelectedItem as TipProjekta).Naziv
            };

            Window mainWindow = ((((Parent as Grid).Parent as Grid).Parent as Grid).Parent as Page).Parent as Window; //wtf
            mainWindow.Content = new DetaljiZaKreiranjeProjektaPage(zapocetiProjekat);
        }

        private bool SvaPoljaPopunjena()
        {
            return  !string.IsNullOrEmpty(tbImeProjekta.Text) &&
                    !string.IsNullOrEmpty(tbInvestitor.Text) &&
                    lvTipoviProjekta.SelectedItem != null;
        }

        //private async void KreirajNoviProjekat()
        //{
        //    var dataProvider = new EFCoreDataProvider();
        //    await dataProvider.AddProjekatAsync(new ClassLibrary.Projekat
        //    {
        //        NazivProjekta = tbImeProjekta.Text,
        //        VrstaProjekta = (lvTipoviProjekta.SelectedItem as TipProjekta).Naziv
        //    });
        //}
    }

    // Mala pomocna klasa 
    public class TipProjekta
    {
        public string Naziv { get; set; }
        public PackIconKind Ikonica { get; set; }

        public TipProjekta(string naziv, PackIconKind ikonica)
        {
            Naziv = naziv;
            Ikonica = ikonica;
        }
    }
}
