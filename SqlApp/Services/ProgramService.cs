using SqlApp.Models;
using System.ComponentModel;

namespace SqlApp.Services
{
    internal class ProgramService
    {
        

        public static async Task CreateContactAsync()
        {
            var customer = new Customers();

            Console.Write("Firstname: ");
            customer.FirstName = Console.ReadLine() ?? "";

            Console.Write("Lastname: ");
            customer.LastName = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            customer.Email = Console.ReadLine() ?? "";
            
            Console.Write("Streetname: ");
            customer.StreetName = Console.ReadLine() ?? "";

            Console.Write("Streetnumber: ");
            customer.StreetNumber = Console.ReadLine() ?? "";

            Console.Write("Postalcode: ");
            customer.PostalCode = Console.ReadLine() ?? "";

            Console.Write("City: ");
            customer.City = Console.ReadLine() ?? "";

            //Save to DB
            await CustomerService.SaveToDatabaseAsync(customer);
        }

        public static async Task ShowAllContactsAsync()
        {
            var customers = await CustomerService.GetCustomersAsync();


            foreach (Customers customer in customers)
            {
                Console.WriteLine($"Customer ID: {customer.Id}");
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine($"Address: {customer.StreetName} {customer.StreetNumber}, {customer.PostalCode}, {customer.City}");
                Console.WriteLine("");
            }
        }

        public static async Task ShowSpecificContactAsync()
        {
            
            Console.WriteLine("Input the email of the customer you wish to find");
            var email = Console.ReadLine();


            if (!string.IsNullOrEmpty(email))
            {
                var customer = await CustomerService.GetAsync(email);

               if (customer != null) { 
                Console.WriteLine($"Customer ID: {customer.Id}");
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine($"Address: {customer.StreetName} {customer.StreetNumber}, {customer.PostalCode}, {customer.City}");
                Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine($"No customer with {email} exists");
            }





        }
        public static async Task UpdateContactAsync()
        {
            Console.WriteLine("Input the email of the customer you wish to update");
            var email = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Contact found! Please fill in the fields below what you want to update");


            if (!string.IsNullOrEmpty(email))
            {
                var customers = await CustomerService.GetAsync(email);

                Console.Write("Firstname: ");
                customers.FirstName = Console.ReadLine() ?? "";

                Console.Write("Lastname: ");
                customers.LastName = Console.ReadLine() ?? "";

                Console.Write("Email: ");
                customers.Email = Console.ReadLine() ?? "";

                Console.Write("Streetname: ");
                customers.StreetName = Console.ReadLine() ?? "";

                Console.Write("Streetnumber: ");
                customers.StreetNumber = Console.ReadLine() ?? "";

                Console.Write("Postalcode: ");
                customers.PostalCode = Console.ReadLine() ?? "";

                Console.Write("City: ");
                customers.City = Console.ReadLine() ?? "";

                await CustomerService.UpdateAsync(customers);
                Console.WriteLine("Contact has been updated!");
            }
            else
            {
                Console.WriteLine($"No customer with {email} exists");
            }
        }


        public static async Task RemoveContactAsync()
        {
            Console.WriteLine("Input the email of the customer you wish to remove");
            var email = Console.ReadLine();


            if (!string.IsNullOrEmpty(email))
            {
               await CustomerService.RemoveAsync(email);
                Console.Clear();
                Console.WriteLine($"customer with {email} has been removed!");

            }
            else
            {
                Console.WriteLine($"No customer with {email} exists");
            }
        }

        
    }
}
