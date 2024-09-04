using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreInfoTrans.Models
{
    public class Tsmp
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Марка")]
        [RequiredNotContainer]
        public string? Brand { get; set; }
        [DisplayName("Модель")]
        public string? Model { get; set; }
        [DisplayName("Тип ТС")]
        public string? Type { get; set; }
        public int TypeCode { get; set; }

        [DisplayName("Регистрационный номер")]
        [MinLength(3, ErrorMessage = "Минимальная длина номера 3 символа")]
        [RegularExpression(@"^[a-zA-Z0-9а-яА-ЯёЁ\- ]*$", ErrorMessage = "Номер может содержать только цифры и буквы и символы «-» (дефис) и пробел")]
        [Required(ErrorMessage = "Регистрационный номер обязателен")]
        public string? RegNum { get; set; }

        [DisplayName("Страна регистрации")]
        [Required(ErrorMessage = "Страна обязательна к заполнению!")]
        public string? RegCountry { get; set; }
        [DisplayName("Идентификационный номер (VIN/шасси/кузов)")]
        [MinLength(17, ErrorMessage = "Длина VIN кода должна быть не меньше 17 символов")]
        [MaxLength(17, ErrorMessage = "Длина VIN кода должна быть не меньше 17 символов")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Идентификационный номер (VIN/шасси/кузов) может содержать только цифры и латинские символы")]
        [VinCodeRequiredIfAutomobile]
        public string? VinCode { get; set; }
        public string? EpiDocName { get; set; }
    }

    internal class RequiredNotContainerAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) 
        {
            var tsmp = (Tsmp)validationContext.ObjectInstance;
            if (tsmp.Type == "контейнер" && string.IsNullOrWhiteSpace(tsmp.Brand))
            {
                return ValidationResult.Success;
            }
            if (!string.IsNullOrWhiteSpace(tsmp.Brand))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Необходимо указать марку ТС!");
        }
    }

    internal class VinCodeRequiredIfAutomobileAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var tsmp = (Tsmp)validationContext.ObjectInstance;
            if (tsmp.TypeCode == 30 && string.IsNullOrWhiteSpace(tsmp.VinCode))
            {
                return new ValidationResult("VIN/шасси/кузов номер обязателен для автодорожного транспорта.");
            }
            return ValidationResult.Success;
        }
    }

}
