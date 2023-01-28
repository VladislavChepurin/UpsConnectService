using System.ComponentModel.DataAnnotations;

namespace UpsConnectService.ViewModels.Devises
{
    public class RegisterViewsModel
    {
        [Required(ErrorMessage = "Поле Код обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Заводской номер", Prompt = "Введите заводской номер")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Код партии", Prompt = "Введите код партии")]
        public string ManufactureCode { get; set; }
    }
}
