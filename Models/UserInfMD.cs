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
        [Required(ErrorMessage = "الرمز إجباري")]
        [Display(Name = "الرمز")]
        [StringLength(10, ErrorMessage = "must be 10 char")]
        public string CR_Mas_User_Information_Code { get; set; }

        [Required(ErrorMessage = "الرقم السري إجباري")]
        [Display(Name = "الرقم السري")]
        public string CR_Mas_User_Information_PassWord { get; set; }

        [Required(ErrorMessage = "الإسم المستخدم بالعربي إجباري")]
        [Display(Name = "الإسم بالعربي")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "must be between 4 and 50")]
        public string CR_Mas_User_Information_Ar_Name { get; set; }

        [Required(ErrorMessage = "الإسم المستخدم بالإنجليزي إجباري")]
        [Display(Name = "الإسم بالإنجليزي")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "must be between 4 and 50")]
        public string CR_Mas_User_Information_En_Name { get; set; }

        [Required(ErrorMessage = "الإسم المستخدم بالفرنسي إجباري")]
        [Display(Name = "الإسم بالفرنسي")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "must be between 4 and 50")]
        public string CR_Mas_User_Information_Fr_Name { get; set; }

        [Required(ErrorMessage = "رقم الهاتف إجباري")]
        [Display(Name = "رقم الهاتف")]
        public string CR_Mas_User_Information_Mobile_No { get; set; }

        [Display(Name = "الحالة")]
        public string CR_Mas_User_Information_Status { get; set; }

        [Display(Name = "المرجع")]
        [StringLength(100)]
        public string CR_Mas_User_Information_Reasons { get; set; }
    }
}