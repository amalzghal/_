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
        [Required(ErrorMessage = "الرمز إجباري")]
        [Display(Name ="الرمز")]
        public string CR_Mas_Sup_Brand_Code { get; set; }
        [Required(ErrorMessage = "عفوا إسم الماركة عربي إجباري")]
        [Display(Name="الإسم العربي")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="must be between 4 and 50")]
        public string CR_Mas_Sup_Brand_Ar_Name { get; set; }
        [Required(ErrorMessage = "عفوا إسم الماركة إنجليزي إجباري")]
        [Display(Name="الإسم الإنجليزي")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "must be between 4 and 50")]
        public string CR_Mas_Sup_Brand_En_Name { get; set; }
        [Required(ErrorMessage = "عفوا إسم الماركة فرنسي إجباري")]
        [Display(Name="الإسم الفرنسي")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "must be between 4 and 50")]
        public string CR_Mas_Sup_Brand_Fr_Name { get; set; }
        
        [Display(Name="الحالة")]
        [StringLength(1)]
        public string CR_Mas_Sup_Brand_Status { get; set; }
        [Display(Name="المرجع")]
        [StringLength(50)]
        public string CR_Mas_Sup_Brand_Reasons { get; set; }

    }
}