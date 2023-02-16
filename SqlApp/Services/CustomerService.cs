using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SqlApp.Models;
using SqlApp.Models.Entities;

namespace SqlApp.Services
{
    //Dapper
    internal class CustomerService
    {
        //Saves and returns Address ID or creates new ID if it doesn't exist
        public static async Task SaveToDatabaseAsync(Customers customers)
        {
            var database = new SQLService();

            var addressId = await database.GetOrSaveAddressAsync(new AddressEntity
            {
                StreetName = customers.StreetName,
                StreetNumber = customers.StreetNumber,
                PostalCode = customers.PostalCode,
                City = customers.City
            });
            
            await database.SaveCustomerAsync(new CustomerEntities
            {
                FirstName = customers.FirstName,
                LastName = customers.LastName,
                Email = customers.Email,
                AddressId = addressId
            });
        }


        //Returns all customers
        public static async Task<IEnumerable<Customers>> GetCustomersAsync()
        {
            var database = new SQLService();
            return (IEnumerable<Customers>)await database.GetAllCustomersAsync();
        }

        //Returns single customer by email
        public static async Task<Customers> GetAsync(string email)
        {
            var database = new SQLService();
            return await database.GetCustomersAsync(email);
        }


        public static async Task UpdateAsync(Customers customers)
        {
            var database = new SQLService();

            var customerEntity = await database.GetCustomersEntityByIdAsync(customers.Id);         
            if(!string.IsNullOrEmpty(customers.FirstName)) { customerEntity.FirstName = customers.FirstName; }
            if (!string.IsNullOrEmpty(customers.LastName)) { customerEntity.LastName = customers.LastName; }
            if (!string.IsNullOrEmpty(customers.Email)) { customerEntity.Email = customers.Email; }

            
            var addressEntity = await database.GetAddressEntityByIdAsync(customerEntity.AddressId);
            if (!string.IsNullOrEmpty(customers.StreetName)) { customers.StreetName = customers.StreetName; }
            if (!string.IsNullOrEmpty(customers.StreetNumber)) { customers.StreetNumber = customers.StreetNumber; }
            if (!string.IsNullOrEmpty(customers.PostalCode)) { customers.PostalCode = customers.PostalCode; }
            if (!string.IsNullOrEmpty(customers.City)) { customers.City = customers.City; }


            //Returns AddressID or created new if there has been updates
            customerEntity.AddressId = await database.GetOrSaveAddressAsync(addressEntity);

            await database.UpdateCustomerAsync(customerEntity);
           
        }

        public static async Task RemoveAsync(string email)
        {
            var database = new SQLService();
            await database.RemoveCustomerAsync(email);
        }
    }
}










/*public void SaveToDatabase(Customers customer) 
       {
           var customerEntity = new CustomerEntities
           {
               FirstName = customer.FirstName,
               LastName = customer.LastName,
               Email= customer.Email,                
               AddressId = GetOrSaveAddress(customer.Address)
           };

           SaveCustomer(customerEntity);
       }


---------------------------------------------------------------------------

       public IEnumerable<Customers> GetCustomers()
       {
           var customers = new List<Customers>();

           using var conn = new SqlConnection(_connectionString);
           conn.Open();

           using var cmd = new SqlCommand("SELECT c.Id, c.FirstName, c.LastName, c.Email, a.StreetName, a.StreetNumber, a.PostalCode, a.City FROM Customers c JOIN Addresses a ON c.AddressId = a.Id", conn);
           var result = cmd.ExecuteReader();

           while (result.Read())
           {
               customers.Add(new Customers
               {
                   Id = result.GetInt32(0),
                   FirstName = result.GetString(1),
                   LastName = result.GetString(2),
                   Email = result.GetString(3),
                   Address = new CustomerAddress
                   {
                       StreetName = result.GetString(4),
                       StreetNumber = result.GetString(5),
                       PostalCode = result.GetString(6),
                       City = result.GetString(7),
                   }
               });
           }
           return customers;
       }


--------------------------------------------------------------------


       public Customers GetCustomers(string email)
       {
           var customer = new Customers();

           using var conn = new SqlConnection(_connectionString);
           conn.Open();

           using var cmd = new SqlCommand("SELECT c.Id, c.FirstName, c.LastName, c.Email, a.StreetName, a.StreetNumber, a.PostalCode, a.City FROM Customers c JOIN Addresses a ON c.AddressId = a.Id WHERE c.Email = @Email", conn);
           cmd.Parameters.AddWithValue("@Email", email);
           var result = cmd.ExecuteReader();

           while (result.Read())
           {
               customer.Id = result.GetInt32(0);
               customer.FirstName = result.GetString(1);
               customer.LastName = result.GetString(2);
               customer.Email = result.GetString(3);
               customer.Address = new CustomerAddress
               {
                   StreetName = result.GetString(4),
                   StreetNumber = result.GetString(5),
                   PostalCode = result.GetString(6),
                   City = result.GetString(7),
               };

           }
           return customer;
       }*/