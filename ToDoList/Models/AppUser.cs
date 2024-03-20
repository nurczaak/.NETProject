using Microsoft.AspNetCore.Identity;

namespace ToDoList.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Note>? Notes { get; set; }
    }
}
