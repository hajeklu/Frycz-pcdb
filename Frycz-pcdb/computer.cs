//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Frycz_pcdb
{
    using System;
    using System.Collections.Generic;
    
    public partial class computer
    {
        public int idcomputer { get; set; }
        public string name { get; set; }
        public string bpcs_sessions { get; set; }
        public string mac_address { get; set; }
        public string serial_number { get; set; }
        public string inventory_number { get; set; }
        public Nullable<System.DateTime> create_time { get; set; }
        public Nullable<System.DateTime> last_update_time { get; set; }
        public string comment { get; set; }
        public Nullable<System.DateTime> guarantee { get; set; }
        public Nullable<int> idos { get; set; }
        public Nullable<int> idcomputer_brand { get; set; }
        public Nullable<int> idcomputer_type { get; set; }
        public Nullable<int> idcomputer_parameters { get; set; }
        public Nullable<int> iddiscarded_info { get; set; }
        public Nullable<int> iduser { get; set; }
    
        public virtual cumputer_brand cumputer_brand { get; set; }
        public virtual computer_parameters computer_parameters { get; set; }
        public virtual computer_type computer_type { get; set; }
        public virtual discarded_info discarded_info { get; set; }
        public virtual o o { get; set; }
        public virtual user user { get; set; }
    }
}
