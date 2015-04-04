using ODataWebAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataWebAPI.Data.Configurations
{
    class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            ToTable("Employees");
            Property(e => e.CompanyID).IsRequired();
            Property(e => e.AddressID).IsRequired();
        }
    }
}
