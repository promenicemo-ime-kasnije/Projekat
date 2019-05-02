using System;
using System.Windows;
using System.Windows.Controls;
using Projekat.Pomocne_klase;

namespace Projekat.UserControls
{
    /// <summary>
    /// Ovo je user control-a koja prestavlja jedan dokument u listiDokumenata koja se koristi u DokumentaPage
    /// Sadrzi jedan Dependency Property Dokument koji se odnosi na dokument koji se prikazuje u ovoj kontroli
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

        // Klikom na ovu kontrolu otvara se novi prozor sa detaljima Dokumenta
        private void OtvoriDetalje_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var obj = (Page)Activator.CreateInstance(Dokument.DetailsPage);
            var window = new Window();
            window.Height = window.Width = 500;
            window.Content = obj;
            window.Show();
        }
    }
}
