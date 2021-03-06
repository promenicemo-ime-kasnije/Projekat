﻿using ClassLibrary.DataProvider;
using Projekat.Pages;
using Projekat.Pomocne_klase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// TODO: arhirivani projekti da mogu da se scroluju
    /// </summary>
    public partial class StartPage : Page
    {
        public ObservableCollection<ClassLibrary.Projekat> ListaAktivnihProjekata { get; set; }
        public ObservableCollection<ClassLibrary.Projekat> ListaArhiviranihProjekata { get; set; }

        public StartPage()
        {
            ListaAktivnihProjekata = new ObservableCollection<ClassLibrary.Projekat>();
            ListaArhiviranihProjekata = new ObservableCollection<ClassLibrary.Projekat>();

            InitializeComponent();
            Loaded += StartPage_Loaded;
            DataContext = this;
        }

        private async void StartPage_Loaded(object sender, RoutedEventArgs e)
        {
            await UcitajProjekte();
            expanderKorisnik.Header = Helper.TrenutniKorisnik.PunoIme;
        }

        private async Task UcitajProjekte()
        {
            var projekti = await new EFCoreDataProvider().GetProjekteAsync() as List<ClassLibrary.Projekat>;
            projekti.Reverse(); // Da ih poredja od novijih ka starijim po ID-u

            ListaAktivnihProjekata.Clear();
            ListaArhiviranihProjekata.Clear();

            foreach (ClassLibrary.Projekat p in projekti)
                if (p.StanjeProjekta.Contains("Aktivan"))
                    ListaAktivnihProjekata.Add(p);
                else if (p.StanjeProjekta.Contains("Arhiviran"))
                    ListaArhiviranihProjekata.Add(p);
        }

        private void OdjaviSe_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Window).Content = new LoginPage();
        }

        private void Arhiva_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Window).Content = new ArhivaPage();
        }

        private void NoviProjekat_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Window).Content = new NoviProjekatPage();
        }

        private void OtvoriProjekat_Click(object sender, MouseButtonEventArgs e)
        {
            var p = (sender as ListView).SelectedItem as ClassLibrary.Projekat;
            if (p != null)
            {
                Helper.TrenutniProjekat = p;
                (Parent as Window).Content = new ProjectPage();
            }
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt = tbSearch.Text;

            if (string.IsNullOrEmpty(txt))
                lvAktivniProjekti.ItemsSource = ListaAktivnihProjekata;
            else
                lvAktivniProjekti.ItemsSource = ListaAktivnihProjekata.Where(p => p.NazivProjekta.ToUpper().Contains(txt.ToUpper()));
        }
    }
}
