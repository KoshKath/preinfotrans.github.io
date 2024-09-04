using static Humanizer.In;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Threading;
using System.Linq.Expressions;

namespace PreInfoTrans.Models
{
    public enum Result 
    {
        [Display(Name = "Создана")]
        Created = 1,
        [Display(Name = "Направлена в ИСТО")]
        Pending = 2,
        [Display(Name = "Аннулирование")]
        Canceling = 3,
        [Display(Name = "Отозвана")]
        Revoked = 4,
        [Display(Name = "Регистрация")]
        Registration = 5,
        [Display(Name = "Отказ регистрации")]
        Denied = 6,
        [Display(Name = "Отзыв")]
        Revoking = 7,
        [Display(Name = "Выпуск")]
        Release = 8,
        [Display(Name = "Отказ выпуска")]
        Refused = 9,
        [Display(Name = "Удалено")]
        Deleted = 0
    }
    public enum Targets
    {
        [Display(Name = "Временный ввоз")]
        TemporaryIn = 0,
        [Display(Name = "Временный вывоз")]
        TemporaryOut = 1,
        [Display(Name = "Обратный ввоз")]
        BackIn = 2,
        [Display(Name = "Обратный вывоз")]
        BackOut = 3
    }
    public class Epi
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Оформляющий")]
        public string? CreatorId { get; set; }

        //[DisplayName("Перевозчик")]
        //public Carrier? Carrier { get; set; }

        //[DisplayName("Ответственный")]
        //public Owner? Owner { get; set; }

        [DisplayName("Ввоз - Вывоз")]
        [DefaultValue(true)]
        public bool DirectionIn { get; set; }

        [DisplayName("Сведения о транспортном средстве")]
        //[Required(ErrorMessage = "не выбран тип ТС")]
        public int? TransportationType {  get; set; }

        [StringLength(7)]
        [RegularExpression(@"^[0-9]*$")]
        [DisplayName("Номер ЭПИ")]
        public string? DocName { get; set; } //порядковый номер ЭПИ ТДТС 

        [DisplayName("Дата и время формирования")]
        public DateTime DocDate { get; set; } //дата и время формирования (дд.мм.гггг чч:мм)
        [DisplayName("Дата завершения")]
        public DateTime? RegEndDate { get; set; } //дата и время завершения (дд.мм.гггг чч:мм)

        [DisplayName("Регистрационный номер ТДТС")]
        public string? RegNumTDTS { get; set; } //регистрационный номер ТДТС Например, 11206604/011223/301234567

        [DisplayName("Дата регистрации (отказа) ТДТС")]
        public DateTime? RegDateTime { get; set; } //дата и время регистрации (отказа) ТДТС

        [DisplayName("Номер выпуска ТДТС")]
        public string? RegNumOutTDTS { get; set; } //номер выпуска ТДТС Например, 06604/31234567. /3 – последняя цифра года

        [DisplayName("Дата и время выпуска ТДТС")]
        public DateTime? RegOutDateTime { get; } //дата и время выпуска (отказа, отзыва) ТДТС

        [DisplayName("Результат оформления")]
        [Column(TypeName = "int")]
        public Result? Result { get; set; } // результат

        [DisplayName("Пункт")]
        public string? Route { get; set; }

        [DisplayName("Страна назначения")]
        public string? RouteCountry { get; set; }

        [DisplayName("Пассажиры")]
        [DefaultValue(0)]
        public int IsPassengers { get; set; } //доп сведения

        [DisplayName("Экипаж")]
        [DefaultValue(0)]
        public int IsCrew { get; set; } //доп сведения

        [DisplayName("Припасы")]
        [DefaultValue(false)]
        public bool IsSupplies { get; set; } //доп сведения

        [DisplayName("Сведения о товаре")]
        [DefaultValue(false)]
        public bool IsGoods { get; set; } //доп сведения

        [DisplayName("Цель ввоза/вывоза")]
        [Column(TypeName = "int")]
        public Targets? Targets { get; set; } //цель ввоза/вывоза

        [DisplayName("Допольнительные сведения")]
        public string? Description { get; set; } //доп сведения цель ввоза/вывоза

        [DisplayName("Номер завершения временного ввоза/вывоза")]
        public string? RegCompleteTDTS { get; set; } //номер завершения временного ввоза/вывоза Например, 11206604/011223/301234567

        [DisplayName("Дата выпуска (отказа, отзыва) ТДТС")]
        public DateTime? RegComleteDateTime { get; set; } //дата завершения ввоза/вывоза 

        [DisplayName("Срок временного ввоза/вывоза")]
        public DateTime? TemporaryInDate { get; set; } //Срок временного ввоза/вывоза 

        //регистрационный номер транспортного средства международной перевозки(далее – ТСМП). Если несколько ТСМП в 1 ЭПИ ТДТС, то через «/»
        [DisplayName("Рег. номер ТСМП")]
        public List<Tsmp>? Tsmps { get; set; } // ссылка на список ТС
        [DisplayName("Зап. части/оборудование")] //Запчасти и оборудование
        public string? SpareParts { get; set; }

        [DisplayName("Причина аннулирования")]
        public string? CancelReason {  get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        [DisplayName("Регистрационный номер ТС")]
        public string? TsmpFormatedString { get; set; }
        
    }
}