using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataWebAPI.Data.Entities
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public int AddressID { get; set; }
        public virtual Address Address { get; set; }

        public int CompanyID { get; set; }
        public virtual Company Company { get; set; }
    }
}
