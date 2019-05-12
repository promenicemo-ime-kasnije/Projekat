using ClassLibrary;
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
    /// Interaction logic for DetaljiTroska.xaml
    /// </summary>
    public partial class DetaljiTroska : UserControl
    {



        public Trosak Trosak
        {
            get { return (Trosak)GetValue(TrosakProperty); }
            set { SetValue(TrosakProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Trosak.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrosakProperty =
            DependencyProperty.Register("Trosak", typeof(Trosak), typeof(DetaljiTroska), new PropertyMetadata(null));




        public DetaljiTroska()
        {
            InitializeComponent();
        }
    }
}
