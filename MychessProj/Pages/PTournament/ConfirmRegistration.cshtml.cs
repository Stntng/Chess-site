using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MychessProj.Models;
using MychessProj.Pages.Account;

namespace MychessProj.Pages.PTournament
{
    public class ConfirmRegistrationModel : PageModel
    {
		private readonly MychessProj.Models.mychessContext _context;
		Hash_Cookie hash { get; set; } = new Hash_Cookie();
		public IList<User> Userss { get; set; } = default!;
		public ConfirmRegistrationModel(MychessProj.Models.mychessContext context)
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
            if (_context.Users != null)
            {
                Userss = _context.Users.ToList();
            }
            var tournament = await _context.Tournaments.FindAsync(id);
            var user = Userss.FirstOrDefault(x => x.FirstName == User.Identity.Name);
			var som = await _context.Registrations.FirstOrDefaultAsync(x => x.IdUser == user.IdUser);
            if (tournament != null && som == null)
			{
				Tournament = tournament;
				
				Registration reg;
				reg= new Registration();
				reg.IdUser = user.IdUser;
				reg.IdTourn = tournament.IdTourn;
				reg.Time = DateTime.Now;
				_context.Registrations.Add(reg);
				//_context.Tournaments.Remove(Tournament);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
