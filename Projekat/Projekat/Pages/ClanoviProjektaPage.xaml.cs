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
    /// Interaction logic for ClanoviProjektaPage.xaml
    /// </summary>
    public partial class ClanoviProjektaPage : Page
    {
        public ClanoviProjektaPage()
        {
            InitializeComponent();
        }

        private void OtvoriProjectPage_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Window).Content = new ProjectPage();
        }
    }
}
