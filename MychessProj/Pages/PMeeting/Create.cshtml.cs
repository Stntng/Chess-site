using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MychessProj.Models;

namespace MychessProj.Pages.PMeeting
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
        ViewData["IdLocation"] = new SelectList(_context.Locations, "IdLocation", "IdLocation");
            return Page();
        }

        [BindProperty]
        public Meeting Meeting { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Meetings == null || Meeting == null)
            {
                return Page();
            }

            _context.Meetings.Add(Meeting);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
