﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ClassLibrary.Korisnik TrenutniKorisnik { get; set; }
        public ClassLibrary.Projekat TrenutniProjekat { get; set; }

    }
}
