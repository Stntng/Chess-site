using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MychessProj.Models;

namespace MychessProj.Pages.PRegistration
{
    public class CreateModel : PageModel
    {
        private readonly MychessProj.Models.mychessContext _context;

        public CreateModel(MychessProj.Models.mychessContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IdTourn"] = new SelectList(_context.Tournaments, "IdTourn", "IdTourn");
        ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "FirstName");
            return Page();
        }

        [BindProperty]
        public Registration Registration { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Registrations == null || Registration == null)
            {
                return Page();
            }

            _context.Registrations.Add(Registration);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
