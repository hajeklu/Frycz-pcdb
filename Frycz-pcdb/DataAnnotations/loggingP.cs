using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Frycz_pcdb
{
    [MetadataType(typeof(logging.MetaData))]
    public partial class logging
    {

        sealed class MetaData
        {

            public int idlogging { get; set; }
            [Display(Name = "Info: ")]
            public string info { get; set; }
            [Display(Name = "Time: ")]
            public Nullable<System.DateTime> time { get; set; }
            public Nullable<int> idregistered_user { get; set; }

        }

    }
}