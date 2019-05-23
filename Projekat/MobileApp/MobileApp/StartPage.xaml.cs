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
    public partial class StartPage : TabbedPage
    {
        public StartPage()
        {
            InitializeComponent();
            //MyListView.ItemsSource = new string[] { "Projekti", "Aktivnosti", "Zahtevi", "Podesavanja", "Odjavi se" };
            BindingContext = this;
        }

        private async void OnClick(object sender, EventArgs e)
        {
            Page gotoPage = null;
            switch ((sender as ToolbarItem).Text)
            {
                case "Podesavanja":
                    gotoPage = new PodesavanjaPage();
                    break;
                case "Nalog":
                    gotoPage = new GenericPage("Nalog Page");
                    break;
                case "Odjavi se": // ova se malo razlikuje
                    App.Current.MainPage = new LoginPage();
                    return;
            }
            await Navigation.PushAsync(gotoPage);
        }

        //private async void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    Page GoToPage = null;

        //    switch (e.Item.ToString())
        //    {
        //        case "Projekti":
        //            GoToPage = new OdabirProjektaPage();
        //            break;
        //        case "Aktivnosti":
        //            GoToPage = new AktivnostiPage();
        //            break;
        //        case "Zahtevi":
        //            GoToPage = new ZahteviPage();
        //            break;
        //        case "Podesavanja":
        //            GoToPage = new PodesavanjaPage();
        //            break;
        //        case "Odjavi se": // ova se malo razlikuje
        //            GoToPage = new LoginPage();
        //            App.Current.MainPage = GoToPage;
        //            return;
        //    }

        //    MyListView.SelectedItem = null;
        //    await Navigation.PushAsync(GoToPage);
        //}
    }
}