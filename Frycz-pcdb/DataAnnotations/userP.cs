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
            [StringLength(int.MaxValue, MinimumLength = 3)]
            public string firstname { get; set; }
            [Display(Name = "Last name: ")]
            [StringLength(int.MaxValue, MinimumLength = 3)]
            public string lastname { get; set; }
            public Nullable<int> iddepartment { get; set; }


        }
    }

    public partial class user
    {
        private string _fullname;

        [Display(Name = "User fullname: ")]
        [NotMapped]
        public string fullname
        {
            get
            {
                return
                    (lastname + " " + firstname);
            }
            set { _fullname = value; }
        }
    }
}