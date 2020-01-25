using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarWorkshop.Data;
using CarWorkshop.Models;
using CarWorkshop.Utility;
using Microsoft.AspNetCore.Authorization;

namespace CarWorkshop.Pages.ServiceTypes
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
        public ServiceType servicetype { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            servicetype = await _db.ServiceType.FirstOrDefaultAsync(m => m.Id == id);

            if (servicetype == null)
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

            var servicefromdb = await _db.ServiceType.FirstOrDefaultAsync(s => s.Id == servicetype.Id);

            servicefromdb.Name = servicetype.Name;
            servicefromdb.Price = servicetype.Price;
            await _db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        
    }
}
