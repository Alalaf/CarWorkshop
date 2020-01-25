using CarWorkshop.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWorkshop.Data
{
    public class DbInializer : IDbInializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly RoleManager<IdentityRole> _rolemanager;

        public DbInializer(ApplicationDbContext db, UserManager<IdentityUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            _db = db;
            _usermanager = usermanager;
            _rolemanager = rolemanager;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count()>0)
                {
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex)
            {

            }

            if (_db.Roles.Any(r => r.Name == SD.AdminEndUser)) return; 
        }
    }
}
