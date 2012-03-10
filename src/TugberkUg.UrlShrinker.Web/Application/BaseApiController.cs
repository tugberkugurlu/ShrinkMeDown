using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Raven.Client;

namespace TugberkUg.UrlShrinker.Web.Application {

    public class BaseApiController : ApiController {

        public IDocumentSession RavenSession { get; private set; }

        protected override void Initialize(
            HttpControllerContext controllerContext) {

            RavenSession = MvcApplication.Store.OpenSession(Constants.UrlShortenerDbName);

            base.Initialize(controllerContext);
        }
    }
}