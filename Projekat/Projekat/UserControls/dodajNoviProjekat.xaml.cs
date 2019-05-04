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
        public dodajNoviProjekat()
        {
            InitializeComponent();
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
}
