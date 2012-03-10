using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using TugberkUg.UrlShrinker.Web.Application;
using TugberkUg.Web.Http.Formatting;

namespace TugberkUg.UrlShrinker.Web {

    public class MvcApplication : System.Web.HttpApplication {

        public static IDocumentStore Store  { get; set; }

        private void registerDocumentStore() {

            var parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionStringName("RavenDB");
            parser.Parse();

            Store = new DocumentStore {
                ApiKey = parser.ConnectionStringOptions.ApiKey,
                Url = parser.ConnectionStringOptions.Url
            }.Initialize();
        }

        public static void Configure(HttpConfiguration httpConfiguration) { 
            
            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new IsoDateTimeConverter());

            httpConfiguration.Formatters.Clear();
            httpConfiguration.Formatters.Add(new JsonNetFormatter(serializerSettings));
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {

            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DefaultMvc",
                url: "{parameter}",
                defaults: new { controller = "default", action = "index", parameter = UrlParameter.Optional  }
            );
        }

        protected void Application_Start() {

            RegisterGlobalFilters(GlobalFilters.Filters);
            Configure(GlobalConfiguration.Configuration);
            RegisterRoutes(RouteTable.Routes);

            registerDocumentStore();

            AutofacWebAPI.Initialize();
        }
    }
}