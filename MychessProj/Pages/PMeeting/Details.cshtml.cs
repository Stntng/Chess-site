using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MychessProj.Models;

namespace MychessProj.Pages.PMeeting
{
    public class DetailsModel : PageModel
    {
        private readonly MychessProj.Models.mychessContext _context;

        public DetailsModel(MychessProj.Models.mychessContext context)
        {
            _context = context;
        }

      public Meeting Meeting { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Meetings == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings.FirstOrDefaultAsync(m => m.IdMeet == id);
            if (meeting == null)
            {
                return NotFound();
            }
            else 
            {
                Meeting = meeting;
            }
            return Page();
        }
    }
}
