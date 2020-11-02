using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RentCar.Models
{
    [MetadataType(typeof(SpecificationsMetaData))]
    public partial class CR_Mas_Sup_Car_Specifications
    {
    }

    public class SpecificationsMetaData
    {
        [Required(ErrorMessage = "الرمز إجباري")]
        [Display(Name ="الرمز")]
        public string CR_Mas_Sup_Car_Specifications_Code { get; set; }
        [Required(ErrorMessage = "الإسم العربي إجباري")]
        [Display(Name="الإسم العربي")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="must be between 4 and 50")]
        public string CR_Mas_Sup_Car_Specifications_Ar { get; set; }
        [Required(ErrorMessage = "الإسم الإنجليزي إجباري")]
        [Display(Name="الإسم الإنجليزي")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "must be between 4 and 50")]
        public string CR_Mas_Sup_Car_Specifications_En { get; set; }
        [Required(ErrorMessage = "الإسم الفرنسي إجباري")]
        [Display(Name="الإسم الفرنسي")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "must be between 4 and 50")]
        public string CR_Mas_Sup_Car_Specifications_Fr { get; set; }
        
        [Display(Name="الحالة")]
        [StringLength(1)]
        public string CR_Mas_Sup_Car_Specifications_Status { get; set; }
        [Display(Name="السبب")]
        [StringLength(100)]
        public string CR_Mas_Sup_Car_Specifications_Reasons { get; set; }

    }
}