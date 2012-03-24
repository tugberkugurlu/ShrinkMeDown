using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Autofac;
using Raven.Client;
using Raven.Client.Document;

namespace TugberkUg.UrlShrinker.Web.Application {

    internal class AutofacWebAPI {

        public static void Initialize() {
            var builder = new ContainerBuilder();
            GlobalConfiguration.Configuration.ServiceResolver.SetResolver(
                new AutofacWebAPIDependencyResolver(RegisterServices(builder))
            );
        }

        private static IContainer RegisterServices(ContainerBuilder builder) {

            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).PropertiesAutowired();

            //builder.RegisterType<WordRepository>().As<IWordRepository>();

            builder.RegisterType<DocumentStore>().As<IDocumentStore>().
                OnActivating(x => {

                    x.Instance.Url = "http://localhost:90";
                    x.Instance.Initialize();
                }).
                SingleInstance();

            return
                builder.Build();
        }
    }
}