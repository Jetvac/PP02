using Ratep.Models;
using System.Windows;

namespace Ratep
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
	{
		#region GlobalVariables
		public static Practik1282Entities DBConnection = new Practik1282Entities();
		public const string ConnectionStringName = "Practik1282Entities";
		public const string URL = "https://localhost:7051/";
		public static string Token = null;
		public static Employee EmployeeData { get; set; }
		#endregion

		#region Methods
		private Window GetCurrentActiveWindow()
		{
			Window CurrentWindow = new Window();
			foreach (Window w in Current.Windows)
				if (w.IsActive)
				{
					CurrentWindow = w;
					break;
				}
			return CurrentWindow;
		}
		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			Window CurrentWindow = GetCurrentActiveWindow();
			CurrentWindow.Close();
		}

		private void OpenButton_Click(object sender, RoutedEventArgs e)
		{
			Window CurrentWindow = GetCurrentActiveWindow();
			if (CurrentWindow.IsActive == false) return;
			if (CurrentWindow.WindowState == WindowState.Maximized)
				CurrentWindow.WindowState = WindowState.Normal;
			else
				CurrentWindow.WindowState = WindowState.Maximized;
		}

		private void HideButton_Click(object sender, RoutedEventArgs e)
		{
			Window CurrentWindow = GetCurrentActiveWindow();
			if (CurrentWindow.IsActive == false) return;
			CurrentWindow.WindowState = WindowState.Minimized;
		}
		#endregion
	}
}
