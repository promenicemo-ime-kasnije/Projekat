using ClassLibrary;
using ClassLibrary.DataProvider;
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

namespace Projekat.Pages
{
    /// <summary>
    /// Interaction logic for TabelaPage.xaml
    /// </summary>
    public partial class TabelaPage : Page
    {
        //    List<object> Kategorije;

        public TabelaPage()
        {
            //        Kategorije = new List<object>()
            //        {
            //            new MenuItem{ Header = "Arhitekutra", Items = {"Tekstualna dokumentacija", "Numericka dokumentaijca", "Graficka dokumentaicja", "3d model", "3d modeli stanova i bilbord"} },
            //            new MenuItem{ Header = "Konstrukcija", Items = { "Projekat konstrukcije", "Detalji armature"} },
            //            new MenuItem{ Header = "Hidro instalacije", Items = { "Projekat sanitarnog vodovoda", "Projekat centralne tople vode", "Projekat fekalne kanalizacije", "Projekat kisne kanalizacije", "Projekat hidrantske mreze" } },
            //            new MenuItem{ Header = "Mašinske instalacije", Items = { "Projekat centralnog grejanja", "Podstanica grejanja", "Ventilacija garaze", "Solarni sistem", "Projekat klimatizera"} },
            //            new MenuItem{ Header = "Elektro instalacije", Items = { } },
            //            new MenuItem{ Header = "Telekomunikacione i signalne instalacije", Items = { } },
            //            new MenuItem{ Header = "PP elaborat", Items = { } },
            //            new MenuItem{ Header = "EE elaborat", Items = { } },
            //            new MenuItem{ Header = "Elaborat zaštite susednih objekata", Items = { } },
            //            new MenuItem{ Header = "Ljudski resursi (personal)", Items = { } },
            //            new MenuItem{ Header = "Putovanja", Items = { } }
            //        };

            //        foreach (MenuItem item in Kategorije)
            //            foreach(MenuItem i in item.Items)
            //                i.Click += I_Click;

            //MyMenu.ItemsSource = Kategorije;
            //        Kategorije.GroupBy


            InitializeComponent();
            Loaded += TabelaPage_Loaded;
        }

        private async void TabelaPage_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Trosak> Aktivnosti = new ObservableCollection<Trosak>();
            foreach (var item in (await new EFCoreDataProvider().GetTroskoveProjektaAsync(30)))
                Aktivnosti.Add(item);

            ListCollectionView collection = new ListCollectionView(Aktivnosti);
            collection.GroupDescriptions.Add(new PropertyGroupDescription("Kategorija"));
            collection.GroupDescriptions.Add(new PropertyGroupDescription("Podkategorija"));
            MyDataGrid.ItemsSource = collection;
        }
    }
}
