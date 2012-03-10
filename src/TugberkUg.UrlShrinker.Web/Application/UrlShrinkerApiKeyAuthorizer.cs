using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TugberkUg.Web.Http;

namespace TugberkUg.UrlShrinker.Web.Application {

    public class UrlShrinkerApiKeyAuthorizer : IApiKeyAuthorizer {

        private readonly bool _allowAnonymousApiAccess =
            bool.Parse(System.Configuration.ConfigurationManager.AppSettings["AllowAnonymousApiAccess"]);

        public bool IsAuthorized(string apiKey) {

            return 
                _allowAnonymousApiAccess;
        }

        public bool IsAuthorized(string apiKey, string[] roles) {

            return
                this.IsAuthorized(apiKey);
        }
    }
}