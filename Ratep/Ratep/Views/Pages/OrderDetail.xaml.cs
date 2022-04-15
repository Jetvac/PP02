using Newtonsoft.Json;
using Ratep.Class;
using Ratep.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;

namespace Ratep.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderDetail.xaml
    /// </summary>
    public partial class OrderDetail : Page
    {
        public TextBox selectedOrderDescription { get; set; }
        public ListView orderDetailData { get; set; }
        static public Order _selectedOrder { get; set; }
        static public bool isChecked { get; set; }

        public ObservableCollection<OrderPosition> OrderPositions { get; set; }

        public OrderDetail(Order order)
        {
            InitializeComponent();

            selectedOrderDescription = SelectedTxtBx;
            orderDetailData = OrderDetailData;
            _selectedOrder = order;

            LoadOrderData(order);
        }

        public async void LoadOrderData(Order order)
        {
            selectedOrderDescription.Text = order.OrderId.ToString();

            OrderPositions = new ObservableCollection<OrderPosition>(order.OrderPositions);

            orderDetailData.ItemsSource = OrderPositions;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        public async void CloseOrder(object sender)
        {
            if (isChecked == true) { isChecked = false; return; }
            MessageBoxResult boxResult = MessageBox.Show("Вы уверены, что хотите закрыть товар?", "Уведомление!", MessageBoxButton.YesNo);
            if (boxResult == MessageBoxResult.No) { isChecked = true; ((CheckBox)sender).IsChecked = true; return; }

            StackPanel currentItem = (StackPanel)((CheckBox)sender).Parent;
            int posID = Convert.ToInt32(((TextBlock)currentItem.Children[0]).Text);

            if (posID == -1) { isChecked = true; ((CheckBox)sender).IsChecked = true; return; }
            else
            {
                string response;
                try
                {
                    response = await CloseOrderApiRequest(posID, App.Token);
                }
                catch (Exception ex)
                {
                    ApiError.ShowError(ApiErrors.ApiConnectionFailed);
                    return;
                }
                if (Convert.ToInt32(response) != 1)
                {
                    isChecked = true; ((CheckBox)sender).IsChecked = false;
                    ApiError.ShowError(ApiError.GetErrorByCode(Convert.ToInt32(response)));
                    return;
                }    
                else
                {
                    ((TextBlock)currentItem.Children[6]).Text = DateTime.Now.ToString("dd.MM.yyyy");
                    MessageBox.Show("Позиция в заказе успешно закрыта!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }

                OrderPage.UpdateTreeView();
            }
        }

        public static async Task<string> CloseOrderApiRequest(int posID, string userToken)
        {
            return await ApiControl.POSTRequest($"OrderPositions/CloseOrderPosition?OrderPositionID={posID}&Token={userToken}", new Dictionary<string, string>());
        }
        public static async Task<string> OpenOrderApiRequest(int posID, string userToken)
        {
            return await ApiControl.POSTRequest($"OrderPositions/OpenOrderPosition?OrderPositionID={posID}&Token={userToken}", new Dictionary<string, string>());
        }

        public async void OpenOrder(object sender)
        {
            if (isChecked == true) { isChecked = false; return; }
            MessageBoxResult boxResult = MessageBox.Show("Вы уверены, что хотите открыть товар?", "Уведомление!", MessageBoxButton.YesNo);
            if (boxResult == MessageBoxResult.No) { isChecked = true; ((CheckBox)sender).IsChecked = true; return; }

            StackPanel currentItem = (StackPanel)((CheckBox)sender).Parent;
            int posID = Convert.ToInt32(((TextBlock)currentItem.Children[0]).Text);

            if (posID == -1) { isChecked = true; ((CheckBox)sender).IsChecked = true; return; }
            else
            {
                string response;
                try
                {
                    response = await OpenOrderApiRequest(posID, App.Token);
                } catch (Exception ex)
                {
                    ApiError.ShowError(ApiErrors.ApiConnectionFailed);
                    return;
                }
                if (Convert.ToInt32(response) != 1)
                {
                    isChecked = true; ((CheckBox)sender).IsChecked = true;
                    ApiError.ShowError(ApiError.GetErrorByCode(Convert.ToInt32(response)));
                    return;
                }
                else
                {
                    ((TextBlock)currentItem.Children[6]).Text = "";
                    MessageBox.Show("Позиция в заказе успешно открыта!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }

                OrderPage.UpdateTreeView();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CloseOrder(sender);
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            OpenOrder(sender);
        }
    }
}
