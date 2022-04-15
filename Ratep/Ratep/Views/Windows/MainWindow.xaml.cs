using Ratep.Models;
using Ratep.Pages;
using System.Windows;
using System.Windows.Controls;

namespace Ratep
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			App.Current.Shutdown();
		}
	}
}
