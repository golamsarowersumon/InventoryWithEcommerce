//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class SubSubCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubSubCategoryId { get; set; }
        public string SubSubCategoryName { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public Nullable<int> CategoryId { get; set; }

    
        public virtual ICollection<SubSubSubCategory> SubSubSubCategories { get; set; }
        public virtual ICollection<SubSubSubSubCategory> SubSubSubSubCategories { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }

        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<ProcurementDetails> ProcurementDetails { get; set; }
        public virtual ICollection<TemporaryTransferInformation> TemporaryTransferInformation { get; set; }

    }
}
