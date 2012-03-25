using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;
using TugberkUg.UrlShrinker.Data.DataAccess;
using TugberkUg.UrlShrinker.Web.Application;

namespace TugberkUg.UrlShrinker.Web.Controllers {

    public class DefaultController : Controller {

        private static readonly string _baseWebsite = 
            ConfigurationManager.AppSettings["BaseWebsite"];

        private readonly IDocumentSession _ravenSession;

        public DefaultController(IDocumentStore documentStore) {

            _ravenSession = documentStore.OpenSession(
                Constants.UrlShortenerDbName
            );
        }

        public ActionResult Index(string parameter) {

            if (string.IsNullOrEmpty(parameter))
                return RedirectPermanent(_baseWebsite);

            var shortenedUrl = _ravenSession.Query<ShortenedUrl>().FirstOrDefault(
                x => x.Alias == parameter
            );

            if (shortenedUrl == null)
                return HttpNotFound();

            return
                RedirectPermanent(shortenedUrl.Url);
        }

    }
}