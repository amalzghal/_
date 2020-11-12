using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RentCar.Models
{
    [MetadataType(typeof(BrandMetaData))]
    public partial class CR_Mas_Sup_Brand
    {
    }
    public class BrandMetaData
    {
        [Display(Name ="الرمز")]
        public string CR_Mas_Sup_Brand_Code { get; set; }
        [Display(Name= "الإسم بالعربي")]
        public string CR_Mas_Sup_Brand_Ar_Name { get; set; }
        [Display(Name= "الإسم بالإنجليزي")]
        public string CR_Mas_Sup_Brand_En_Name { get; set; }
        [Display(Name= "الإسم بالفرنسي")]
        public string CR_Mas_Sup_Brand_Fr_Name { get; set; }  
        [Display(Name="الحالة")]
        public string CR_Mas_Sup_Brand_Status { get; set; }
        [Display(Name="المرجع")]
        public string CR_Mas_Sup_Brand_Reasons { get; set; }
    }
}