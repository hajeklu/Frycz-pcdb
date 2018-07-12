using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.ApplicationInsights.Web;

namespace Frycz_pcdb.Controllers
{
    public class AllComputerController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                var comps = entities.computers.Include(c => c.discarded_info).Include(c => c.computer_brand).Include(c => c.user).Include(c => c.computer_parameters).Include(c => c.computer_type).ToList();
                return View("AllComputersView",comps);
            }
        }
    }
}