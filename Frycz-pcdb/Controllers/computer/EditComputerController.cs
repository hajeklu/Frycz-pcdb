using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Frycz_pcdb.Models;
using Google.Protobuf.WellKnownTypes;

namespace Frycz_pcdb.Controllers
{
    public class EditComputerController : Controller
    {
        [Authorize]
        public ActionResult Index(computer computer)
        {

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                var comp = entities.computers.Where(c => c.idcomputer == computer.idcomputer).Include(c => c.user)
                    .Include(c => c.computer_parameters).Include(c => c.o)
                    .Include(c => c.computer_type).Include(c => c.computer_brand).FirstOrDefault();

                // for dropdown list
                makeViewBag(entities, comp);

                return View("EditComputer", comp);
            }
        }

        private void makeViewBag(frycz_pcdbEntities entities, computer comp)
        {

            // add type list for drop down list
            var listTypePc = new List<SelectListItem>();
            foreach (var pctype in entities.computer_type.ToList())
            {
                var item = new SelectListItem()
                {
                    Text = pctype.name,
                    Value = pctype.idcomputer_type.ToString()
                };
                if (comp.computer_type != null)
                {
                    if (pctype.idcomputer_type == comp.idcomputer_type)
                        item.Selected = true;
                }

                listTypePc.Add(item);
            }

            ViewBag.type = listTypePc;



            // add list os for dropdown list
            var listOs = new List<SelectListItem>();
            foreach (var os in entities.os.ToList())
            {
                var item = new SelectListItem()
                {
                    Text = os.name + " x" + os.version,
                    Value = os.idos.ToString()
                };
                if (comp.idos == os.idos)
                    item.Selected = true;
                listOs.Add(item);
            }

            ViewBag.os = listOs;



            var listmodel = new List<SelectListItem>();
            var modelEmpty = new SelectListItem()
            {
                Text = null,
                Value = null
            };

            listmodel.Add(modelEmpty);

            foreach (var model in entities.computer_brand.ToList())
            {
                var item = new SelectListItem()
                {
                    Text = model.model + " " + model.maker,
                    Value = model.idcumputer_brand.ToString()
                };
                if (model.idcumputer_brand == comp.idcomputer_brand)
                    item.Selected = true;
                listmodel.Add(item);
            }

            ViewBag.model = listmodel;


            var listparam = new List<SelectListItem>();
            var Emptyitem = new SelectListItem()
            {
                Text = null,
                Value = null
            };
            listparam.Add(Emptyitem);



            foreach (var param in entities.computer_parameters.ToList())
            {
                var item = new SelectListItem()
                {
                    Text = param.processor + " " + param.ram + " GB " + param.hdd + " GB",
                    Value = param.idcomputer_parameters.ToString()
                };
                if (param.idcomputer_parameters == comp.idcomputer_parameters)
                    item.Selected = true;
                listparam.Add(item);
            }

            ViewBag.parameters = listparam;


            if (comp.discardedDate == null)
            {
                ViewBag.discarded = "In use";
            }
            else
            {
                ViewBag.discarded = comp.discardedDate;
            }
        }

        public JsonResult GetSearchValueUser(string search)
        {
            List<string> result = new List<string>();
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                List<Frycz_pcdb.user> usereList = entities.users
                    .Where(x => x.lastname.Contains(search) || x.firstname.Contains(search)).ToList();

                foreach (Frycz_pcdb.user user in usereList)
                {

                    result.Add(user.lastname + " " + user.firstname);
                }


            }

            return new JsonResult {Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }


        public String ajaxUser(string name)
        {
            Frycz_pcdb.user user = UserManager.tryCreateUser(name);

            if (user == null)
                return null;
            if (Validator.checkExistUser(user))
            {
                return null;
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                entities.users.Add(user);
                entities.SaveChanges();
            }

            return "{\"msg\":\"success\"}";
        }



        [Authorize]
        public ActionResult Save(computer computerIn, string userInput, Nullable<int> idcomputer_parameters,
            Nullable<int> idcomputerBrand, Nullable<int> idcomputerType)
        {
            string userErrorMessage = "User name not found.";
            string computerErrorMessage = "Computer name is invalid or in use.";

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {

                if (!Validator.validComputerName(computerIn.name))
                {
                    ModelState.AddModelError("computerName", computerErrorMessage);

                    entities.Configuration.LazyLoadingEnabled = false;
                    var co = entities.computers.Where(c => c.idcomputer == computerIn.idcomputer)
                        .Include(c => c.user).Include(c => c.computer_parameters).Include(c => c.o)
                        .Include(c => c.computer_type).Include(c => c.computer_brand)
                        .FirstOrDefault();
                    makeViewBag(entities, co);
                    return View("EditComputer", co);


                }

                entities.Configuration.LazyLoadingEnabled = false;
                computer comp = entities.computers.FirstOrDefault(c => c.idcomputer == computerIn.idcomputer);
                
                try
                {
                    Frycz_pcdb.user u = Validator.findUser(userInput);
                    comp.iduser = u.iduser;

                }
                catch (Exception e)
                {
                    var co = entities.computers.Where(c => c.idcomputer == computerIn.idcomputer)
                        .Include(c => c.user).Include(c => c.computer_parameters).Include(c => c.o)
                        .Include(c => c.computer_type).Include(c => c.computer_brand)
                        .FirstOrDefault();
                    makeViewBag(entities, co);
                    ModelState.AddModelError("userNotFound", userErrorMessage);
                    return View("EditComputer", co);
                }

                // update computers
                comp.bpcs_sessions = computerIn.bpcs_sessions;
                comp.mac_address = computerIn.mac_address;
                comp.serial_number = computerIn.serial_number;
                comp.idos = computerIn.idos;
                comp.comment = computerIn.comment;
                comp.inventory_number = computerIn.inventory_number;
                if (idcomputer_parameters == null)
                {
                    comp.idcomputer_parameters = null;
                }
                else
                {
                    comp.computer_parameters =
                        entities.computer_parameters.FirstOrDefault(e =>
                            e.idcomputer_parameters == idcomputer_parameters);
                }

                if (idcomputerBrand == null)
                {
                    comp.idcomputer_brand = null;
                }
                else
                {
                    comp.computer_brand =
                        entities.computer_brand.FirstOrDefault(e => e.idcumputer_brand == idcomputerBrand);
                }

                comp.idcomputer_type = idcomputerType;
                comp.name = computerIn.name.ToUpper();
                comp.guarantee = computerIn.guarantee;
                comp.last_update_time = DateTime.Now;
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerDetail", comp);
            }
        }
    }
}