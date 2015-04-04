using Microsoft.Data.OData;
using ODataWebAPI.Data;
using ODataWebAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;

namespace ODataWebAPI.API.Controllers
{
    public class CompaniesController : EntitySetController<Company, int>
    {
        private EntitiesContext _dbContext;

        public CompaniesController()
        {
            this._dbContext = new EntitiesContext();
        }

        [Queryable]
        public override IQueryable<Company> Get()
        {
            try
            {
                return _dbContext.Companies;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        protected override Company GetEntityByKey(int key)
        {
            return _dbContext.Companies.Where(c => c.ID == key).FirstOrDefault();
        }

        // Uses name convention
        // /odata/Companies(1)/Employees
        [Queryable]
        public IQueryable<Employee> GetEmployeesFromCompany([FromODataUri] int key)
        {
            return _dbContext.Employees.Where(e => e.CompanyID == key);
        }

        [HttpPost]
        public Employee AddCompanyEmployee([FromODataUri] int key, ODataActionParameters actionParams)
        {
            Employee employee = null;
            try
            {
                if (actionParams["name"] != null && actionParams["surname"] != null && actionParams["email"] != null && actionParams["address"] != null)
                {
                    string employeeName = actionParams["name"].ToString();
                    string employeeSurname = actionParams["surname"].ToString();
                    string employeeEmail = actionParams["email"].ToString();
                    int employeeAddress = Int32.Parse(actionParams["address"].ToString());

                    employee = new Employee()
                    {
                        FirstName = employeeName,
                        Surname = employeeSurname,
                        Email = employeeEmail,
                        AddressID = employeeAddress,
                        CompanyID = key
                    };

                    _dbContext.Employees.Add(employee);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(
                    Request.CreateODataErrorResponse(
                    HttpStatusCode.NotFound,
                    new ODataError
                    {
                        ErrorCode = "Error",
                        Message = ex.Message
                    }));
            }

            return employee;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _dbContext.Dispose();
        }
    }


    /*
    public class CompaniesController : ApiController
    {
        private EntitiesContext _dbContext;

        public CompaniesController()
        {
            this._dbContext = new EntitiesContext();
        }

        [Queryable]
        public IQueryable<Company> GetEmployees()
        {
            try
            {
                return _dbContext.Companies;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _dbContext.Dispose();
        }
    }
    */
}
