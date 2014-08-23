using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PropertyManager.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "LoginGetClaims",
                routeTemplate: "api/Login/GetClaims",
                defaults: new { controller = "Login", action = "GetClaims" }
            );

            config.Routes.MapHttpRoute(
                name: "UniqueBaseGetAll",
                routeTemplate: "api/UniqueBase/Get/{typeName}",
                defaults: new { controller = "UniqueBase", action = "GetAll" }
            );

            config.Routes.MapHttpRoute(
                name: "UniqueBaseGet",
                routeTemplate: "api/UniqueBase/Get/{typeName}/{id}",
                defaults: new { controller = "UniqueBase", action = "GetUniqueBase" }
            );

            config.Routes.MapHttpRoute(
                name: "UniqueBasePut",
                routeTemplate: "api/UniqueBase/Put/{typeName}",
                defaults: new { controller = "UniqueBase", action = "PutUniqueBase" }
            );

            config.Routes.MapHttpRoute(
                name: "UniqueBasePost",
                routeTemplate: "api/UniqueBase/Post/{typeName}",
                defaults: new { controller = "UniqueBase", action = "PostUniqueBase" }

            );

            config.Routes.MapHttpRoute(
                name: "UniqueBaseDelete",
                routeTemplate: "api/UniqueBase/Delete/{typeName}/{id}",
                defaults: new { controller = "UniqueBase", action = "DeleteUniqueBase" }
            );



            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
