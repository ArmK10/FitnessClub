using System.ComponentModel.DataAnnotations;

namespace FitnessClub.ViewModels.Services
{
    public class EditServiceViewModel
    {
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите название услуги")]
        [Display(Name = "Виды услуг")]
        public string ServiceName { get; set; }
    }
}
