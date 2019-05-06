using ClassLibrary;
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

namespace Projekat.UserControls
{
    /// <summary>
    /// Sadrzi vise DokumentItem userControl-a i objedinjuje ih u jednu kontrolu, kao atribute ima Naslov i listu dokumenata koji se prikazuju
    /// </summary>
    public partial class ListaDokumenata : UserControl
    {
        public string Naslov
        {
            get { return (string)GetValue(NaslovProperty); }
            set { SetValue(NaslovProperty, value); }
        }

        public static readonly DependencyProperty NaslovProperty =
            DependencyProperty.Register("Naslov", typeof(string), typeof(ListaDokumenata), new PropertyMetadata(string.Empty));


        public ObservableCollection<Dokumentacija> Dokumenta
        {
            get { return (ObservableCollection<Dokumentacija>)GetValue(DokumentaProperty); }
            set { SetValue(DokumentaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dokumenta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DokumentaProperty =
            DependencyProperty.Register("Dokumenta", typeof(ObservableCollection<Dokumentacija>), typeof(ListaDokumenata), new PropertyMetadata(null));


        public ListaDokumenata()
        {
            InitializeComponent();
        }
    }
}
