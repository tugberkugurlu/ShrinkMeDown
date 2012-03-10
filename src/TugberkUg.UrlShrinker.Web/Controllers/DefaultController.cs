using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TugberkUg.UrlShrinker.Data.DataAccess;
using TugberkUg.UrlShrinker.Web.Application;

namespace TugberkUg.UrlShrinker.Web.Controllers {

    public class DefaultController : BaseMvcController {

        private static readonly string _baseWebsite = 
            ConfigurationManager.AppSettings["BaseWebsite"];

        public ActionResult Index(string parameter) {

            if (string.IsNullOrEmpty(parameter))
                return RedirectPermanent(_baseWebsite);

            var shortenedUrl = RavenSession.Query<ShortenedUrl>().FirstOrDefault(
                x => x.Alias == parameter
            );

            if (shortenedUrl == null)
                return HttpNotFound();

            return
                RedirectPermanent(shortenedUrl.Url);
        }

    }
}