using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frycz_pcdb.Models
{
    public static class UserManager
    {

        public static user tryCreateUser(string name)
        {
            if (name == null)
                return null;
            if (name.Equals(""))
                return null;
            if (name.Length < 6)
                return null;

            
            string[] userStrig = name.Split(' ');

            if (userStrig.Length < 2)
            {
                return null;
            }

            string lastname = userStrig[0];
            string firstname = userStrig[1];

            if (lastname == null || firstname == null)
                return null;
            if (lastname.Length < 3 || firstname.Length < 3)
                return null;
            
            user user = new user();
            user.lastname = lastname;
            user.firstname = firstname;
            return user;
        }

    }
}