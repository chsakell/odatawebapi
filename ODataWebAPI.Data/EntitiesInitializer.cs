using ODataWebAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataWebAPI.Data
{
    public class EntitiesInitializer : DropCreateDatabaseIfModelChanges<EntitiesContext>
    {
        protected override void Seed(EntitiesContext context)
        {
            // Companies
            GetCompanies().ForEach(c => context.Companies.Add(c));

            // Addresses
            GetAddresses().ForEach(a => context.Addresses.Add(a));

            // Employees
            GetEmployees().ForEach(e => context.Employees.Add(e));

            base.Seed(context);
        }

        private static List<Company> GetCompanies()
        {
            List<Company> _companies = new List<Company>();

            for (int i = 1; i <= 10; i++)
            {
                _companies.Add(new Company()
                    {
                        ID = i,
                        Name = MockData.Company.Name()
                    });
            }

            return _companies;
        }

        private static List<Address> GetAddresses()
        {
            List<Address> _addresses = new List<Address>();

            for (int i = 1; i <= 200; i++)
            {
                _addresses.Add(new Address()
                {
                    ID = i,
                    City = MockData.Address.City(),
                    Country = MockData.Address.Country(),
                    State = MockData.Address.State(),
                    ZipCode = MockData.Address.ZipCode()
                });
            }

            return _addresses;
        }

        private static List<Employee> GetEmployees()
        {
            List<Employee> _employees = new List<Employee>();

            for (int i = 1; i <= 200; i++)
            {
                _employees.Add(new Employee()
                {
                    ID = i,
                    FirstName = MockData.Person.FirstName(),
                    Surname = MockData.Person.Surname(),
                    AddressID = i,
                    CompanyID = new Random().Next(1, 10),
                    Email = MockData.Internet.Email(),
                });
            }

            return _employees;
        }
    }
}
