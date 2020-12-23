﻿using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    [MetadataType(typeof(FeaturesMetaData))]
    public partial class CR_Mas_Sup_Car_Features
    {
    }
    public class FeaturesMetaData
    {
        [Display(Name = "الرمز")]
        public string CR_Mas_Sup_Car_Features_Code { get; set; }
        [Display(Name = "الميزة عربي")]
        public string CR_Mas_Sup_Car_Features_Ar_Name { get; set; }
        [Display(Name = "الميزة إنجليزي")]
        public string CR_Mas_Sup_Car_Features_En_Name { get; set; }
        [Display(Name = "الميزة فرنسي")]
        public string CR_Mas_Sup_Car_Features_Fr_Name { get; set; }
        [Display(Name = "الحالة")]
        public string CR_Mas_Sup_Car_Features_Status { get; set; }
        [Display(Name = "المرجع")]
        public string CR_Mas_Sup_Car_Features_Reasons { get; set; }
    }
}