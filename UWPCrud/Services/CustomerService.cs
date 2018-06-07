using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UWPCrud.Model;
using UWPCrud.Services.Abstract;

namespace UWPCrud.Services
{
    internal class CustomerService : ICustomerService
    {
        private const string ConnectionString = "";

        private const string AddCustomerCommandText =
            "INSERT INTO Customer(FirstName, LastName, Age, Pesel, Occupation) " +
            "VALUES(@firstName, @lastName, @age, @pesel, @occupation)";

        private const string GetAllCustomersCommandText =
            "SELECT Id, FirstName, LastName, Age, Pesel, Occupation FROM Customer";

        public IEnumerable<CustomerModel> GetAllCustomers()
        {
            var customers = new List<CustomerModel>();
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        var command = new SqlCommand(GetAllCustomersCommandText, connection);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customers.Add(new CustomerModel
                                {
                                    Id = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Age = reader.GetInt32(3),
                                    Pesel = reader.GetString(4),
                                    Occupation = reader.GetString(5)
                                });
                            }
                        }
                    }
                }

                return customers;
            }
            catch (Exception)
            {
                return new List<CustomerModel>();
            }
        }

        public bool DeleteCustomer(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool AddCustomer(CustomerModel model)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        var command = new SqlCommand
                        {
                            Connection = connection,
                            CommandText = AddCustomerCommandText
                        };
                        command.Parameters.AddWithValue("firstName", model.FirstName);
                        command.Parameters.AddWithValue("lastName", model.LastName);
                        command.Parameters.AddWithValue("age", model.Age);
                        command.Parameters.AddWithValue("pesel", model.Pesel);
                        command.Parameters.AddWithValue("occupation", model.Occupation);

                        var rows = command.ExecuteNonQuery();

                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditCustomer(CustomerModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}