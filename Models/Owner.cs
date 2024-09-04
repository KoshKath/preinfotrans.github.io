using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreInfoTrans.Models
{
    public class Owner
    {
        // Лицо, ответственное за использование транспортного средства
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Ф.И.О.")]
        [Required(ErrorMessage ="Обязательно к заполнению!")]
        public string? Name { get; set; }
        [DisplayName("Серия и номер паспорта")]
        [Required(ErrorMessage = "Обязательно к заполнению!")]
        public string? Passport { get; set; }
        [DisplayName("Идентификационный номер")]
        [MinLength(14, ErrorMessage = "Длина ИН должна быть не меньше 14 символов")]
        [MaxLength(14, ErrorMessage = "Длина ИН должна быть не меньше 14 символов")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Идентификационный номер может содержать только цифры и латинские символы")]
        [RequiredIfOwnerFromBelarus("Country")]
        public string? IdNumber { get; set; }
        [DisplayName("Дата выдачи")]
        [DataType(DataType.Date)]
        [DateNotInFuture]
        public DateTime? DateIssue { get; set; }
        [DisplayName("Страна")]
        [Required]
        public string? Country { get; set; }
        [DisplayName("Адрес прописки")]
        [Required(ErrorMessage = "Обязательно к заполнению!")]
        public string? Address { get; set; }
        [Required]
        public int EpiId { get; set; }
        [DisplayName("Лицо, ответственное за использование транспортного средства")]
        public string GetAllFieldsAsString()
        {
            string name = Name ?? "Unknown";
            string passport = Passport ?? "";
            string idNumber = IdNumber ?? "";
            string country = Country ?? "";
            string address = Address ?? "";
            string dateIssue = DateIssue.Value.ToString("dd.MM.yyyy") ?? "";
            return $"{name}, Паспорт №: {passport}, И/н: {idNumber}, Выдан: {dateIssue}, Адрес: {country}, {address}"; // Добавьте остальные поля, если необходимо
        }
    }
    internal class RequiredIfOwnerFromBelarusAttribute : ValidationAttribute
    {
        private readonly string _countryPropertyName;

        public RequiredIfOwnerFromBelarusAttribute(string countryPropertyName)
        {
            _countryPropertyName = countryPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var countryProperty = validationContext.ObjectType.GetProperty(_countryPropertyName);
            if (countryProperty == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            var countryValue = countryProperty.GetValue(validationContext.ObjectInstance)?.ToString();

            if (countryValue == "БЕЛАРУСЬ" && string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult($"Необходимо указать {validationContext.DisplayName}.");
            }

            return ValidationResult.Success;
        }
    }
    public class DateNotInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue > DateTime.Now)
                {
                    return new ValidationResult("Дата выдачи не может быть больше текущей даты.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
