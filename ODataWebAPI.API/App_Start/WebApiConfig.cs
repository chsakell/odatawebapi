using Microsoft.Data.Edm;
using ODataWebAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;

namespace ODataWebAPI.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapODataRoute("ODataRoute", "odata", GenerateEntityDataModel());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static IEdmModel GenerateEntityDataModel()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Employee>("Employees");
            builder.EntitySet<Address>("Addresses");
            var companies = builder.EntitySet<Company>("Companies");

            // Define a custom action on Companies
            var addCompanyEmployeeByIdAction = builder.Entity<Company>().Action("AddCompanyEmployee");
            addCompanyEmployeeByIdAction.Parameter<string>("name");
            addCompanyEmployeeByIdAction.Parameter<string>("surname");
            addCompanyEmployeeByIdAction.Parameter<string>("email");
            addCompanyEmployeeByIdAction.Parameter<int>("address");
            addCompanyEmployeeByIdAction.ReturnsFromEntitySet<Employee>("Employees");

            return builder.GetEdmModel();
        }
    }
}
