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
            //txtImeProjekta.Text = "Projekat: " + Helper.GetTrenutniProjekat(this).NazivProjekta;
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Window).Content = new StartPage();
        }

        private void PrikaziDokumenta_Click(object sender, RoutedEventArgs e)
        {
            int projectID = (this.Parent as MainWindow).TrenutniProjekat.IDProjekta;
            Frame.Content = new DokumentaPage(projectID);
        }

        private void PrikaziTroskove_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new TroskoviPage();
        }

        private void PrikaziTabelu_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new TabelaPage();
        }

        private void PrikaziTimeline_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new TimelinePage();
        }

        private void ComingSoon_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coming soon!");
        }

        private void ClanoviProjekta_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Frame.Content = new ClanoviProjektaPage();
        }
    }
}
