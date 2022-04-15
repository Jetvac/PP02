using System;
using Ratep.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;
using Ratep.Views.Windows;

namespace Ratep.Pages
{
	/// <summary>
	/// Логика взаимодействия для Nomenclature.xaml
	/// </summary>
	public partial class Nomenclature : Page
	{
		public DataGrid nomData;
		public Part_assembly_unit currentItem;
		public List<Part_assembly_unit> Data = new List<Part_assembly_unit>();
		public static Redact redWindow;
		public Nomenclature()
		{
			InitializeComponent();
			nomData = NomData;
		}
		public void UpdateData()
        {
			nomData.ItemsSource = App.DBConnection.Part_assembly_unit.ToList();
			Data = (List<Part_assembly_unit>)nomData.ItemsSource;
		}
		public List<Part_assembly_unit> DataAdapter(List<GetAvailableItems_Result> MiscData)
		{
			List<Part_assembly_unit> Adapted = new List<Part_assembly_unit>();
			
			foreach (GetAvailableItems_Result item in MiscData)
			{
				Adapted.Add(new Part_assembly_unit() { PAUID = item.PAUID, NameProduct = item.NameProduct });
			}
			return Adapted;
		}
		public void UpdateNonLoopData(int Num_product)
		{
			nomData.ItemsSource = DataAdapter(App.DBConnection.GetAvailableItems(Num_product).ToList());
			Data = (List<Part_assembly_unit>)nomData.ItemsSource;
		}
		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			NomRedact.CurentSceanceAddOrRedact = false;
			redWindow = new Redact();
			redWindow.ShowDialog();
		}
		private void NomData_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (StructurePage.MiscSeance != null)
			{
				if ((Part_assembly_unit)StructurePage.MiscSeance.NomData.SelectedItem == null) return;

				StructurePage.SetAdd();
				StructurePage.itemQuantity.Text = "";
				StructurePage.MiscSeanceResult = (Part_assembly_unit)StructurePage.MiscSeance.NomData.SelectedItem;
				StructurePage.MiscSeance = null;
				StructurePage.MiscTxtBxInterface.Text = StructurePage.MiscSeanceResult.NameProduct;
				Interface.frame.Navigate(Interface.StructurePage);
			} else
			{
				if ((Part_assembly_unit)Interface.NomenclaturePage.NomData.SelectedItem == null) return;

				Properties.Settings.Default.SelectedIndex = NomData.SelectedIndex;
				currentItem = (Part_assembly_unit)Interface.NomenclaturePage.NomData.SelectedItem;
				StructurePage.controlPanel.Header = "";
				StructurePage.Message.Text = StructurePage.MessagePatternText + currentItem.NameProduct;
				StructurePage.stData.ItemsSource = StructurePage.GetStructureData(currentItem.PAUID).Nodes;
			}
		}
		private void NomData_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Interface.frame.Navigate(Interface.StructurePage);
		}
		private void RadioButton_Checked(object sender, RoutedEventArgs e)
		{
			if (nomData.Columns[0].SortDirection == ListSortDirection.Descending)
				nomData.Columns[0].SortDirection = ListSortDirection.Ascending;
			else
				nomData.Columns[0].SortDirection = ListSortDirection.Descending;
		}
		private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (((TextBox)sender).Text.Length == 0) { nomData.ItemsSource = Data; return; }
			nomData.SelectedItem = null; 

			List<Part_assembly_unit> FilteredData = new List<Part_assembly_unit>();
			string pattern = $"^{((TextBox)sender).Text}";

			foreach(Part_assembly_unit item in Data)
			{
				if (Regex.IsMatch(item.NameProduct, pattern, RegexOptions.IgnoreCase))
				{
					FilteredData.Add(item);
				}
			}
			nomData.ItemsSource = FilteredData;
		}
		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			if (currentItem == null) return;
			if (Interface.StructurePage != null)
				if (StructurePage.MiscSeance != null)
				{
					MessageBox.Show("Нельзя удалять данные во время сеанся выбора детали для прикрепления.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
			App.DBConnection.DT_Nomencloture(currentItem.PAUID);
			App.DBConnection.SaveChanges();
			currentItem = null;
			Interface.StructurePage = new StructurePage();
			UpdateData();
		}
		private void UpdateButton_Click(object sender, RoutedEventArgs e)
		{
			if (currentItem == null) return;

			NomRedact.CurentSceanceAddOrRedact = true;
			redWindow = new Redact();
			redWindow.ShowDialog();
		}
		private void StructureDataButton_Click(object sender, RoutedEventArgs e)
		{
			if (currentItem == null) return;

			List<GetConstruction_Result> Data = App.DBConnection.GetConstruction(currentItem.PAUID, 1).ToList();
			Word.Document doc = null;
			Word.Application app = new Word.Application();
			try
			{
				string source = $@"{Directory.GetCurrentDirectory()}\pattern.docx";

				doc = app.Documents.Open(source);

				doc.Activate();
				Word.Bookmarks bookmarks = doc.Bookmarks;
				Word.Table Structure = bookmarks["Структура"].Range.Tables.Add(bookmarks["Структура"].Range, Data.Count, 2);

				Structure.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
				Structure.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;


				bookmarks["Номер"].Range.Text = Convert.ToString(currentItem.PAUID);
				bookmarks["Наименование"].Range.Text = currentItem.NameProduct;


				Structure.Cell(1, 1).Range.Text = "Деталь";
				Structure.Cell(1, 2).Range.Text = "Кол-во";

				for (int i = 0; i < Data.Count; i++)
				{
					Structure.Cell(i + 2, 1).Range.Text = Convert.ToString(App.DBConnection.Part_assembly_unit.Find(Data[i].Num_product).NameProduct);
					Structure.Cell(i + 2, 2).Range.Text = Convert.ToString(Data[i].Quantity);
				}

				doc.SaveAs2($@"{Directory.GetCurrentDirectory()}\Структура {currentItem.NameProduct}.docx");
				doc.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
				doc = null;
				app.Quit(Word.WdSaveOptions.wdDoNotSaveChanges);
				MessageBox.Show("Файл успешно сохранён");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}