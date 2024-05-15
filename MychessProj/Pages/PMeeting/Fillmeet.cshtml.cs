using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MychessProj.Models;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;

namespace MychessProj.Pages.PMeeting
{
    public class FillmeetModel : PageModel
    {
        private readonly MychessProj.Models.mychessContext _context;

        public FillmeetModel(MychessProj.Models.mychessContext context)
        {
            _context = context;
        }
        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection("Host=localhost;Database=mychess;Username=postgres;password=1234567Asd;Persist Security Info=True");

        }
            public bool Fill_meet(int id)
            {
                bool filled = false;
                using (var cn = GetConnection())
                {
                NpgsqlCommand cmd = new NpgsqlCommand("CALL fill_meet(id_tour)", cn);
                cmd.Parameters.AddWithValue("id_tour", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"CALL fill_meet({id})";
                cn.Open();
                int affectedFill = cmd.ExecuteNonQuery();
                filled = affectedFill == 1;
                }
            return filled;
            }

        [BindProperty]
        public Tournament Tournament { get; set; } = default!;
        public IActionResult OnGet()
        {
            ViewData["IdTourn"] = new SelectList(_context.Tournaments, "IdTourn", "IdTourn");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Fill_meet(Tournament.IdTourn);
            }
            catch (Exception ex) 
            { return NotFound(); }
            return RedirectToPage("./Index");
        }
    }
}
