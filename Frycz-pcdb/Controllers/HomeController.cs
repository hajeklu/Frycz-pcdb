using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Frycz_pcdb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.Name != null && !User.Identity.Name.Equals(""))
            {
                ViewBag.name = User.Identity.Name;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchText)
        {
            
            if (searchText.Length < 3)
                return View("ListComputers", new List<computer>());


            string searchTextUpper = searchText.ToUpper();
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                var pc = from computer in entities.computers
                    where computer.name.Contains(searchTextUpper)
                    select computer;
                pc = pc.Include(m => m.user);
                pc = pc.Include(m => m.computer_type);

                List<computer> computersList = pc.ToList();

                if (computersList.Count == 1)
                    return RedirectToAction("Index","ComputerDetail" ,computersList.FirstOrDefault());

                return View("ListComputers",pc.ToArray());
            }
        }

        public void Logout()
        {
            Response.Cookies.Clear();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();
            var cookie = Request.Cookies["myCookie"];
            FormsAuthentication.SignOut();
            cookie.Values["id"] = null;
            Response.Redirect(Url.Action("Index", "Home"));

        }
    }
}