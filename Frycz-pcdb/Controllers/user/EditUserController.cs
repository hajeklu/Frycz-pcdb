using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Frycz_pcdb.Models;

namespace Frycz_pcdb.Controllers
{
    public class EditUserController : Controller
    {
        
        [Authorize]
        public ActionResult Index(Frycz_pcdb.user userIn)
        {
            return View("EditUser", userIn);
        }

        [Authorize]
        public ActionResult Save(Frycz_pcdb.user userIn)
        {
            if (!Validator.validUser(userIn))
            {
                ModelState.AddModelError("valid", "Firstname or lastname is invalid.");
                return View("EditUser", userIn);
            }



            if (ModelState.IsValid)
            {
                if (Validator.checkExistUser(userIn))
                {
                    ModelState.AddModelError("exist", "User is already exist.");
                    return View("EditUser", userIn);
                }

                using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
                    {
                        Frycz_pcdb.user user = entities.users.FirstOrDefault(u => u.iduser == userIn.iduser);
                        user.lastname = userIn.lastname;
                        user.firstname = userIn.firstname;
                        entities.SaveChanges();
                        return RedirectToAction("Index", "UserDetail", user);
                    }
                    
                }
                return View("EditUser", userIn);
        }
    }
}