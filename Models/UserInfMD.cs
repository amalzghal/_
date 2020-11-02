using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace RentCar.Models
{
    [MetadataType(typeof(UserInfMD))]
    public partial class CR_Mas_User_Information
    {
    }
    public class UserInfMD
    {
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [Display(Name = "الرمز")]
        public string CR_Mas_User_Information_Code { get; set; }

        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [Display(Name = "الرقم السري")]
        public string CR_Mas_User_Information_PassWord { get; set; }

        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [Display(Name = "الإسم")]
        public string CR_Mas_User_Information_Ar_Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [Display(Name = "Name")]
        public string CR_Mas_User_Information_En_Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [Display(Name = "Nom")]
        public string CR_Mas_User_Information_Fr_Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [Display(Name = "رقم الهاتف")]
        public string CR_Mas_User_Information_Mobile_No { get; set; }

        [Display(Name = "الحالة")]
        public string CR_Mas_User_Information_Status { get; set; }
        [Display(Name = "المرجع")]
        public string CR_Mas_User_Information_Reasons { get; set; }
    }
}