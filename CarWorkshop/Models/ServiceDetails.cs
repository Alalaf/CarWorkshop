using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarWorkshop.Models
{
    public class ServiceDetails
    {
        public int Id { get; set; }

        public int ServiceHeaderId { get; set; }
        [ForeignKey("ServiceHeaderId")]
        public virtual ServiceHeader ServiceHeader { get; set; }

        [Display(Name="Service")]
        public int ServiceTypeId { get; set; }
        [ForeignKey("ServiceTypeId")]
        public virtual ServiceType ServiceType { get; set; }

        public double ServicePrice { get; set; }
        public String ServiceName { get; set; }
    }
}
