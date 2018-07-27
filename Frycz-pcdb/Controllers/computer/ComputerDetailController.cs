using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Frycz_pcdb.Models;

namespace Frycz_pcdb.Controllers
{
    public class ComputerDetailController : Controller
    {
        // GET: ComputerDetail
        public ActionResult Index(computer computerIn)
        {

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                var comps = entities.computers.Where(c => c.idcomputer == computerIn.idcomputer).Include(c => c.user).Include(c => c.computer_parameters).Include(c => c.o)
                    .Include(c => c.computer_type).Include(c => c.computer_brand).FirstOrDefault();

                return View("ComputerDetail", comps);
            }


        }

        [Authorize]
        public ActionResult Discard(computer computerIn)
        {
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                var comp = entities.computers.FirstOrDefault(c => c.idcomputer == computerIn.idcomputer);
                comp.discardedDate = DateTime.Now;
                entities.SaveChanges();
                Logger.logComputer(comp,"Discard",User);
                return RedirectToAction("Index", "ComputerDetail", comp);
            }
        }

        public ActionResult Delete(computer computerIn)
        {
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                var comp = entities.computers.FirstOrDefault(c => c.idcomputer == computerIn.idcomputer);
                entities.computers.Remove(comp);
                entities.SaveChanges();
                Logger.logComputer(comp, "Delete", User);
                return RedirectToAction("Index", "AllComputer", comp);
            }
        }
    }
}