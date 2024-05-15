using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MychessProj.Models;

namespace MychessProj.Pages.PUser
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
        ViewData["CityCode"] = new SelectList(_context.Cities, "CityCode", "CityCode");
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Users == null || User == null)
            {
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
