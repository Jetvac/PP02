using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Ratep.Pages
{
	/// <summary>
	/// Логика взаимодействия для Interface.xaml
	/// </summary>
	public partial class Interface : Page
	{
		static public Nomenclature NomenclaturePage = new Nomenclature();
		static public StructurePage StructurePage = new StructurePage();
		static public OrderPage orderPage = new OrderPage();
		static public Frame frame;
		public Interface()
		{
			InitializeComponent();
			frame = DataFrame;
		}

		private void NomenclatureNavigationButton_Click(object sender, RoutedEventArgs e)
		{
			if (StructurePage.MiscSeance != null) { StructurePage.MiscSeance = null; }

			DataFrame.Navigate(NomenclaturePage);
			NomenclaturePage.UpdateData();
		}

		private void StructureNavigationBtn_Click(object sender, RoutedEventArgs e)
		{
			DataFrame.Navigate(StructurePage);
		}

		private void AboutBtn_Click(object sender, RoutedEventArgs e)
		{
			new Info().ShowDialog();
		}

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
			NavigationService.Navigate(new Autorization());
        }

        private void OrderNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
			DataFrame.Navigate(orderPage);
        }
    }
}
