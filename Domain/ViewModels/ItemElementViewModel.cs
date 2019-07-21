using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class ItemElementViewModel
    {
       
      
        public int ItemElementId { get; set; }
        
        public string ItemElementName { get; set; }

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

    }
}
