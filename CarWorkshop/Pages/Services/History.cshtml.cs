using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWorkshop.Data;
using CarWorkshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Pages.Services
{
    [Authorize]
    public class HistoryModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public HistoryModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public List<ServiceHeader> serviceheader { get; set; }

        public string UserId { get; set; }

        public async Task OnGetAsync(int carid)
        {
            serviceheader = await _db.ServiceHeader.Where(s => s.CarId == carid).Include(c => c.Car).Include(u => u.Car.ApplicationUser).ToListAsync();
            UserId = _db.Car.Where(c => c.Id == carid).ToList().FirstOrDefault().UserId;
        }
    }
}