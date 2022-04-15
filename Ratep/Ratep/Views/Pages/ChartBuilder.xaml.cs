using Ratep.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Ratep.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChartBuilder.xaml
    /// </summary>
    public partial class ChartBuilder : Page
    {
        private DateTime _fromDate { get; set; }
        private DateTime _toDate { get; set; }
        private List<OrderPosition> _orders;

        public ChartBuilder(List<OrderPosition> orders, DateTime fromDate, DateTime toDate)
        {
            InitializeComponent();
            _orders = orders;
            _fromDate = fromDate;
            _toDate = toDate;
        }

        private void UnloadOrderStatus_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public static async Task<int> BuildChart(List<OrderPosition> orders, DateTime FromDate, DateTime ToDate)
        {
            if (orders == null) { return -1; }
            SortedDictionary<DateTime, double> counted = new SortedDictionary<DateTime, double>();
            foreach (OrderPosition order in orders)
            {
                if (order.CompletionDate != null)
                {
                    if (order.CompletionDate >= FromDate && order.CompletionDate <= ToDate)
                    {
                        DateTime converted = Convert.ToDateTime(order.CompletionDate);
                        DateTime currentDate = new DateTime(converted.Year, converted.Month, 1);
                        if (counted.Keys.ToList().Contains(currentDate))
                        {
                            counted[currentDate] += 1;
                        }
                        else
                        {
                            counted.Add(currentDate, 1);
                        }
                    }
                }
            }
            return 1;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SortedDictionary<DateTime, double> counted = new SortedDictionary<DateTime, double>();
            foreach (OrderPosition order in _orders)
            {
                if (order.CompletionDate != null)
                {
                    if (order.CompletionDate >= _fromDate && order.CompletionDate <= _toDate)
                    {
                        DateTime converted = Convert.ToDateTime(order.CompletionDate);
                        DateTime currentDate = new DateTime(converted.Year, converted.Month, 1);
                        if (counted.Keys.ToList().Contains(currentDate))
                        {
                            counted[currentDate] += 1;
                        }
                        else
                        {
                            counted.Add(currentDate, 1);
                        }
                    }
                }
            }
            Random rand = new Random(1);
            double[] values = counted.Values.ToArray();
            DateTime[] dates = counted.Keys.ToArray();
            double[] xs = dates.Select(x => x.ToOADate()).ToArray();
            var sig = ChartPlace.Plot.AddScatter(xs, values);

            if (xs.Count() == 0) { MessageBox.Show("Записи не найдены!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            ChartPlace.Plot.XAxis.DateTimeFormat(true);
            ChartPlace.Plot.YAxis2.SetSizeLimit(min: 40);

            ChartPlace.Refresh();
        }
    }
}
