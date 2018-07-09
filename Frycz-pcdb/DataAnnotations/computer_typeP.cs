using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Frycz_pcdb
{
    [MetadataType(typeof(computer_type.MetaData))]
    public partial class computer_type
    {
        sealed class MetaData
        {
            public int idcomputer_type { get; set; }
            [Display(Name = "Type of computer: ")]
            public string name { get; set; }
        }
    }
}