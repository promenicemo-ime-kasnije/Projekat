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
    /// Interaction logic for pocetnaStranaUC.xaml
    /// </summary>
    public partial class pocetnaStranaUC : UserControl
    {
        public pocetnaStranaUC()
        {
            InitializeComponent();
        }

        private void DodajNoviProj_Click(object sender, RoutedEventArgs e)
        {
            dodajNoviProjekat dnp = new dodajNoviProjekat();
            Grid parentform = (this.Parent as Grid);
            parentform.Children.Remove(this);
            parentform.Children.Add(dnp);
        }
    }
}
