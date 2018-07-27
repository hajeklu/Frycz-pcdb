using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Frycz_pcdb.Models;

namespace Frycz_pcdb.Controllers
{
    public class ComputerModelController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return View("AllModels", entities.computer_brand.ToList());
            }

        }

        [Authorize]
        public ActionResult AddModel()
        {
            return View("AddModel");
        }

        [Authorize]
        public ActionResult EditModel(computer_brand brand)
        {
            if (brand == null)
            {
                return RedirectToAction("Index");
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                computer_brand cb =
                    entities.computer_brand.FirstOrDefault(e => e.idcumputer_brand == brand.idcumputer_brand);
                return View("EditModel", cb);
            }
        }

        [Authorize]
        public ActionResult Delete(computer_brand brand)
        {
            if (brand == null)
                return RedirectToAction("Index");
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                computer_brand b =
                    entities.computer_brand.FirstOrDefault(e => e.idcumputer_brand == brand.idcumputer_brand);
                entities.computer_brand.Remove(b);
                Logger.logModel(b,"Delete", User);
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerModel");
            }
        }

        public ActionResult SaveAdd(computer_brand brand)
        {
            if (!Validator.validModel(brand))
            {
                ModelState.AddModelError("exist", "Computer model is invalid or inuse.");
                return View("AddModel", brand);
            }
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.computer_brand.Add(brand);
                Logger.logModel(brand,"Add",User);
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerModel");
            }
        }

        public ActionResult SaveEdit(computer_brand brand)
        {
            if (!Validator.validModel(brand))
            {
                ModelState.AddModelError("exist", "Computer model is invalid or inuse.");
                return View("EditModel", brand);
            }
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                computer_brand b =
                    entities.computer_brand.FirstOrDefault(e => e.idcumputer_brand == brand.idcumputer_brand);
                b.maker = brand.maker;
                b.model = brand.model;
                Logger.logModel(brand, "Edit", User);
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerModel");
            }
        }

        public string AddAjax(string model, string maker)
        {
            computer_brand c = new computer_brand();
            c.maker = maker;
            c.model = model;
            if (!Validator.validModel(c))
            {
                return null;
            }
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.computer_brand.Add(c);
                Logger.logModel(c, "Add", User);
                entities.SaveChanges();

                return "{\"msg\":\"success\"}";
            }
        }
    }
}