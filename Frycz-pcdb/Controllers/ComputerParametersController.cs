using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Frycz_pcdb.Models;
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

        [Authorize]
        public ActionResult AddComputerParameters()
        {
            return View("AddComputerParameters");
        }

        public ActionResult SaveAdd(computer_parameters parameters)
        {
            if (!Validator.validParameters(parameters))
            {
                ModelState.AddModelError("exist", "Computer parameters is invalid or in use.");
                return View("AddComputerParameters", parameters);
            }


            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.computer_parameters.Add(parameters);

                Logger.logParameters(parameters, "Add", User);
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerParameters");
            }
        }

        [Authorize]
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
                Logger.logParameters(par, "Delete", User);
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerParameters");
            }
        }

        [Authorize]
        public ActionResult Edit(computer_parameters parameters)
        {
            if (parameters == null)
            {
                return RedirectToAction("Index", "ComputerParameters");
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
            if (!Validator.validParameters(parameters))
            {
                ModelState.AddModelError("exist", "Computer parameters is invalid or in use.");
                return View("EditParameters", parameters);
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
            computer_parameters computerParameters = new computer_parameters();
            computerParameters.hdd = hddN;
            computerParameters.ram = ramN;
            computerParameters.processor = processor;
            if (!Validator.validParameters(computerParameters))
            {
                return null;
            }


                using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {

                entities.computer_parameters.Add(computerParameters);

                Logger.logParameters(computerParameters, "Add", User);
                entities.SaveChanges();

            }

            return "{\"msg\":\"success\"}";
        }
    }
}