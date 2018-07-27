using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Frycz_pcdb.Models
{
    public static class Logger
    {

        public static void logToDB(string message, IPrincipal user)
        {
            logToDB(message, user.Identity.Name);
        }

        public static void logToDB(string message, string userName)
        {
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                logToDB(message, entities, userName);
                entities.SaveChanges();
            }
        }

        public static void logToDB(string message, frycz_pcdbEntities entities, string userName)
        {
            logging logging = new logging();
            logging.info = message;
            logging.time = DateTime.Now;
            logging.registered_user = entities.registered_user.FirstOrDefault(u => u.login.Equals((userName)));
            entities.loggings.Add(logging);
        }

        public static void logParameters(computer_parameters parameters, string operationmessage, IPrincipal user)
        {
            logToDB(operationmessage + " parameters: " + " " + parameters.processor + " " + parameters.ram + "GB " + parameters.hdd + "GB ", user);
        }

        public static void logModel(computer_brand brand, string operationMessage, IPrincipal user)
        {
            logToDB(operationMessage + " computer model: " + brand.maker + " " + brand.model,user);
        }

        public static void logComputerType(computer_type computerType, string operationMessage, IPrincipal user)
        {
            logToDB(operationMessage + " computer type: " + computerType.name, user);
        }

        public static void logComputer(computer computer, string operationMessage, IPrincipal user)
        {
            logToDB(operationMessage + " computer: " + computer.name, user.Identity.Name);
        }

        public static void logComputer(computer computer, string operationMessage, IPrincipal user, frycz_pcdbEntities entities)
        {
            logToDB(operationMessage + " computer: " + computer.name, entities ,user.Identity.Name);
        }

        public static void logUser(user user, string operationMessage, IPrincipal loginuser)
        {
            logToDB(operationMessage + " user: " + user.firstname + " ",loginuser);
        }


    }
}