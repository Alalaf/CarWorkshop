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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ApplicationUser applicationuser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            applicationuser = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == id);
            if (applicationuser == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                
                var user = _db.ApplicationUser.SingleOrDefault(u => u.Id == applicationuser.Id);
                if (user == null)
                {
                    return NotFound();
                }
                user.Name = applicationuser.Name;
                user.PhoneNumber = applicationuser.PhoneNumber;
                user.Address = applicationuser.Address;
                user.City = applicationuser.City;
                user.Postalcode = applicationuser.Postalcode;
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            
        }

    }
}