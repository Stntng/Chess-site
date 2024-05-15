using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MychessProj.Models;

namespace MychessProj.Pages.PTournament
{
    public class EditModel : PageModel
    {
        private readonly MychessProj.Models.mychessContext _context;

        public EditModel(MychessProj.Models.mychessContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tournament Tournament { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Tournaments == null)
            {
                return NotFound();
            }

            var tournament =  await _context.Tournaments.FirstOrDefaultAsync(m => m.IdTourn == id);
            if (tournament == null)
            {
                return NotFound();
            }
            Tournament = tournament;
           ViewData["IdLocation"] = new SelectList(_context.Locations, "IdLocation", "IdLocation");
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

            _context.Attach(Tournament).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentExists(Tournament.IdTourn))
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

        private bool TournamentExists(int id)
        {
          return (_context.Tournaments?.Any(e => e.IdTourn == id)).GetValueOrDefault();
        }
    }
}
