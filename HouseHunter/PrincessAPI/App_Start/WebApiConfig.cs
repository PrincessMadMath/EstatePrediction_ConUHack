using System.Web.Http;
using PrincessAPI.Infrastructure;
using System.Data.Entity;
using PrincessAPI.API.Clarifai;

namespace PrincessAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            /*config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );*/

            Database.SetInitializer<SystemDBContext>(
                new DropCreateDatabaseIfModelChanges<SystemDBContext>()
                );


            // Initialise Clarifai token
            ClarifaiAccess.Authentify();
        }
    }
}
