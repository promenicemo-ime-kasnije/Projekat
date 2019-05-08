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
            else if (oldContent?.GetType() == typeof(LoginPage))
            {
                this.Height = 720;
                this.Width = 1280;
                this.ResizeMode = ResizeMode.CanResize;
            }
        }
    }
}
