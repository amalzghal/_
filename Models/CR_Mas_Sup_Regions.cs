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
    
    public partial class CR_Mas_Sup_Regions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CR_Mas_Sup_Regions()
        {
            this.CR_Mas_Sup_City = new HashSet<CR_Mas_Sup_City>();
        }
    
        public string CR_Mas_Sup_Regions_Code { get; set; }
        public string CR_Mas_Sup_Regions_Ar_Name { get; set; }
        public string CR_Mas_Sup_Regions_En_Name { get; set; }
        public string CR_Mas_Sup_Regions_Fr_Name { get; set; }
        public string CR_Mas_Sup_Regions_Status { get; set; }
        public string CR_Mas_Sup_Regions_Reasons { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CR_Mas_Sup_City> CR_Mas_Sup_City { get; set; }
    }
}
