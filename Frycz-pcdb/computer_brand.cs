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
    
    public partial class computer_brand
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public computer_brand()
        {
            this.computers = new HashSet<computer>();
        }
    
        public int idcumputer_brand { get; set; }
        public string maker { get; set; }
        public string model { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<computer> computers { get; set; }
    }
}
