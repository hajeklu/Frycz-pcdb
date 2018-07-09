using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Frycz_pcdb
{
    [MetadataType(typeof(o.MetaData))]
    public partial class o
    {
        sealed class MetaData
        {
            [Display(Name = "Operation system: ")]
            public string name { get; set; }
            [Display(Name = "Architecture(x64/x86): ")]
            public Nullable<int> version { get; set; }
        }
    }
}