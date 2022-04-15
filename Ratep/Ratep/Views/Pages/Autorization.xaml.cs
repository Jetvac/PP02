using Ratep.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Ratep.Class;
using System.Collections.Generic;
using System.IO;
using System;
using Ratep.Views.Pages;
using System.Configuration;
using Ratep.Class;
using System.Threading.Tasks;

namespace Ratep.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Page
	{
		public Autorization()
		{
			InitializeComponent();
		}


		public async void Button_Click(object sender, RoutedEventArgs e)
		{
            string response = await Authorizate(LoginTxtBx.Text, PasswordTxtBx.Password);
            if (response.Length == 3)
            {
                ApiError.ShowError(ApiError.GetErrorByCode(Convert.ToInt32(response)));
                return;
            }

            if (response != null)
            {
                App.Token = response;
                NavigationService.Navigate(new Interface());
            }
        }

        public static async Task<string> Authorizate(string Login, string Password)
        {
            string request;
            string CryptedLogin = Cryptograph.EncryptStringToSHA256(Login);
            string CryptedPassword = Cryptograph.EncryptStringToSHA256(Password);

            try
            {
                request = await ApiControl.POSTRequest($"Users?CryptedLogin={CryptedLogin}&CryptedPassword={CryptedPassword}");
            }
            catch (Exception ex)
            {
                return ((int)ApiErrors.ApiConnectionFailed).ToString();
            }

            return request;
        }

        private void DatabaseClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DatabaseConnectionSettings());
        }
    }
}
