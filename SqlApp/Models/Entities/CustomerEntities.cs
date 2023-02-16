using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlApp.Models.Entities
{
    internal class CustomerEntities
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

       
        public int AddressId { get; set; }
    }
}
//Ser ut exakt som SQL-modell