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
    
    public partial class CR_Mas_Sup_Car_Model_Category
    {
        public string CR_Mas_Sup_Car_Model_Category_Code { get; set; }
        public int CR_Mas_Sup_Car_Model_Category_Year { get; set; }
        public string CR_Mas_Sup_Car_Category_Code { get; set; }
        public Nullable<int> CR_Mas_Sup_Car_Model_Category_Door_No { get; set; }
        public Nullable<int> CR_Mas_Sup_Car_Model_Category_Bag_Bags { get; set; }
        public Nullable<int> CR_Mas_Sup_Car_Model_Category_Small_Bags { get; set; }
        public Nullable<int> CR_Mas_Sup_Car_Model_Category_Passengers_No { get; set; }
        public Nullable<int> CR_Mas_Sup_Car_Model_Category_Weight { get; set; }
        public Nullable<int> CR_Mas_Sup_Car_Model_Category_Clinder { get; set; }
        public Nullable<int> CR_Mas_Sup_Car_Model_Category_Hourses { get; set; }
        public Nullable<int> CR_Mas_Sup_Car_Model_Category_Payload { get; set; }
        public string CR_Mas_Sup_Car_Model_Category_Picture { get; set; }
        public string CR_Mas_Sup_Car_Model_Category_Status { get; set; }
        public string CR_Mas_Sup_Car_Model_Category_Reasons { get; set; }
    
        public virtual CR_Mas_Sup_Category_Car CR_Mas_Sup_Category_Car { get; set; }
        public virtual CR_Mas_Sup_Model CR_Mas_Sup_Model { get; set; }
    }
}
