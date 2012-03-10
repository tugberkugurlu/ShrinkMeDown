using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Raven.Client;
using TugberkUg.UrlShrinker.Data.DataAccess;
using TugberkUg.UrlShrinker.Web.Application;
using TugberkUg.Web.Http.Filters;

namespace TugberkUg.UrlShrinker.Web.APIs {

    [ApiKeyAuth("apiKey", typeof(UrlShrinkerApiKeyAuthorizer))]
    public class UrlsController : BaseApiController {

        public UrlsController() { }

        public IEnumerable<ShortenedUrl> GetShortenedUrls() {

            var shortenedUrls = RavenSession.Query<ShortenedUrl>();

            return
                shortenedUrls;
        }

        public string Get(int id) {

            return null;
        }

        public void Put() { 

        }

        [Validation]
        public HttpResponseMessage<ShortenedUrl> PostShortenedUrl(ShortenedUrl shortenedUrl) {

            shortenedUrl.CreatedOn = DateTime.Now;

            RavenSession.Store(shortenedUrl);
            RavenSession.SaveChanges();

            var response = new HttpResponseMessage<ShortenedUrl>(
                shortenedUrl, 
                HttpStatusCode.Created
            );

            response.Headers.Location = new Uri(
                string.Format("http://tugberk.me/{0}", shortenedUrl.Alias)
            );

            return
                response;
        }
    }
}