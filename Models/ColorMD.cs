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
        [Required(ErrorMessage = "الرمز إجباري")]
        [StringLength(10, ErrorMessage = "must be 10 char")]
        public string CR_Mas_Sup_Color_Code { get; set; }

        [Display(Name = "المجموعة")]
        public string CR_Mas_Sup_Color_Group_Code { get; set; }

        [Required(ErrorMessage = "عفوا إسم اللون بالعربي إجباري")]
        [Display(Name = "الإسم بالعربي")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "must be between 4 and 50")]
        public string CR_Mas_Sup_Color_Ar_Name { get; set; }

        [Required(ErrorMessage = "عفوا إسم اللون بالإنجليزي إجباري")]
        [Display(Name = "الإسم بالإنجليزي")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "must be between 4 and 50")]
        public string CR_Mas_Sup_Color_En_Name { get; set; }

        [Required(ErrorMessage = "عفوا إسم اللون بالفرنسي إجباري")]
        [Display(Name = "الإسم بالفرنسي")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "must be between 4 and 50")]
        public string CR_Mas_Sup_Color_Fr_Name { get; set; }

        [Display(Name = "الحالة")]
        public string CR_Mas_Sup_Color_Status { get; set; }

        [Display(Name = "المرجع")]
        [StringLength(100)]
        public string CR_Mas_Sup_Color_Reasons { get; set; }

        public virtual CR_Mas_Sup_Group CR_Mas_Sup_Group { get; set; }
    }
}