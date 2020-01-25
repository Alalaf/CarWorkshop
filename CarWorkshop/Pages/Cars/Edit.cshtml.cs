using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CarWorkshop.Data;
using CarWorkshop.Models;
using CarWorkshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Pages.Cars
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
        public Car car { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            car = await _db.Car.FirstOrDefaultAsync(c => c.Id == id);
            if (car == null)
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

                var cardb = _db.Car.SingleOrDefault(c => c.Id == car.Id);
                if (cardb == null)
                {
                    return NotFound();
                }
                cardb.VIN = car.VIN;
                cardb.Model = car.Model;
                cardb.Make = car.Make;
                cardb.Style = car.Style;
                cardb.Year = car.Year;
                cardb.Miles = car.Miles;
                cardb.color = car.color;
                

                await _db.SaveChangesAsync();
                return RedirectToPage("Index", new { userid = car.UserId });
            }

        }

    }
}