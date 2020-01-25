using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarWorkshop.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarWorkshop.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var claim = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim==null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            if (User.IsInRole(SD.AdminEndUser))
            {
                return RedirectToPage("/Users/Index");
            }
            return RedirectToPage("/Cars/Index");
        }
    }
}
