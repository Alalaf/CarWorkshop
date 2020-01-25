using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWorkshop.Data;
using CarWorkshop.Models;
using CarWorkshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db; 
        }

        [BindProperty]
        public ApplicationUser applicationuser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            else
            {
                applicationuser = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == id);
                if(applicationuser == null)
                {
                    return NotFound();
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Id == applicationuser.Id);
            if(user == null)
            {
                return NotFound();
            }
            else
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
        }
    }
}