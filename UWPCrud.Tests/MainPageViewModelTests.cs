using System.Linq;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UWPCrud.Model;
using UWPCrud.Services.Abstract;
using UWPCrud.ViewModels;

namespace UWPCrud.Tests
{
    [TestClass]
    public class MainPageViewModelTests
    {
        private Mock<ICustomerService> _customerService;
        private MainPageViewModel _target;
        private IFixture _fixture;

        [TestInitialize]
        public void SetUp()
        {
            _fixture = new Fixture();
            _customerService = new Mock<ICustomerService>();

            _customerService.Setup(x => x.GetAllCustomers())
                .Returns(_fixture.CreateMany<CustomerModel>(5));

            _target = new MainPageViewModel(_customerService.Object);
        }

        [TestMethod]
        public void Add_ForEverythingOk_ShouldAddCustomer()
        {
            // Arrange
            _customerService.Setup(x => x.AddCustomer(It.IsAny<CustomerModel>()))
                .Returns(true);
            var customer = _fixture.Create<CustomerModel>();
            _target.CurrentCustomer = customer;

            // Act
            _target.Add.Execute();

            // Assert
            Assert.IsTrue(_target.Customers.Any(c => c.Id == customer.Id));
        }

        [TestMethod]
        public void Add_ForEverythingOk_ShouldInvokeServiceMethod()
        {
            // Arrange
            _customerService.Setup(x => x.AddCustomer(It.IsAny<CustomerModel>()))
                .Returns(true);
            var customer = _fixture.Create<CustomerModel>();
            _target.CurrentCustomer = customer;

            // Act
            _target.Add.Execute();

            // Assert
            _customerService.Verify(x => x.AddCustomer(customer), Times.Once);
        }

        [TestMethod]
        public void Add_ForSomethingWrong_ShouldNotChangeCollection()
        {
            // Arrange
            _customerService.Setup(x => x.AddCustomer(It.IsAny<CustomerModel>()))
                .Returns(false);
            var customers = _target.Customers;
            _target.CurrentCustomer = _fixture.Create<CustomerModel>();

            // Act
            _target.Add.Execute();

            // Assert
            Assert.IsTrue(_target.Customers.Equals(customers));
        }

        [TestMethod]
        public void Edit_ForEverythingOk_ShouldChangeProperObject()
        {
            // Arrange
            _customerService.Setup(x => x.EditCustomer(It.IsAny<CustomerModel>()))
                .Returns(true);
            _target.CurrentCustomer = _target.Customers.First();
            var id = _target.CurrentCustomer.Id;
            _target.CurrentCustomer = _fixture.Create<CustomerModel>();
            _target.CurrentCustomer.Id = id;

            // Act
            _target.Edit.Execute();

            // Assert
            var customer = _target.Customers.First(c => c.Id == id);
            Assert.IsTrue(customer.FirstName ==_target.CurrentCustomer.FirstName
                          && customer.LastName ==_target.CurrentCustomer.LastName
                          && customer.Pesel ==_target.CurrentCustomer.Pesel
                          && customer.Occupation ==_target.CurrentCustomer.Occupation
                          && customer.Age ==_target.CurrentCustomer.Age);
        }

        [TestMethod]
        public void Edit_ForEverythingOk_ShouldInvokeServiceMethod()
        {
            // Arrange
            _customerService.Setup(x => x.EditCustomer(It.IsAny<CustomerModel>()))
                .Returns(true);
            _target.CurrentCustomer = _fixture.Create<CustomerModel>();
            _target.CurrentCustomer.Id = _target.Customers.First().Id;
            var customerToEdit = _target.CurrentCustomer;

            // Act
            _target.Edit.Execute();

            // Assert
            _customerService.Verify(x => x.EditCustomer(customerToEdit), Times.Once);
        }

        [TestMethod]
        public void Edit_ForSomethingWrong_ShouldNotChangeCollection()
        {
            // Arrange
            _customerService.Setup(x => x.EditCustomer(It.IsAny<CustomerModel>()))
                .Returns(false);
            _target.CurrentCustomer = _fixture.Create<CustomerModel>();
            var initialCollection = _target.Customers;

            // Act
            _target.Edit.Execute();

            // Assert
            Assert.AreEqual(initialCollection, _target.Customers);
        }

        [TestMethod]
        public void Delete_ForEverythingOk_ShouldRemoveCustomerFromCollection()
        {
            // Arrange
            _customerService.Setup(x => x.DeleteCustomer(It.IsAny<int>()))
                .Returns(true);
            var customerToDelete = _target.CurrentCustomer = _target.Customers.First();

            // Act
            _target.Delete.Execute();

            // Assert
            Assert.IsFalse(_target.Customers.Contains(customerToDelete));
        }

        [TestMethod]
        public void Delete_ForEverythingOk_ShouldInvokeServiceMethod()
        {
            // Arrange
            _customerService.Setup(x => x.DeleteCustomer(It.IsAny<int>()))
                .Returns(true);
            _target.CurrentCustomer = _target.Customers.First();

            // Act
            _target.Delete.Execute();

            // Assert
            _customerService.Verify(x => x.DeleteCustomer(_target.CurrentCustomer.Id), Times.Once);
        }

        [TestMethod]
        public void Delete_ForSomethingWrong_ShouldNotChangeCollection()
        {
            // Arrange
            _customerService.Setup(x => x.DeleteCustomer(It.IsAny<int>()))
                .Returns(false);
            _target.CurrentCustomer = _target.Customers.First();
            var initialCollection = _target.Customers;

            // Act
            _target.Delete.Execute();

            // Assert
            Assert.AreEqual(initialCollection, _target.Customers);
        }
    }
}
