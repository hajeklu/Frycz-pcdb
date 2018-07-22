using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Frycz_pcdb.Models;

namespace Frycz_pcdb.Controllers
{
    public class ComputerTypeController : Controller
    {
        // GET: ComputerType
        public ActionResult Index()
        {
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return View("AllComputerType", entities.computer_type.ToList());
            }

        }

        public ActionResult EditComputerType(computer_type computerType)
        {
            return View("EditComputerType", computerType);
        }

        public ActionResult EditSave(computer_type computerType)
        {
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {

                if (!Validator.validType(computerType))
                {
                    ModelState.AddModelError("exist", "Computer type is invalid or in use.");
                    return View("EditComputerType", computerType);
                }

                computer_type type =
                    entities.computer_type.FirstOrDefault(t => t.idcomputer_type == computerType.idcomputer_type);

                type.name = computerType.name;
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerType");
            }
        }

        public ActionResult DeleteType(computer_type computerType)
        {
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                computer_type type =
                    entities.computer_type.FirstOrDefault(t => t.idcomputer_type == computerType.idcomputer_type);

                entities.computer_type.Remove(type);
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerType");
            }
        }

        public ActionResult AddComputerType(computer computer)
        {
            return View("AddComputerType");
        }

        public ActionResult SaveAdd(computer_type computerType)
        {


            if (!Validator.validType(computerType))
            {
                ModelState.AddModelError("exist", "Computer type is invalid or in use.");
                return View("AddComputerType", computerType);
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.computer_type.Add(computerType);
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerType");

            }
        }

        public string addJax(string name)
        {
            computer_type computerType = new computer_type();

            computerType.name = name;

            if (!Validator.validType(computerType))
            {
                ModelState.AddModelError("exist", "Computer type is invalid or in use.");
                return null;
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.computer_type.Add(computerType);
                entities.SaveChanges();

            }

            return "{\"msg\":\"success\"}";
        }
    }
}