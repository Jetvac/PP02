using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ratep.Pages;
using Ratep;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LoginWithValidDataGetUserData()
        {
            //Arrange
            string login = "Pascale";
            string password = "QQT92STK6QU";
            int expectedID = 2;
            Users actual = new Users();
        
            //Act
            actual = Autorization.GetUserData(login, password);
        
            //Assert
            Assert.AreEqual(expectedID, actual.AccountID);
        }
        [TestMethod]
        public void LoginNullWithValidDataGetUserData()
        {
            //Arrange
            string login = "Pascaled";
            string password = "QQT92STK6QU";
            Users expected = null;
            Users actual = new Users();

            //Act
            actual = Autorization.GetUserData(login, password);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EmployeeWithValidDataGetEmployeeData()
        {
            //Arrange
            int accountID = 2;
            int expectedID = 2;
            Employee actual = new Employee();

            //Act
            actual = Autorization.GetEmployeeByID(accountID);

            //Assert
            Assert.AreEqual(expectedID, actual.EmployeeID);
        }
        [TestMethod]
        public void EmployeeNullWithValidDataGetEmployeeData()
        {
            //Arrange
            int accountID = -1;
            Employee expected = null;
            Employee actual = new Employee();

            //Act
            actual = Autorization.GetEmployeeByID(accountID);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnloadFileWithNullDataWithValidDataUnloadFileData()
        {
            //Arrange
            int orderID = 33;
            int expected = 1;
            int actual;

            //Act
            actual = Ratep.Pages.Order.UnloadFileData(null, null, orderID);

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void UnloadFileValidDateWithValidDataUnloadFileData()
        {
            //Arrange
            int orderID = 33;
            DateTime dateTime = DateTime.MinValue;
            DateTime dateTime1 = DateTime.MaxValue;
            int expected = 1;
            int actual;

            //Act
            actual = Ratep.Pages.Order.UnloadFileData(dateTime, dateTime1, orderID);

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void UnloadFileNullWithValidDataUnloadFileData()
        {
            //Arrange
            int orderID = -1;
            int expected = -1;
            int actual;

            //Act
            actual = Ratep.Pages.Order.UnloadFileData(null, null, orderID);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OrderPositionWithValidDataCloseOrder()
        {
            //Arrange
            int orderID = 2;
            int expected = 1;
            int actual;

            //Act
            actual = OrderDetail.CloseOrder(orderID);

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void OrderPositionNullWithValidDataCloseOrder()
        {
            //Arrange
            int orderID = -1;
            int expected = -1;
            int actual;

            //Act
            actual = OrderDetail.CloseOrder(orderID);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OrderPositionWithValidDataOpenOrder()
        {
            //Arrange
            int orderID = 2;
            int expected = 1;
            int actual;

            //Act
            actual = OrderDetail.CloseOrder(orderID);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
