using System.ComponentModel.DataAnnotations;

namespace FitnessClub.ViewModels.Subscriptions
{
    public class EditSubscriptionViewModel
    {
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
    }
}
