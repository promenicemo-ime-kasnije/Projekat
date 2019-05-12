using ClassLibrary;
using ClassLibrary.DataProvider;
using Projekat.Pomocne_klase;
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
        public TabelaPage()
        {
            InitializeComponent();
            Loaded += TabelaPage_Loaded;
        }

        private async void TabelaPage_Loaded(object sender, RoutedEventArgs e)
        {
            await UcitajTroskove();
        }

        private async Task UcitajTroskove()
        {
            var lista = await new EFCoreDataProvider().GetTroskoveProjektaAsync(30) as List<Trosak>;

            // Za grupisanje u datagridu itemssource se veze za ListCollectionView
            ListCollectionView collection = new ListCollectionView(lista);

            //Na osnovu cega se vrsi grupisanje
            collection.GroupDescriptions.Add(new PropertyGroupDescription("Kategorija"));
            collection.GroupDescriptions.Add(new PropertyGroupDescription("Podkategorija"));
            MyDataGrid.ItemsSource = collection;
        }

        private void DodajTrosak_Click(object sender, RoutedEventArgs e)
        {
            DetaljiTroska.Trosak = new Trosak();
        }

        private void IzmeniTrosak_Click(object sender, RoutedEventArgs e)
        {
            if (MyDataGrid.SelectedItem == null)
                MessageBox.Show("Niste selektovali nijedan trosak!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                DetaljiTroska.Trosak = MyDataGrid.SelectedItem as Trosak;
            }
        }

        private async void IzbrisiTrosak_Click(object sender, RoutedEventArgs e)
        {
            if (MyDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Niste selektovali nijedan trosak!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(MessageBox.Show("Da li ste sigurni da zelite da izbrisete selektovani trosak?", "Brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Brise se selektovani trosak
                var trosak = MyDataGrid.SelectedItem as Trosak;
                await new EFCoreDataProvider().DeleteTrosak(trosak);

                //Azuriraj tabelu
                await UcitajTroskove();
            }
        }

        private void ExportUExcel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Export svih troskova u excel tabelu");
        }

        private void DetaljiUplata_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ovde se postavlja broj uplata i koji procenat se uplacuje u kojoj uplati");
        }

        private async void Sacuvaj_Click(object sender, RoutedEventArgs e)
        {
            // Kad se klikne dugme sacuvaj treba da zna da li treba da vrsi update ili add u bazi
            // To odlucuje na osnovu IDTroska, ako je 0 onda ga jos nema u bazi treba add, ako je razlicit od 0 onda se vrsi update

            var trosak = DetaljiTroska.Trosak;
            trosak.IDProjekta = Helper.GetTrenutniProjekat(this).IDProjekta;

            var dataProvider = new EFCoreDataProvider();
            if (trosak.IDTroska == 0)
            {
                // ADD
                await dataProvider.AddTrosakAsync(trosak);
            }
            else
            {
                // UPDATE
                await dataProvider.UpdateTrosakAsync(trosak);
            }
            // Azuriraj tabelu
            await UcitajTroskove();
        }
    }
}
