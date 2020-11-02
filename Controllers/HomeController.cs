using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentCar.Controllers
{
    public class HomeController : Controller
    {
        public static string Language = "1";

        // GET: Home
        [ActionName("Index")]
        public ActionResult Index_Get()
        {
            Session["Lang"] = "English";
            return View();
        }


        [HttpPost]
        [ActionName("Index")]
        public ActionResult Index_Post(string lang)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                if (Language == "1")
                {
                    Language = "2";
                    Session["Lang"] = "Arabic";
                }
                else
                {
                    if (Language == "2")
                    {
                        Language = "1";
                        Session["Lang"] = "English";
                    }
                }
            }
                

            return View();
        }

    }
}