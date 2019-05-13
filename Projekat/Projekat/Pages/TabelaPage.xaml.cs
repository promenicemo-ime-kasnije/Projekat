using ClassLibrary;
using ClassLibrary.DataProvider;
using Microsoft.Win32;
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
        private int idProjekta;
        private List<Trosak> listaTroskova;

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
            idProjekta = Helper.GetTrenutniProjekat(this).IDProjekta;
            listaTroskova = await new EFCoreDataProvider().GetTroskoveProjektaAsync(idProjekta) as List<Trosak>;

            // Za grupisanje u datagridu itemssource se veze za ListCollectionView
            ListCollectionView collection = new ListCollectionView(listaTroskova);

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
            else 
            {
                var trosak = MyDataGrid.SelectedItem as Trosak;

                if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete {trosak.Artikal} iz kategorije {trosak.Kategorija}/{trosak.Podkategorija}?", "Brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    // Brise se selektovani trosak
                    await new EFCoreDataProvider().DeleteTrosak(trosak);
                    //Azuriraj tabelu
                    await UcitajTroskove();
                }
            }
        }

        private async void ExportUExcel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog1.DefaultExt = "xlsx";
            saveFileDialog1.Filter = "Excel file (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 0;

            if (saveFileDialog1.ShowDialog() == true)
            {
                GeneralniTrosak generalniTrosak = (await new EFCoreDataProvider().GetGeneralniTrosakAsync(idProjekta))[0];
                string[] procenti = generalniTrosak.Procenti.Split(',');

                new ExcelExport(saveFileDialog1.FileName, listaTroskova, NizStringovaUNizBrojeva(procenti)).Exportuj();
            }
        }

        private double[] NizStringovaUNizBrojeva(string[] procenti)
        {
            double[] temp = new double[procenti.Length];
            for (int i = 0; i < procenti.Length; i++)
            {
                temp[i] = Double.Parse(procenti[i]);
            }
            return temp;
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
