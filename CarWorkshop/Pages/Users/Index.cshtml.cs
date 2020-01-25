using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarWorkshop.Data;
using CarWorkshop.Models;
using CarWorkshop.Models.ViewModel;
using CarWorkshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public UsersListViewModel userslistvm { get; set; }
        public async Task<IActionResult> OnGetAsync(int productpage=1, string searchEmail = null, string searchName=null, string searchPhone=null)
        {
            userslistvm = new UsersListViewModel()
            {
                applicationuserlist = await _db.ApplicationUser.ToListAsync()
            };

            StringBuilder param = new StringBuilder();
            param.Append("users?productpage=:");
            param.Append("&searchEmail=");
            if (searchEmail!=null)
            {
                param.Append(searchEmail);
            }

            param.Append("&searchName=");
            if (searchName != null)
            {
                param.Append(searchName);
            }

            param.Append("&searchPhone=");
            if (searchPhone != null)
            {
                param.Append(searchPhone);
            }

            if (searchEmail != null)
            {
                userslistvm.applicationuserlist = await _db.ApplicationUser.Where(u => u.Email.ToLower().Contains(searchEmail.ToLower())).ToListAsync();
            }
            else
            {
                if(searchName != null)
                {
                    userslistvm.applicationuserlist = await _db.ApplicationUser.Where(u => u.Name.ToLower().Contains(searchName.ToLower())).ToListAsync();

                }
                else
                {
                    if (searchPhone != null)
                    {
                        userslistvm.applicationuserlist = await _db.ApplicationUser.Where(u => u.PhoneNumber.ToLower().Contains(searchPhone.ToLower())).ToListAsync();

                    }
                }

            }

            var count = userslistvm.applicationuserlist.Count;
            userslistvm.PagingInfo = new PagingInfo
            {
                CurrentPage = productpage,
                ItemsPage = SD.PaginationUserPageSize,
                TotalItems = count,
                UrlParam = param.ToString()
            };
            userslistvm.applicationuserlist = userslistvm.applicationuserlist.OrderBy(p => p.Email)
                .Skip((productpage - 1) * SD.PaginationUserPageSize)
                .Take(SD.PaginationUserPageSize).ToList();
            return Page();
        }

    }
}