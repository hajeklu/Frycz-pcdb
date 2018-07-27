using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Web;

namespace Frycz_pcdb.Models
{
    public static class Validator
    {
        public static bool validComputerName(string name)
        {
            if ((name == null) || name.Equals("") || name.Length < 3) 
            {
                return false;
            }
            name = name.ToUpper();
            return true;
        }

        public static bool checkExistComputer(computer computer)
        {
            return checkExistComputer(computer.name);
        }

        public static bool checkExistComputer(string computer)
        {
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                return checkExistComputer(computer, entities);
            }

        }

        public static bool checkExistComputer(string computer, frycz_pcdbEntities entities)
        {
            int count = entities.computers.Count(e => e.name.Equals(computer));

            return count > 0;
        }

        public static bool checkExistUser(user user)
        {
            return checkExistUser(user.firstname, user.lastname);
        }

        public static bool checkExistUser(string firstname, string lastname)
        {
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                int count = entities.users.Count(e => e.lastname.Equals(lastname) && e.firstname.Equals(firstname));
                return count > 0;
            }
        }

        public static bool validUser(user userIn)
        {
            if (userIn == null)
                return false;
            return validUser(userIn.firstname, userIn.lastname);
        }

        public static bool validUser(string firstname, string lastname)
        {
            if (firstname == null || lastname == null)
                return false;
            if (firstname.Contains(" ") || lastname.Contains(" "))
                return false;
            return lastname.Length >= 3 && firstname.Length >= 3;
        }

        public static user findUser(String userInput)
        {
            // validation user name
            if (userInput == null || userInput.Equals(String.Empty) || userInput.Length < 3)
            {
                throw new Exception("User not found");
            }
            string[] userStrig = userInput.Split(' ');

            if (userStrig.Length < 2)
            {
                throw new Exception("User not found");
            }


                string lastname = userStrig[0];
                string firstname = userStrig[1];


                using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
                {

                    entities.Configuration.LazyLoadingEnabled = false;
                    user user = entities.users.FirstOrDefault(e =>
                        e.lastname.Equals(lastname) && e.firstname.Equals(firstname));

                    // if user not found return view back
                    if (user != null)
                    {
                        return user;
                    }
                    else
                    {
                        throw new Exception("User not found");
                    }
                }
            }


        public static bool validType(computer_type computerType)
        {

            if (computerType == null || computerType.name == null || computerType.name.Equals(String.Empty))
            {
                return false;
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                int count = entities.computer_type.Count(t => t.name.Equals(computerType.name));
                return count <= 0;
            }
        }

        public static bool validModel(computer_brand brand)
        {
            if (brand == null)
                return false;
            if (brand.model == null || brand.model.Equals(String.Empty) || brand.maker == null ||
                brand.maker.Equals(String.Empty))
            {
                return false;
            }
            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                int count = entities.computer_brand.Count(t => t.maker.Equals(brand.maker) && t.model.Equals(brand.model));

                return count <= 0;
            }
        }

        public static bool validOS(o oIn)
        {
            if (oIn == null)
                return false;
            if (oIn.name == null || oIn.name.Equals(String.Empty) || oIn.version == null ||
                oIn.version.Equals(String.Empty))
                return false;

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                int count = entities.os.Count(t => t.name.Equals(oIn.name) && t.version == oIn.version);
                return count <= 0;

            }
        }

        public static bool validParameters(computer_parameters parameters)
        {
            if (parameters == null)
            {
                return false;
            }

            using (frycz_pcdbEntities entities = new frycz_pcdbEntities())
            {
                int count = entities.computer_parameters.Count(e =>
                    e.hdd == parameters.hdd && e.processor.Equals(parameters.processor) && e.ram == parameters.ram);
                return count <= 0;
            }
        }
    }
}