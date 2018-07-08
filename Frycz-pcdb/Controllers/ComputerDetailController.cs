using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frycz_pcdb.Controllers
{
    public class ComputerDetailController : Controller
    {
        // GET: ComputerDetail
        public ActionResult Index(computer computerIn)
        {

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                var computerdetail = from computer in entities.computers
                    where computer.idcomputer == computerIn.idcomputer
                    select computer;
                computerdetail.Include(c => c.user);
                computerdetail.Include(c => c.idcomputer_parameters);
                computerdetail.Include(c => c.computer_type);
                computer finish = computerdetail.FirstOrDefault();
                string s = finish.computer_type.name;
                string s1 = finish.user.firstname;
                string s2 = finish.user.lastname;
                return View("ComputerDetail", finish);
            }


        }
    }
}