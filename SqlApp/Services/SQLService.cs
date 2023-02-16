using Dapper;
using Microsoft.Data.SqlClient;
using SqlApp.Models;
using SqlApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SqlApp.Services
{
    //Dapper
    internal class SQLService
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hundmamma\OneDrive\Skrivbord\SqlCourse\SqlApp\SqlApp\Data\local_sql_db.mdf;Integrated Security=True;Connect Timeout=30";
        
        
        //Saves a customer
       public async Task SaveCustomerAsync(CustomerEntities customerEntities)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.ExecuteAsync("IF NOT EXISTS(SELECT Id FROM Customers WHERE Email = @Email) INSERT INTO Customers VALUES(@FirstName, @LastName, @Email, @AddressId)", customerEntities);
        }


        //Returns address
        public async Task<int> GetOrSaveAddressAsync(AddressEntity addressEntity)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.ExecuteScalarAsync<int>("IF NOT EXISTS (SELECT Id FROM Addresses WHERE StreetName = @StreetName AND StreetNumber = @StreetNumber AND PostalCode = @PostalCode AND City = @City) INSERT INTO Addresses OUTPUT INSERTED.Id VALUES(@StreetName, @StreetNumber, @PostalCode, @City) ELSE SELECT Id FROM Addresses WHERE StreetName = @StreetName AND StreetNumber = @StreetNumber AND PostalCode = @PostalCode AND City = @City", addressEntity);
        }


        //Returns all customers
        public async Task<IEnumerable<Customers>> GetAllCustomersAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<Customers>("SELECT c.Id, c.FirstName, c.LastName, c.Email, a.StreetName, a.StreetNumber, a.PostalCode, a.City FROM Customers c JOIN Addresses a ON c.AddressId = a.Id", conn);
        }





        //Returns specific customers by email
        public async Task<Customers> GetCustomersAsync(string email)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<Customers>("SELECT c.Id, c.FirstName, c.LastName, c.Email, a.StreetName, a.StreetNumber, a.PostalCode, a.City FROM Customers c JOIN Addresses a ON c.AddressId = a.Id WHERE c.Email = @Email", new {Email = email});
        }


        public async Task<CustomerEntities> GetCustomersEntityByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<CustomerEntities>("SELECT * FROM Customers WHERE Id = @Id", new { Id = id });
        }



        public async Task UpdateCustomerAsync(CustomerEntities customerEntities)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.ExecuteAsync("UPDATE Customers SET FirstName = @FirstName, LastName = @LastName, Email = @Email, AddressId = @AddressId WHERE Id = @Id", customerEntities);
        }



        //Removes customers by email
        public async Task RemoveCustomerAsync(string email)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.ExecuteAsync("DELETE FROM Customers WHERE Email = @Email", new {Email = email});
        }

        //Return specific address by ID
        public async Task<AddressEntity> GetAddressEntityByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<AddressEntity>("SELECT * FROM Addresses WHERE Id = @Id", new { Id = id });
        }


    }
}









/*



-----------------------------------------
       private int GetOrSaveAddress(CustomerAddress address) 
       {
           using var conn = new SqlConnection(_connectionString);
           conn.Open();

           using var cmd = new SqlCommand("IF NOT EXISTS (SELECT Id FROM Addresses WHERE StreetName = @StreetName AND StreetNumber = @StreetNumber AND PostalCode = @PostalCode AND City = @City) INSERT INTO Addresses OUTPUT INSERTED.Id VALUES(@StreetName, @StreetNumber, @PostalCode, @City) ELSE SELECT Id FROM Addresses WHERE StreetName = @StreetName AND StreetNumber = @StreetNumber AND PostalCode = @PostalCode AND City = @City", conn);
           cmd.Parameters.AddWithValue("@StreetName", address.StreetName);
           cmd.Parameters.AddWithValue("@StreetNumber", address.StreetNumber);
           cmd.Parameters.AddWithValue("@PostalCode", address.PostalCode);
           cmd.Parameters.AddWithValue("@City", address.City);

           return int.Parse(cmd.ExecuteScalar().ToString()!);
       }

      




------------------------------------------

       private void SaveCustomer(CustomerEntities customerEntity)
       {
           using var conn = new SqlConnection(_connectionString);
           conn.Open();

           using var cmd = new SqlCommand("IF NOT EXISTS (SELECT Id FROM Customers WHERE Email = @Email) INSERT INTO Customers VALUES (@FirstName, @LastName, @Email, @AddressId)", conn);
           cmd.Parameters.AddWithValue("@FirstName", customerEntity.FirstName);
           cmd.Parameters.AddWithValue("@LastName", customerEntity.LastName);
           cmd.Parameters.AddWithValue("@Email", customerEntity.Email);            
           cmd.Parameters.AddWithValue("@AddressId", customerEntity.AddressId);


           cmd.ExecuteNonQuery();
       }*/
