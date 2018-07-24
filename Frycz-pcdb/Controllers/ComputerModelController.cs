using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace Frycz_pcdb.Controllers
{
    public class ComputerModelController : Controller
    {
        // GET: ComputerModel
        public ActionResult Index()
        {

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return View("AllModels", entities.computer_brand.ToList());
            }

        }

        public ActionResult AddModel()
        {
            return View("AddModel");
        }

        public ActionResult EditModel(computer_brand brand)
        {
            if (brand == null)
            {
                return null;
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                computer_brand cb =
                    entities.computer_brand.FirstOrDefault(e => e.idcumputer_brand == brand.idcumputer_brand);
                return View("EditModel", cb);
            }
        }

        public ActionResult Delete(computer_brand brand)
        {
            if (brand == null)
                return null;
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                computer_brand b =
                    entities.computer_brand.FirstOrDefault(e => e.idcumputer_brand == brand.idcumputer_brand);
                entities.computer_brand.Remove(b);
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerModel");
            }
        }

        public ActionResult SaveAdd(computer_brand brand)
        {
            if (brand == null)
                return null;
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.computer_brand.Add(brand);
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerModel");
            }
        }

        public ActionResult SaveEdit(computer_brand brand)
        {
            if (brand == null)
                return null;
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                computer_brand b =
                    entities.computer_brand.FirstOrDefault(e => e.idcumputer_brand == brand.idcumputer_brand);
                b.maker = brand.maker;
                b.model = brand.model;
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerModel");
            }
        }

        public string AddAjax(string model, string maker)
        {
            if (model == null || maker == null)
                return null;
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                computer_brand c = new computer_brand();
                c.maker = maker;
                c.model = model;
                entities.computer_brand.Add(c);
                entities.SaveChanges();

                return "{\"msg\":\"success\"}";
            }
        }
    }
}