using Ratep.Pages;
using System.Windows;

namespace Ratep.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для Redact.xaml
    /// </summary>
    public partial class Redact : Window
    {
		public Redact()
		{
			InitializeComponent();

			this.SizeToContent = SizeToContent.WidthAndHeight;
		}

		private void Window_Loaded_1(object sender, RoutedEventArgs e)
		{
			MainFra.Navigate(new NomRedact());
		}
	}
}
