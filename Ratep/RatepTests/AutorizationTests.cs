using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Ratep.Class;
using Ratep.Models.ApiModels;
using Ratep.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ratep.Pages.Tests
{
    [TestClass()]
    public class AutorizationTests
    {
        [TestMethod]
        public async Task AuthorizateTrue()
        {
            string Actual = await Autorization.Authorizate("admin", "admin");

            Assert.IsTrue(Actual != ((int)ApiErrors.UserNotFound).ToString());
        }
        [TestMethod]
        public async Task AuthorizateFalse()
        {
            string Actual = await Autorization.Authorizate("1", "1");

            Assert.IsTrue(Actual == ((int)ApiErrors.UserNotFound).ToString());
        }
        [TestMethod]
        public async Task AuthorizateWithErrorDBFalse()
        {
            await DatabaseConnectionSettings.ConnectChange("a", "VeloRa");
            string Actual = await Autorization.Authorizate("admin", "admin");
            await DatabaseConnectionSettings.ConnectChange(".\\SQLEXPRESS", "VeloRa");

            Assert.IsTrue(Actual == ((int)ApiErrors.DBNotFound).ToString());
        }
        [TestMethod]
        public async Task DBConnectChangeTrue()
        {
            await DatabaseConnectionSettings.ConnectChange(".\\SQLEXPRESS", "VeloRa");
            string Actual = await Autorization.Authorizate("admin", "admin");

            Assert.IsTrue(Actual != ((int)ApiErrors.UserNotFound).ToString());
        }
        [TestMethod]
        public async Task DBConnectChangeFalse()
        {
            await DatabaseConnectionSettings.ConnectChange("a", "a");
            string Actual = await Autorization.Authorizate("admin", "admin");
            await DatabaseConnectionSettings.ConnectChange(".\\SQLEXPRESS", "VeloRa");

            Assert.IsTrue(Actual == ((int)ApiErrors.DBNotFound).ToString());
        }
        [TestMethod]
        public async Task OrderDataLoadoutTrue()
        {
            string Actual = await ApiControl.GetRequest($"OrderPositions/GetClientsOrderList");

            Assert.IsTrue(Actual != ((int)ApiErrors.DBNotFound).ToString());
        }
        [TestMethod]
        public async Task FIOFilterTrue()
        {
            string Actual = await ApiControl.GetRequest($"OrderPositions/GetClientsOrderList");
            string searched = "a";
            ObservableCollection<Client> NonFiltered = JsonConvert.DeserializeObject<ObservableCollection<Client>>(Actual);
            ObservableCollection<Client> Filtered = OrderPage.SearchByFIO(NonFiltered, searched);

            Assert.IsTrue(Filtered.Count != 0);
        }
        [TestMethod]
        public async Task FIOFilterFalse()
        {
            string Actual = await ApiControl.GetRequest($"OrderPositions/GetClientsOrderList");
            string searched = "123142";
            ObservableCollection<Client> NonFiltered = JsonConvert.DeserializeObject<ObservableCollection<Client>>(Actual);
            ObservableCollection<Client> Filtered = OrderPage.SearchByFIO(NonFiltered, searched);

            Assert.IsTrue(Filtered.Count == 0);
        }
        [TestMethod]
        public async Task OrderNumFilterTrue()
        {
            string Actual = await ApiControl.GetRequest($"OrderPositions/GetClientsOrderList");
            string searched = "2";
            ObservableCollection<Client> NonFiltered = JsonConvert.DeserializeObject<ObservableCollection<Client>>(Actual);
            ObservableCollection<Client> Filtered = OrderPage.SearchByNum(NonFiltered, searched);

            Assert.IsTrue(Filtered.Count != 0);
        }
        [TestMethod]
        public async Task OrderNumFilterFalse()
        {
            string Actual = await ApiControl.GetRequest($"OrderPositions/GetClientsOrderList");
            string searched = "211111";
            ObservableCollection<Client> NonFiltered = JsonConvert.DeserializeObject<ObservableCollection<Client>>(Actual);
            ObservableCollection<Client> Filtered = OrderPage.SearchByNum(NonFiltered, searched);

            Assert.IsTrue(Filtered.Count == 0);
        }
        [TestMethod]
        public async Task WordUnloadTrue()
        {
            string Actual = await ApiControl.GetRequest($"OrderPositions/GetClientsOrderList");
            ObservableCollection<Client> ClientData = JsonConvert.DeserializeObject<ObservableCollection<Client>>(Actual);

            int response = await OrderPage.UnloadFileData(new DateTime(2020, 04, 01), DateTime.Now, ClientData[2].Contracts.ToList()[0], @"C:\\");

            Assert.IsTrue(1 == response);
        }
        [TestMethod]
        public async Task WordUnloadFalse()
        {
            int response = await OrderPage.UnloadFileData(new DateTime(2020, 04, 01), DateTime.Now, null);

            Assert.IsTrue(-1 == response);
        }
        [TestMethod]
        public async Task ExcelUnloadTrue()
        {
            string Actual = await ApiControl.GetRequest($"OrderPositions/GetClientsOrderList");
            ObservableCollection<Client> ClientData = JsonConvert.DeserializeObject<ObservableCollection<Client>>(Actual);

            int response = await OrderPage.PrintExcel(ClientData[2].Contracts.ToList()[0].Order, @"C:\\");

            Assert.IsTrue(1 == response);
        }
        [TestMethod]
        public async Task ExcelUnloadFalse()
        {
            int response = await OrderPage.PrintExcel(null);

            Assert.IsTrue(-1 == response);
        }
        [TestMethod]
        public async Task GraphBuildTrue()
        {
            string Actual = await ApiControl.GetRequest($"OrderPositions/GetClientsOrderList");
            ObservableCollection<Client> ClientData = JsonConvert.DeserializeObject<ObservableCollection<Client>>(Actual);
            List<OrderPosition> orders = new List<OrderPosition>();
            foreach (Client client in ClientData)
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
            int response = await ChartBuilder.BuildChart(orders, new DateTime(2020, 04, 01), DateTime.Now);

            Assert.IsTrue(1 == response);
        }
        [TestMethod]
        public async Task GraphBuildFalse()
        {
            int response = await ChartBuilder.BuildChart(null, new DateTime(2020, 04, 01), DateTime.Now);

            Assert.IsTrue(-1 == response);
        }
        [TestMethod]
        public async Task OrderOpenTrue()
        {
            string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IlN5YmlsbCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3N1cm5hbWUiOiJCcmFkZm9yZCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkVuZ2luZXIiLCJleHAiOjE2NDk4Njk0ODQsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwNTEvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA1MS8ifQ.K8y6fVnvpxwN9NkvOV-4lOUbv0Q3fXRIq4LUj9dswqA";
            string response = await OrderDetail.OpenOrderApiRequest(10, Token);

            Assert.IsTrue(((int)ApiErrors.UserNotFound).ToString() != response);
        }
        [TestMethod]
        public async Task OrderOpenFalse()
        {
            string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IlN5YmlsbCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3N1cm5hbWUiOiJCcmFkZm9yZCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkVuZ2luZXIiLCJleHAiOjE2NDk4Njk0ODQsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwNTEvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA1MS8ifQ.K8y6fVnvpxwN9NkvOV-4lOUbv0Q3fXRIq4LUj9dswqA";
            string response = await OrderDetail.OpenOrderApiRequest(999, Token);

            Assert.IsTrue(((int)ApiErrors.ErrorData).ToString() == response);
        }
        [TestMethod]
        public async Task OrderCloseTrue()
        {
            string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IlN5YmlsbCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3N1cm5hbWUiOiJCcmFkZm9yZCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkVuZ2luZXIiLCJleHAiOjE2NDk4Njk0ODQsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwNTEvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA1MS8ifQ.K8y6fVnvpxwN9NkvOV-4lOUbv0Q3fXRIq4LUj9dswqA";
            string response = await OrderDetail.CloseOrderApiRequest(10, Token);

            Assert.IsTrue(((int)ApiErrors.UserNotFound).ToString() != response);
        }
        [TestMethod]
        public async Task OrderCloseFalse()
        {
            string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IlN5YmlsbCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3N1cm5hbWUiOiJCcmFkZm9yZCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkVuZ2luZXIiLCJleHAiOjE2NDk4Njk0ODQsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwNTEvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA1MS8ifQ.K8y6fVnvpxwN9NkvOV-4lOUbv0Q3fXRIq4LUj9dswqA";
            string response = await OrderDetail.CloseOrderApiRequest(999, Token);

            Assert.IsTrue(((int)ApiErrors.ErrorData).ToString() == response);
        }
    }
}