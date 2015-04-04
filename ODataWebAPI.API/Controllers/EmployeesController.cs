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

    public class EmployeesController : EntitySetController<Employee, int>
    {
        private EntitiesContext _dbContext;

        public EmployeesController()
        {
            this._dbContext = new EntitiesContext();
        }

        [Queryable]
        public override IQueryable<Employee> Get()
        {
            return _dbContext.Employees;
        }

        protected override Employee GetEntityByKey(int key)
        {
            return _dbContext.Employees.Where(e => e.ID == key).FirstOrDefault();
        }

        // /odata/Employees(1)/Address
        public Address GetAddressFromEmployee(int key)
        {
            return _dbContext.Employees.Where(e => e.ID == key)
                .Select(e => e.Address).FirstOrDefault();
        }

        // /odata/Employees(1)/Company
        public Company GetCompanyFromEmployee(int key)
        {
            return _dbContext.Employees.Where(e => e.ID == key)
                .Select(e => e.Company).FirstOrDefault();
        }

        protected override Employee CreateEntity(Employee entity)
        {
            _dbContext.Employees.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        protected override Employee UpdateEntity(int key, Employee update)
        {
            if (!_dbContext.Employees.Any(e => e.ID == key))
            {
                throw new HttpResponseException(
                    Request.CreateODataErrorResponse(
                    HttpStatusCode.NotFound,
                    new ODataError
                    {
                        ErrorCode = "NotFound.",
                        Message = "Employee " + key + " not found."
                    }));
            }

            update.ID = key;

            _dbContext.Employees.Attach(update);
            _dbContext.Entry(update).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
            return update;
        }

        protected override Employee PatchEntity(int key, Delta<Employee> patch)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.ID == key);
            if (employee == null)
                throw new HttpResponseException(
                    Request.CreateODataErrorResponse(
                    HttpStatusCode.NotFound,
                    new ODataError
                    {
                        ErrorCode = "NotFound.",
                        Message = "Employee " + key + " not found."
                    }));

            patch.Patch(employee);
            _dbContext.SaveChanges();

            return employee;
        }

        public override void Delete(int key)
        {
            var employee = _dbContext.Employees.Where(a => a.ID == key).FirstOrDefault();
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
            }
            else
                throw new HttpResponseException(
                    Request.CreateODataErrorResponse(
                    HttpStatusCode.NotFound,
                    new ODataError
                    {
                        ErrorCode = "NotFound.",
                        Message = "Employee " + key + " not found."
                    }));
        }

        // The base POST method will call to form the location header
        protected override int GetKey(Employee entity)
        {
            return entity.ID;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _dbContext.Dispose();
        }
    }


    /*
    public class EmployeesController : ApiController
    {
        private EntitiesContext _dbContext;

        public EmployeesController()
        {
            this._dbContext = new EntitiesContext();
        }

        [Queryable]
        public IQueryable<Employee> GetEmployees()
        {
            return _dbContext.Employees;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _dbContext.Dispose();
        }
    }
    */
}
