using ClassLibrary;
using ClassLibrary.DataProvider;
using Projekat.Pages;
using Projekat.Pomocne_klase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ClassLibrary.Korisnik TrenutniKorisnik { get; set; }
        public ClassLibrary.Projekat TrenutniProjekat { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Content = new LoginPage();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            new ExcelExport("Text.xls", await new EFCoreDataProvider().GetTroskoveProjektaAsync(30) as List<Trosak>, new double[] { 20, 30, 5, 45 }).Exportuj(); ;
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            if (newContent.GetType() == typeof(LoginPage))
            {
                this.Height = 485;
                this.Width = 810;
                this.WindowState = WindowState.Normal;
                this.ResizeMode = ResizeMode.NoResize;
            }
            else if (newContent?.GetType() == typeof(StartPage))
            {
                this.Height = 635;
                this.Width = 1075;
                this.ResizeMode = ResizeMode.CanMinimize;
                this.WindowState = WindowState.Normal;
            }
            else if (newContent?.GetType() == typeof(ProjectPage))
            {
                this.Height = 635;
                this.Width = 1075;
                this.ResizeMode = ResizeMode.CanResize;
                this.WindowState = WindowState.Maximized;
            }
        }
    }
}
