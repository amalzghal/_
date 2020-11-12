using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using RentCar.Controllers;
using Antlr.Runtime;

namespace RentCar.Models
{
    [MetadataType(typeof(ColorMetaData))]
    public partial class CR_Mas_Sup_Color
    {
    }
    public class ColorMetaData
    {
        [Display(Name = "الرمز")]
        public string CR_Mas_Sup_Color_Code { get; set; }
        [Display(Name = "المجموعة")]
        public string CR_Mas_Sup_Color_Group_Code { get; set; }
        [Display(Name = "الإسم بالعربي")]
        public string CR_Mas_Sup_Color_Ar_Name { get; set; }
        [Display(Name = "الإسم بالإنجليزي")]
        public string CR_Mas_Sup_Color_En_Name { get; set; }
        [Display(Name = "الإسم بالفرنسي")]
        public string CR_Mas_Sup_Color_Fr_Name { get; set; }
        [Display(Name = "الحالة")]
        public string CR_Mas_Sup_Color_Status { get; set; }
        [Display(Name = "المرجع")]
        public string CR_Mas_Sup_Color_Reasons { get; set; }
        public virtual CR_Mas_Sup_Group CR_Mas_Sup_Group { get; set; }
    }
}