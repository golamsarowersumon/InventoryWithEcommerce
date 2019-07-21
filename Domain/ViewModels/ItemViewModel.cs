using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Domain.ViewModels
{
    public class ItemViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public Nullable<int> SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }

        public Nullable<int> SubSubCategoryId { get; set; }
        public string SubSubCategoryName { get; set; }

        public Nullable<int> SubSubSubCategoryId { get; set; }
        public string SubSubSubCategoryName { get; set; }

        public Nullable<int> SubSubSubSubCategoryId { get; set; }
        public string SubSubSubSubCategoryName { get; set; }
      
        public Nullable<int> BrandId { get; set; }
        public string BrandName { get; set; }
      
        public Nullable<int> ModelId { get; set; }
        public string ModelName { get; set; }
        public int MethodId { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string ItemDetails { get; set; }
        public Nullable<int> Height { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Weight { get; set; }
  
        [Display(Name ="Images")]
        public string Product_Image { get; set; }
        public string Product_Image1 { get; set; }
        public string Product_Image2 { get; set; }

        public HttpPostedFileBase File_Image { get; set; }
        public HttpPostedFileBase File_Image1 { get; set; }
        public HttpPostedFileBase File_Image2 { get; set; }
    }
}
