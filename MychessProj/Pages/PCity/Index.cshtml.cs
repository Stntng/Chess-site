using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MychessProj.Models;

namespace MychessProj.Pages.PCity
{
    public class IndexModel : PageModel
    {
        private readonly MychessProj.Models.mychessContext _context;

        public IndexModel(MychessProj.Models.mychessContext context)
        {
            _context = context;
        }

        public IList<City> City { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Cities != null)
            {
                City = await _context.Cities.ToListAsync();
            }
        }
    }
}
