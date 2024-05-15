using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MychessProj.Models;

namespace MychessProj.Pages.PTournament
{
    public class IndexModel : PageModel
    {
        private readonly MychessProj.Models.mychessContext _context;

        public IndexModel(MychessProj.Models.mychessContext context)
        {
            _context = context;
        }

        public IList<Tournament> Tournament { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Tournaments != null)
            {
                Tournament = await _context.Tournaments
                .Include(t => t.IdLocationNavigation).ToListAsync();
            }
        }
    }
}
