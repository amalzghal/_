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
        [Required(ErrorMessage = "الرمز إجباري")]
        [Display(Name = "الرمز")]
        public string CR_Mas_Sup_Car_Features_Code { get; set; }

        [Required(ErrorMessage = "عفوا إسم الميزة بالعربي إجباري")]
        [Display(Name = "الإسم بالعربي")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "must be between 4 and 50")]
        public string CR_Mas_Sup_Car_Features_Ar { get; set; }

        [Required(ErrorMessage = " عفواالإسم الميزة بالإنجليزي إجباري")]
        [Display(Name = "الإسم بالإنجليزي")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "must be between 4 and 50")]
        public string CR_Mas_Sup_Car_Features_En { get; set; }

        [Required(ErrorMessage = "الإسم الميزة بالفرنسي إجباري")]
        [Display(Name = "الإسم بالفرنسي")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "must be between 4 and 50")]
        public string CR_Mas_Sup_Car_Features_Fr { get; set; }

        [Display(Name = "الحالة")]
        [StringLength(1)]
        public string CR_Mas_Sup_Car_Features_Status { get; set; }

        [Display(Name = "السبب")]
        [StringLength(100)]
        public string CR_Mas_Sup_Car_Features_Reasons { get; set; }
    }
}