using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MychessProj.Models;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace MychessProj.Pages.Account
{
    public class AuthorizeModel : PageModel
    {
		[BindProperty]
		public User Credential { get; set; }
		public IList<User> Userss { get; set; } = default!;
		private readonly MychessProj.Models.mychessContext _context;
		public AuthorizeModel(MychessProj.Models.mychessContext context)
		{
			_context = context;
		}
		Hash_Cookie hash { get; set; } = new Hash_Cookie();
		
		public async Task<IActionResult> OnPostAsync()
        {
			if (_context.Users != null)
			{
				//TempData["error"] = "Что-то пошло не так :-(";
				Userss =  _context.Users.ToList();
			}
	
			var user = Userss.FirstOrDefault(x => x.FirstName == Credential.FirstName && x.SecondName == Credential.SecondName && x.LastName == Credential.LastName);
			//var user = _context.Users.FirstOrDefault(x => x.FirstName == Credential.FirstName && x.SecondName == Credential.SecondName);
			//var user = await _context.Users.FirstOrDefaultAsync(x => x.FirstName == Credential.FirstName && x.SecondName == Credential.SecondName);
			if (user == null)
            {
				TempData["error"] = "Неправильное Имя, Фамилия или Отчество";
				return Page();
            }
            var userpas = hash.HashPassword(Credential.Password);
            if (userpas != user.Password) 
            {
				TempData["error"] = "Неправильный пароль!!!";
				return Page();
            }
            var result = hash.Authenticate(user);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(result));
			TempData["success"] = "Вы вошли в аккаунт";
			return RedirectToPage("/Index");
		}
    }
}
