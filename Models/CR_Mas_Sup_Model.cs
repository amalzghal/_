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
    
    public partial class CR_Mas_Sup_Model
    {
        public string CR_Mas_Sup_Model_Code { get; set; }
        public string CR_Mas_Sup_Model_Group_Code { get; set; }
        public string CR_Mas_Sup_Model_Brand_Code { get; set; }
        public string CR_Mas_Sup_Model_Ar_Name { get; set; }
        public string CR_Mas_Sup_Model_En_Name { get; set; }
        public string CR_Mas_Sup_Model_Fr_Name { get; set; }
        public Nullable<int> CR_Mas_Sup_Model_Counter { get; set; }
        public string CR_Mas_Sup_Model_Status { get; set; }
        public string CR_Mas_Sup_Model_Reasons { get; set; }
    
        public virtual CR_Mas_Sup_Brand CR_Mas_Sup_Brand { get; set; }
        public virtual CR_Mas_Sup_Group CR_Mas_Sup_Group { get; set; }
    }
}