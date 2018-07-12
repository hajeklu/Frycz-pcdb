using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
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
                var comps = entities.computers.Where(c => c.idcomputer == computer.idcomputer).Include(c => c.user).Include(c => c.computer_parameters).Include(c => c.o)
                    .Include(c => c.computer_type).Include(c => c.computer_brand).Include(c => c.discarded_info).FirstOrDefault();


                var listTypePc = new List<SelectListItem>();
                foreach (var pctype in entities.computer_type.ToList())
                {
                    var item = new SelectListItem()
                    {
                        Text = pctype.name,
                        Value = pctype.idcomputer_type.ToString()
                    };
                    if (comps.computer_type != null)
                    {
                        if (pctype.idcomputer_type == comps.idcomputer_type)
                            item.Selected = true;
                    }

                    listTypePc.Add(item);
                }
                ViewBag.type = listTypePc;

                var listOs = new List<SelectListItem>();
                foreach (var os in entities.os.ToList())
                {
                    var item = new SelectListItem()
                    {
                        Text = os.name + " x" + os.version,
                        Value = os.idos.ToString()
                    };
                    if (comps.idos == os.idos)
                        item.Selected = true;
                    listOs.Add(item);
                }
                ViewBag.os = listOs;

                var listmodel = new List<SelectListItem>();
                foreach (var model in entities.computer_brand.ToList())
                {
                    var item = new SelectListItem()
                    {
                        Text = model.model + " " + model.maker,
                        Value = model.idcumputer_brand.ToString()
                    };
                    if (model.idcumputer_brand == comps.idcomputer_brand)
                        item.Selected = true;
                    listmodel.Add(item);
                }
                ViewBag.model = listmodel;


                var listparam = new List<SelectListItem>();
                foreach (var param in entities.computer_parameters.ToList())
                {
                    var item = new SelectListItem()
                    {
                        Text = param.processor + " " + param.ram + " GB " + param.hdd + " GB",
                        Value = param.idcomputer_parameters.ToString()
                    };
                    if (param.idcomputer_parameters == comps.idcomputer_parameters)
                        item.Selected = true;
                    listparam.Add(item);
                }

                ViewBag.parameters = listparam;


                if (comps.iddiscarded_info == null)
                {
                    ViewBag.discarded = "In use";
                }
                else
                {
                    ViewBag.discarded = comps.discarded_info.date.ToString();

                    if (comps.discarded_info.iduser != null)
                    {
                        ViewBag.discarded = comps.discarded_info.date.ToString() + " - " + comps.discarded_info.user.fullname;
                    }
                }

                return View("EditComputer", comps);
            }
        }

        public JsonResult GetSearchValueUser(string search)
        {
            List<string> result = new List<string>();
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                List<user> usereList = entities.users.Where(x => x.lastname.Contains(search) || x.firstname.Contains(search)).ToList();

                foreach (user user in usereList)
                {
                    result.Add(user.lastname + " " + user.firstname);
                }

               
            }
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult Save(computer computerIn, String userInput)
        {
            if (userInput == null || userInput.Equals(String.Empty) || userInput.Length < 3)
            {
                ModelState.AddModelError("userNotFound", "Invalid user");
            }
            string[] userStrig = userInput.Split(' ');

            if (userStrig.Length < 2)
            {
                ModelState.AddModelError("userNotFound", "Invalid user");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "EditComputer", computerIn);
            }

            string lastname = userStrig[0];
            string firstname = userStrig[1];
             
            

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                user user = entities.users.FirstOrDefault(e => e.lastname.Equals(lastname) && e.firstname.Equals(firstname));

                computer comp = entities.computers.FirstOrDefault(c => c.idcomputer == computerIn.idcomputer);
                if (user != null)
                {
                    comp.user = user;
                }
                
                comp.idos = computerIn.idos;
                comp.comment = computerIn.comment;

                comp.last_update_time = DateTime.Now;
                entities.SaveChanges();
                return RedirectToAction("Index", "ComputerDetail", comp);
            }
            
        }

    }

}