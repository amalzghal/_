//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentCar.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CR_Mas_Sys_System_Name
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CR_Mas_Sys_System_Name()
        {
            this.CR_Mas_Sys_Tasks = new HashSet<CR_Mas_Sys_Tasks>();
        }
    
        public string CR_Mas_Sys_System_Code { get; set; }
        public string CR_Mas_Sys_System_Ar_Name { get; set; }
        public string CR_Mas_Sys_System_En_Name { get; set; }
        public string CR_Mas_Sys_System_Fn_Name { get; set; }
        public string CR_Mas_Sys_System_Status { get; set; }
        public string CR_Mas_Sys_System_Reasons { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CR_Mas_Sys_Tasks> CR_Mas_Sys_Tasks { get; set; }
    }
}
