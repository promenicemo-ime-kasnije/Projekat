using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenericPage : ContentPage
    {
        public GenericPage(string naslov)
        {
            InitializeComponent();
            Naslov.Text = naslov;
            this.Title = naslov;
        }
    }
}