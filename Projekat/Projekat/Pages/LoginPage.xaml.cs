using ClassLibrary;
using ClassLibrary.DataProvider;
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

namespace Projekat
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async Task<bool> IspravanLogin(string korisnickoIme, string lozinka)
        {
            Korisnik korisnik = await new EFCoreDataProvider().LoginApp(korisnickoIme, lozinka);
            return korisnik != null;
        }

        private bool ImaPraznihPolja()
        {
            return string.IsNullOrEmpty(tbKorisnickoIme.Text) || string.IsNullOrEmpty(tbLozinka.Password);
        }

        private void OcistiPolja()
        {
            tbKorisnickoIme.Text = tbLozinka.Password = string.Empty;
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Meni za obnovu lozinke!");
        }

        private async void BtnPrijaviSe_Click(object sender, RoutedEventArgs e)
        {
            if (ImaPraznihPolja())
            {
                tbGreska.Text = "Niste popunili sva polja!";
            }
            else if (await IspravanLogin(tbKorisnickoIme.Text, tbLozinka.Password))
            {
                // Otvori StartPage
                (Parent as Window).Content = new StartPage();
            }
            else
            {
                tbGreska.Text = "Neispravno korisnicko ime ili lozinka!";
                OcistiPolja();
            }
        }
    }
}
