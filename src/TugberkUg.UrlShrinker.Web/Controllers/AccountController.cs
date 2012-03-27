using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;
using TugberkUg.UrlShrinker.Web.Application;
using TugberkUg.UrlShrinker.Web.Application.Services;

namespace TugberkUg.UrlShrinker.Web.Controllers {

    public class AccountController : Controller {

        private readonly IDocumentSession _ravenSession;
        private readonly IAuthorizationService _authorizationService;
        private readonly IFormsAuthenticationService _formsAuthenticationService;

        public AccountController(
            IDocumentStore documentStore, 
            IAuthorizationService authorizationService,
            IFormsAuthenticationService formsAuthenticationService) {

            _ravenSession = documentStore.OpenSession(
                Constants.UrlShortenerDbName
            );

            _authorizationService = authorizationService;
            _formsAuthenticationService = formsAuthenticationService;
        }

        public ViewResult LogOn() {

            return View();
        }

        [ActionName("LogOn"), HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOn_post(
            [Bind(Exclude = "Id,IsApproved,HashAlgorithm")]
            TugberkUg.UrlShrinker.Data.DataAccess.User user) {

            if (ModelState.IsValid && 
                _authorizationService.Authorize(user.UserName, user.Password)) {

                _formsAuthenticationService.SignIn(user.UserName, true);

                return RedirectToAction("index", "client");
            }

            return View();
        }

        [Authorize]
        public ActionResult LogOff() {

            _formsAuthenticationService.SignOut();

            return RedirectToAction("index", "client");
        }
    }
}
