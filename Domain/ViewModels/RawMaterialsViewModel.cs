using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.ViewModels
{
  public  class RawMaterialsViewModel
    {
      
        [Display(Name = "Raw Materials Id")]
        public int RawMaterialId { get; set; }
        
        [Display(Name ="Item Id")]
        public Nullable<int>ItemId { get; set; }

        public int ItemId1 { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Item Element Id")]
        public Nullable<int>ItemElementId { get; set; }
        public int[]ItemElementId1 { get; set; }

        [Display(Name = "Item Element Name")]
        public string ItemElementName { get; set; }





    }
}
