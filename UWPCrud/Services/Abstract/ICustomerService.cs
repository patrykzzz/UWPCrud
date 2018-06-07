using System.Collections.Generic;
using UWPCrud.Model;

namespace UWPCrud.Services.Abstract
{
    public interface ICustomerService
    {
        IEnumerable<CustomerModel> GetAllCustomers();
        bool DeleteCustomer(int id);
        bool AddCustomer(CustomerModel model);
        bool EditCustomer(CustomerModel model);
    }

    internal class CustomerService : ICustomerService
    {
        public IEnumerable<CustomerModel> GetAllCustomers()
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteCustomer(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool AddCustomer(CustomerModel model)
        {
            throw new System.NotImplementedException();
        }

        public bool EditCustomer(CustomerModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}