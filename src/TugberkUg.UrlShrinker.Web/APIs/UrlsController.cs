using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class UrlsController : ApiController {

        private readonly IDocumentSession _ravenSession;

        public UrlsController(IDocumentStore documentStore) {

            _ravenSession = documentStore.OpenSession(
                Constants.UrlShortenerDbName
            );
        }

        public IEnumerable<ShortenedUrl> GetShortenedUrls() {

            var shortenedUrls = _ravenSession.Query<ShortenedUrl>();

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

            _ravenSession.Store(shortenedUrl);
            _ravenSession.SaveChanges();

            var response = new HttpResponseMessage<ShortenedUrl>(
                shortenedUrl, 
                HttpStatusCode.Created
            );

            response.Headers.Location = new Uri(
                string.Format(
                    "{0}/{1}",
                    ConfigurationManager.AppSettings["WebAppBaseAddress"], 
                    shortenedUrl.Alias
                )
            );

            return
                response;
        }
    }
}