using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace Frycz_pcdb.Controllers
{
    public class EditComputerController : Controller
    {
        [Authorize]
        public ActionResult Index(computer computer)
        {




            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {


                var listTypePc = new List<SelectListItem>();
                foreach (var pctype in entities.computer_type.ToList())
                {
                    var item = new SelectListItem()
                    {
                        Text = pctype.name,
                        Value = pctype.idcomputer_type.ToString()
                    };
                    if(pctype.name.Equals("Desktop"))
                        item.Selected = true;
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
                    if (os.name.Equals("Windows 10"))
                        item.Selected = true;
                    listOs.Add(item);
                }
                ViewBag.os = listOs;




                entities.Configuration.LazyLoadingEnabled = false;
                var comps = entities.computers.Where(c => c.idcomputer == computer.idcomputer).Include(c => c.user).Include(c => c.computer_parameters).Include(c => c.o)
                    .Include(c => c.computer_type).FirstOrDefault();
                return View("EditComputer", comps);
            }
        }
    }
}