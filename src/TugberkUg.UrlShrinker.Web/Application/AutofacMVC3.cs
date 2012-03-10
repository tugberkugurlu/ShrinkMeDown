using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace TugberkUg.UrlShrinker.Web.Application {

    internal class AutofacMVC3 {

        public static void Initialize() {

            var builder = new ContainerBuilder();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(RegisterServices(builder)));
        }

        private static IContainer RegisterServices(ContainerBuilder builder) {

            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            //builder.RegisterType<WordRepository>().As<IWordRepository>();

            return
                builder.Build();
        }
    }
}