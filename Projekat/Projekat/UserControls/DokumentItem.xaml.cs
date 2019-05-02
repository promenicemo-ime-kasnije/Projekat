using System;
using System.Windows;
using System.Windows.Controls;
using Projekat.Pomocne_klase;

namespace Projekat.UserControls
{
    /// <summary>
    /// Interaction logic for DokumentItem.xaml
    /// </summary>
    public partial class DokumentItem : UserControl
    {


        public FakeDokument Dokument
        {
            get { return (FakeDokument)GetValue(DokumentProperty); }
            set { SetValue(DokumentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dokument.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DokumentProperty =
            DependencyProperty.Register("Dokument", typeof(FakeDokument), typeof(DokumentItem), new PropertyMetadata(null));



        public DokumentItem()
        {
            InitializeComponent();
        }

        private void OtvoriDetalje_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var obj = (Page)Activator.CreateInstance(Dokument.DetailsPage);
            var window = new Window();
            window.Content = obj;
            window.RenderSize = obj.RenderSize;
            window.Show();
        }
    }
}
