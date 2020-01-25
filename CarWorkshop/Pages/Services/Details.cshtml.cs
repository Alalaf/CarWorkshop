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
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public DetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public ServiceHeader serviceheader { get; set; }
        public List<ServiceDetails> servicedetails { get; set; }

        public void OnGet(int serviceid)
        {
            serviceheader = _db.ServiceHeader.Include(c => c.Car).Include(u => u.Car.ApplicationUser).FirstOrDefault(s => s.Id == serviceid);
            servicedetails = _db.ServiceDetails.Where(s => s.ServiceHeaderId == serviceid).ToList();
        }
    }
}