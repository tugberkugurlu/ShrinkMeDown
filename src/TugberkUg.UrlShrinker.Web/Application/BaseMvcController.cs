using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;

namespace TugberkUg.UrlShrinker.Web.Application {

    public class BaseMvcController : Controller {

        public IDocumentSession RavenSession { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext) {

            if (filterContext.IsChildAction)
                return;

            RavenSession = MvcApplication.Store.OpenSession(Constants.UrlShortenerDbName);
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext) {

            if (filterContext.IsChildAction)
                return;

            using (RavenSession) {

                if (filterContext.Exception != null)
                    return;

                if (RavenSession != null)
                    RavenSession.SaveChanges();
            }

            base.OnActionExecuted(filterContext);
        }
    }
}