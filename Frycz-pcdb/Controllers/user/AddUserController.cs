using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Frycz_pcdb.Models;

namespace Frycz_pcdb.Controllers.user
{
    public class AddUserController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View("AddUser");
        }

        [Authorize]
        public ActionResult Save(Frycz_pcdb.user userIn)
        {

            if (!Validator.validUser(userIn))
            {
                ModelState.AddModelError("valid", "Firstname or lastname is invalid.");
                return View("AddUser", userIn);
            }

            if (ModelState.IsValid)
            {

                if (Validator.checkExistUser(userIn))
                {
                    ModelState.AddModelError("exist", "User is already exist.");
                    return View("AddUser", userIn);
                }


                using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
                {
                    entities.users.Add(userIn);
                    entities.SaveChanges();
                    return RedirectToAction("Index", "AllUsers");
                }
            }

            return View("AddUser", userIn);
        }

        public string addJax(string firstname, string lastname)
        {
            if (!Validator.validUser(firstname, lastname))
            {
                return null;
            }

            if (Validator.checkExistUser(firstname, lastname))
            {
                return null;
            }
            Frycz_pcdb.user u = new Frycz_pcdb.user();
            u.lastname = lastname;
            u.firstname = firstname;
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.users.Add(u);
                entities.SaveChanges();
                return "{\"msg\":\"success\"}";
            }
        }
    }
}