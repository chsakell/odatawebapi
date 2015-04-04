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
