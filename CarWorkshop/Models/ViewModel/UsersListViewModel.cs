using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWorkshop.Models.ViewModel
{
    public class UsersListViewModel
    {
        public List<ApplicationUser> applicationuserlist { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
