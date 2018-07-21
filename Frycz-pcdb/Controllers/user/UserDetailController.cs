using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace Frycz_pcdb.Controllers
{
    public class UserDetailController : Controller
    {
        [Authorize]
        public ActionResult Index(Frycz_pcdb.user user)
        {
            Frycz_pcdb.user userIn = null;
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                if (user.iduser != null)
                {
                    userIn = entities.users.Include(e => e.computers).FirstOrDefault(e => e.iduser == user.iduser);

                }
                return View("UserDetail", userIn);

            }
        }

        [Authorize]
        public ActionResult Delete(Frycz_pcdb.user userIn)
        {
            if (userIn != null && userIn.iduser != null)
            {
                using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
                {
                    Frycz_pcdb.user userdelete = entities.users.FirstOrDefault(u => u.iduser == userIn.iduser);
                    entities.users.Remove(userdelete);
                    entities.SaveChanges();
                    return RedirectToAction("Index", "AllUsers");
                }
            } 
            return null;
        }
    }
}