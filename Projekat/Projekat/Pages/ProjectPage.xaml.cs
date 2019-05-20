using Projekat.Pomocne_klase;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Projekat.Pages
{
    /// <summary>
    /// Interaction logic for ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        public ProjectPage()
        {
            InitializeComponent();
            DataContext = this;
            //PrikaziDokumenta_Click(this, new RoutedEventArgs());
            this.Loaded += ProjectPage_Loaded;
        }

        private void ProjectPage_Loaded(object sender, RoutedEventArgs e)
        {
            txtImeProjekta.Text = "";
            //txtImeProjekta.Text = "Projekat: " + Helper.GetTrenutniProjekat(this).NazivProjekta;
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Window).Content = new StartPage();
        }

        private void PrikaziDokumenta_Click(object sender, RoutedEventArgs e)
        {
            var trenutniProjekat = Helper.TrenutniProjekat;
            Frame.Content = new DokumentaPage(trenutniProjekat.IDProjekta);
            txtImeProjekta.Text = "Dokumenta | " + trenutniProjekat.NazivProjekta;
        }

        private void PrikaziTroskove_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ZahteviPage();
            txtImeProjekta.Text = "Zahtevi | " + Helper.TrenutniProjekat.NazivProjekta;
        }

        private void PrikaziTabelu_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new TabelaPage();
            txtImeProjekta.Text = "Tabela | " + Helper.TrenutniProjekat.NazivProjekta;
        }

        private void PrikaziTimeline_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new TimelinePage(false);
            txtImeProjekta.Text = "Zapisnik aktivnosti | " + Helper.TrenutniProjekat.NazivProjekta;
        }

        private void ComingSoon_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coming soon!");
        }

        private void ClanoviProjekta_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Frame.Content = new ClanoviProjektaPage();
            txtImeProjekta.Text = "Članovi projekta | " + Helper.TrenutniProjekat.NazivProjekta;
        }
    }
}
