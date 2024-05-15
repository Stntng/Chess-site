using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MychessProj.Models;

namespace MychessProj.Pages.PRegistration
{
    public class DeleteModel : PageModel
    {
        private readonly MychessProj.Models.mychessContext _context;

        public DeleteModel(MychessProj.Models.mychessContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Registration Registration { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Registrations == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FirstOrDefaultAsync(m => m.IdUser == id);

            if (registration == null)
            {
                return NotFound();
            }
            else 
            {
                Registration = registration;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Registrations == null)
            {
                return NotFound();
            }
            var registration = await _context.Registrations.FindAsync(id);

            if (registration != null)
            {
                Registration = registration;
                _context.Registrations.Remove(Registration);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
