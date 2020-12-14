using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    [MetadataType(typeof(CategoryMetaData))]
    public partial class CR_Mas_Sup_Category_Car
    {
    }
    public class CategoryMetaData
    {
        [Display(Name = "الرمز")]
        public string CR_Mas_Sup_Category_Car_Code { get; set; }
        [Display(Name = "المجموعة")]
        public string CR_Mas_Sup_Category_Car_Group_Code { get; set; }
        [Display(Name = "الإسم بالعربي")]
        public string CR_Mas_Sup_Category_Car_Ar_Name { get; set; }
        [Display(Name = "الإسم بالإنجليزي")]
        public string CR_Mas_Sup_Category_Car_En_Name { get; set; }
        [Display(Name = "الإسم بالفرنسي")]
        public string CR_Mas_Sup_Category_Car_Fr_Name { get; set; }
        [Display(Name = "الحالة")]
        public string CR_Mas_Sup_Category_Car_Status { get; set; }
        [Display(Name = "المرجع")]
        public string CR_Mas_Sup_Category_Car_Reasons { get; set; }
        public virtual CR_Mas_Sup_Group CR_Mas_Sup_Group { get; set; }
    }
}