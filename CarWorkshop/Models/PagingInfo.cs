using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWorkshop.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPage);
        public string UrlParam { get; set; }
    }
}
