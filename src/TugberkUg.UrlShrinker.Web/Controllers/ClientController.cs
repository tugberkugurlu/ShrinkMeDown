using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TugberkUg.UrlShrinker.Web.Controllers {

    [Authorize(Users = "tgbrk")]
    public class ClientController : Controller {

        public ActionResult Index() {

            return View();
        }

    }
}