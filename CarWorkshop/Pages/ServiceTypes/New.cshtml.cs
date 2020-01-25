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

namespace CarWorkshop.Pages.ServiceTypes
{
    [Authorize(Roles =SD.AdminEndUser)]
    public class NewModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public NewModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ServiceType servicetype { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult>  OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _db.ServiceType.Add(servicetype);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}