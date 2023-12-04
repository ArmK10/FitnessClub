using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Models.Data
{
    public class Subscription
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Введите кол-во посещений")]
        [Display(Name = "Кол-во посещений")]
        public int CountVisits { get; set; }

        [Required(ErrorMessage = "Введите кол-во дней")]
        [Display(Name = "Кол-во дней")]
        public int CountDays { get; set; }

        [Required]
        [Display(Name = "Услуга")]
        public short IdService { get; set; }


        // Навигационные свойства
        [Display(Name = "Услуга")]
        [ForeignKey("IdService")]
        public Service Service { get; set; }
    }
}
