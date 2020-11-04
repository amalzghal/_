using System.Web.Mvc;

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