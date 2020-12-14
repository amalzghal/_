using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    [MetadataType(typeof(QuestionsMetaData))]
    public partial class CR_Mas_Msg_Questions_Answer
    {
    }
    public class QuestionsMetaData
    {
        [Display(Name = "الرمز")]
        public string CR_Mas_Msg_Questions_Answer_No { get; set; }
        
        public string CR_Mas_Msg_Tasks_Code { get; set; }
        [Display(Name = "السؤال بالعربي")]
        public string CR_Mas_Msg_Ar_Questions { get; set; }
        [Display(Name = "الجواب بالعربي")]
        public string CR_Mas_Msg_Ar_Answer { get; set; }
        [Display(Name = "السؤال بالإنجليزي")]
        public string CR_Mas_Msg_En_Questions { get; set; }
        [Display(Name = "الجواب بالإنجليزي")]
        public string CR_Mas_Msg_En_Answer { get; set; }
        [Display(Name = "السؤال بالفرنسي")]
        public string CR_Mas_Msg_Fr_Questions { get; set; }
        [Display(Name = "الجواب بالفرنسي")]
        public string CR_Mas_Msg_Fr_Answer { get; set; }
        [Display(Name = "الحالة")]
        public string CR_Mas_Msg_Questions_Answer_Status { get; set; }
        [Display(Name = "المرجع")]
        public string CR_Mas_Msg_Questions_Answer_Reasons { get; set; }

        public virtual CR_Mas_Sys_Tasks CR_Mas_Sys_Tasks { get; set; }
    }
}