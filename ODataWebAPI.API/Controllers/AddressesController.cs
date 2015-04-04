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
    public class AddressesController : EntitySetController<Address, int>
    {
        private EntitiesContext _dbContext;

        public AddressesController()
        {
            this._dbContext = new EntitiesContext();
        }

        [Queryable]
        public override IQueryable<Address> Get()
        {
            return _dbContext.Addresses;
            //return base.Get();
        }

        protected override Address GetEntityByKey(int key)
        {
            return _dbContext.Addresses.Where(a => a.ID == key).FirstOrDefault();
        }

        public override void Delete(int key)
        {
            var address = _dbContext.Addresses.Where(a => a.ID == key).FirstOrDefault();
            if (address != null)
            {
                _dbContext.Addresses.Remove(address);
                _dbContext.SaveChanges();
            }
            else
                throw new HttpResponseException(
                    Request.CreateODataErrorResponse(
                    HttpStatusCode.NotFound,
                    new ODataError
                    {
                        ErrorCode = "NotFound.",
                        Message = "Address " + key + " not found."
                    }));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _dbContext.Dispose();
        }
    }
}
