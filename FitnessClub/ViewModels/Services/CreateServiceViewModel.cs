using System.ComponentModel.DataAnnotations;

namespace FitnessClub.ViewModels.Services
{
    public class CreateServiceViewModel
    {
        [Required(ErrorMessage = "Введите название услуги")]
        [Display(Name = "Виды услуг")]
        public string ServiceName { get; set; }
    }
}
