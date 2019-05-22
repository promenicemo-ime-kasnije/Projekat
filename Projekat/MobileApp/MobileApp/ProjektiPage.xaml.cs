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
    public partial class ProjektiPage : MasterDetailPage
    {
        public ProjektiPage()
        {
            InitializeComponent();
            string[] myPageNames = { "Dokumenta", "Zahtevi", "Troskovi", "Zapisnik aktivnosti", "Clanovi projekta", "Podesavanja projekta", "Nazad" };
            menu.ItemsSource = myPageNames;

            // default page 
            Detail = new NavigationPage(new GenericPage("Dokumenta"));
        }

        private async void Menu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item.ToString() == "Nazad")
            {
                await Navigation.PopAsync();
            }


            Page gotoPage = new GenericPage(e.Item.ToString());
            switch (e.Item.ToString())
            {
                case "Projekti":
                    //gotoPage = new DetailsPage();
                    break;
                case "Aktivnosti":
                    //gotoPage = new MainPage();
                    break;
                case "Zahtevi":
                    //gotoPage = new DokumentaPage();
                    break;
                default:
                    //gotoPage = new MainPage();
                    break;
            }
            Detail = new NavigationPage(gotoPage);
            ((ListView)sender).SelectedItem = null;
            this.IsPresented = false;
        }
    }
}