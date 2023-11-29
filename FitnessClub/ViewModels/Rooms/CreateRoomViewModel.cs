using System.ComponentModel.DataAnnotations;

namespace FitnessClub.ViewModels.Rooms
{
    public class CreateRoomViewModel
    {
        [Required(ErrorMessage = "Введите название помещения")]
        [Display(Name = "Помещение")]
        public string RoomName { get; set; }
    }
}
