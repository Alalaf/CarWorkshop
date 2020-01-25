using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWorkshop.Data;
using CarWorkshop.Models;
using CarWorkshop.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Pages.Services
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public CarServiceViewModel  CarServiceVM { get; set; }

        public async Task<IActionResult> OnGet(int carid)
        {
            CarServiceVM = new CarServiceViewModel
            {
                Car = await _db.Car.Include(u => u.ApplicationUser).FirstOrDefaultAsync(c => c.Id == carid),
                ServiceHeader = new Models.ServiceHeader()
            };
            List<String> lstservicetypeinshoppingcart =  _db.ServiceShoppingCart
                .Include(s => s.ServiceType)
                .Where(c => c.Car.Id == carid)
                .Select(s => s.ServiceType.Name).ToList();

            IQueryable<ServiceType> lstservice = from s in _db.ServiceType
                                                 where !(lstservicetypeinshoppingcart.Contains(s.Name))
                                                 select s;

            CarServiceVM.ServiceTypesList = lstservice.ToList();

            CarServiceVM.ServiceShoppingCart = _db.ServiceShoppingCart.Include(s => s.ServiceType).Where(c => c.CarId == carid).ToList();
            CarServiceVM.ServiceHeader.TotalPrice = 0;

            foreach (var item in CarServiceVM.ServiceShoppingCart)
            {
                CarServiceVM.ServiceHeader.TotalPrice += item.ServiceType.Price;
            }

            return Page();  
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                CarServiceVM.ServiceHeader.DateAdded = DateTime.Now;
                CarServiceVM.ServiceShoppingCart = _db.ServiceShoppingCart.Include(c => c.ServiceType).Where(c => c.CarId == CarServiceVM.Car.Id).ToList();
                foreach (var item in CarServiceVM.ServiceShoppingCart)
                {
                    CarServiceVM.ServiceHeader.TotalPrice += item.ServiceType.Price;
                }
                CarServiceVM.ServiceHeader.CarId = CarServiceVM.Car.Id;
                _db.ServiceHeader.Add(CarServiceVM.ServiceHeader);
                await _db.SaveChangesAsync();

                foreach (var detail in CarServiceVM.ServiceShoppingCart)
                {
                    ServiceDetails serviceDetails = new ServiceDetails
                    {
                        ServiceHeaderId = CarServiceVM.ServiceHeader.Id,
                        ServiceName = detail.ServiceType.Name,
                        ServicePrice = detail.ServiceType.Price,
                        ServiceTypeId = detail.ServiceTypeId
                    };

                    _db.ServiceDetails.Add(serviceDetails);
                }
                _db.ServiceShoppingCart.RemoveRange(CarServiceVM.ServiceShoppingCart);
                await _db.SaveChangesAsync();
                return RedirectToPage("../Cars/Index", new { userid = CarServiceVM.Car.UserId });
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCart()
        {
            ServiceShoppingCart objservicecart = new ServiceShoppingCart() 
            {
                CarId = CarServiceVM.Car.Id,
                ServiceTypeId = CarServiceVM.ServiceDetails.ServiceTypeId
            };

            _db.ServiceShoppingCart.Add(objservicecart);
            await _db.SaveChangesAsync();
            return RedirectToPage("Create", new { carid = CarServiceVM.Car.Id });
        }

        public async Task<IActionResult> OnPostRemoveFromCart(int servicetypeid)
        {
            ServiceShoppingCart objservicecart = _db.ServiceShoppingCart
                .FirstOrDefault(c => c.CarId == CarServiceVM.Car.Id && c.ServiceTypeId == servicetypeid);
            

            _db.ServiceShoppingCart.Remove(objservicecart);
            await _db.SaveChangesAsync();
            return RedirectToPage("Create", new { carid = CarServiceVM.Car.Id });
        }
    }
}