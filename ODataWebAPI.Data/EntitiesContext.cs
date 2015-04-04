using ODataWebAPI.Data.Configurations;
using ODataWebAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataWebAPI.Data
{
    public class EntitiesContext : DbContext
    {
        public EntitiesContext()
            : base("EntitiesContext") 
        {
            // Do not forget to set the connection string in Web.config..
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new CompanyConfiguration());
        }
    }
}
