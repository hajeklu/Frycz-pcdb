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
    
    public partial class discarded_info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public discarded_info()
        {
            this.computers = new HashSet<computer>();
        }
    
        public int iddiscarded_info { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> iduser { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<computer> computers { get; set; }
        public virtual user user { get; set; }
    }
}
