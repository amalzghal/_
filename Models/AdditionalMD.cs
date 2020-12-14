using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RentCar.Models
{
    [MetadataType(typeof(AdditionalMD))]
    public partial class CR_Mas_Sup_Additional
    {
    }

    public class AdditionalMD
    {
        [Display(Name = "الرمز")]
        public string CR_Mas_Sup_Additional_Code { get; set; }
        public string CR_Mas_Sup_Additional_Group_Code { get; set; }
        [Display(Name = "الإسم بالعربي")]
        public string CR_Mas_Sup_Additional_Ar_Name { get; set; }
        [Display(Name = "الإسم بالإنجليزي")]
        public string CR_Mas_Sup_Additional_En_Name { get; set; }
        [Display(Name = "الإسم بالفرنسي")]
        public string CR_Mas_Sup_Additional_Fr_Name { get; set; }
        [Display(Name = "الحالة")]
        public string CR_Mas_Sup_Additional_Status { get; set; }
        [Display(Name = "المرجع")]
        public string CR_Mas_Sup_Additional_Reasons { get; set; }
        [Display(Name = "المجموعة")]
        public virtual CR_Mas_Sup_Group CR_Mas_Sup_Group { get; set; }
    }
}