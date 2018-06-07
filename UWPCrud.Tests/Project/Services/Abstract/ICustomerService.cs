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
}