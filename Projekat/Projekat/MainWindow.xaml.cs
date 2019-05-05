using ClassLibrary;
using ClassLibrary.DataProvider;
using Projekat.Pages;
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
        public MainWindow()
        {
            InitializeComponent();
            Content = new LoginPage();
            TestBaze();
        }

        private async void TestBaze()
        {
            IList<Korisnik> korisnici = await new EFCoreDataProvider().GetKorisniciAsync();
            foreach (Korisnik k in korisnici)
            {
                Console.WriteLine($"{k.Ime} {k.Prezime}");
            }
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            if (newContent.GetType() == typeof(LoginPage))
            {
                this.Height = 485;
                this.Width = 810;
                this.ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                this.Height = 720;
                this.Width = 1280;
                this.ResizeMode = ResizeMode.CanResize;
            }
        }
    }
}
