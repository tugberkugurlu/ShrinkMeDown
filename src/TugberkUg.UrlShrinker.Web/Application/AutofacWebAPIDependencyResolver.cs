using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;

namespace TugberkUg.UrlShrinker.Web.Application {

    internal class AutofacWebAPIDependencyResolver : 
        System.Web.Http.Services.IDependencyResolver {

        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public AutofacWebAPIDependencyResolver(IContainer container) {

            _container = container;
        }

        /// <summary>
        /// Get a single instance of a service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The single instance if resolved; otherwise, <c>null</c>.</returns>
        public object GetService(Type serviceType) {

            return _container.IsRegistered(serviceType) ? _container.Resolve(serviceType) : null;
        }

        /// <summary>
        /// Gets all available instances of a services.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The list of instances if any were resolved; otherwise, an empty list.</returns>
        public IEnumerable<object> GetServices(Type serviceType) {

            Type enumerableServiceType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            object instance = _container.Resolve(enumerableServiceType);
            return ((IEnumerable)instance).Cast<object>();
        }
    }
}