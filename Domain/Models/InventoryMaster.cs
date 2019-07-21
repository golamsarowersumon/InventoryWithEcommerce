using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class InventoryMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Inv_HD_ID { get; set; }
        public Nullable<int> PO_HD_ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DateOfPurchase { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DateOfReceive { get; set; }
       
        public decimal PO_GRAND_TOTAL { get; set; }
        public Nullable<int> ProcurementTypeId { get; set; }
        public Nullable<int> SupplierCompanyId { get; set; }
        public Nullable<int> SubContractCompanyId { get; set; }

        public Nullable<int> StoreId { get; set; }
        public Nullable<int> SubStoreId { get; set; }
        public Nullable<int> SubSubStoreId { get; set; }
        public Nullable<int> SubSubSubStoreId { get; set; }
        public Nullable<int> SubSubSubSubStoreId { get; set; }

        public Nullable<int> PurchasedBy_ProcureBy { get; set; }
        public string CreateBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> CreateDate { get; set; }
        public string UpdateBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> UpdateDate { get; set; }
        public  Nullable<int> TransferId { get; set; }
        public Nullable<int> DamagedItemId { get; set; }
        public Nullable<int> ProductionMasterId { get; set; }

        public virtual ProductionMaster ProductionMaster { get; set; }
        public virtual ProcurementMaster ProcurementMaster { get; set; }
        public virtual DamagedItem DamagedItem { get; set; }


        public virtual ICollection<InventoryDetail> InventoryDetail { get; set; }
        public virtual TransferMaster TransferMaster { get; set; }




    }
}
