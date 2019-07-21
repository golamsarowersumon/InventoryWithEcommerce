using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class RequisitionViewModel
    {
        public int ToStoreId { get; set; }
        public string ToStoreName { get; set; }
        public Nullable<int> ToSubStoreId { get; set; }
        public Nullable<int> ToSubSubStoreId { get; set; }
        public Nullable<int> ToSubSubSubStoreId { get; set; }
        public Nullable<int> ToSubSubSubSubStoreId { get; set; }

        public int FromStoreId { get; set; }
        public string FromStoreName { get; set; }
        public string ToSubStoreName { get; set; }
        public string ToSubSubStoreName { get; set; }
        public string ToSubSubSubStoreName { get; set; }
        public string ToSubSubSubSubStoreName { get; set; }

        public int RequisitionId { get; set; }
   
        public string RequisitionSent { get; set; }

     
        public bool IsChecked { get; set; }
        public string IsNull { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }
        public int TransactionQuantity { get; set; }
        public string Requisitionby { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Requisitiondate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? PossibleDeliverydate { get; set; }

        public string RequisitionRecieve { get; set; }
        public string RequisitionReceiveby { get; set; }

       

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? RequisitionReceiveDate { get; set; }
        public Nullable<int> PendingItemQuantity { get; set; }

        public Nullable<int> TransferTypeId { get; set; }
        public string TransferTypeName { get; set; }
        public Nullable<int> UnitId { get; set; }
        public Nullable<int> ConditionOfItemId { get; set; }
        public string ConditionOfItemName { get; set; }
        public string UnitName { get; set; }
        public int SID { get; set; }
        public string Name { get; set; }
        public string TransferCriteria { get; set; }

        public string ItemCriteria { get; set; }
        public string ProductCriteria { get; set; }
        public string StoreCriteria { get; set; }
        public string TransTypeCriteria { get; set; }
        public string ConditionCriteria { get; set; }
        public string DateCriteria { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ToDate { get; set; }
        public string ProjectName { get; set; }
        public string Objective { get; set; }
        public Decimal Price { get; set; }
        public Decimal EstimatePrice { get; set; }

        public List<RequisitionViewModel> RequisitionList { get; set; }



    }
}
