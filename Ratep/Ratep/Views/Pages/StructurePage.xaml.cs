using Ratep.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Ratep.Pages
{
	/// <summary>
	/// Логика взаимодействия для Structure.xaml
	/// </summary>
	public partial class StructurePage : Page
	{
		public class DataTemp
		{
			public int ProductNum { get; set; }
			public int ParentNum { get; set; }
			public int Quantity { get; set; }
			public int CalculateQuantity { get; set; }
			public string Name { get; set; }
			public ObservableCollection<DataTemp> Nodes { get; set; }
		}
		public static Nomenclature MiscSeance;
		public static Part_assembly_unit MiscSeanceResult;
		public static DataTemp SelectedNode;
		public static Boolean CurrentSeanceRedactOrAdd; // 0 - Redact | 1 - Add
		public static TreeView stData;
		public static TextBox itemQuantity;
		public static TextBox Message;
		public static TextBox MiscTxtBxInterface;
		public static GroupBox controlPanel;
		public static string MessagePatternText = "Выбранное изделие: ";
		public static Stack<int> NavigationItemHistory = new Stack<int>();
		public StructurePage()
		{
			InitializeComponent();

			stData = STData;
			Message = SelectedTxtBx;
			MiscTxtBxInterface = SelectedItemNameTxtBx;
			controlPanel = ControlPanel;
			itemQuantity = AmmountTxtBx;
			DataObject.AddPastingHandler(AmmountTxtBx, new DataObjectPastingEventHandler(OnPaste));
		}
		public static DataTemp GetConvertPrimaryNode(Part_assembly_unit data)
		{
			return new DataTemp() { ProductNum = data.PAUID, Name = data.NameProduct };
		}
		public static Part_assembly_unit GetNomenclotureItem(int ItemID)
		{
			return App.DBConnection.Part_assembly_unit.Find(ItemID);
		}
		public static ObservableCollection<DataTemp> GetStructure(int NumProduct, int CalculatedQuantity = 1)
		{
			List<Structure> Data = App.DBConnection.Structure.ToList();
			ObservableCollection<DataTemp> Output = new ObservableCollection<DataTemp>();

			foreach(Structure item in Data)
			{
				if (item.Num_product_where == NumProduct)
				{
					Output.Add(new DataTemp()
					{
						ProductNum = item.Num_product_what,
						ParentNum = NumProduct,
						Quantity = item.Quantity,
						CalculateQuantity = item.Quantity * CalculatedQuantity,
						Name = GetNomenclotureItem(item.Num_product_what).NameProduct
					});
				}
			}

			return Output;
		}
		public static DataTemp GetStructureData(int NumProduct)
		{
			DataTemp InitStructureData = new DataTemp()
			{
				ProductNum = NumProduct,
				Name = GetNomenclotureItem(NumProduct).NameProduct,
				Nodes = GetStructure(NumProduct)
			}; //Initializate visible level of tree

			foreach (DataTemp item in InitStructureData.Nodes)
			{ 
				ObjectParameter Result = new ObjectParameter("Result", 0);
				App.DBConnection.NodeAmmount(item.ProductNum, Result);
				if ((int)Result.Value > 0)
					item.Nodes = new ObservableCollection<DataTemp>() { new DataTemp() };
			}

			return InitStructureData;
		}
		private void MiscButton_Click(object sender, RoutedEventArgs e)
		{
			if (Interface.NomenclaturePage.currentItem == null) return;

			MiscSeance = new Nomenclature();
			MiscSeance.UpdateNonLoopData(Interface.NomenclaturePage.currentItem.PAUID);
			Interface.frame.Navigate(MiscSeance);
		}
		public static void SetRedact()
		{
			CurrentSeanceRedactOrAdd = false;
			controlPanel.Header = "Изменение";
		}
		public static void SetAdd()
		{
			CurrentSeanceRedactOrAdd = true;
			controlPanel.Header = "Добавление";
		}
		private void BindButton_Click(object sender, RoutedEventArgs e)
		{
			ObservableCollection<DataTemp> StructureData = (ObservableCollection<DataTemp>)STData.ItemsSource;
			//Item bind only at first level
			if (CurrentSeanceRedactOrAdd) //Add
			{
				if (AmmountTxtBx.Text.Length == 0) { MessageBox.Show("Введите кол-во деталей."); return; }

				int Ammount;
				try
				{
					Ammount = Convert.ToInt32(AmmountTxtBx.Text);
				}
				catch (Exception)
				{
					MessageBox.Show("Превышено максимальное значение.");
					return;
				}

				//Update tree
				DataTemp BindableItem = new DataTemp()
				{
					ProductNum = MiscSeanceResult.PAUID,
					ParentNum = Interface.NomenclaturePage.currentItem.PAUID,
					Name = MiscSeanceResult.NameProduct,
					Quantity = Ammount,
					CalculateQuantity = Ammount,
					Nodes = GetStructure(MiscSeanceResult.PAUID)
				};
				StructureData.Add(BindableItem);

				App.DBConnection.IT_Structure(BindableItem.ParentNum, BindableItem.ProductNum, BindableItem.Quantity);
				App.DBConnection.SaveChanges();
			} else //Redact
			{
				if (SelectedNode == null) return;
				int Index = StructureData.IndexOf(SelectedNode);
				int Ammount;
				if (Index == -1) return;

				if (AmmountTxtBx.Text.Length == 0) { MessageBox.Show("Введите кол-во деталей."); return; }
				try
				{
					Ammount = Convert.ToInt32(AmmountTxtBx.Text);
				}
				catch(Exception)
				{
					MessageBox.Show("Превышено максимальное значение."); 
					return;
				}
					
				//Change visualize
				StructureData[Index].Quantity = Ammount;
				StructureData[Index].CalculateQuantity = Ammount;
				StructureData[Index].Nodes = GetStructure(SelectedNode.ProductNum, Ammount);

				App.DBConnection.UT_Structure(StructureData[Index].ParentNum, StructureData[Index].ProductNum, Ammount);
				App.DBConnection.SaveChanges();

				App.DBConnection = new Practik1282Entities();
			}

			STData.ItemsSource = new ObservableCollection<DataTemp>();
			STData.ItemsSource = StructureData;
		}
		private void UnbindButton_Click(object sender, RoutedEventArgs e)
		{
			if (SelectedNode == null) return;

			ObservableCollection<DataTemp> StructureData = (ObservableCollection<DataTemp>)STData.ItemsSource;
			int Index = StructureData.IndexOf(SelectedNode);

			App.DBConnection.DT_Structure(SelectedNode.ParentNum, SelectedNode.ProductNum);
			App.DBConnection.SaveChanges();

			if (Index != -1)
				StructureData.RemoveAt(Index);

			STData.ItemsSource = StructureData;
		}
		private void STData_Expanded(object sender, RoutedEventArgs e)
		{
			TreeViewItem item = e.Source as TreeViewItem;
			if (item == null) return;
			try
			{
				DataTemp node = (DataTemp)item.DataContext;

				node.Nodes = GetStructure(node.ProductNum, node.CalculateQuantity);
				foreach (DataTemp branch in node.Nodes)
				{
					ObjectParameter Result = new ObjectParameter("Result", 0);
					App.DBConnection.NodeAmmount(branch.ProductNum, Result);
					if ((int)Result.Value > 0)
						branch.Nodes = new ObservableCollection<DataTemp>() { new DataTemp() };
				}
				item.ItemsSource = node.Nodes;
			} catch (Exception)
			{
				return;
			}
		}
		private void OnPaste(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
		}
		public static void NavigateItemProcedure(int NumProuct)
		{
			Part_assembly_unit Selected = App.DBConnection.Part_assembly_unit.Find(NumProuct);
			Interface.NomenclaturePage.currentItem = Selected;

			Message.Text = MessagePatternText + Selected.NameProduct;
			stData.ItemsSource = null;
			stData.ItemsSource = GetStructureData(Selected.PAUID).Nodes;

			//Control panel is dropped
			MiscTxtBxInterface.Text = "";
			itemQuantity.Text = "";
			MiscSeanceResult = null;
		}
		private void STData_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if ((DataTemp)STData.SelectedItem == null) return;

			NavigationItemHistory.Push(Interface.NomenclaturePage.currentItem.PAUID);

			NavigateItemProcedure(((DataTemp)STData.SelectedItem).ProductNum);
		}
		private void STData_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			ObservableCollection<DataTemp> StructureData = (ObservableCollection<DataTemp>)STData.ItemsSource;
			DataTemp Branch = (DataTemp)STData.SelectedItem;

			if (Branch == null) return;

			//In user permission redact only first element of primary item
			if (StructureData.Contains(Branch))
			{
				SetRedact();
				SelectedNode = Branch;
				SelectedItemNameTxtBx.Text = SelectedNode.Name;
				itemQuantity.Text = Convert.ToString(SelectedNode.Quantity);
			}
		}
		private void AmmountTxtBx_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
			if (AmmountTxtBx.Text.Length == 0)
			{
				Regex condition = new Regex("^[1-9]+");
				e.Handled = !condition.IsMatch(e.Text);
			}
			else
			{
				Regex condition = new Regex("^[0-9]+");
				e.Handled = !condition.IsMatch(e.Text);
			}
		}
		private void ItemNavigationButton_Click(object sender, RoutedEventArgs e)
		{
			if (NavigationItemHistory.Count == 0) return;

			int PreviousElement = NavigationItemHistory.Pop();

			NavigateItemProcedure(PreviousElement);
		}
	}
}