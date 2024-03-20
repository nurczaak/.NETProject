using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Pages.Notes
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Note> Notes { get; set; }

        public void OnGet()
        {
            var userId = _userManager.GetUserId(User);
            Notes = _context.Notes.Where(n => n.UserId == userId).ToList();
        }
    }
}
