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
            "INSERT INTO Customer(Id, FirstName, LastName, Age, Pesel, Occupation) " +
            "VALUES(@id, @firstName, @lastName, @age, @pesel, @occupation)";

        private const string GetAllCustomersCommandText =
            "SELECT Id, FirstName, LastName, Age, Pesel, Occupation FROM Customer";

        private const string EditCustomerCommandText =
            "UPDATE Customer SET FirstName=@firstName, LastName=@lastName, Age=@age, " +
            "Pesel=@pesel, Occupation=@occupation WHERE Id=@id";

        private const string DeleteCustomerCommandText =
            "DELETE FROM Customer WHERE Id=@id";

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
                            CommandText = DeleteCustomerCommandText
                        };
                        command.Parameters.AddWithValue("id", id);

                        if (command.ExecuteNonQuery() == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
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
                        command.Parameters.AddWithValue("id", model.Id);
                        command.Parameters.AddWithValue("firstName", model.FirstName);
                        command.Parameters.AddWithValue("lastName", model.LastName);
                        command.Parameters.AddWithValue("age", model.Age);
                        command.Parameters.AddWithValue("pesel", model.Pesel);
                        command.Parameters.AddWithValue("occupation", model.Occupation);

                        command.ExecuteNonQuery();
                        return true;
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditCustomer(CustomerModel model)
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
                            CommandText = EditCustomerCommandText
                        };
                        command.Parameters.AddWithValue("firstName", model.FirstName);
                        command.Parameters.AddWithValue("lastName", model.LastName);
                        command.Parameters.AddWithValue("age", model.Age);
                        command.Parameters.AddWithValue("pesel", model.Pesel);
                        command.Parameters.AddWithValue("occupation", model.Occupation);
                        command.Parameters.AddWithValue("id", model.Id);
                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}