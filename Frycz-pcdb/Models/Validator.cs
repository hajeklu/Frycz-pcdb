using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frycz_pcdb.Models
{
    public class Validator
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
    }
}