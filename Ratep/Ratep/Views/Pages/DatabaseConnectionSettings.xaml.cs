using Ratep.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Ratep.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для DatabaseConnectionSettings.xaml
    /// </summary>
    public partial class DatabaseConnectionSettings : Page
    {
        public DatabaseConnectionSettings()
        {
            InitializeComponent();
        }

        private void ConnectButtonClick(object sender, RoutedEventArgs e)
        {
            ConnectChange(ServerName.Text, DBName.Text);
            MessageBox.Show("Соединение успешно изменено!");
        }

        public static async Task<int> ConnectChange(string ServerName, string DBName)
        {
            string response = await ApiControl.POSTRequest($"DBSettings/ChangeConnectString?ServerName={ServerName}&DBName={DBName}");
            return 1;
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
