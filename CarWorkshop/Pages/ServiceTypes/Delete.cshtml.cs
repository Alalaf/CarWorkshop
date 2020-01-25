using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarWorkshop.Data;
using CarWorkshop.Models;
using Microsoft.AspNetCore.Authorization;
using CarWorkshop.Utility;

namespace CarWorkshop.Pages.ServiceTypes
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

        //public async Task<IActionResult> OnPostAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var servicetypedb = await _db.ServiceType.FindAsync(id);

        //    if (servicetypedb != null)
        //    {
        //        _db.ServiceType.Remove(servicetypedb);
        //        await _db.SaveChangesAsync();
        //    }

        //    return RedirectToPage("./Index");
        //}


        public async Task<IActionResult> OnPostAsync()
        {
            if (servicetype == null)
            {
                return NotFound();
            }

            _db.ServiceType.Remove(servicetype);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

