using System.Collections.Generic;
using System.Linq;
using Windows.UI.Input;
using Windows.UI.Xaml.Data;
using Prism.Commands;
using Prism.Windows.Mvvm;
using UWPCrud.Model;
using UWPCrud.Services.Abstract;

namespace UWPCrud.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        public ICollectionView Customers { get; private set; }
        public CustomerModel CurrentCustomer { get; set; }
        private CustomerModel _selectedCustomer;
        public CustomerModel SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                CurrentCustomer.FirstName = _selectedCustomer.FirstName;
                CurrentCustomer.LastName = _selectedCustomer.LastName;
                CurrentCustomer.Pesel = _selectedCustomer.Pesel;
                CurrentCustomer.Occupation = _selectedCustomer.Occupation;
                CurrentCustomer.Age = _selectedCustomer.Age;
                CurrentCustomer.Id = _selectedCustomer.Id;
            }
        }


        public MainPageViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
            CurrentCustomer = new CustomerModel();
            SelectedCustomer = new CustomerModel();
            

            Customers = new CollectionViewSource
            {
                Source = _customerService.GetAllCustomers()
            }.View;

            Customers.MoveCurrentToPosition(-1);

            Add = new DelegateCommand(() =>
            {
                if (_customerService.AddCustomer(CurrentCustomer))
                {
                    Customers.Add(CurrentCustomer);
                };
            });

            Edit = new DelegateCommand(() =>
            {
                if (_customerService.EditCustomer(CurrentCustomer))
                {
                    var customers = Customers.Cast<CustomerModel>().ToList();
                    var customerToEdit = customers.First(customer => customer.Id == CurrentCustomer.Id);
                    customerToEdit.FirstName = CurrentCustomer.FirstName;
                    customerToEdit.LastName = CurrentCustomer.LastName;
                    customerToEdit.Pesel = CurrentCustomer.Pesel;
                    customerToEdit.Age = CurrentCustomer.Age;
                    customerToEdit.Occupation = CurrentCustomer.Occupation;
                };
            });

            Delete = new DelegateCommand(() =>
            {
                _customerService.DeleteCustomer(CurrentCustomer.Id);
            });
        }


        public DelegateCommand Add { get; private set; }
        public DelegateCommand Edit { get; private set; }
        public DelegateCommand Delete { get; private set; }
    }
}