using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OdabirProjektaPage : ContentPage
    {
        public OdabirProjektaPage()
        {
            InitializeComponent();
            lvProjekti.ItemsSource = new string[] { "Fabrika Blok", "Stambena zgrada Neboder", "Hotel Hibis", "Ribnjak Oslic", "Fabrika Blok", "Stambena zgrada Neboder", "Hotel Hibis", "Ribnjak Oslic", "Fabrika Blok", "Stambena zgrada Neboder", "Hotel Hibis", "Ribnjak Oslic", "Fabrika Blok", "Stambena zgrada Neboder", "Hotel Hibis", "Ribnjak Oslic", };
        }

        private void LvProjekti_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            lvProjekti.SelectedItem = null;
            Navigation.PushAsync(new ProjektiPage());
        }
    }
}