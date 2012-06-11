using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Euricom.Timesheets.Models.Entities;
using MongoDB.Driver;
using Newtonsoft.Json;
using Euricom.Timesheets.Util;

namespace Euricom.Timesheets
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapHttpRoute(
                name: "Timesheets",
                routeTemplate: "api/timesheets/{name}/{year}/{month}",
                defaults: new { controller = "Timesheets", action = "Get" }
            );

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BundleTable.Bundles.RegisterTemplateBundles();

            InitializeDatabase();          

            ConfigureApi(GlobalConfiguration.Configuration);
        }

        private void ConfigureApi(HttpConfiguration configuration)
        {
            // JSON formatter
            configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new UnixDateTimeConverter());
            var jsonFormatter = new JsonNetFormatter(serializerSettings);

            configuration.Formatters[0] = jsonFormatter;

            // DI
            var modules = new[] { new RuntimeModule() };
            var resolver = new NinjectResolver(modules);
            configuration.ServiceResolver.SetResolver(resolver);
        }

        private void InitializeDatabase()
        {
            var database = MongoDatabase.Create(ConfigurationManager.AppSettings["MONGOHQ_URL"]);

            var applicationName = database.GetCollection<ApplicationName>("ApplicationName");
            if (applicationName.Count() == 0)
            {
                applicationName.Save(new ApplicationName { Value = "Euricom Timesheet" }, SafeMode.True);
            }
        }
    }
}