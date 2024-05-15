using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MychessProj.Models;
using MychessProj.Pages;
using MychessProj.Pages.PMeeting;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
//builder.Services.AddExceptionHandler<CustomExceptionHandler>();
services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
	options =>
	{
		options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
		options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
	});

// Add services to the container.
builder.Services.AddRazorPages();

var connection = builder.Configuration.GetConnectionString("mychesscontext");
builder.Services.AddDbContext<mychessContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("mychesscontext")));

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();

app.Run();
