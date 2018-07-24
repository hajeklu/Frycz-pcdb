using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using WebGrease.Configuration;

namespace Frycz_pcdb.Controllers
{
    public class ComputerParametersController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                var parameters = entities.computer_parameters.ToList();
                return View("AllParameters", parameters);
            }
        }

        public ActionResult AddComputerParameters()
        {
            return View("AddComputerParameters");
        }

        public ActionResult SaveAdd(computer_parameters parameters)
        {
            if (parameters == null)
            {
                return View("AddComputerParameters");
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.computer_parameters.Add(parameters);
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerParameters");
            }
        }

        public ActionResult Delete(computer_parameters parameters)
        {
            if (parameters == null)
            {
                return View("AddComputerParameters");
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                computer_parameters par = entities.computer_parameters.FirstOrDefault(p =>
                    p.idcomputer_parameters == parameters.idcomputer_parameters);
                entities.computer_parameters.Remove(par);
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerParameters");
            }
        }

        public ActionResult Edit(computer_parameters parameters)
        {
            if (parameters == null)
            {
                return null;
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                computer_parameters p = entities.computer_parameters.FirstOrDefault(e =>
                    e.idcomputer_parameters == parameters.idcomputer_parameters);

                return View("EditParameters", p);
            }
        }

        public ActionResult SaveEdit(computer_parameters parameters)
        {
            if (parameters == null)
            {
                return null;
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                computer_parameters p = entities.computer_parameters.FirstOrDefault(e =>
                    e.idcomputer_parameters == parameters.idcomputer_parameters);

                p.hdd = parameters.hdd;
                p.processor = parameters.processor;
                p.ram = parameters.ram;

                entities.SaveChanges();

                return RedirectToAction("Index", "ComputerParameters");

            }

        }

        public string AddAjax(string processor, string ram, string hdd)
        {
            int ramN = 0;
            int hddN = 0;
            if (!(Int32.TryParse(ram, out ramN) && Int32.TryParse(hdd,out hddN)) || processor == null)
            {
                return null;
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                computer_parameters computerParameters = new computer_parameters();
                computerParameters.hdd = hddN;
                computerParameters.ram = ramN;
                computerParameters.processor = processor;

                entities.computer_parameters.Add(computerParameters);
                entities.SaveChanges();

            }

            return "{\"msg\":\"success\"}";
        }
    }
}