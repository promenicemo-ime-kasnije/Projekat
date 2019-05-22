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
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            MyListView.ItemsSource = new string[] { "Projekti", "Aktivnosti", "Zahtevi", "Podesavanja", "Odjavi se" };
            BindingContext = this;
        }

        private async void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Page GoToPage = null;

            switch (e.Item.ToString())
            {
                case "Projekti":
                    GoToPage = new OdabirProjektaPage();
                    break;
                case "Aktivnosti":
                    GoToPage = new AktivnostiPage();
                    break;
                case "Zahtevi":
                    GoToPage = new ZahteviPage();
                    break;
                case "Podesavanja":
                    GoToPage = new PodesavanjaPage();
                    break;
                case "Odjavi se": // ova se malo razlikuje
                    GoToPage = new LoginPage();
                    App.Current.MainPage = GoToPage;
                    return;
            }

            MyListView.SelectedItem = null;
            await Navigation.PushAsync(GoToPage);
        }
    }
}