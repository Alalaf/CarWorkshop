using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarWorkshop.Data;
using CarWorkshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarWorkshop.Pages.Cars
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Car car { get; set; }

        public IActionResult OnGet(string userid = null)
        {
            car = new Car();
            if (userid == null)
            {
                var claimsidentity = (ClaimsIdentity)User.Identity;
                var claim = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);
                userid = claim.Value;
            }
            car.UserId = userid;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _db.Car.Add(car);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index", new { userid = car.UserId });
        }
    }
}