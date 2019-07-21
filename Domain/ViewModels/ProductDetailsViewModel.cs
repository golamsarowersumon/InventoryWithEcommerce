using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class ProductDetailsViewModel
    {
        [Display(Name ="Serial Id")]
        public int SerialId { get; set; }


        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Display(Name ="Product Name")]
        public string ProductName { get; set; }
        public string Msg { get; set; }

        [Display(Name ="Product Quantity")]
        public decimal ProductQuantity { get; set; }

        [Display(Name ="Item Id")]
        public int ItemId { get; set; }

        [Display(Name ="Item Name")]
        public string ItemName { get; set; }

        [Display(Name ="Item Quantity")]
        public decimal ItemQuantity { get; set; }

        public decimal PO_Price { get; set; }

        public decimal AvailableQty { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }

        public decimal TransactionQty { get; set; }
        public decimal productionqty { get; set; }
        public decimal invdetailsqty { get; set; }


        [Display(Name = "Item Id")]
        public int[] ArrayItemId { get; set; }

       

        [Display(Name = "Item Quantity")]
        public decimal[] ArraryItemQuantity { get; set; }


        public decimal[] ArrayPrice { get; set; }



    }
}
