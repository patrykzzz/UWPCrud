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
        public CustomerModel SelectedCustomer { get; set; }

        public MainPageViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
            CurrentCustomer = new CustomerModel();
            SelectedCustomer = new CustomerModel();

            Customers = new CollectionViewSource
            {
                Source = _customerService.GetAllCustomers()
            }.View;

            Add = new DelegateCommand(() =>
            {
                _customerService.AddCustomer(CurrentCustomer);
            });

            Edit = new DelegateCommand(() =>
            {
                _customerService.EditCustomer(CurrentCustomer);
            });

            Delete = new DelegateCommand(() =>
            {
                _customerService.DeleteCustomer(CurrentCustomer.Id);
            });

            SelectCustomer = new DelegateCommand(() =>
            {
                CurrentCustomer.FirstName = SelectedCustomer.FirstName;
                CurrentCustomer.LastName = SelectedCustomer.LastName;
                CurrentCustomer.Age = SelectedCustomer.Age;
                CurrentCustomer.Occupation = SelectedCustomer.Occupation;
                CurrentCustomer.Pesel = SelectedCustomer.Pesel;
                CurrentCustomer.Id = SelectedCustomer.Id;
            });
        }

        public void HandleTap(object o, TappedEventArgs args)
        {
            SelectCustomer.Execute();
        }

        
        public DelegateCommand Add { get; private set; }
        public DelegateCommand Edit { get; private set; }
        public DelegateCommand Delete { get; private set; }
        public DelegateCommand SelectCustomer { get; set; }
    }
}