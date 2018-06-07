using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Windows.UI.Xaml.Data;
using Prism.Commands;
using Prism.Windows.Mvvm;
using UWPCrud.Model;

namespace UWPCrud.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        public ICollectionView Customers { get; private set; }
        public CustomerModel CurrentCustomer { get; set; }

        public MainPageViewModel()
        {
            CurrentCustomer = new CustomerModel();
            
            var exampleCustomers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Mark",
                    LastName = "Zuckerberg",
                    Occupation = "Facebook",
                    Pesel = "96758434567"   
                },
                new CustomerModel
                {
                    FirstName = "John",
                    LastName = "Markowski",
                    Occupation = "Facebook",
                    Pesel = "96758434561"   
                }
            };

            Customers = new CollectionViewSource
            {
                Source = exampleCustomers
            }.View;

            Add = new DelegateCommand(() =>
            {
                CurrentCustomer.FirstName = new Random().NextDouble().ToString(CultureInfo.InvariantCulture);
            });
        }

        public DelegateCommand Add { get; private set; }
        public DelegateCommand Update { get; private set; }
        public DelegateCommand Delete { get; private set; }
    }
}