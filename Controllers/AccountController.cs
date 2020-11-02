using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.UI;
using System.Web.Script.Serialization;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;

namespace RentCar.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

       

        [ActionName("Login")]
        [HttpGet]
        public ActionResult Login_GET()
        {
            return View();
        }


        [ActionName("Login")]
        [HttpPost]
        public ActionResult Login_Post()
        {
            return RedirectToAction("home", "index");
        }



        

    }
}