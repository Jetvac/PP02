using Ratep.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Ratep.Pages
{
	/// <summary>
	/// Логика взаимодействия для NomRedact.xaml
	/// </summary>
	public partial class NomRedact : Page
	{
		public static TextBox InputData;
		public static Boolean CurentSceanceAddOrRedact;
		public NomRedact()
		{
			InitializeComponent();
			InputData = NomenclotureNameTxtbx;
		}

		private void SaveOrAddButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (InputData.Text.Length == 0) { MessageBox.Show("Введите наименование!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return; }
			if (InputData.Text.Length == 50) { MessageBox.Show("Превышен лимит символов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return; }

			if (CurentSceanceAddOrRedact) //Redact
			{
				App.DBConnection.UT_Nomencloture(Interface.NomenclaturePage.currentItem.PAUID, InputData.Text);
				App.DBConnection.SaveChanges();
				App.DBConnection = new Practik1282Entities();
				Interface.NomenclaturePage.UpdateData();
				MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
				Nomenclature.redWindow.Close();
			} else
			{
				App.DBConnection.IT_Nomencloture(InputData.Text);
				App.DBConnection.SaveChanges();
				Interface.NomenclaturePage.UpdateData();
				MessageBox.Show("Данные успешно внесены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
				Nomenclature.redWindow.Close();
			}
		}

		private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			if (CurentSceanceAddOrRedact)
				InputData.Text = Interface.NomenclaturePage.currentItem.NameProduct;
		}
	}
}
