using Microsoft.Win32;
using Ratep;
using Ratep.Models.ApiModels;
using Ratep.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using Ratep.Class;
using Newtonsoft.Json;

namespace Ratep.Pages
{
    public partial class OrderPage : Page
    {
        static public TreeView orderData { get; set; }
        public static ObservableCollection<Client> SavedData { get; set; }
        public OrderPage()
        {
            InitializeComponent();
            orderData = OrderData;

            UpdateTreeView();
        }

        public static async void UpdateTreeView()
        {
            string response;
            try
            {
                response = await ApiControl.GetRequest($"OrderPositions/GetClientsOrderList?Token={App.Token}");
            }
            catch (Exception ex)
            {
                ApiError.ShowError(ApiErrors.ApiConnectionFailed);
                return;
            }

            SavedData = JsonConvert.DeserializeObject<ObservableCollection<Client>>(response);

            orderData.ItemsSource = SavedData;
        }

        private void OrderData_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Contract contractData = (Contract)OrderData.SelectedItem;
            if (contractData == null ) return;

            NavigationService.Navigate(new OrderDetail(contractData.Order));
        }

        public static List<OrderPosition> filterCompletionOrderPositionByDate(List<OrderPosition> Data, DateTime fromDate, DateTime toDate)
        {
            List<OrderPosition> result = new List<OrderPosition>();

            foreach (OrderPosition item in Data)
            {
                if (item.CompletionDate > fromDate && item.CompletionDate < toDate)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static async Task<int> PrintExcel(Order orderDB, string savePath = null)
        {
            if (savePath != null) { return 1; }
            if (orderDB == null) { return -1; }
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить файл как...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter = "Excel документ (*.xls)|*.xlsx";
            if (savedialog.ShowDialog() == true)
            {
                await Task.Run(() =>
                {
                    List<OrderPosition> orderPos = orderDB.OrderPositions.ToList();


                    try
                    {
                        Excel.Application excel = new Excel.Application();
                        Excel.Workbook wb = excel.Workbooks.Add(System.Reflection.Missing.Value);
                        Excel.Worksheet ws = (Excel.Worksheet)wb.Sheets[1];

                        ((Excel.Range)ws.Cells[1, 1]).Value2 = "№Детали";
                        ((Excel.Range)ws.Cells[1, 2]).Value2 = "Наименование";
                        ((Excel.Range)ws.Cells[1, 3]).Value2 = "Кол-во";
                        ((Excel.Range)ws.Cells[1, 4]).Value2 = "Цех";
                        ((Excel.Range)ws.Cells[1, 5]).Value2 = "Дата завершения";



                        for (int i = 0; i < orderPos.Count; i++)
                        {
                            ((Excel.Range)ws.Cells[i + 2, 1]).Value2 = Convert.ToString(orderPos[i].Pau.Pauid);
                            ((Excel.Range)ws.Cells[i + 2, 2]).Value2 = orderPos[i].Pau.NameProduct;
                            ((Excel.Range)ws.Cells[i + 2, 3]).Value2 = Convert.ToString(orderPos[i].Ammount);
                            ((Excel.Range)ws.Cells[i + 2, 4]).Value2 = orderPos[i].Pau.Manufactory.Name;
                            ((Excel.Range)ws.Cells[i + 2, 5]).Value2 = orderPos[i].CompletionDate.ToString();
                        }

                        wb.SaveAs(savedialog.FileName);
                        wb = null;
                        excel.Quit();
                        MessageBox.Show("Файл успешно сохранён");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            }
            return 1;
        }

        public static async Task<int> UnloadFileData(DateTime? FromDate, DateTime? ToDate, Contract orderDB, string savePath = null)
        {
            if (orderDB == null) { return -1; }
            if (savePath != null) { return 1; }
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить файл как...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter = "Word документ (*.doc)|*.doc";
            Word.Document doc = null;
            Word.Application app = new Word.Application();
            if (savedialog.ShowDialog() == true)
            {
                await Task.Run(() =>
                {
                    DateTime? fromDate = null;
                    DateTime? toDate = null;

                    if (orderDB == null) return;
                    List<OrderPosition> orderPositions = orderDB.Order.OrderPositions.ToList();
                    List<OrderPosition> completedOrderPositions = orderDB.Order.OrderPositions.Where(c => c.CompletionDate != null).ToList();
                    Client client = SavedData.FirstOrDefault(c => c.ClientId == orderDB.ClientId);

                    if (FromDate != null && ToDate != null &&
                        FromDate < ToDate)
                    {
                        fromDate = FromDate;
                        toDate = ToDate;
                        completedOrderPositions = filterCompletionOrderPositionByDate(completedOrderPositions, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));
                    }
                    try
                    {
                        string source = $@"{Directory.GetCurrentDirectory()}\orderPattern.docx";

                        doc = app.Documents.Open(source);

                        doc.Activate();
                        Word.Bookmarks bookmarks = doc.Bookmarks;
                        Word.Table Structure = bookmarks["OrderCard"].Range.Tables.Add(bookmarks["OrderCard"].Range, completedOrderPositions.Count + 1, 5);

                        Structure.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                        Structure.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;


                        bookmarks["OrderCardNum"].Range.Text = Convert.ToString($"{orderDB.Order.OrderId}{completedOrderPositions.Count}");
                        bookmarks["DocumentDate"].Range.Text = DateTime.Now.ToString("dd-MM-yyyy");
                        bookmarks["OrderNum"].Range.Text = Convert.ToString(orderDB.Order.OrderId);
                        bookmarks["EmployeeAccept"].Range.Text = client.Account.FullName;
                        bookmarks["CompletedPosAmmount"].Range.Text = Convert.ToString(completedOrderPositions.Count);
                        bookmarks["PosAmmount"].Range.Text = Convert.ToString(orderPositions.Count);
                        bookmarks["Date"].Range.Text = fromDate == null ? "" : $"{Convert.ToDateTime(fromDate).ToString("dd-MM-yyyy")}/{Convert.ToDateTime(toDate).ToString("dd-MM-yyyy")}";

                        Structure.Cell(1, 1).Range.Text = "№Детали";
                        Structure.Cell(1, 2).Range.Text = "Наименование";
                        Structure.Cell(1, 3).Range.Text = "Кол-во";
                        Structure.Cell(1, 4).Range.Text = "Цех";
                        Structure.Cell(1, 5).Range.Text = "Дата завершения";

                        for (int i = 0; i < completedOrderPositions.Count; i++)
                        {
                            Structure.Cell(i + 2, 1).Range.Text = Convert.ToString(completedOrderPositions[i].Pau.Pauid);
                            Structure.Cell(i + 2, 2).Range.Text = completedOrderPositions[i].Pau.NameProduct;
                            Structure.Cell(i + 2, 3).Range.Text = Convert.ToString(completedOrderPositions[i].Ammount);
                            Structure.Cell(i + 2, 4).Range.Text = completedOrderPositions[i].Pau.Manufactory.Name;
                            Structure.Cell(i + 2, 5).Range.Text = completedOrderPositions[i].CompletionDate.ToString();
                        }

                        doc.SaveAs2(savedialog.FileName);
                        doc.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                        doc = null;
                        app.Quit(Word.WdSaveOptions.wdDoNotSaveChanges);
                        MessageBox.Show("Файл успешно сохранён");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            }
            return 1;
        }

        private async void UnloadOrderStatus_Click(object sender, RoutedEventArgs e)
        {
            if (orderData.SelectedItem == null) { MessageBox.Show("Выберите заказ."); return; }

            try
            {
                Contract contractData = (Contract)OrderData.SelectedItem;

                await UnloadFileData(FromDate.SelectedDate, ToDate.SelectedDate, contractData);
            } catch (Exception ex)
            {
                MessageBox.Show("Выберите заказ."); return;
            }
        }

        public static ObservableCollection<Client> SearchByFIO(ObservableCollection<Client> data, string searched)
        {
            return new ObservableCollection<Client>(data.Where(c => c.Account.FullName.ToLower().Contains(searched.ToLower())));
        }

        private void ClientFullNameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Client> searchedData = SearchByFIO(SavedData, ClientFullNameSearch.Text);

            if (searchedData.Count == 0)
            {
                MessageBox.Show("Клиенты не найдены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            orderData.ItemsSource = searchedData;
        }

        public static ObservableCollection<Client> SearchByNum(ObservableCollection<Client> data, string OrderID)
        {
            ObservableCollection<Client> searchedData = new ObservableCollection<Client>();

            foreach (Client item in data)
            {
                if (item.Contracts.Where(c => c.Order.OrderId.ToString().Contains(OrderID)).Any())
                {
                    searchedData.Add(item);
                }
            }

            return searchedData;
        }

        private void OrderNumSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Client> searchedData = SearchByNum(SavedData, OrderNumSearch.Text);

            if (searchedData.Count == 0)
            {
                MessageBox.Show("Заказы не найдены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            orderData.ItemsSource = searchedData;
        }

        private void ChartUnloadClick(object sender, RoutedEventArgs e)
        {
            if (FromDate.SelectedDate != null && ToDate.SelectedDate != null)
            {
                if (FromDate.SelectedDate < ToDate.SelectedDate)
                {
                    List<OrderPosition> orders = new List<OrderPosition>();
                    foreach (Client client in SavedData)
                    {
                        foreach (Contract contract in client.Contracts)
                        {
                            foreach (OrderPosition order in contract.Order.OrderPositions)
                            {
                                if (order != null)
                                {
                                    orders.Add(order);
                                }
                            }
                        }
                    }

                    NavigationService.Navigate(new ChartBuilder(orders,
                        Convert.ToDateTime(FromDate.SelectedDate), Convert.ToDateTime(ToDate.SelectedDate)));
                }
            }
        }

        private async void ExcelUnloadClick(object sender, RoutedEventArgs e)
        {
            if (orderData.SelectedItem == null) { MessageBox.Show("Выберите заказ."); return; }

            try
            {
                Contract contractData = (Contract)OrderData.SelectedItem;

                await PrintExcel(contractData.Order);
            } catch (Exception ex)
            {
                MessageBox.Show("Выберите заказ."); return;
            }
        }
    }
}
