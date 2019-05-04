using Projekat.Pages;
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
