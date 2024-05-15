﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MychessProj.Models;

namespace MychessProj.Pages.PTournament
{
    public class DeleteModel : PageModel
    {
        private readonly MychessProj.Models.mychessContext _context;

        public DeleteModel(MychessProj.Models.mychessContext context)
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

            var tournament = await _context.Tournaments.FirstOrDefaultAsync(m => m.IdTourn == id);

            if (tournament == null)
            {
                return NotFound();
            }
            else 
            {
                Tournament = tournament;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Tournaments == null)
            {
                return NotFound();
            }
            var tournament = await _context.Tournaments.FindAsync(id);

            if (tournament != null)
            {
                Tournament = tournament;
                _context.Tournaments.Remove(Tournament);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}