using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Windows.Mvvm;
using UWPCrud.Model;
using UWPCrud.Services.Abstract;

namespace UWPCrud.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        public ObservableCollection<CustomerModel> Customers { get; private set; }
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

            Customers = new ObservableCollection<CustomerModel>(_customerService.GetAllCustomers());
            
            Add = new DelegateCommand(() =>
            {
                CurrentCustomer.Id = Customers.Max(x => x.Id) + 1;
                if (_customerService.AddCustomer(CurrentCustomer))
                {
                    Customers.Add(new CustomerModel
                    {
                        FirstName = CurrentCustomer.FirstName,
                        LastName = CurrentCustomer.LastName,
                        Age = CurrentCustomer.Age,
                        Occupation = CurrentCustomer.Occupation,
                        Pesel = CurrentCustomer.Pesel,
                        Id = CurrentCustomer.Id
                    });
                }
            });

            Edit = new DelegateCommand(() =>
            {
                if (_customerService.EditCustomer(CurrentCustomer))
                {
                    var customerToEdit = Customers.First(customer => customer.Id == CurrentCustomer.Id);
                    customerToEdit.FirstName = CurrentCustomer.FirstName;
                    customerToEdit.LastName = CurrentCustomer.LastName;
                    customerToEdit.Pesel = CurrentCustomer.Pesel;
                    customerToEdit.Age = CurrentCustomer.Age;
                    customerToEdit.Occupation = CurrentCustomer.Occupation;
                };
            });

            Delete = new DelegateCommand(() =>
            {
                if (_customerService.DeleteCustomer(CurrentCustomer.Id))
                {
                    Customers.Remove(Customers.First(x => x.Id == CurrentCustomer.Id));
                }
            });
        }


        public DelegateCommand Add { get; private set; }
        public DelegateCommand Edit { get; private set; }
        public DelegateCommand Delete { get; private set; }
    }
}