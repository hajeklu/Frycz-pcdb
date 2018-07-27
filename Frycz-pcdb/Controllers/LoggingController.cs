using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frycz_pcdb.Controllers
{
    public class LoggingController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                List<logging> log = entities.loggings.Include(e => e.registered_user).OrderByDescending(e => e.time).Take(100).ToList();
                return View("LoggingAll", log);
            }
        }
    }
}