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
                computerdetail.Include(c => c.user.lastname);
                computerdetail.Include(c => c.user.firstname);
                computerdetail.Include(c => c.idcomputer_parameters);
                computerdetail.Include(c => c.computer_type);
                computerdetail.Include(c => c.computer_brand);
                computerdetail.Include(c => c.o.name);
                computerdetail.Include(c => c.o.version);
                computer finish = computerdetail.FirstOrDefault();
                string s = finish.computer_type.name;
                string s1 = finish.user.firstname;
                string s2 = finish.user.lastname;
                string s3 = finish.computer_brand.maker;
                string s4 = finish.computer_brand.model;
                string s5 = finish.computer_parameters.processor;
                string s6 = finish.o.name;
                Nullable<int> s7 = finish.o.version;
                Nullable<int> s8 = finish.computer_parameters.hdd;
                Nullable<decimal> s9 = finish.computer_parameters.ram;
                return View("ComputerDetail", finish);
            }


        }
    }
}