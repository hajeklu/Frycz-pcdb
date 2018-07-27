using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Frycz_pcdb
{
    [MetadataType(typeof(registered_user.MetaData))]
    public partial class registered_user
    {

        sealed class MetaData
        {
            public int idregistered_user { get; set; }
            [Display(Name = "User: ")]
            public string login { get; set; }
            [Display(Name = "Password: ")]
            public string password { get; set; }
            [Display(Name = "Last login: ")]
            public Nullable<System.DateTime> last_login { get; set; }
        }

    }
}