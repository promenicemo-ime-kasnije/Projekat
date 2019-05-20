using ClassLibrary;
using System.Windows;

namespace Projekat.Pomocne_klase
{
    public class Helper
    {
        public static MainWindow MainWindow => Application.Current.MainWindow as MainWindow;

        public static Korisnik TrenutniKorisnik
        {
            get => (Application.Current as App).TrenutniKorisnik;
            set { (Application.Current as App).TrenutniKorisnik = value; }
        }

        public static ClassLibrary.Projekat TrenutniProjekat
        {
            get => (Application.Current as App).TrenutniProjekat;
            set { (Application.Current as App).TrenutniProjekat = value; }
        }
    }
}
