using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
        [Display(Name = "الإسم بالعربي")]
        public string CR_Mas_Sup_Car_Features_Ar { get; set; }
        [Display(Name = "الإسم بالإنجليزي")]
        public string CR_Mas_Sup_Car_Features_En { get; set; }
        [Display(Name = "الإسم بالفرنسي")]
        public string CR_Mas_Sup_Car_Features_Fr { get; set; }
        [Display(Name = "الحالة")]
        public string CR_Mas_Sup_Car_Features_Status { get; set; }
        [Display(Name = "السبب")]
        public string CR_Mas_Sup_Car_Features_Reasons { get; set; }
    }
}