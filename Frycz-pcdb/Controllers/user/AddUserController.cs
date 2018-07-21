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
    }
}