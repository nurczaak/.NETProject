using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; } = false;
        public string UserId { get; set; }
        public AppUser? User { get; set; }

    }
}
