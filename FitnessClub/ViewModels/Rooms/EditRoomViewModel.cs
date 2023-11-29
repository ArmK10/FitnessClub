using System.ComponentModel.DataAnnotations;

namespace FitnessClub.ViewModels.Rooms
{
    public class EditRoomViewModel
    {
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите название помещения")]
        [Display(Name = "Помещение")]
        public string RoomName { get; set; }
    }
}
