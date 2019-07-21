using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DamagedItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DamagedItemId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string DamagedItemType { get; set; }
        public decimal DamageQuantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DamageDate { get; set; }
        public decimal PO_Price { get; set; }
        public String Item_Unique_Number { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DateOfPurchase { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> SubStoreId { get; set; }
        public Nullable<int> SubSubStoreId { get; set; }
        public Nullable<int> SubSubSubStoreId { get; set; }
        public Nullable<int> SubSubSubSubStoreId { get; set; }

        public virtual Store Store { get; set; }
        public virtual SubStore SubStore { get; set; }
        public virtual SubSubStore SubSubStore { get; set; }
        public virtual SubSubSubStore SubSubSubStore { get; set; }
        public virtual SubSubSubSubStore SubSubSubSubStore { get; set; }
        public virtual Item Item { get; set; }
        public virtual ICollection<InventoryMaster> InventoryMaster { get; set; }
    }
}
