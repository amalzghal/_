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
    
    public partial class CR_Mas_Sup_Category_Car
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CR_Mas_Sup_Category_Car()
        {
            this.CR_Mas_Sup_Car_Model_Category = new HashSet<CR_Mas_Sup_Car_Model_Category>();
        }
    
        public string CR_Mas_Sup_Category_Car_Code { get; set; }
        public string CR_Mas_Sup_Category_Car_Group_Code { get; set; }
        public string CR_Mas_Sup_Category_Car_Ar_Name { get; set; }
        public string CR_Mas_Sup_Category_Car_En_Name { get; set; }
        public string CR_Mas_Sup_Category_Car_Fr_Name { get; set; }
        public string CR_Mas_Sup_Category_Car_Status { get; set; }
        public string CR_Mas_Sup_Category_Car_Reasons { get; set; }
    
        public virtual CR_Mas_Sup_Group CR_Mas_Sup_Group { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CR_Mas_Sup_Car_Model_Category> CR_Mas_Sup_Car_Model_Category { get; set; }
    }
}
