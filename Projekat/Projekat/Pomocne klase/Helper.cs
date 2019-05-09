using ClassLibrary;
using System.Windows;

namespace Projekat.Pomocne_klase
{
    public class Helper
    {
        public static MainWindow GetMainWindow(DependencyObject dependencyObject)
        {
            return Window.GetWindow(dependencyObject) as MainWindow;
        }

        public static Korisnik GetTrenutniKorisnik(DependencyObject dependencyObject)
        {
            return GetMainWindow(dependencyObject).TrenutniKorisnik;
        }

        public static ClassLibrary.Projekat GetTrenutniProjekat(DependencyObject dependencyObject)
        {
            return GetMainWindow(dependencyObject).TrenutniProjekat;
        }
    }
}
