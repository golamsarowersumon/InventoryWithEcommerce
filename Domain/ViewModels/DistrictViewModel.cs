using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class DistrictViewModel
    {


     
       
        public int DistrictId { get; set; }
        
       
        
        public string DistrictName { get; set; }
        public decimal ShippingCharge { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
      

    }
}
