using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MychessProj.Models;

namespace MychessProj.Pages.PRegistration
{
    public class EditModel : PageModel
    {
        private readonly MychessProj.Models.mychessContext _context;

        public EditModel(MychessProj.Models.mychessContext context)
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

            var registration =  await _context.Registrations.FirstOrDefaultAsync(m => m.IdUser == id);
            if (registration == null)
            {
                return NotFound();
            }
            Registration = registration;
           ViewData["IdTourn"] = new SelectList(_context.Tournaments, "IdTourn", "IdTourn");
           ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "FirstName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Registration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationExists(Registration.IdUser))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RegistrationExists(int id)
        {
          return (_context.Registrations?.Any(e => e.IdUser == id)).GetValueOrDefault();
        }
    }
}
