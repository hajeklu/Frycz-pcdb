using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Frycz_pcdb
{
    [MetadataType(typeof(computer_parameters.MetaData))]
    public partial class computer_parameters
    {

        sealed class MetaData
        {
            public int idcomputer_parameters { get; set; }
            [Display(Name = "Processor: ")]
            public string processor { get; set; }
            [Display(Name = "RAM: ")]
            public Nullable<decimal> ram { get; set; }
            [Display(Name = "Hard Disk: ")]
            public Nullable<int> hdd { get; set; }
            [Display(Name = "Optical disc drive: ")]
            public Nullable<bool> optical_disc_drive { get; set; }
            [Display(Name = "External GPU: ")]
            public Nullable<bool> external_gpu { get; set; }
        }
    }
}