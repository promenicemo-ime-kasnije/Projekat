﻿using System;
using System.Collections.Generic;
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
    /// Interaction logic for DokumentaPage.xaml
    /// </summary>
    public partial class DokumentaPage : Page
    {
        public List<string> List { get; set; }
        public DokumentaPage()
        {
            InitializeComponent();
            List = new List<string>();
            List.Add("Dokaz o uplati administrativne takse");
            List.Add("Idejno resenje");
            List.Add("Kopija plana za kat. parcelu");
            List.Add("Izvod i katastra vodova");
            List.Add("Podatak o povrsini parcele");
            List.Add("Dokaz o uplati administrativne takse");
            List.Add("Idejno resenje");
            List.Add("Kopija plana za kat. parcelu");
            List.Add("Izvod i katastra vodova");
            List.Add("Podatak o povrsini parcele");
            List.Add("Dokaz o uplati administrativne takse");
            List.Add("Idejno resenje");
            List.Add("Kopija plana za kat. parcelu");
            List.Add("Izvod i katastra vodova");
            List.Add("Podatak o povrsini parcele");
            DataContext = this;
        }
    }
}
