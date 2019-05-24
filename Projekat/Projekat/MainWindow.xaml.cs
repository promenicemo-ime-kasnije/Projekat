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
        public MainWindow()
        {
            InitializeComponent();
            Content = new LoginPage();
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            if (newContent is LoginPage)
            {
                this.Height = 485;
                this.Width = 810;
                this.WindowState = WindowState.Normal;
                this.ResizeMode = ResizeMode.NoResize;
            }
            else if (newContent is StartPage)
            {
                this.Height = 635;
                this.Width = 1075;
                this.ResizeMode = ResizeMode.CanMinimize;
                this.WindowState = WindowState.Normal;
            }
            else if (newContent is ProjectPage)
            {
                //this.Height = 635;
                //this.Width = 1075;
                this.ResizeMode = ResizeMode.CanResize;
                this.WindowState = WindowState.Maximized;
            }
            else if (newContent is TimelinePage)
            {
                this.Height = 800;
                this.Width = 1333;
                this.ResizeMode = ResizeMode.CanResize;
            }
        }
    }
}
