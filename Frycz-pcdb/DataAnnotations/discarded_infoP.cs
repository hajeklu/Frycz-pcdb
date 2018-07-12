using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Frycz_pcdb
{
    [MetadataType(typeof(discarded_info.MetaData))]
    public partial class discarded_info
    {
        sealed class MetaData
        {
            
            public int iddiscarded_info { get; set; }
            [Display(Name = "Date of discarded: ")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yyyy HH:mm}")]
            public Nullable<System.DateTime> date { get; set; }
        }
    }
}