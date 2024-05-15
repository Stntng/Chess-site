using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MychessProj.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using MychessProj.Pages.Account;
using System.Net;
using System.Web.Http;

namespace MychessProj.Pages.Account
{
    public class LoginModel : PageModel
    {

		private readonly MychessProj.Models.mychessContext _context;
		public LoginModel(MychessProj.Models.mychessContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			
			ViewData["CityCode"] = new SelectList(_context.Cities, "CityCode", "CityCode");
			return Page();
		}
		Hash_Cookie hash { get; set; } = new Hash_Cookie();
        List<string> errors = new List<string>();
        [BindProperty]
		public User Credential { get; set; } 
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || _context.Users == null || Credential == null)
			{
				return Page();
			}
			try
			{
                if (Credential.FirstName == "Админ" && Credential.LastName == "Админ" && Credential.SecondName == "Админ")
                {
                    Credential.Password = hash.HashPassword(Credential.Password);
                    Credential.role = MychessProj.Models.User.roles.mod;
                    Credential.IdUser = _context.Users.ToList().Last().IdUser + 1;
                    _context.Users.Add(Credential);
                    var result = hash.Authenticate(Credential);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(result));
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Index");
                }
				else if (ModelState.IsValid)
				{
					Credential.Password = hash.HashPassword(Credential.Password);
					Credential.role = MychessProj.Models.User.roles.player;
					Credential.IdUser = _context.Users.ToList().Last().IdUser + 1;
					_context.Users.Add(Credential);
					var result = hash.Authenticate(Credential);
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(result));
					await _context.SaveChangesAsync();
					return RedirectToPage("/Index");
				}
				
			}
			catch (Exception ex) {
                //errors.Add("Customer Name cannot be empty");
                //var responseMessage = new HttpResponseMessage<List<string>>(errors, HttpStatusCode.BadRequest);
                //throw new HttpResponseException(responseMessage);
            }
			//if(Credential.FirstName == "admin" && Credential.SecondName == "admin" )
			//{
			//	var claims = new List<Claim>
			//	{
			//		new Claim(ClaimTypes.Name,"admin"),
			//		new Claim(ClaimTypes.Email,"admin@mysite.com")
			//	};
			//	var identity = new ClaimsIdentity(claims,"MycookieAuth");
			//	ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
			//	await HttpContext.SignInAsync("MycookieAuth", claimsPrincipal);
			//	Credential.IdUser = _context.Users.ToList().Last().IdUser + 1;
			//	_context.Users.Add(Credential);
			//	await _context.SaveChangesAsync();
			//	return RedirectToPage("/Index");
			//}


			return RedirectToPage("/Index");
		}

        private class HttpResponseMessage<T> : HttpResponseMessage
        {
            private List<string> errors;
            private HttpStatusCode badRequest;

            public HttpResponseMessage(List<string> errors, HttpStatusCode badRequest)
            {
                this.errors = errors;
                this.badRequest = badRequest;
            }
        }
    }
}
