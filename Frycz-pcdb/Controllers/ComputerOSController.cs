using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Frycz_pcdb.Models;

namespace Frycz_pcdb.Controllers
{
    public class ComputerOSController : Controller
    {
        // GET: ComputerOS
        public ActionResult Index()
        {
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;

                return View("AllOS", entities.os.ToList());
            }

        }

        public ActionResult Edit(o oIn)
        {
            if (oIn == null)
            {
                return RedirectToAction("Index", "ComputerOS");
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                o oOut = entities.os.FirstOrDefault(e => e.idos == oIn.idos);
                return View("EditOS", oOut);
            }
        }

        public ActionResult SaveEdit(o oIn)
        {
            if (!Validator.validOS(oIn))
            {
                ModelState.AddModelError("exist", "Operation system is invalid or in use.");
                return View("EditOS", oIn);
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                o oO = entities.os.FirstOrDefault(e => e.idos == oIn.idos);

                oO.name = oIn.name;
                oO.version = oIn.version;
                entities.SaveChanges();

                return RedirectToAction("Index", "ComputerOS");
            }
        }

        public ActionResult AddOS()
        {
            return View("AddOS");
        }

        public ActionResult SaceAdd(o oIn)
        {
            if (!Validator.validOS(oIn))
            {
                ModelState.AddModelError("exist", "Operation system is invalid or in use.");
                return View("AddOS", oIn);
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.os.Add(oIn);
                entities.SaveChanges();

                return RedirectToAction("Index", "ComputerOS");
            }
        }

        public ActionResult Delete(o oIn)
        {
            if (oIn == null)
            {
                return RedirectToAction("Index", "ComputerOS");
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.os.Remove(entities.os.FirstOrDefault(e => e.idos == oIn.idos));
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerOS");
            }
        }

        public string AddAjax(string name, string version)
        {
            int versionN = 0;
            if (!(Int32.TryParse(version, out versionN)|| name == null))
            {
                return null;
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                
                o os = new o();

                os.name = name;
                os.version = versionN;
                entities.os.Add(os);
                entities.SaveChanges();

            }

            return "{\"msg\":\"success\"}";
        }

    }
}