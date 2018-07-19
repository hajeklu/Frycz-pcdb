using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frycz_pcdb.Controllers
{
    public class UserDetailController : Controller
    {
        // GET: UserDetail
        public ActionResult Index(user user)
        {
            user userIn = null;
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
    }
}