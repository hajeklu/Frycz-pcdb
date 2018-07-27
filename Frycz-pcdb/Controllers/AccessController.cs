using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Frycz_pcdb.Models;

namespace Frycz_pcdb.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            ViewBag.noti = TempData["notifi"];
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                List<registered_user> users = entities.registered_user.ToList();
                return View("AllRegisteredUser", users);
            }

        }

        public ActionResult Delete(registered_user user)
        {
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                registered_user u =
                    entities.registered_user.FirstOrDefault(e => e.idregistered_user == user.idregistered_user);

                entities.registered_user.Remove(u);
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        

        public ActionResult ResetPassword(registered_user user)
        {
            user.password = "";
            return View("ResetPassword", user);
        }

        public ActionResult SaveReset(registered_user user, string password1, string password2)
        {
            if (!password1.Equals(password2))
            {
                ModelState.AddModelError("same", "Passwords must be same.");
                return View("ResetPassword", user);
            }

            if (password1.Length < 4)
            {
                ModelState.AddModelError("same", "Minimum password length is 4 characters.");
                return View("ResetPassword", user);
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                registered_user u =
                    entities.registered_user.FirstOrDefault(e => e.login.Equals(user.login));

                u.password = EncryptHelper.encryptPassword(password1);
                Logger.logToDB("Reset password to user " + u.login, entities, User.Identity.Name);
                entities.SaveChanges();
                TempData["notifi"] = "true";
                return RedirectToAction("Index");

            }
            

        }

    }
}