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

        private void Xdodajnoviprojekat_Click(object sender, RoutedEventArgs e)
        {
            pocetnaStranaUC psuc = new pocetnaStranaUC();
            Grid parentform = (this.Parent as Grid);
            parentform.Children.Remove(this);
            parentform.Children.Add(psuc);
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            pocetnaStranaUC psuc = new pocetnaStranaUC();
            Grid parentform = (this.Parent as Grid);
            parentform.Children.Remove(this);
            parentform.Children.Add(psuc);
        }
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
