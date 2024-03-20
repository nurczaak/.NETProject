using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Pages.Notes
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public EditModel(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Note Note { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve the current user's ID
            var userId = _userManager.GetUserId(User);

            Note = await _context.Notes
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (Note == null)
            {
                return NotFound();
            }

            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Log ModelState errors
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                }

                return Page();
            }

            // Ensure that the Note exists and is associated with the current user
            var existingNote = await _context.Notes
                .Where(n => n.Id == Note.Id && n.UserId == Note.UserId)
                .FirstOrDefaultAsync();

            if (existingNote == null)
            {
                Console.WriteLine("Note not found or not associated with the current user.");
                return NotFound();
            }

            // Update only the modifiable fields
            existingNote.Title = Note.Title;
            existingNote.Description = Note.Description;
            existingNote.Image = Note.Image;

            // Set EntityState to Modified and save changes
            _context.Entry(existingNote).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            Console.WriteLine("Note updated successfully.");

            return RedirectToPage("./Index");
        }
    }
}
