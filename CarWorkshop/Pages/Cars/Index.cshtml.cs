using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarWorkshop.Data;
using CarWorkshop.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Pages.Cars
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public CarAndCustomerViewModel carandcustvm { get; set; }

        public  async Task<IActionResult> OnGet(string userid = null)
        {
            if (userid == null)
            {
                var claimsidentity = (ClaimsIdentity)User.Identity;
                var claim = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);
                userid = claim.Value;
            }

            carandcustvm = new CarAndCustomerViewModel()
            {
                Cars = await _db.Car.Where(c => c.UserId == userid).ToListAsync(),
                UserObj = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == userid)
            };

            return Page();

        }
    }
}