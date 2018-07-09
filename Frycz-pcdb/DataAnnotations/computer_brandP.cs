using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Frycz_pcdb
{
    [MetadataType(typeof(computer_brand.MetaData))]
    public partial class computer_brand
    {

        sealed class MetaData
        {
            public int idcumputer_brand { get; set; }
            [Display(Name = "Maker: ")]
            public string maker { get; set; }
            [Display(Name = "Model: ")]
            public string model { get; set; }
        }
    }
}