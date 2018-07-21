using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Frycz_pcdb.Models;
using Microsoft.Ajax.Utilities;

namespace Frycz_pcdb.Controllers
{
    public class AddComputerController : Controller
    {
        // GET: AddComputer
        [Authorize]
        public ActionResult Index()
        {
            return View("CountOfAdd");

        }

        [Authorize]
        public ActionResult AddForm(string firstname, int numberofadd)
        {
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                ViewBag.guar = DateTime.Now.AddYears(2).ToString("dd/MM/yyyy");
                ViewBag.first = firstname.ToUpper();
                ViewBag.numberofadd = numberofadd;
                makeViewBag(entities, new computer());
                return View("AddComputer");

            }
        }

        [Authorize]
        public ActionResult save(FormCollection formCollection, int count, computer computerIn, string userInput, Nullable<int> idcomputer_parameters,
            Nullable<int> idcomputerBrand, Nullable<int> idcomputerType, string guar)
        {
            //check if all input computer`s names are not same.
            List<computer> computers = new List<computer>();

            for (int i = 1; i <= count; i++)
            {
                computer comp = new computer();
                string pcname = formCollection[i.ToString()];
                comp.name = pcname;

                foreach (computer computer in computers)
                {
                    if (computer.name.Equals(pcname))
                    {
                        ModelState.AddModelError("eqcomp", "Computer's names can not be same.");
                        return returnValidationMessage(computerIn, count, formCollection);
                    }
                }
                computers.Add(comp);
            }


            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {

                for (int i = 1; i <= count; i++)
                {
                    computer comp = new computer();
                    string pcname = formCollection[i.ToString()];
                    if (!Validator.validComputerName(pcname))
                    {
                        ModelState.AddModelError("eqcomp", "Some computer name is not valid");
                        return returnValidationMessage(computerIn, count, formCollection);
                    }

                    if (Validator.checkExistComputer(pcname, entities))
                    {
                        ModelState.AddModelError("eqcomp", "Some computer name is already exist.");
                        return returnValidationMessage(computerIn, count, formCollection);
                    }

                    try
                    {
                        Frycz_pcdb.user user = Validator.findUser(userInput);
                        comp.iduser = user.iduser;
                    }
                    catch (Exception e)
                    {

                        ModelState.AddModelError("userNotFound", "User not found. ");
                        return returnValidationMessage(computerIn, count, formCollection);
                    }



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
                    comp.name = pcname.ToUpper();
                    comp.guarantee = computerIn.guarantee;
                    comp.create_time = DateTime.Now;
                    comp.last_update_time = DateTime.Now;

                    entities.computers.Add(comp);

                }
                entities.SaveChanges();
            }

            return RedirectToAction("Index","AllComputer");
        }

        private ActionResult returnValidationMessage(computer computer, int count, FormCollection formCollection)
        {
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                ViewBag.guar = DateTime.Now.AddYears(2).ToString("dd/MM/yyyy");
                ViewBag.numberofadd = count;
                ViewBag.first = formCollection["1"].ToUpper();
                makeViewBag(entities, new computer());
                return View("AddComputer", computer);
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
                List<Frycz_pcdb.user> usereList = entities.users.Where(x => x.lastname.Contains(search) || x.firstname.Contains(search)).ToList();

                foreach (Frycz_pcdb.user user in usereList)
                {

                    result.Add(user.lastname + " " + user.firstname);
                }


            }
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public String ajaxUser(string name)
        {
            Frycz_pcdb.user user = UserManager.tryAddUser(name);

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
    }
}