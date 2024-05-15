using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MychessProj.Models;

namespace MychessProj.Pages.PUser
{
    public class IndexModel : PageModel
    {
        private readonly MychessProj.Models.mychessContext _context;

        public IndexModel(MychessProj.Models.mychessContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;
        public User Credential { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                User = await _context.Users
                .Include(u => u.CityCodeNavigation).ToListAsync();
            }
        }
    }
}
