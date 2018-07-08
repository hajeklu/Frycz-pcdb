using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frycz_pcdb.Controllers
{
    public class AllComputerController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                var pc = from computer in entities.computers
                    select computer;
                pc = pc.Include(m => m.user);
                return View("AllComputersView", pc.ToArray());
            }
        }
    }
}