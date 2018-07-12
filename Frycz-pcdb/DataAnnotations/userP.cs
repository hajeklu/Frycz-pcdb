using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Frycz_pcdb
{
    [MetadataType(typeof(user.MetaData))]
    public partial class user
    {
        sealed class MetaData
        {
            public int iduser { get; set; }
            [Display(Name = "First name: ")]
            public string firstname { get; set; }
            [Display(Name = "Last name: ")]
            public string lastname { get; set; }
            public Nullable<int> iddepartment { get; set; }


        }
    }

    public partial class user
    {
        [Display(Name = "User fullname: ")]
        [NotMapped]
        public string fullname
        {
            get
            {
                return
                    (lastname + " " + firstname);
            }
            set { fullname = value; }
        }
    }
}