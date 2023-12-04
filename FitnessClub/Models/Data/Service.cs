using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace FitnessClub.Models.Data
{
    public class Service
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите название услуги")]
        [Display(Name = "Виды услуг")]
        public string ServiceName { get; set; }

        [Required]
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
