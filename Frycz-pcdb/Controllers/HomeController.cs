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

            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchText)
        {
            
            if (searchText.Length < 2)
                return View("ListComputers", new List<computer>());


            string searchTextUpper = searchText.ToUpper();
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {

                entities.Configuration.LazyLoadingEnabled = false;
                var pc = entities.computers.Include(c => c.user).Include(c => c.computer_type).Where(c => c.name.Contains(searchText) || c.user.lastname.Contains(searchText) || c.user.firstname.Contains(searchText));

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
            Response.Redirect(Url.Action("Index", "Home"));

        }
    }
}