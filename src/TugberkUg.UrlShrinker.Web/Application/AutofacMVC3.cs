using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Raven.Client;
using Raven.Client.Document;
using TugberkUg.UrlShrinker.Web.Application.Services;

namespace TugberkUg.UrlShrinker.Web.Application {

    internal class AutofacMVC3 {

        public static void Initialize() {

            var builder = new ContainerBuilder();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(RegisterServices(builder)));
        }

        private static IContainer RegisterServices(ContainerBuilder builder) {

            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            //builder.RegisterType<WordRepository>().As<IWordRepository>();

            builder.RegisterType<DocumentStore>().As<IDocumentStore>()
                .OnActivating(x => {

                    x.Instance.Url = "http://localhost:90";
                    x.Instance.Initialize();
                })
                .SingleInstance();

            builder.RegisterType<AuthorizationService>()
                .As<IAuthorizationService>()
                .UsingConstructor(typeof(IDocumentStore))
                .SingleInstance();

            builder.RegisterType<FormsAuthenticationService>().As<IFormsAuthenticationService>().SingleInstance();

            return
                builder.Build();
        }
    }
}