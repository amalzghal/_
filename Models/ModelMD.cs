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
    [MetadataType(typeof(ModelMetaData))]
    public partial class CR_Mas_Sup_Model
    {
        
    }
    public class ModelMetaData
    {
        [Display(Name ="الرمز")]
        [Required(ErrorMessage = "الرمز إجباري")]
        [StringLength(10, ErrorMessage = "must be 10 char")]
        public string CR_Mas_Sup_Model_Code { get; set; }

        [Display(Name = "المجموعة")]
        public string CR_Mas_Sup_Model_Group_Code { get; set; }

        [Required(ErrorMessage = "عفوا إسم الماركة إجباري")]
        [Display(Name = "الماركة")]
        public string CR_Mas_Sup_Model_Brand_Code { get; set; }

        [Required(ErrorMessage = "عفوا إسم الطراز بالعربي إجباري")]
        [Display(Name = "الإسم بالعربي")]
        public string CR_Mas_Sup_Model_Ar_Name { get; set; }

        [Required(ErrorMessage = "عفوا إسم الطراز بالإنجليزي إجباري")]
        [Display(Name = "الإسم بالإنجليزي")]
        public string CR_Mas_Sup_Model_En_Name { get; set; }

        [Required(ErrorMessage = "عفوا إسم الطراز بالفرنسي إجباري")]
        [Display(Name = "الإسم بالفرنسي")]
        public string CR_Mas_Sup_Model_Fr_Name { get; set; }

        [Display(Name = "رقم العداد")]
        public Nullable<int> CR_Mas_Sup_Model_Counter { get; set; }

        [Display(Name = "الحالة")]
        public string CR_Mas_Sup_Model_Status { get; set; }

        [Display(Name = "المرجع")]
        public string CR_Mas_Sup_Model_Reasons { get; set; }
        
        public virtual CR_Mas_Sup_Brand CR_Mas_Sup_Brand { get; set; }
        
        public virtual CR_Mas_Sup_Group CR_Mas_Sup_Group { get; set; }
    }
}