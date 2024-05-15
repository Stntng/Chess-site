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
    public class IndexModel : PageModel
    {
        private readonly MychessProj.Models.mychessContext _context;

        public IndexModel(MychessProj.Models.mychessContext context)
        {
            _context = context;
        }

        public IList<Registration> Registration { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Registrations != null)
            {
                Registration = await _context.Registrations
                .Include(r => r.IdTournNavigation)
                .Include(r => r.IdUserNavigation).ToListAsync();
            }
        }
    }
}
