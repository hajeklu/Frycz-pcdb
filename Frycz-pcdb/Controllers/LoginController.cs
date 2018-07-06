using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Frycz_pcdb.Models;

namespace Frycz_pcdb.Controllers
{

    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            return View();
        }

        [HttpPost]
        public ActionResult Singin(String login, String password, String remember)
        {

            bool remem = remember != null;
            if (new Frycz_pcdb_MemberShipProvider().ValidateUser(login, EncryptHelper.encryptPassword(password)))
            {
                using (var dc = new frycz_pcdbEntities())
                {
                    registered_user user = dc.registered_user.FirstOrDefault(u => u.login.Equals(login));
                    var myCookie = new HttpCookie("myCookie");
                    user.last_login = DateTime.Now;
                    dc.SaveChanges();
                    myCookie.Values.Add("id", user.idregistered_user.ToString());
                    Response.Cookies.Add(myCookie);
                    FormsAuthentication.SetAuthCookie(login, remem);
                    
                }
                return RedirectToAction("Index", "Home");
            }

            TempData["error"] = "Login or password is invalid";
            return RedirectToAction("Index", "Login");
        }
    }
}