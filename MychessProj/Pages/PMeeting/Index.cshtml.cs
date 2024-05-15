using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MychessProj.Models;
using Npgsql;

namespace MychessProj.Pages.PMeeting
{
    public class IndexModel : PageModel
    {
        private readonly MychessProj.Models.mychessContext _context;

        public IndexModel(MychessProj.Models.mychessContext context)
        {
            _context = context;
        }

        public IList<Meeting> Meeting { get;set; } = default!;
        
        
        public async Task OnGetAsync()
        {
            NpgsqlConnection myconnect = new NpgsqlConnection("Host=localhost;Database=mychess;Username=postgres;password=1234567Asd;Persist Security Info=True");
            //var transaction = myconnect.BeginTransaction();
            myconnect.Open();
            //NpgsqlCommand
            if (_context.Meetings != null)
            {
                Meeting = await _context.Meetings
                .Include(m => m.IdLocationNavigation).ToListAsync();
            }
        }
    }
}
