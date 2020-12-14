using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    [MetadataType(typeof(SupportingMetaData))]
    public partial class CR_Mas_Com_Supporting
    {
    }
    public class SupportingMetaData
    {        
        [Display(Name = "الرمز")]
        public string CR_Mas_Com_Supporting_Code { get; set; }
        [Display(Name = "النوع")]
        public string CR_Mas_Com_Supporting_Type { get; set; }
        [Display(Name = "الإسم الكامل بالعربي")]
        public string CR_Mas_Com_Supporting_Ar_Long_Name { get; set; }
        [Display(Name = "الإسم المختصر بالعربي")]
        public string CR_Mas_Com_Supporting_Ar_Short_Name { get; set; }
        [Display(Name = "الإسم الكامل بالإنجليزي")]
        public string CR_Mas_Com_Supporting_En_Long_Name { get; set; }
        [Display(Name = "الإسم المختصر بالإنجليزي")]
        public string CR_Mas_Com_Supporting_En_Short_Name { get; set; }
        [Display(Name = "الرقم الضريبي")]
        public string CR_Mas_Com_Supporting_Tax_Number { get; set; }
        [Display(Name = "الحالة")]
        public string CR_Mas_Com_Supporting_Status { get; set; }
        [Display(Name = "المرجع")]
        public string CR_Mas_Com_Supporting_Reasons { get; set; }
    }
}