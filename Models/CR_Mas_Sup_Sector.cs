//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentCar.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CR_Mas_Sup_Sector
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CR_Mas_Sup_Sector()
        {
            this.CR_Mas_Sup_Employer = new HashSet<CR_Mas_Sup_Employer>();
        }
    
        public string CR_Mas_Sup_Sector_Code { get; set; }
        public string CR_Mas_Sup_Sector_Ar_Name { get; set; }
        public string CR_Mas_Sup_Sector_En_Name { get; set; }
        public string CR_Mas_Sup_Sector_Fr_Name { get; set; }
        public string CR_Mas_Sup_Sector_Status { get; set; }
        public string CR_Mas_Sup_Sector_Reasons { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CR_Mas_Sup_Employer> CR_Mas_Sup_Employer { get; set; }
    }
}
