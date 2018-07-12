using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Frycz_pcdb.Controllers;

namespace Frycz_pcdb
{
    [MetadataType(typeof(computer.MetaData))]
    public partial class computer
    {
        sealed class MetaData
        {
            public int idcomputer { get; set; }

            [Required(ErrorMessage = "Computer name is required")]
            [Display(Name = "Computer name: ")]
            public string name { get; set; }

            [Display(Name = "BPCS Sessions: ")]
            public string bpcs_sessions { get; set; }

            [Display(Name = "MAC Address: ")]
            public string mac_address { get; set; }

            [Display(Name = "Serial number: ")]
            public string serial_number { get; set; }

            [Display(Name = "Inventory number: ")]
            public string inventory_number { get; set; }

            [Display(Name = "Creation time: ")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yyyy HH:mm}")]
            public Nullable<System.DateTime> create_time { get; set; }

            [Display(Name = "Last update time: ")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yyyy HH:mm}")]
            public Nullable<System.DateTime> last_update_time { get; set; }

            [Display(Name = "Comment: ")]
            public string comment { get; set; }

            [Display(Name = "Guarantee to: ")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yyyy HH:mm}")]
            public Nullable<System.DateTime> guarantee { get; set; }

            public Nullable<int> idos { get; set; }
            public Nullable<int> idcomputer_brand { get; set; }
            public Nullable<int> idcomputer_type { get; set; }
            public Nullable<int> idcomputer_parameters { get; set; }
            public Nullable<int> iddiscarded_info { get; set; }
            public Nullable<int> iduser { get; set; }
        }
    }
}