using Domain.Repositories;
using Domain.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Core.Services
{
   public class TransferService
    {
        public UnitOfWork unitOfWork;

        public TransferService(UnitOfWork UnitOfWork)
        {

            unitOfWork = UnitOfWork;
        }

        public string SaveTransferRecord(TransferMasterViewModel transferMasterViewModel, TransferDetailsViewModel[] TransferDetailsViewModel)
        {
            string msg = "";

            //Inserting data in TransferMaster table which is save in database only one time

            var TransferMaster = new TransferMaster
            {
                FromStoreId = transferMasterViewModel.FromStoreId,
               

                CreateBy = transferMasterViewModel.CreateBy,
                CreateDate =DateTime.Now.Date,
                UpdateBy = transferMasterViewModel.CreateBy,
                UpdateDate =DateTime.Now.Date

        };

            unitOfWork.TransferMasterRepository.Insert(TransferMaster);
            unitOfWork.Save();


            //Takin the TransferMaster table's primary key TransferId
            var THID = TransferMaster.TransferId;

            //Multiple Entry 
            foreach (var items in TransferDetailsViewModel)
            {

                //Inserting data in TransferDetails table which is save in database Multiple time
                var TransferDetails = new TransferDetails
                {
                   TransferOrderId=items.TransferOrderId,
                    ItemId = items.ItemId,
                    ToStoreId = items.ToStoreId,
                    TransactionQuantity = items.TransactionQuantity,
                    UnitId = items.UnitId,
                    TransferTypeId = items.TransferTypeId,
                    DateOfActualTransfer = items.DateOfActualTransfer,
                    //DateOfTransferOrder = items.DateOfTransferOrder,
                    ConditionOfItemId = items.ConditionOfItemId,
                    TransferId = THID,
                    Recieve = "Pending"




                }; 

                unitOfWork.TransferDetailsRepository.Insert(TransferDetails);
                unitOfWork.Save();

                var TDID = TransferDetails.TransferDetailId;

                // assign transaction value into qty
                var qty = items.TransactionQuantity;

                //Counting for Store
                int count = (from x in unitOfWork.StoreRepository.Get() where x.StoreId == transferMasterViewModel.FromStoreId select x.StoreId).Count();

                if (count == 1)
                {

                    // Calculating the total Available quantity for Transfer Store

                    var TotalAvailable = (from a in unitOfWork.InventoryMasterRepository.Get()
                                          join id in unitOfWork.InventoryDetailRepository.Get() on a.Inv_HD_ID equals id.Inv_HD_ID
                                          where id.ItemId == items.ItemId && a.StoreId == transferMasterViewModel.FromStoreId && a.SubStoreId == null && a.SubSubStoreId == null && a.SubSubSubStoreId == null && a.SubSubSubSubStoreId == null
                                          select (int?)id.AvailableQty).Sum() ?? 0;



                    if (qty > 0)
                    {


                        if (TotalAvailable >= qty)
                        {
                            // Geting the Method Name for Item
                            var B = from s in unitOfWork.ItemRepository.Get()
                                    join M in unitOfWork.MethodRepository.Get() on s.MethodId equals M.MethodId
                                    where s.ItemId == items.ItemId
                                    select new
                                    {
                                        M.MethodName
                                    };

                            var methodname = "";
                            foreach (var i in B)
                            {
                                methodname = i.MethodName;
                            }
                            if (methodname.ToUpper() == "FIFO")
                            {

                                //var shortList = GetDetails();

                                var shortList = (from im in unitOfWork.InventoryMasterRepository.Get()
                                         join id in unitOfWork.InventoryDetailRepository.Get() on im.Inv_HD_ID equals id.Inv_HD_ID
                                                 where id.AvailableQty > 0 && id.ItemId == items.ItemId && im.StoreId == transferMasterViewModel.FromStoreId && im.SubStoreId == null && im.SubSubStoreId == null && im.SubSubSubStoreId == null && im.SubSubSubSubStoreId == null
                                                 orderby id.TransactionQty ascending
                                                 select new
                                                 {
                                                     im.Inv_HD_ID,
                                                     im.StoreId,
                                                     im.SubStoreId,
                                                     im.SubSubStoreId,
                                                     im.SubSubSubStoreId,
                                                     im.SubSubSubSubStoreId,
                                                     im.SupplierCompanyId,
                                                     im.SubContractCompanyId,
                                                     id.Inv_Details_ID,
                                                     id.DateOfExpired,
                                                     id.DateOfNextMaintainance,
                                                     id.Barcode,
                                                     id.Item_Unique_Number,
                                                     id.Chesis_Number,
                                                     id.Engine_Number,
                                                  id.ItemId,
                                                  id.ProductId,
                                                  id.CountryId,
                                                  id.Principle_id,
                                                  id.ConditionOfItemId,
                                                  id.WarrantyId,
                                                  id.TransactionQty,
                                                  id.AvailableQty,
                                                  id.PO_Price,
                                                id.CategoryId,
                                                id.SubCategoryId,
                                                id.SubSubCategoryId,
                                                id.SubSubSubCategoryId,
                                                id.SubSubSubSubCategoryId,
                                                id.BrandId,
                                                id.ModelId,
                                                id.UnitId,
                                                id.MethodId
                                         }).ToList();


                                foreach (var item in shortList)
                                {
                                    if (qty != 0)
                                    {
                                        if (item.AvailableQty > 0)
                                        {

                                            if (item.AvailableQty >= qty)
                                            {
                                                var remainQuantity = qty;
                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID==item.Inv_Details_ID);
                                                var order = unitOfWork.TransferOrderRepository.Get().SingleOrDefault(b => b.TransferOrderId == items.TransferOrderId);
                                                if (result != null && order != null)
                                                {
                                                    result.AvailableQty = item.AvailableQty - qty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    order.TransferOrderSent = "Yes";
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID=item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = qty,
                                                        AvailableQty=qty,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId=item.SupplierCompanyId,
                                                        SubContractCompanyId=item.SubContractCompanyId,
                                                       
                                                        DateOfExpired=item.DateOfExpired,
                                                        DateOfNextMaintainance=item.DateOfNextMaintainance,
                                                        Barcode=item.Barcode,
                                                        Item_Unique_Number=item.Item_Unique_Number,
                                                        Chesis_Number=item.Chesis_Number,
                                                        Engine_Number=item.Engine_Number,
                                                        
                                                        ProductId=item.ProductId,
                                                        CountryId=item.CountryId,
                                                        Principle_id=item.Principle_id,
                                                        ConditionOfItemId=items.ConditionOfItemId,
                                                        WarrantyId=item.WarrantyId,
                                                        
                                                        
                                                        PO_Price=item.PO_Price,
                                                        PO_SubTotal= qty * item.PO_Price,
                                                        CategoryId =item.CategoryId,
                                                        SubCategoryId=item.SubCategoryId,
                                                        SubSubCategoryId=item.SubSubCategoryId,
                                                        SubSubSubCategoryId=item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId=item.SubSubSubSubCategoryId,
                                                        BrandId=item.BrandId,
                                                        ModelId=item.MethodId
                                                        
                                                       
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();
                                                    qty = qty - remainQuantity;
                                                    msg = "Successfully Transfered";
                                                }
                                                //unitOfWork.Save();


                                            }





                                            else
                                            {

                                                var available = item.AvailableQty;

                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);
                                                if (result != null)
                                                {
                                                    result.AvailableQty = result.AvailableQty - item.AvailableQty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = available,
                                                        AvailableQty = available,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = available * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId


                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();

                                                }

                                                qty = qty - available;
                                                available = 0;




                                            }
                                        }
                                    }
                                }


                            }

                            else if (methodname.ToUpper() == "LIFO")
                            {




                                var shortList = (from im in unitOfWork.InventoryMasterRepository.Get()
                                                 join id in unitOfWork.InventoryDetailRepository.Get() on im.Inv_HD_ID equals id.Inv_HD_ID
                                                 where id.AvailableQty > 0 && id.ItemId == items.ItemId && im.StoreId == transferMasterViewModel.FromStoreId && im.SubStoreId == null && im.SubSubStoreId == null && im.SubSubSubStoreId == null && im.SubSubSubSubStoreId == null
                                                 orderby id.TransactionQty descending
                                                 select new
                                                 {
                                                     im.Inv_HD_ID,
                                                     im.StoreId,
                                                     im.SubStoreId,
                                                     im.SubSubStoreId,
                                                     im.SubSubSubStoreId,
                                                     im.SubSubSubSubStoreId,
                                                     im.SupplierCompanyId,
                                                     im.SubContractCompanyId,
                                                     id.Inv_Details_ID,
                                                     id.DateOfExpired,
                                                     id.DateOfNextMaintainance,
                                                     id.Barcode,
                                                     id.Item_Unique_Number,
                                                     id.Chesis_Number,
                                                     id.Engine_Number,
                                                     id.ItemId,
                                                     id.ProductId,
                                                     id.CountryId,
                                                     id.Principle_id,
                                                     id.ConditionOfItemId,
                                                     id.WarrantyId,
                                                     id.TransactionQty,
                                                     id.AvailableQty,
                                                     id.PO_Price,
                                                     id.CategoryId,
                                                     id.SubCategoryId,
                                                     id.SubSubCategoryId,
                                                     id.SubSubSubCategoryId,
                                                     id.SubSubSubSubCategoryId,
                                                     id.BrandId,
                                                     id.ModelId,
                                                     id.UnitId,
                                                     id.MethodId
                                                 }).ToList();


                                foreach (var item in shortList)
                                {
                                    if (qty != 0)
                                    {
                                        if (item.AvailableQty > 0)
                                        {

                                            if (item.AvailableQty >= qty)
                                            {
                                                var remainQuantity = qty;
                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_Details_ID == item.Inv_Details_ID && b.Inv_HD_ID == item.Inv_HD_ID);
                                                var order = unitOfWork.TransferOrderRepository.Get().SingleOrDefault(b => b.TransferOrderId == items.TransferOrderId);
                                                if (result != null && order != null)
                                                {
                                                    result.AvailableQty = item.AvailableQty - qty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    order.TransferOrderSent = "Yes";
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {


                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = qty,
                                                        AvailableQty = qty,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = qty * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();
                                                    msg = "Successfully Transfered";
                                                    qty = qty - remainQuantity;
                                                }



                                            }

                                            else
                                            {

                                                var available = item.AvailableQty;

                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID==item.Inv_Details_ID);
                                                if (result != null)
                                                {
                                                    result.AvailableQty = result.AvailableQty - item.AvailableQty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = available,
                                                        AvailableQty = available,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,

                                                      
                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = available * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();

                                                }

                                                qty = qty - available;
                                                available = 0;




                                            }
                                        }
                                    }
                                }


                            }

                            else
                            {

                            }



                        }

                        else
                        {

                            msg = "You Don't Have Sufficient Item in Your Store";
                            return msg;

                        }




                    }


                    else
                    {

                        msg = "Item Quantity is Null!!";
                        return msg;

                    }


                }




                int count2 = (from x in unitOfWork.SubStoreRepository.Get() where x.SubStoreId == transferMasterViewModel.FromStoreId select x.SubStoreId).Count();

                if (count2 == 1)
                {



                    var storeid = unitOfWork.SubStoreRepository.Get().Where(a => a.SubStoreId == transferMasterViewModel.FromStoreId).Select(a => a.StoreId).Single();


                    // Calculating the total Available quantity for Transfer Store

                    var TotalAvailable = (from a in unitOfWork.InventoryMasterRepository.Get()
                                          join id in unitOfWork.InventoryDetailRepository.Get() on a.Inv_HD_ID equals id.Inv_HD_ID
                                          where id.ItemId == items.ItemId && a.StoreId == storeid && a.SubStoreId == transferMasterViewModel.FromStoreId && a.SubSubStoreId == null && a.SubSubSubStoreId == null && a.SubSubSubSubStoreId == null
                                          select (int?)id.AvailableQty).Sum() ?? 0;


                    if (qty > 0)
                    {


                        if (TotalAvailable >= qty)
                        {
                            // Geting the Method Name for Item
                            var B = from s in unitOfWork.ItemRepository.Get()
                                    join M in unitOfWork.MethodRepository.Get() on s.MethodId equals M.MethodId
                                    where s.ItemId == items.ItemId
                                    select new
                                    {
                                        M.MethodName
                                    };

                            var methodname = "";
                            foreach (var i in B)
                            {
                                methodname = i.MethodName;
                            }
                            if (methodname.ToUpper() == "FIFO")
                            {

                                var shortList = (from im in unitOfWork.InventoryMasterRepository.Get()
                                                 join id in unitOfWork.InventoryDetailRepository.Get() on im.Inv_HD_ID equals id.Inv_HD_ID
                                                 where id.AvailableQty > 0 && id.ItemId == items.ItemId && im.StoreId == storeid  && im.SubStoreId == transferMasterViewModel.FromStoreId && im.SubSubStoreId == null && im.SubSubSubStoreId == null && im.SubSubSubSubStoreId == null
                                                 orderby id.TransactionQty ascending
                                                 select new
                                                 {
                                                     im.Inv_HD_ID,
                                                     im.StoreId,
                                                     im.SubStoreId,
                                                     im.SubSubStoreId,
                                                     im.SubSubSubStoreId,
                                                     im.SubSubSubSubStoreId,
                                                     im.SupplierCompanyId,
                                                     im.SubContractCompanyId,
                                                     id.Inv_Details_ID,
                                                     id.DateOfExpired,
                                                     id.DateOfNextMaintainance,
                                                     id.Barcode,
                                                     id.Item_Unique_Number,
                                                     id.Chesis_Number,
                                                     id.Engine_Number,
                                                     id.ItemId,
                                                     id.ProductId,
                                                     id.CountryId,
                                                     id.Principle_id,
                                                     id.ConditionOfItemId,
                                                     id.WarrantyId,
                                                     id.TransactionQty,
                                                     id.AvailableQty,
                                                     id.PO_Price,
                                                     id.CategoryId,
                                                     id.SubCategoryId,
                                                     id.SubSubCategoryId,
                                                     id.SubSubSubCategoryId,
                                                     id.SubSubSubSubCategoryId,
                                                     id.BrandId,
                                                     id.ModelId,
                                                     id.UnitId,
                                                     id.MethodId
                                                 }).ToList();



                                foreach (var item in shortList)
                                {
                                    if (qty != 0)
                                    {
                                        if (item.AvailableQty > 0)
                                        {

                                            if (item.AvailableQty >= qty)
                                            {
                                                var remainQuantity = qty;
                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);
                                                var order = unitOfWork.TransferOrderRepository.Get().SingleOrDefault(b => b.TransferOrderId == items.TransferOrderId);
                                                if (result != null && order != null)
                                                {
                                                    result.AvailableQty = item.AvailableQty - qty;
                                                    result.PO_SubTotal = result.PO_SubTotal * result.AvailableQty;
                                                    order.TransferOrderSent = "Yes";
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {


                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = qty,
                                                        AvailableQty = qty,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = qty * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();
                                                    msg = "Successfully Transfered";

                                                    qty = qty - remainQuantity;
                                                }


                                            }





                                            else
                                            {

                                                var available = item.AvailableQty;

                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);
                                                if (result != null)
                                                {
                                                    result.AvailableQty = result.AvailableQty - item.AvailableQty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = available,
                                                        AvailableQty = available,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = available * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();

                                                }

                                                qty = qty - available;
                                                available = 0;




                                            }
                                        }
                                    }
                                }


                            }

                            else if (methodname.ToUpper() == "LIFO")
                            {

                                var shortList = (from im in unitOfWork.InventoryMasterRepository.Get()
                                                 join id in unitOfWork.InventoryDetailRepository.Get() on im.Inv_HD_ID equals id.Inv_HD_ID
                                                 where id.AvailableQty > 0 && id.ItemId == items.ItemId && im.StoreId == storeid && im.SubStoreId == transferMasterViewModel.FromStoreId && im.SubSubStoreId == null && im.SubSubSubStoreId == null && im.SubSubSubSubStoreId == null
                                                 orderby id.TransactionQty descending
                                                 select new
                                                 {
                                                     im.Inv_HD_ID,
                                                     im.StoreId,
                                                     im.SubStoreId,
                                                     im.SubSubStoreId,
                                                     im.SubSubSubStoreId,
                                                     im.SubSubSubSubStoreId,
                                                     im.SupplierCompanyId,
                                                     im.SubContractCompanyId,
                                                     id.Inv_Details_ID,
                                                     id.DateOfExpired,
                                                     id.DateOfNextMaintainance,
                                                     id.Barcode,
                                                     id.Item_Unique_Number,
                                                     id.Chesis_Number,
                                                     id.Engine_Number,
                                                     id.ItemId,
                                                     id.ProductId,
                                                     id.CountryId,
                                                     id.Principle_id,
                                                     id.ConditionOfItemId,
                                                     id.WarrantyId,
                                                     id.TransactionQty,
                                                     id.AvailableQty,
                                                     id.PO_Price,
                                                     id.CategoryId,
                                                     id.SubCategoryId,
                                                     id.SubSubCategoryId,
                                                     id.SubSubSubCategoryId,
                                                     id.SubSubSubSubCategoryId,
                                                     id.BrandId,
                                                     id.ModelId,
                                                     id.UnitId,
                                                     id.MethodId
                                                 }).ToList();


                                foreach (var item in shortList)
                                {
                                    if (qty != 0)
                                    {
                                        if (item.AvailableQty > 0)
                                        {

                                            if (item.AvailableQty >= qty)
                                            {
                                                var remainQuantity = qty;
                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);
                                                var order = unitOfWork.TransferOrderRepository.Get().SingleOrDefault(b => b.TransferOrderId == items.TransferOrderId);
                                                if (result != null && order != null)
                                                {
                                                    result.AvailableQty = item.AvailableQty - qty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    order.TransferOrderSent = "Yes";
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {


                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = qty,
                                                        AvailableQty = qty,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = qty * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();
                                                    msg = "Successfully Transfered";
                                                    qty = qty - remainQuantity;
                                                }



                                            }

                                            else
                                            {

                                                var available = item.AvailableQty;

                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);
                                                if (result != null)
                                                {
                                                    result.AvailableQty = result.AvailableQty - item.AvailableQty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = available,
                                                        AvailableQty = available,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = available * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();

                                                }

                                                qty = qty - available;
                                                available = 0;




                                            }
                                        }
                                    }
                                }


                            }

                            else
                            {

                            }



                        }

                        else
                        {

                            msg = "You Don't Have Sufficient Item in Your Store";
                            return msg;

                        }




                    }


                    else
                    {

                        msg = "Item Quantity is Null!!";
                        return msg;

                    }


                }



                int count3 = (from x in unitOfWork.SubSubStoreRepository.Get() where x.SubSubStoreId == transferMasterViewModel.FromStoreId select x.SubSubStoreId).Count();

                if (count3 == 1)
                {


                    var storeid = unitOfWork.SubSubStoreRepository.Get().Where(a => a.SubSubStoreId == transferMasterViewModel.FromStoreId).Select(a => a.StoreId).Single();
                    var substoreid = unitOfWork.SubSubStoreRepository.Get().Where(a => a.SubSubStoreId == transferMasterViewModel.FromStoreId).Select(a => a.SubStoreId).Single();


                    // Calculating the total Available quantity for Transfer Store
                    var TotalAvailable = (from a in unitOfWork.InventoryMasterRepository.Get()
                                          join id in unitOfWork.InventoryDetailRepository.Get() on a.Inv_HD_ID equals id.Inv_HD_ID
                                          where id.ItemId == items.ItemId && a.StoreId == storeid && a.SubStoreId == substoreid  && a.SubSubStoreId == transferMasterViewModel.FromStoreId && a.SubSubSubStoreId == null && a.SubSubSubSubStoreId == null
                                          select (int?)id.AvailableQty).Sum() ?? 0;

                    if (qty > 0)
                    {


                        if (TotalAvailable >= qty)
                        {
                            // Geting the Method Name for Item
                            var B = from s in unitOfWork.ItemRepository.Get()
                                    join M in unitOfWork.MethodRepository.Get() on s.MethodId equals M.MethodId
                                    where s.ItemId == items.ItemId
                                    select new
                                    {
                                        M.MethodName
                                    };

                            var methodname = "";
                            foreach (var i in B)
                            {
                                methodname = i.MethodName;
                            }
                            if (methodname.ToUpper() == "FIFO")
                            {

                                var shortList = (from im in unitOfWork.InventoryMasterRepository.Get()
                                                 join id in unitOfWork.InventoryDetailRepository.Get() on im.Inv_HD_ID equals id.Inv_HD_ID
                                                 where id.AvailableQty > 0 && id.ItemId == items.ItemId && im.StoreId == storeid && im.SubStoreId == substoreid && im.SubSubStoreId == transferMasterViewModel.FromStoreId && im.SubSubSubStoreId == null && im.SubSubSubSubStoreId == null
                                                 orderby id.TransactionQty ascending
                                                 select new
                                                 {
                                                     im.Inv_HD_ID,
                                                     im.StoreId,
                                                     im.SubStoreId,
                                                     im.SubSubStoreId,
                                                     im.SubSubSubStoreId,
                                                     im.SubSubSubSubStoreId,
                                                     im.SupplierCompanyId,
                                                     im.SubContractCompanyId,
                                                     id.Inv_Details_ID,
                                                     id.DateOfExpired,
                                                     id.DateOfNextMaintainance,
                                                     id.Barcode,
                                                     id.Item_Unique_Number,
                                                     id.Chesis_Number,
                                                     id.Engine_Number,
                                                     id.ItemId,
                                                     id.ProductId,
                                                     id.CountryId,
                                                     id.Principle_id,
                                                     id.ConditionOfItemId,
                                                     id.WarrantyId,
                                                     id.TransactionQty,
                                                     id.AvailableQty,
                                                     id.PO_Price,
                                                     id.CategoryId,
                                                     id.SubCategoryId,
                                                     id.SubSubCategoryId,
                                                     id.SubSubSubCategoryId,
                                                     id.SubSubSubSubCategoryId,
                                                     id.BrandId,
                                                     id.ModelId,
                                                     id.UnitId,
                                                     id.MethodId
                                                 }).ToList();


                                foreach (var item in shortList)
                                {
                                    if (qty != 0)
                                    {
                                        if (item.AvailableQty > 0)
                                        {

                                            if (item.AvailableQty >= qty)
                                            {
                                                var remainQuantity = qty;
                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);

                                                var order = unitOfWork.TransferOrderRepository.Get().SingleOrDefault(b => b.TransferOrderId == items.TransferOrderId);
                                                if (result != null && order != null)
                                                {
                                                    result.AvailableQty = item.AvailableQty - qty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    order.TransferOrderSent = "Yes";
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = qty,
                                                        AvailableQty = qty,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = qty * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();
                                                    qty = qty - remainQuantity;

                                                    msg = "Successfully Transfered";
                                                }


                                            }





                                            else
                                            {

                                                var available = item.AvailableQty;

                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);
                                                if (result != null)
                                                {
                                                    result.AvailableQty = result.AvailableQty - item.AvailableQty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = available,
                                                        AvailableQty = available,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = available * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();

                                                }

                                                qty = qty - available;
                                                available = 0;




                                            }
                                        }
                                    }
                                }


                            }

                            else if (methodname.ToUpper() == "LIFO")
                            {

                                var shortList = (from im in unitOfWork.InventoryMasterRepository.Get()
                                                 join id in unitOfWork.InventoryDetailRepository.Get() on im.Inv_HD_ID equals id.Inv_HD_ID
                                                 where id.AvailableQty > 0 && id.ItemId == items.ItemId && im.StoreId == storeid && im.SubStoreId == substoreid && im.SubSubStoreId == transferMasterViewModel.FromStoreId && im.SubSubSubStoreId ==null  && im.SubSubSubSubStoreId == null
                                                 orderby id.TransactionQty descending
                                                 select new
                                                 {
                                                     im.Inv_HD_ID,
                                                     im.StoreId,
                                                     im.SubStoreId,
                                                     im.SubSubStoreId,
                                                     im.SubSubSubStoreId,
                                                     im.SubSubSubSubStoreId,
                                                     im.SupplierCompanyId,
                                                     im.SubContractCompanyId,
                                                     id.Inv_Details_ID,
                                                     id.DateOfExpired,
                                                     id.DateOfNextMaintainance,
                                                     id.Barcode,
                                                     id.Item_Unique_Number,
                                                     id.Chesis_Number,
                                                     id.Engine_Number,
                                                     id.ItemId,
                                                     id.ProductId,
                                                     id.CountryId,
                                                     id.Principle_id,
                                                     id.ConditionOfItemId,
                                                     id.WarrantyId,
                                                     id.TransactionQty,
                                                     id.AvailableQty,
                                                     id.PO_Price,
                                                     id.CategoryId,
                                                     id.SubCategoryId,
                                                     id.SubSubCategoryId,
                                                     id.SubSubSubCategoryId,
                                                     id.SubSubSubSubCategoryId,
                                                     id.BrandId,
                                                     id.ModelId,
                                                     id.UnitId,
                                                     id.MethodId
                                                 }).ToList();


                                foreach (var item in shortList)
                                {
                                    if (qty != 0)
                                    {
                                        if (item.AvailableQty > 0)
                                        {

                                            if (item.AvailableQty >= qty)
                                            {
                                                var remainQuantity = qty;
                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);

                                                var order = unitOfWork.TransferOrderRepository.Get().SingleOrDefault(b => b.TransferOrderId == items.TransferOrderId);
                                                if (result != null && order != null)
                                                {
                                                    result.AvailableQty = item.AvailableQty - qty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    order.TransferOrderSent = "Yes";
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = qty,
                                                        AvailableQty = qty,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = qty * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();
                                                    qty = qty - remainQuantity;
                                                    msg = "Successfully Transfered";
                                                }
                                                unitOfWork.Save();


                                            }

                                            else
                                            {

                                                var available = item.AvailableQty;

                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);
                                                if (result != null)
                                                {
                                                    result.AvailableQty = result.AvailableQty - item.AvailableQty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = available,
                                                        AvailableQty = available,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = available * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();

                                                }

                                                qty = qty - available;
                                                available = 0;




                                            }
                                        }
                                    }
                                }


                            }

                            else
                            {

                            }



                        }

                        else
                        {

                            msg = "You Don't Have Sufficient Item in Your Store";
                            return msg;

                        }




                    }


                    else
                    {

                        msg = "Item Quantity is Null!!";
                        return msg;

                    }

                }




                int count4 = (from x in unitOfWork.SubSubSubStoreRepository.Get() where x.SubSubSubStoreId == transferMasterViewModel.FromStoreId select x.SubSubSubStoreId).Count();

                if (count4 == 1)
                {


                    var storeid = unitOfWork.SubSubSubStoreRepository.Get().Where(a => a.SubSubSubStoreId == transferMasterViewModel.FromStoreId).Select(a => a.StoreId).Single();
                    var substoreid = unitOfWork.SubSubSubStoreRepository.Get().Where(a => a.SubSubSubStoreId == transferMasterViewModel.FromStoreId).Select(a => a.SubStoreId).Single();
                    var subsubstoreid = unitOfWork.SubSubSubStoreRepository.Get().Where(a => a.SubSubSubStoreId == transferMasterViewModel.FromStoreId).Select(a => a.SubSubStoreId).Single();


                    // Calculating the total Available quantity for Transfer Store
                    var TotalAvailable = (from a in unitOfWork.InventoryMasterRepository.Get()
                                          join id in unitOfWork.InventoryDetailRepository.Get() on a.Inv_HD_ID equals id.Inv_HD_ID
                                          where id.ItemId == items.ItemId && a.StoreId == storeid && a.SubStoreId == substoreid && a.SubSubStoreId == subsubstoreid && a.SubSubSubStoreId == transferMasterViewModel.FromStoreId && a.SubSubSubSubStoreId == null
                                          select (int?)id.AvailableQty).Sum() ?? 0;

                    if (qty > 0)
                    {


                        if (TotalAvailable >= qty)
                        {
                            // Geting the Method Name for Item
                            var B = from s in unitOfWork.ItemRepository.Get()
                                    join M in unitOfWork.MethodRepository.Get() on s.MethodId equals M.MethodId
                                    where s.ItemId == items.ItemId
                                    select new
                                    {
                                        M.MethodName
                                    };

                            var methodname = "";
                            foreach (var i in B)
                            {
                                methodname = i.MethodName;
                            }
                            if (methodname.ToUpper() == "FIFO")
                            {

                                var shortList = (from im in unitOfWork.InventoryMasterRepository.Get()
                                                 join id in unitOfWork.InventoryDetailRepository.Get() on im.Inv_HD_ID equals id.Inv_HD_ID
                                                 where id.AvailableQty > 0 && id.ItemId == items.ItemId && im.StoreId == storeid && im.SubStoreId == substoreid && im.SubSubStoreId == subsubstoreid  && im.SubSubSubStoreId == transferMasterViewModel.FromStoreId && im.SubSubSubSubStoreId == null
                                                 orderby id.TransactionQty ascending
                                                 select new
                                                 {
                                                     im.Inv_HD_ID,
                                                     im.StoreId,
                                                     im.SubStoreId,
                                                     im.SubSubStoreId,
                                                     im.SubSubSubStoreId,
                                                     im.SubSubSubSubStoreId,
                                                     im.SupplierCompanyId,
                                                     im.SubContractCompanyId,
                                                     id.Inv_Details_ID,
                                                     id.DateOfExpired,
                                                     id.DateOfNextMaintainance,
                                                     id.Barcode,
                                                     id.Item_Unique_Number,
                                                     id.Chesis_Number,
                                                     id.Engine_Number,
                                                     id.ItemId,
                                                     id.ProductId,
                                                     id.CountryId,
                                                     id.Principle_id,
                                                     id.ConditionOfItemId,
                                                     id.WarrantyId,
                                                     id.TransactionQty,
                                                     id.AvailableQty,
                                                     id.PO_Price,
                                                     id.CategoryId,
                                                     id.SubCategoryId,
                                                     id.SubSubCategoryId,
                                                     id.SubSubSubCategoryId,
                                                     id.SubSubSubSubCategoryId,
                                                     id.BrandId,
                                                     id.ModelId,
                                                     id.UnitId,
                                                     id.MethodId
                                                 }).ToList();



                                foreach (var item in shortList)
                                {
                                    if (qty != 0)
                                    {
                                        if (item.AvailableQty > 0)
                                        {

                                            if (item.AvailableQty >= qty)
                                            {
                                                var remainQuantity = qty;
                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);

                                                var order = unitOfWork.TransferOrderRepository.Get().SingleOrDefault(b => b.TransferOrderId == items.TransferOrderId);
                                                if (result != null && order != null)
                                                {
                                                    result.AvailableQty = item.AvailableQty - qty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    order.TransferOrderSent = "Yes";
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = qty,
                                                        AvailableQty = qty,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = qty * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();
                                                    msg = "Successfully Transfered";
                                                    qty = qty - remainQuantity;
                                                }


                                            }





                                            else
                                            {

                                                var available = item.AvailableQty;

                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);
                                                if (result != null)
                                                {
                                                    result.AvailableQty = result.AvailableQty - item.AvailableQty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = available,
                                                        AvailableQty = available,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = available * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();

                                                }

                                                qty = qty - available;
                                                available = 0;




                                            }
                                        }
                                    }
                                }


                            }

                            else if (methodname.ToUpper() == "LIFO")
                            {

                                var shortList = (from im in unitOfWork.InventoryMasterRepository.Get()
                                                 join id in unitOfWork.InventoryDetailRepository.Get() on im.Inv_HD_ID equals id.Inv_HD_ID
                                                 where id.AvailableQty > 0 && id.ItemId == items.ItemId && im.StoreId == storeid && im.SubStoreId == substoreid && im.SubSubStoreId == subsubstoreid  && im.SubSubSubStoreId == transferMasterViewModel.FromStoreId && im.SubSubSubSubStoreId == null
                                                 orderby id.TransactionQty descending
                                                 select new
                                                 {
                                                     im.Inv_HD_ID,
                                                     im.StoreId,
                                                     im.SubStoreId,
                                                     im.SubSubStoreId,
                                                     im.SubSubSubStoreId,
                                                     im.SubSubSubSubStoreId,
                                                     im.SupplierCompanyId,
                                                     im.SubContractCompanyId,
                                                     id.Inv_Details_ID,
                                                     id.DateOfExpired,
                                                     id.DateOfNextMaintainance,
                                                     id.Barcode,
                                                     id.Item_Unique_Number,
                                                     id.Chesis_Number,
                                                     id.Engine_Number,
                                                     id.ItemId,
                                                     id.ProductId,
                                                     id.CountryId,
                                                     id.Principle_id,
                                                     id.ConditionOfItemId,
                                                     id.WarrantyId,
                                                     id.TransactionQty,
                                                     id.AvailableQty,
                                                     id.PO_Price,
                                                     id.CategoryId,
                                                     id.SubCategoryId,
                                                     id.SubSubCategoryId,
                                                     id.SubSubSubCategoryId,
                                                     id.SubSubSubSubCategoryId,
                                                     id.BrandId,
                                                     id.ModelId,
                                                     id.UnitId,
                                                     id.MethodId
                                                 }).ToList();



                                foreach (var item in shortList)
                                {
                                    if (qty != 0)
                                    {
                                        if (item.AvailableQty > 0)
                                        {

                                            if (item.AvailableQty >= qty)
                                            {
                                                var remainQuantity = qty;
                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);

                                                var order = unitOfWork.TransferOrderRepository.Get().SingleOrDefault(b => b.TransferOrderId == items.TransferOrderId);
                                                if (result != null && order != null)
                                                {
                                                    result.AvailableQty = item.AvailableQty - qty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    order.TransferOrderSent = "Yes";
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = qty,
                                                        AvailableQty = qty,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = qty * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();
                                                    msg = "Successfully Transfered";
                                                    qty = qty - remainQuantity;
                                                }



                                            }

                                            else
                                            {

                                                var available = item.AvailableQty;

                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);
                                                if (result != null)
                                                {
                                                    result.AvailableQty = result.AvailableQty - item.AvailableQty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = available,
                                                        AvailableQty = available,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = available * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();

                                                }

                                                qty = qty - available;
                                                available = 0;




                                            }
                                        }
                                    }
                                }


                            }

                            else
                            {

                            }



                        }

                        else
                        {

                            msg = "You Don't Have Sufficient Item in Your Store";
                            return msg;

                        }




                    }


                    else
                    {

                        msg = "Item Quantity is Null!!";
                        return msg;

                    }



                }


                int count5 = (from x in unitOfWork.SubSubSubSubStoreRepository.Get() where x.SubSubSubSubStoreId == transferMasterViewModel.FromStoreId select x.SubSubSubSubStoreId).Count();

                if (count5 == 1)
                {


                    var storeid = unitOfWork.SubSubSubSubStoreRepository.Get().Where(a => a.SubSubSubSubStoreId == transferMasterViewModel.FromStoreId).Select(a => a.StoreId).Single();
                    var substoreid = unitOfWork.SubSubSubSubStoreRepository.Get().Where(a => a.SubSubSubSubStoreId == transferMasterViewModel.FromStoreId).Select(a => a.SubStoreId).Single();
                    var subsubstoreid = unitOfWork.SubSubSubSubStoreRepository.Get().Where(a => a.SubSubSubSubStoreId == transferMasterViewModel.FromStoreId).Select(a => a.SubSubStoreId).Single();
                    var subsubsubstoreid = unitOfWork.SubSubSubSubStoreRepository.Get().Where(a => a.SubSubSubSubStoreId == transferMasterViewModel.FromStoreId).Select(a => a.SubSubSubStoreId).Single();


                    // Calculating the total Available quantity for Transfer Store
                    var TotalAvailable = (from a in unitOfWork.InventoryMasterRepository.Get()
                                          join id in unitOfWork.InventoryDetailRepository.Get() on a.Inv_HD_ID equals id.Inv_HD_ID
                                          where id.ItemId == items.ItemId && a.StoreId == storeid && a.SubStoreId == substoreid && a.SubSubStoreId == subsubstoreid && a.SubSubSubStoreId == subsubsubstoreid && a.SubSubSubSubStoreId == transferMasterViewModel.FromStoreId
                                          select (int?)id.AvailableQty).Sum() ?? 0;

                    if (qty > 0)
                    {


                        if (TotalAvailable >= qty)
                        {
                            // Geting the Method Name for Item
                            var B = from s in unitOfWork.ItemRepository.Get()
                                    join M in unitOfWork.MethodRepository.Get() on s.MethodId equals M.MethodId
                                    where s.ItemId == items.ItemId
                                    select new
                                    {
                                        M.MethodName
                                    };

                            var methodname = "";
                            foreach (var i in B)
                            {
                                methodname = i.MethodName;
                            }
                            if (methodname.ToUpper() == "FIFO")
                            {

                                var shortList = (from im in unitOfWork.InventoryMasterRepository.Get()
                                                 join id in unitOfWork.InventoryDetailRepository.Get() on im.Inv_HD_ID equals id.Inv_HD_ID
                                                 where id.AvailableQty > 0 && id.ItemId == items.ItemId && im.StoreId == storeid && im.SubStoreId == substoreid && im.SubSubStoreId == subsubstoreid && im.SubSubSubStoreId == subsubsubstoreid  && im.SubSubSubSubStoreId == transferMasterViewModel.FromStoreId
                                                 orderby id.TransactionQty ascending
                                                 select new
                                                 {
                                                     im.Inv_HD_ID,
                                                     im.StoreId,
                                                     im.SubStoreId,
                                                     im.SubSubStoreId,
                                                     im.SubSubSubStoreId,
                                                     im.SubSubSubSubStoreId,
                                                     im.SupplierCompanyId,
                                                     im.SubContractCompanyId,
                                                     id.Inv_Details_ID,
                                                     id.DateOfExpired,
                                                     id.DateOfNextMaintainance,
                                                     id.Barcode,
                                                     id.Item_Unique_Number,
                                                     id.Chesis_Number,
                                                     id.Engine_Number,
                                                     id.ItemId,
                                                     id.ProductId,
                                                     id.CountryId,
                                                     id.Principle_id,
                                                     id.ConditionOfItemId,
                                                     id.WarrantyId,
                                                     id.TransactionQty,
                                                     id.AvailableQty,
                                                     id.PO_Price,
                                                     id.CategoryId,
                                                     id.SubCategoryId,
                                                     id.SubSubCategoryId,
                                                     id.SubSubSubCategoryId,
                                                     id.SubSubSubSubCategoryId,
                                                     id.BrandId,
                                                     id.ModelId,
                                                     id.UnitId,
                                                     id.MethodId
                                                 }).ToList();


                                foreach (var item in shortList)
                                {
                                    if (qty != 0)
                                    {
                                        if (item.AvailableQty > 0)
                                        {

                                            if (item.AvailableQty >= qty)
                                            {
                                                var remainQuantity = qty;
                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);

                                                var order = unitOfWork.TransferOrderRepository.Get().SingleOrDefault(b => b.TransferOrderId == items.TransferOrderId);
                                                if (result != null && order != null)
                                                {
                                                    result.AvailableQty = item.AvailableQty - qty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    order.TransferOrderSent = "Yes";
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = qty,
                                                        AvailableQty = qty,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = qty * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();
                                                    msg = "Successfully Transfered";
                                                    qty = qty - remainQuantity;
                                                }


                                            }





                                            else
                                            {

                                                var available = item.AvailableQty;

                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);
                                                if (result != null)
                                                {
                                                    result.AvailableQty = result.AvailableQty - item.AvailableQty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = available,
                                                        AvailableQty = available,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = available * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();

                                                }

                                                qty = qty - available;
                                                available = 0;




                                            }
                                        }
                                    }
                                }


                            }

                            else if (methodname.ToUpper() == "LIFO")
                            {

                                var shortList = (from im in unitOfWork.InventoryMasterRepository.Get()
                                                 join id in unitOfWork.InventoryDetailRepository.Get() on im.Inv_HD_ID equals id.Inv_HD_ID
                                                 where id.AvailableQty > 0 && id.ItemId == items.ItemId && im.StoreId == storeid && im.SubStoreId == substoreid && im.SubSubStoreId == subsubstoreid && im.SubSubSubStoreId == subsubsubstoreid && im.SubSubSubSubStoreId == transferMasterViewModel.FromStoreId
                                                 orderby id.TransactionQty descending
                                                 select new
                                                 {
                                                     im.Inv_HD_ID,
                                                     im.StoreId,
                                                     im.SubStoreId,
                                                     im.SubSubStoreId,
                                                     im.SubSubSubStoreId,
                                                     im.SubSubSubSubStoreId,
                                                     im.SupplierCompanyId,
                                                     im.SubContractCompanyId,
                                                     id.Inv_Details_ID,
                                                     id.DateOfExpired,
                                                     id.DateOfNextMaintainance,
                                                     id.Barcode,
                                                     id.Item_Unique_Number,
                                                     id.Chesis_Number,
                                                     id.Engine_Number,
                                                     id.ItemId,
                                                     id.ProductId,
                                                     id.CountryId,
                                                     id.Principle_id,
                                                     id.ConditionOfItemId,
                                                     id.WarrantyId,
                                                     id.TransactionQty,
                                                     id.AvailableQty,
                                                     id.PO_Price,
                                                     id.CategoryId,
                                                     id.SubCategoryId,
                                                     id.SubSubCategoryId,
                                                     id.SubSubSubCategoryId,
                                                     id.SubSubSubSubCategoryId,
                                                     id.BrandId,
                                                     id.ModelId,
                                                     id.UnitId,
                                                     id.MethodId
                                                 }).ToList();

                                foreach (var item in shortList)
                                {
                                    if (qty != 0)
                                    {
                                        if (item.AvailableQty > 0)
                                        {

                                            if (item.AvailableQty >= qty)
                                            {
                                                var remainQuantity = qty;
                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);

                                                var order = unitOfWork.TransferOrderRepository.Get().SingleOrDefault(b => b.TransferOrderId == items.TransferOrderId);
                                                if (result != null && order != null)
                                                {
                                                    result.AvailableQty = item.AvailableQty - qty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    order.TransferOrderSent = "Yes";
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = qty,
                                                        AvailableQty = qty,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = qty * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();
                                                    msg = "Successfully Transfered";
                                                    qty = qty - remainQuantity;
                                                }



                                            }

                                            else
                                            {

                                                var available = item.AvailableQty;

                                                var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_HD_ID == item.Inv_HD_ID && b.Inv_Details_ID == item.Inv_Details_ID);
                                                if (result != null)
                                                {
                                                    result.AvailableQty = result.AvailableQty - item.AvailableQty;
                                                    result.PO_SubTotal = result.PO_Price * result.AvailableQty;
                                                    var temporaryTransferInformation = new TemporaryTransferInformation
                                                    {

                                                        Inv_HD_ID = item.Inv_HD_ID,
                                                        Inv_Details_ID = item.Inv_Details_ID,
                                                        TransferId = THID,
                                                        TransferDetailId = TDID,
                                                        ItemId = item.ItemId,
                                                        TransactionQty = available,
                                                        AvailableQty = available,
                                                        DateOfActualTransfer = items.DateOfActualTransfer,
                                                        DateOfTransferOrder = items.DateOfTransferOrder,
                                                        ToStoreId = items.ToStoreId,
                                                        FromStoreId = transferMasterViewModel.FromStoreId,
                                                        TransferTypeId = items.TransferTypeId,
                                                        UnitId = item.UnitId,
                                                        MethodId = item.MethodId,
                                                        Recieve = "Pending",
                                                        SupplierCompanyId = item.SupplierCompanyId,
                                                        SubContractCompanyId = item.SubContractCompanyId,

                                                        DateOfExpired = item.DateOfExpired,
                                                        DateOfNextMaintainance = item.DateOfNextMaintainance,
                                                        Barcode = item.Barcode,
                                                        Item_Unique_Number = item.Item_Unique_Number,
                                                        Chesis_Number = item.Chesis_Number,
                                                        Engine_Number = item.Engine_Number,

                                                        ProductId = item.ProductId,
                                                        CountryId = item.CountryId,
                                                        Principle_id = item.Principle_id,
                                                        ConditionOfItemId = items.ConditionOfItemId,
                                                        WarrantyId = item.WarrantyId,


                                                        PO_Price = item.PO_Price,
                                                        PO_SubTotal = available * item.PO_Price,
                                                        CategoryId = item.CategoryId,
                                                        SubCategoryId = item.SubCategoryId,
                                                        SubSubCategoryId = item.SubSubCategoryId,
                                                        SubSubSubCategoryId = item.SubSubSubCategoryId,
                                                        SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                                        BrandId = item.BrandId,
                                                        ModelId = item.MethodId
                                                    };
                                                    unitOfWork.TemporaryTransferInformationRepository.Insert(temporaryTransferInformation);
                                                    unitOfWork.Save();

                                                }

                                                qty = qty - available;
                                                available = 0;




                                            }
                                        }
                                    }
                                }


                            }

                            else
                            {

                            }



                        }

                        else
                        {

                            msg = "You Don't Have Sufficient Item in Your Store";
                            return msg;

                        }




                    }


                    else
                    {

                        msg = "Item Quantity is Null!!";
                        return msg;

                    }




                }

            }


            return msg;

        }



        public int GetPending(string id)
        {
            var sid = Convert.ToInt32(id);
            int count = (from x in unitOfWork.TransferMasterRepository.Get() join td in unitOfWork.TransferDetailsRepository.Get() on x.TransferId equals td.TransferId where td.ToStoreId==sid &&  td.Recieve == "Pending" select td.Recieve).Count();
            return count;

        }

        public List<TransferViewModel> TransferPendingInformation(string strstoreid)
        {

            var stid = Convert.ToInt32(strstoreid);
            var data = GetAllPendingReceiveInfo(stid);

            return data;



        }
      

        public string ReceiveTransfer(TransferViewModel TransferViewModel)
        {
            decimal totaltransferquantity = 0;
            var flag = 0;
            var IMID = 0;
            var stid = Convert.ToInt32(TransferViewModel.StoreId);
            var getall = GetAllPendingReceiveInfo(stid);


            for (int i = 0; i < TransferViewModel.TransferViewModelList.Count; i++)
            {
                var transferid = TransferViewModel.TransferViewModelList[i].TransferId;
                var temptransinfoid = TransferViewModel.TransferViewModelList[i].Id;
                var inventoryid = TransferViewModel.TransferViewModelList[i].Inv_HD_ID;
                var transferDetailsid = TransferViewModel.TransferViewModelList[i].TransferDetailsId;
                var transactionquantity = TransferViewModel.TransferViewModelList[i].TransactionQuantity;





                foreach (var item in getall)
                {
                    if (item.TransferId == transferid && item.Inv_HD_ID == inventoryid && item.TransferDetailsId == transferDetailsid && item.Id== temptransinfoid)
                    {
                        int count = (from x in unitOfWork.StoreRepository.Get() where x.StoreId == TransferViewModel.StoreId select x.StoreId).Count();
                        if (count == 1)
                        {


                            var transferquantity = (from a in unitOfWork.TemporaryTransferInformationRepository.Get()
                                                    where a.ItemId == item.ItemId && a.TransferId == transferid && a.TransferDetailId == transferDetailsid && a.Inv_HD_ID == inventoryid && a.Id == temptransinfoid
                                                    select (int?)a.TransactionQty).SingleOrDefault();


                            //var transferquantity = unitOfWork.TemporaryTransferInformationRepository.Get().Where(a => (((((a.ItemId == item.ItemId) && (a.TransferId == transferid)) && (a.TransferDetailId == transferDetailsid)) && (a.InventoryId == inventoryid)) && (a.Id == temptransinfoid))).Select(a => a.TransactionQuantity).SingleOrDefault();
                            if (transferquantity >= transactionquantity)
                            {
                                if (transferquantity == transactionquantity)
                                {
                                    totaltransferquantity = totaltransferquantity + transactionquantity;

                                    //var result = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId==item.TransferId);

                                    var tdresult = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId == item.TransferId);
                                    var tresult = unitOfWork.TemporaryTransferInformationRepository.Get().SingleOrDefault(b => b.Id == temptransinfoid);
                                    if (tresult != null)
                                    {
                                        tdresult.Recieve = "Received";
                                        tdresult.RecieveBy = TransferViewModel.RecieveBy;
                                        tdresult.ReceiveItemQuantity = transactionquantity;
                                        tdresult.PendingItemQuantity = transferquantity - transactionquantity;
                                        tdresult.DateOfReceive = DateTime.Now.Date;
                                        tresult.Recieve = "Received";
                                        tresult.ReceiveItemQuantity = transactionquantity;
                                        tresult.PendingItemQuantity = transferquantity - transactionquantity;
                                        

                                        if (item.TransferId == transferid && item.Inv_HD_ID == inventoryid && item.TransferDetailsId == transferDetailsid && item.Id == temptransinfoid)
                                        { 
                                        var InventoryMaster = new InventoryMaster
                                        {

                                            StoreId = item.ToStoreId,
                                            CreateBy = TransferViewModel.RecieveBy,
                                            CreateDate = DateTime.Now,
                                            TransferId=item.TransferId,
                                            SupplierCompanyId = item.SupplierCompanyId,
                                            SubContractCompanyId = item.SubContractCompanyId
                                        };
                                            unitOfWork.InventoryMasterRepository.Insert(InventoryMaster);
                                            unitOfWork.Save();
                                            flag = 1;
                                             IMID = InventoryMaster.Inv_HD_ID;
                                        }
                                        
                                        var InventoryDetail = new InventoryDetail
                                        {
                                            Inv_HD_ID = IMID,
                                            ItemId = item.ItemId,
                                            TransactionQty = transactionquantity,
                                            AvailableQty = transactionquantity,
                                            UnitId = item.UnitId,
                                            MethodId = item.MethodId,
                                           
                                       

                                            DateOfExpired = item.DateOfExpired,
                                            DateOfNextMaintainance = item.DateOfNextMaintainance,
                                            Barcode = item.Barcode,
                                            Item_Unique_Number = item.Item_Unique_Number,
                                            Chesis_Number = item.Chesis_Number,
                                            Engine_Number = item.Engine_Number,

                                            ProductId = item.ProductId,
                                            CountryId = item.CountryId,
                                            Principle_id = item.Principle_id,
                                            ConditionOfItemId = item.ConditionOfItemId,
                                            WarrantyId = item.WarrantyId,
                                            PO_Price = item.PO_Price,
                                            PO_SubTotal = transactionquantity * item.PO_Price,
                                            CategoryId = item.CategoryId,
                                            SubCategoryId = item.SubCategoryId,
                                            SubSubCategoryId = item.SubSubCategoryId,
                                            SubSubSubCategoryId = item.SubSubSubCategoryId,
                                            SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                            BrandId = item.BrandId,
                                            ModelId = item.MethodId,
                                           

                                          };
                                        unitOfWork.InventoryDetailRepository.Insert(InventoryDetail);
                                        unitOfWork.Save();
                                    }
                                }

                                else
                                {


                                    // var result = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId==item.TransferId);

                                    var tqty = transferquantity - transactionquantity;

                                    totaltransferquantity = totaltransferquantity + transactionquantity;
                                    var tdresult = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId==item.TransferId);
                                    var tresult = unitOfWork.TemporaryTransferInformationRepository.Get().SingleOrDefault(b => b.Id == item.Id);
                                    if (tresult != null)
                                    {
                                        tresult.Recieve = "Received";
                                        tdresult.Recieve = "Received";
                                        tdresult.RecieveBy = TransferViewModel.RecieveBy;
                                        tdresult.ReceiveItemQuantity = transactionquantity;
                                        tdresult.PendingItemQuantity = tqty;
                                        tdresult.DateOfReceive = DateTime.Now.Date;

                                        tresult.ReceiveItemQuantity = transactionquantity;
                                        tresult.PendingItemQuantity = tqty;
                                        if (flag != 1 && item.TransferId == transferid)
                                        {
                                            var InventoryMaster = new InventoryMaster
                                            {

                                                StoreId = item.ToStoreId,
                                                CreateBy = TransferViewModel.RecieveBy,
                                                CreateDate = DateTime.Now,
                                                TransferId = item.TransferId,
                                                SupplierCompanyId = item.SupplierCompanyId,
                                                SubContractCompanyId = item.SubContractCompanyId
                                            };
                                            unitOfWork.InventoryMasterRepository.Insert(InventoryMaster);
                                            unitOfWork.Save();
                                            flag = 1;
                                            IMID = InventoryMaster.Inv_HD_ID;
                                        }

                                        var InventoryDetail = new InventoryDetail
                                        {
                                            Inv_HD_ID = IMID,
                                            ItemId = item.ItemId,
                                            TransactionQty = transactionquantity,
                                            AvailableQty = transactionquantity,
                                            UnitId = item.UnitId,
                                            MethodId = item.MethodId,



                                            DateOfExpired = item.DateOfExpired,
                                            DateOfNextMaintainance = item.DateOfNextMaintainance,
                                            Barcode = item.Barcode,
                                            Item_Unique_Number = item.Item_Unique_Number,
                                            Chesis_Number = item.Chesis_Number,
                                            Engine_Number = item.Engine_Number,

                                            ProductId = item.ProductId,
                                            CountryId = item.CountryId,
                                            Principle_id = item.Principle_id,
                                            ConditionOfItemId = item.ConditionOfItemId,
                                            WarrantyId = item.WarrantyId,
                                            PO_Price = item.PO_Price,
                                            PO_SubTotal = transactionquantity * item.PO_Price,
                                            CategoryId = item.CategoryId,
                                            SubCategoryId = item.SubCategoryId,
                                            SubSubCategoryId = item.SubSubCategoryId,
                                            SubSubSubCategoryId = item.SubSubSubCategoryId,
                                            SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                            BrandId = item.BrandId,
                                            ModelId = item.MethodId


                                        };
                                        unitOfWork.InventoryDetailRepository.Insert(InventoryDetail);
                                        unitOfWork.Save();
                                    }

                                }
                               
                            }
                            else
                            {

                                return "You Can not transfer more than want!!";

                            }



                        }



                        int count1 = (from x in unitOfWork.SubStoreRepository.Get() where x.SubStoreId == TransferViewModel.StoreId select x.SubStoreId).Count();
                        if (count1 == 1)
                        {
                            var storeid = unitOfWork.SubStoreRepository.Get().Where(a => a.SubStoreId == TransferViewModel.StoreId).Select(a => a.StoreId).Single();


                            var transferquantity = (from a in unitOfWork.TemporaryTransferInformationRepository.Get()
                                                    where a.ItemId == item.ItemId && a.TransferId == transferid && a.TransferDetailId == transferDetailsid && a.Inv_HD_ID == inventoryid && a.Id == temptransinfoid
                                                    select (int?)a.TransactionQty).SingleOrDefault();


                            //var transferquantity = unitOfWork.TemporaryTransferInformationRepository.Get().Where(a => (((((a.ItemId == item.ItemId) && (a.TransferId == transferid)) && (a.TransferDetailId == transferDetailsid)) && (a.InventoryId == inventoryid)) && (a.Id == temptransinfoid))).Select(a => a.TransactionQuantity).SingleOrDefault();
                            if (transferquantity >= transactionquantity)
                            {
                                if (transferquantity == transactionquantity)
                                {
                                    totaltransferquantity = totaltransferquantity + transactionquantity;

                                    //var result = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId==item.TransferId);

                                    var tdresult = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId == item.TransferId);
                                    var tresult = unitOfWork.TemporaryTransferInformationRepository.Get().SingleOrDefault(b => b.Id == temptransinfoid);
                                    if (tresult != null)
                                    {
                                        tdresult.Recieve = "Received";
                                        tdresult.RecieveBy = TransferViewModel.RecieveBy;
                                        tdresult.ReceiveItemQuantity = transactionquantity;
                                        tdresult.PendingItemQuantity = transferquantity - transactionquantity;
                                        tdresult.DateOfReceive = DateTime.Now.Date;
                                        tresult.Recieve = "Received";
                                        tresult.ReceiveItemQuantity = transactionquantity;
                                        tresult.PendingItemQuantity = transferquantity - transactionquantity;
                                        if (item.TransferId == transferid && item.Inv_HD_ID == inventoryid && item.TransferDetailsId == transferDetailsid && item.Id == temptransinfoid)
                                        {
                                            var InventoryMaster = new InventoryMaster
                                            {

                                                StoreId = storeid,
                                                SubStoreId=item.ToStoreId,
                                                CreateBy = TransferViewModel.RecieveBy,
                                                CreateDate = DateTime.Now,
                                                TransferId = item.TransferId,
                                                SupplierCompanyId = item.SupplierCompanyId,
                                                SubContractCompanyId = item.SubContractCompanyId
                                            };
                                            unitOfWork.InventoryMasterRepository.Insert(InventoryMaster);
                                            unitOfWork.Save();
                                            flag = 1;
                                            IMID = InventoryMaster.Inv_HD_ID;
                                        }

                                        var InventoryDetail = new InventoryDetail
                                        {
                                            Inv_HD_ID = IMID,
                                            ItemId = item.ItemId,
                                            TransactionQty = transactionquantity,
                                            AvailableQty = transactionquantity,
                                            UnitId = item.UnitId,
                                            MethodId = item.MethodId,



                                            DateOfExpired = item.DateOfExpired,
                                            DateOfNextMaintainance = item.DateOfNextMaintainance,
                                            Barcode = item.Barcode,
                                            Item_Unique_Number = item.Item_Unique_Number,
                                            Chesis_Number = item.Chesis_Number,
                                            Engine_Number = item.Engine_Number,

                                            ProductId = item.ProductId,
                                            CountryId = item.CountryId,
                                            Principle_id = item.Principle_id,
                                            ConditionOfItemId = item.ConditionOfItemId,
                                            WarrantyId = item.WarrantyId,
                                            PO_Price = item.PO_Price,
                                            PO_SubTotal = transactionquantity * item.PO_Price,
                                            CategoryId = item.CategoryId,
                                            SubCategoryId = item.SubCategoryId,
                                            SubSubCategoryId = item.SubSubCategoryId,
                                            SubSubSubCategoryId = item.SubSubSubCategoryId,
                                            SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                            BrandId = item.BrandId,
                                            ModelId = item.MethodId,


                                        };
                                        unitOfWork.InventoryDetailRepository.Insert(InventoryDetail);
                                        unitOfWork.Save();
                                    }
                                }

                                else
                                {


                                    // var result = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId==item.TransferId);

                                    var tqty = transferquantity - transactionquantity;

                                    totaltransferquantity = totaltransferquantity + transactionquantity;
                                    var tdresult = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId == item.TransferId);
                                    var tresult = unitOfWork.TemporaryTransferInformationRepository.Get().SingleOrDefault(b => b.Id == item.Id);
                                    if (tresult != null)
                                    {
                                        tresult.Recieve = "Received";
                                        tdresult.Recieve = "Received";
                                        tdresult.RecieveBy = TransferViewModel.RecieveBy;
                                        tdresult.ReceiveItemQuantity = transactionquantity;
                                        tdresult.PendingItemQuantity = tqty;
                                        tdresult.DateOfReceive = DateTime.Now.Date;
                                        tresult.ReceiveItemQuantity = transactionquantity;
                                        tresult.PendingItemQuantity = tqty;

                                        if (flag != 1 && item.TransferId == transferid)
                                        {
                                            var InventoryMaster = new InventoryMaster
                                            {

                                                StoreId = item.ToStoreId,
                                                CreateBy = TransferViewModel.RecieveBy,
                                                CreateDate = DateTime.Now,
                                                TransferId = item.TransferId,
                                                SupplierCompanyId = item.SupplierCompanyId,
                                                SubContractCompanyId = item.SubContractCompanyId
                                            };
                                            unitOfWork.InventoryMasterRepository.Insert(InventoryMaster);
                                            unitOfWork.Save();
                                            flag = 1;
                                            IMID = InventoryMaster.Inv_HD_ID;
                                        }

                                        var InventoryDetail = new InventoryDetail
                                        {
                                            Inv_HD_ID = IMID,
                                            ItemId = item.ItemId,
                                            TransactionQty = transactionquantity,
                                            AvailableQty = transactionquantity,
                                            UnitId = item.UnitId,
                                            MethodId = item.MethodId,



                                            DateOfExpired = item.DateOfExpired,
                                            DateOfNextMaintainance = item.DateOfNextMaintainance,
                                            Barcode = item.Barcode,
                                            Item_Unique_Number = item.Item_Unique_Number,
                                            Chesis_Number = item.Chesis_Number,
                                            Engine_Number = item.Engine_Number,

                                            ProductId = item.ProductId,
                                            CountryId = item.CountryId,
                                            Principle_id = item.Principle_id,
                                            ConditionOfItemId = item.ConditionOfItemId,
                                            WarrantyId = item.WarrantyId,
                                            PO_Price = item.PO_Price,
                                            PO_SubTotal = transactionquantity * item.PO_Price,
                                            CategoryId = item.CategoryId,
                                            SubCategoryId = item.SubCategoryId,
                                            SubSubCategoryId = item.SubSubCategoryId,
                                            SubSubSubCategoryId = item.SubSubSubCategoryId,
                                            SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                            BrandId = item.BrandId,
                                            ModelId = item.MethodId


                                        };
                                        unitOfWork.InventoryDetailRepository.Insert(InventoryDetail);
                                        unitOfWork.Save();
                                    }

                                }

                            }
                            else
                            {

                                return "You Can not transfer more than want!!";

                            }



                        }


                        int count2 = (from x in unitOfWork.SubSubStoreRepository.Get() where x.SubSubStoreId == TransferViewModel.StoreId select x.SubSubStoreId).Count();
                        if (count2 == 1)
                        {

                            var storeid = unitOfWork.SubSubStoreRepository.Get().Where(a => a.SubSubStoreId == TransferViewModel.StoreId).Select(a => a.StoreId).Single();
                            var substoreid = unitOfWork.SubSubStoreRepository.Get().Where(a => a.SubSubStoreId == TransferViewModel.StoreId).Select(a => a.SubStoreId).Single();




                            var transferquantity = (from a in unitOfWork.TemporaryTransferInformationRepository.Get()
                                                    where a.ItemId == item.ItemId && a.TransferId == transferid && a.TransferDetailId == transferDetailsid && a.Inv_HD_ID == inventoryid && a.Id == temptransinfoid
                                                    select (int?)a.TransactionQty).SingleOrDefault();


                            //var transferquantity = unitOfWork.TemporaryTransferInformationRepository.Get().Where(a => (((((a.ItemId == item.ItemId) && (a.TransferId == transferid)) && (a.TransferDetailId == transferDetailsid)) && (a.InventoryId == inventoryid)) && (a.Id == temptransinfoid))).Select(a => a.TransactionQuantity).SingleOrDefault();
                            if (transferquantity >= transactionquantity)
                            {
                                if (transferquantity == transactionquantity)
                                {
                                    totaltransferquantity = totaltransferquantity + transactionquantity;

                                    //var result = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId==item.TransferId);

                                    var tdresult = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId == item.TransferId);
                                    var tresult = unitOfWork.TemporaryTransferInformationRepository.Get().SingleOrDefault(b => b.Id == temptransinfoid);
                                    if (tresult != null)
                                    {
                                        tdresult.Recieve = "Received";
                                        tdresult.RecieveBy = TransferViewModel.RecieveBy;
                                        tdresult.ReceiveItemQuantity = transactionquantity;
                                        tdresult.PendingItemQuantity = transferquantity - transactionquantity;
                                        tdresult.DateOfReceive = DateTime.Now.Date;
                                        tresult.Recieve = "Received";
                                        tresult.ReceiveItemQuantity = transactionquantity;
                                        tresult.PendingItemQuantity = transferquantity - transactionquantity;
                                        if (item.TransferId == transferid && item.Inv_HD_ID == inventoryid && item.TransferDetailsId == transferDetailsid && item.Id == temptransinfoid)
                                        {
                                            var InventoryMaster = new InventoryMaster
                                            {

                                                StoreId = storeid,
                                                SubStoreId = substoreid,
                                                SubSubStoreId = item.ToStoreId,
                                                CreateBy = TransferViewModel.RecieveBy,
                                                CreateDate = DateTime.Now,
                                                TransferId = item.TransferId,
                                                SupplierCompanyId = item.SupplierCompanyId,
                                                SubContractCompanyId = item.SubContractCompanyId
                                            };
                                            unitOfWork.InventoryMasterRepository.Insert(InventoryMaster);
                                            unitOfWork.Save();
                                            flag = 1;
                                            IMID = InventoryMaster.Inv_HD_ID;
                                        }

                                        var InventoryDetail = new InventoryDetail
                                        {
                                            Inv_HD_ID = IMID,
                                            ItemId = item.ItemId,
                                            TransactionQty = transactionquantity,
                                            AvailableQty = transactionquantity,
                                            UnitId = item.UnitId,
                                            MethodId = item.MethodId,



                                            DateOfExpired = item.DateOfExpired,
                                            DateOfNextMaintainance = item.DateOfNextMaintainance,
                                            Barcode = item.Barcode,
                                            Item_Unique_Number = item.Item_Unique_Number,
                                            Chesis_Number = item.Chesis_Number,
                                            Engine_Number = item.Engine_Number,

                                            ProductId = item.ProductId,
                                            CountryId = item.CountryId,
                                            Principle_id = item.Principle_id,
                                            ConditionOfItemId = item.ConditionOfItemId,
                                            WarrantyId = item.WarrantyId,
                                            PO_Price = item.PO_Price,
                                            PO_SubTotal = transactionquantity * item.PO_Price,
                                            CategoryId = item.CategoryId,
                                            SubCategoryId = item.SubCategoryId,
                                            SubSubCategoryId = item.SubSubCategoryId,
                                            SubSubSubCategoryId = item.SubSubSubCategoryId,
                                            SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                            BrandId = item.BrandId,
                                            ModelId = item.MethodId,


                                        };
                                        unitOfWork.InventoryDetailRepository.Insert(InventoryDetail);
                                        unitOfWork.Save();
                                    }
                                }

                                else
                                {


                                    // var result = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId==item.TransferId);

                                    var tqty = transferquantity - transactionquantity;

                                    totaltransferquantity = totaltransferquantity + transactionquantity;
                                    var tdresult = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId == item.TransferId);
                                    var tresult = unitOfWork.TemporaryTransferInformationRepository.Get().SingleOrDefault(b => b.Id == item.Id);
                                    if (tresult != null)
                                    {
                                        tresult.Recieve = "Received";
                                        tdresult.Recieve = "Received";
                                        tdresult.RecieveBy = TransferViewModel.RecieveBy;
                                        tdresult.ReceiveItemQuantity = transactionquantity;
                                        tdresult.PendingItemQuantity = tqty;
                                        tdresult.DateOfReceive = DateTime.Now.Date;
                                        tresult.ReceiveItemQuantity = transactionquantity;
                                        tresult.PendingItemQuantity = tqty;

                                        if (flag != 1 && item.TransferId == transferid)
                                        {
                                            var InventoryMaster = new InventoryMaster
                                            {

                                                StoreId = item.ToStoreId,
                                                CreateBy = TransferViewModel.RecieveBy,
                                                CreateDate = DateTime.Now,
                                                TransferId = item.TransferId,
                                                SupplierCompanyId = item.SupplierCompanyId,
                                                SubContractCompanyId = item.SubContractCompanyId
                                            };
                                            unitOfWork.InventoryMasterRepository.Insert(InventoryMaster);
                                            unitOfWork.Save();
                                            flag = 1;
                                            IMID = InventoryMaster.Inv_HD_ID;
                                        }

                                        var InventoryDetail = new InventoryDetail
                                        {
                                            Inv_HD_ID = IMID,
                                            ItemId = item.ItemId,
                                            TransactionQty = transactionquantity,
                                            AvailableQty = transactionquantity,
                                            UnitId = item.UnitId,
                                            MethodId = item.MethodId,



                                            DateOfExpired = item.DateOfExpired,
                                            DateOfNextMaintainance = item.DateOfNextMaintainance,
                                            Barcode = item.Barcode,
                                            Item_Unique_Number = item.Item_Unique_Number,
                                            Chesis_Number = item.Chesis_Number,
                                            Engine_Number = item.Engine_Number,

                                            ProductId = item.ProductId,
                                            CountryId = item.CountryId,
                                            Principle_id = item.Principle_id,
                                            ConditionOfItemId = item.ConditionOfItemId,
                                            WarrantyId = item.WarrantyId,
                                            PO_Price = item.PO_Price,
                                            PO_SubTotal = transactionquantity * item.PO_Price,
                                            CategoryId = item.CategoryId,
                                            SubCategoryId = item.SubCategoryId,
                                            SubSubCategoryId = item.SubSubCategoryId,
                                            SubSubSubCategoryId = item.SubSubSubCategoryId,
                                            SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                            BrandId = item.BrandId,
                                            ModelId = item.MethodId


                                        };
                                        unitOfWork.InventoryDetailRepository.Insert(InventoryDetail);
                                        unitOfWork.Save();
                                    }

                                }

                            }
                            else
                            {

                                return "You Can not transfer more than want!!";

                            }





                        }



                        int count3 = (from x in unitOfWork.SubSubSubStoreRepository.Get() where x.SubSubSubStoreId == TransferViewModel.StoreId select x.SubSubSubStoreId).Count();
                        if (count3 == 1)
                        {

                            var storeid = unitOfWork.SubSubSubStoreRepository.Get().Where(a => a.SubSubSubStoreId == TransferViewModel.StoreId).Select(a => a.StoreId).Single();
                            var substoreid = unitOfWork.SubSubSubStoreRepository.Get().Where(a => a.SubSubSubStoreId == TransferViewModel.StoreId).Select(a => a.SubStoreId).Single();
                            var subsubstoreid = unitOfWork.SubSubSubStoreRepository.Get().Where(a => a.SubSubSubStoreId == TransferViewModel.StoreId).Select(a => a.SubSubStoreId).Single();



                            var transferquantity = (from a in unitOfWork.TemporaryTransferInformationRepository.Get()
                                                    where a.ItemId == item.ItemId && a.TransferId == transferid && a.TransferDetailId == transferDetailsid && a.Inv_HD_ID == inventoryid && a.Id == temptransinfoid
                                                    select (int?)a.TransactionQty).SingleOrDefault();


                            //var transferquantity = unitOfWork.TemporaryTransferInformationRepository.Get().Where(a => (((((a.ItemId == item.ItemId) && (a.TransferId == transferid)) && (a.TransferDetailId == transferDetailsid)) && (a.InventoryId == inventoryid)) && (a.Id == temptransinfoid))).Select(a => a.TransactionQuantity).SingleOrDefault();
                            if (transferquantity >= transactionquantity)
                            {
                                if (transferquantity == transactionquantity)
                                {
                                    totaltransferquantity = totaltransferquantity + transactionquantity;

                                    //var result = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId==item.TransferId);

                                    var tdresult = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId == item.TransferId);
                                    var tresult = unitOfWork.TemporaryTransferInformationRepository.Get().SingleOrDefault(b => b.Id == temptransinfoid);
                                    if (tresult != null)
                                    {
                                        tdresult.Recieve = "Received";
                                        tdresult.RecieveBy = TransferViewModel.RecieveBy;
                                        tdresult.ReceiveItemQuantity = transactionquantity;
                                        tdresult.PendingItemQuantity = transferquantity - transactionquantity;
                                        tdresult.DateOfReceive = DateTime.Now.Date;
                                        tresult.Recieve = "Received";
                                        tresult.ReceiveItemQuantity = transactionquantity;
                                        tresult.PendingItemQuantity = transferquantity - transactionquantity;
                                        if (item.TransferId == transferid && item.Inv_HD_ID == inventoryid && item.TransferDetailsId == transferDetailsid && item.Id == temptransinfoid)
                                        {
                                            var InventoryMaster = new InventoryMaster
                                            {

                                                StoreId = storeid,
                                                SubStoreId = substoreid,
                                                SubSubStoreId = subsubstoreid,
                                                SubSubSubStoreId= item.ToStoreId,
                                                CreateBy = TransferViewModel.RecieveBy,
                                                CreateDate = DateTime.Now,
                                                TransferId = item.TransferId,
                                                SupplierCompanyId = item.SupplierCompanyId,
                                                SubContractCompanyId = item.SubContractCompanyId
                                            };
                                            unitOfWork.InventoryMasterRepository.Insert(InventoryMaster);
                                            unitOfWork.Save();
                                            flag = 1;
                                            IMID = InventoryMaster.Inv_HD_ID;
                                        }

                                        var InventoryDetail = new InventoryDetail
                                        {
                                            Inv_HD_ID = IMID,
                                            ItemId = item.ItemId,
                                            TransactionQty = transactionquantity,
                                            AvailableQty = transactionquantity,
                                            UnitId = item.UnitId,
                                            MethodId = item.MethodId,



                                            DateOfExpired = item.DateOfExpired,
                                            DateOfNextMaintainance = item.DateOfNextMaintainance,
                                            Barcode = item.Barcode,
                                            Item_Unique_Number = item.Item_Unique_Number,
                                            Chesis_Number = item.Chesis_Number,
                                            Engine_Number = item.Engine_Number,

                                            ProductId = item.ProductId,
                                            CountryId = item.CountryId,
                                            Principle_id = item.Principle_id,
                                            ConditionOfItemId = item.ConditionOfItemId,
                                            WarrantyId = item.WarrantyId,
                                            PO_Price = item.PO_Price,
                                            PO_SubTotal = transactionquantity * item.PO_Price,
                                            CategoryId = item.CategoryId,
                                            SubCategoryId = item.SubCategoryId,
                                            SubSubCategoryId = item.SubSubCategoryId,
                                            SubSubSubCategoryId = item.SubSubSubCategoryId,
                                            SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                            BrandId = item.BrandId,
                                            ModelId = item.MethodId,


                                        };
                                        unitOfWork.InventoryDetailRepository.Insert(InventoryDetail);
                                        unitOfWork.Save();
                                    }
                                }

                                else
                                {


                                    // var result = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId==item.TransferId);

                                    var tqty = transferquantity - transactionquantity;

                                    totaltransferquantity = totaltransferquantity + transactionquantity;
                                    var tdresult = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId == item.TransferId);
                                    var tresult = unitOfWork.TemporaryTransferInformationRepository.Get().SingleOrDefault(b => b.Id == item.Id);
                                    if (tresult != null)
                                    {
                                        tresult.Recieve = "Received";
                                        tdresult.Recieve = "Received";
                                        tdresult.RecieveBy = TransferViewModel.RecieveBy;
                                        tdresult.ReceiveItemQuantity = transactionquantity;
                                        tdresult.PendingItemQuantity = tqty;
                                        tdresult.DateOfReceive = DateTime.Now.Date;
                                        tresult.ReceiveItemQuantity = transactionquantity;
                                        tresult.PendingItemQuantity = tqty;


                                        
                                        if (flag != 1 && item.TransferId == transferid)
                                        {
                                            var InventoryMaster = new InventoryMaster
                                            {

                                                StoreId = item.ToStoreId,
                                                CreateBy = TransferViewModel.RecieveBy,
                                                CreateDate = DateTime.Now,
                                                TransferId = item.TransferId,
                                                SupplierCompanyId = item.SupplierCompanyId,
                                                SubContractCompanyId = item.SubContractCompanyId
                                            };
                                            unitOfWork.InventoryMasterRepository.Insert(InventoryMaster);
                                            unitOfWork.Save();
                                            flag = 1;
                                            IMID = InventoryMaster.Inv_HD_ID;
                                        }

                                        var InventoryDetail = new InventoryDetail
                                        {
                                            Inv_HD_ID = IMID,
                                            ItemId = item.ItemId,
                                            TransactionQty = transactionquantity,
                                            AvailableQty = transactionquantity,
                                            UnitId = item.UnitId,
                                            MethodId = item.MethodId,



                                            DateOfExpired = item.DateOfExpired,
                                            DateOfNextMaintainance = item.DateOfNextMaintainance,
                                            Barcode = item.Barcode,
                                            Item_Unique_Number = item.Item_Unique_Number,
                                            Chesis_Number = item.Chesis_Number,
                                            Engine_Number = item.Engine_Number,

                                            ProductId = item.ProductId,
                                            CountryId = item.CountryId,
                                            Principle_id = item.Principle_id,
                                            ConditionOfItemId = item.ConditionOfItemId,
                                            WarrantyId = item.WarrantyId,
                                            PO_Price = item.PO_Price,
                                            PO_SubTotal = transactionquantity * item.PO_Price,
                                            CategoryId = item.CategoryId,
                                            SubCategoryId = item.SubCategoryId,
                                            SubSubCategoryId = item.SubSubCategoryId,
                                            SubSubSubCategoryId = item.SubSubSubCategoryId,
                                            SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                            BrandId = item.BrandId,
                                            ModelId = item.MethodId


                                        };
                                        unitOfWork.InventoryDetailRepository.Insert(InventoryDetail);
                                        unitOfWork.Save();
                                    }

                                }

                            }
                            else
                            {

                                return "You Can not transfer more than want!!";

                            }






                        }


                        int count4 = (from x in unitOfWork.SubSubSubSubStoreRepository.Get() where x.SubSubSubSubStoreId == TransferViewModel.StoreId select x.SubSubSubSubStoreId).Count();
                        if (count4 == 1)
                        {
                            var storeid = unitOfWork.SubSubSubSubStoreRepository.Get().Where(a => a.SubSubSubSubStoreId == TransferViewModel.StoreId).Select(a => a.StoreId).Single();
                            var substoreid = unitOfWork.SubSubSubSubStoreRepository.Get().Where(a => a.SubSubSubSubStoreId == TransferViewModel.StoreId).Select(a => a.SubStoreId).Single();
                            var subsubstoreid = unitOfWork.SubSubSubSubStoreRepository.Get().Where(a => a.SubSubSubSubStoreId == TransferViewModel.StoreId).Select(a => a.SubSubStoreId).Single();
                            var subsubsubstoreid = unitOfWork.SubSubSubSubStoreRepository.Get().Where(a => a.SubSubSubSubStoreId == TransferViewModel.StoreId).Select(a => a.SubSubSubStoreId).Single();



                            var transferquantity = (from a in unitOfWork.TemporaryTransferInformationRepository.Get()
                                                    where a.ItemId == item.ItemId && a.TransferId == transferid && a.TransferDetailId == transferDetailsid && a.Inv_HD_ID == inventoryid && a.Id == temptransinfoid
                                                    select (int?)a.TransactionQty).SingleOrDefault();


                            //var transferquantity = unitOfWork.TemporaryTransferInformationRepository.Get().Where(a => (((((a.ItemId == item.ItemId) && (a.TransferId == transferid)) && (a.TransferDetailId == transferDetailsid)) && (a.InventoryId == inventoryid)) && (a.Id == temptransinfoid))).Select(a => a.TransactionQuantity).SingleOrDefault();
                            if (transferquantity >= transactionquantity)
                            {
                                if (transferquantity == transactionquantity)
                                {
                                    totaltransferquantity = totaltransferquantity + transactionquantity;

                                    //var result = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId==item.TransferId);

                                    var tdresult = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId == item.TransferId);
                                    var tresult = unitOfWork.TemporaryTransferInformationRepository.Get().SingleOrDefault(b => b.Id == temptransinfoid);
                                    if (tresult != null)
                                    {
                                        tdresult.Recieve = "Received";
                                        tdresult.RecieveBy = TransferViewModel.RecieveBy;
                                        tdresult.ReceiveItemQuantity = transactionquantity;
                                        tdresult.PendingItemQuantity = transferquantity - transactionquantity;
                                        tdresult.DateOfReceive = DateTime.Now.Date;
                                        tresult.Recieve = "Received";
                                        tresult.ReceiveItemQuantity = transactionquantity;
                                        tresult.PendingItemQuantity = transferquantity - transactionquantity;
                                        if (item.TransferId == transferid && item.Inv_HD_ID == inventoryid && item.TransferDetailsId == transferDetailsid && item.Id == temptransinfoid)
                                        {
                                            var InventoryMaster = new InventoryMaster
                                            {

                                                StoreId = storeid,
                                                SubStoreId = substoreid,
                                                SubSubStoreId = subsubstoreid,
                                                SubSubSubStoreId = subsubsubstoreid,
                                                SubSubSubSubStoreId = item.ToStoreId,
                                                CreateBy = TransferViewModel.RecieveBy,
                                                CreateDate = DateTime.Now,
                                                TransferId = item.TransferId,
                                                SupplierCompanyId = item.SupplierCompanyId,
                                                SubContractCompanyId = item.SubContractCompanyId
                                            };
                                            unitOfWork.InventoryMasterRepository.Insert(InventoryMaster);
                                            unitOfWork.Save();
                                            flag = 1;
                                            IMID = InventoryMaster.Inv_HD_ID;
                                        }

                                        var InventoryDetail = new InventoryDetail
                                        {
                                            Inv_HD_ID = IMID,
                                            ItemId = item.ItemId,
                                            TransactionQty = transactionquantity,
                                            AvailableQty = transactionquantity,
                                            UnitId = item.UnitId,
                                            MethodId = item.MethodId,



                                            DateOfExpired = item.DateOfExpired,
                                            DateOfNextMaintainance = item.DateOfNextMaintainance,
                                            Barcode = item.Barcode,
                                            Item_Unique_Number = item.Item_Unique_Number,
                                            Chesis_Number = item.Chesis_Number,
                                            Engine_Number = item.Engine_Number,

                                            ProductId = item.ProductId,
                                            CountryId = item.CountryId,
                                            Principle_id = item.Principle_id,
                                            ConditionOfItemId = item.ConditionOfItemId,
                                            WarrantyId = item.WarrantyId,
                                            PO_Price = item.PO_Price,
                                            PO_SubTotal = transactionquantity * item.PO_Price,
                                            CategoryId = item.CategoryId,
                                            SubCategoryId = item.SubCategoryId,
                                            SubSubCategoryId = item.SubSubCategoryId,
                                            SubSubSubCategoryId = item.SubSubSubCategoryId,
                                            SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                            BrandId = item.BrandId,
                                            ModelId = item.MethodId,


                                        };
                                        unitOfWork.InventoryDetailRepository.Insert(InventoryDetail);
                                        unitOfWork.Save();
                                    }
                                }

                                else
                                {


                                    // var result = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId==item.TransferId);

                                    var tqty = transferquantity - transactionquantity;

                                    totaltransferquantity = totaltransferquantity + transactionquantity;
                                    var tdresult = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferDetailId == item.TransferDetailsId && b.TransferId == item.TransferId);
                                    var tresult = unitOfWork.TemporaryTransferInformationRepository.Get().SingleOrDefault(b => b.Id == item.Id);
                                    if (tresult != null)
                                    {
                                        tresult.Recieve = "Received";
                                        tdresult.Recieve = "Received";
                                        tdresult.RecieveBy = TransferViewModel.RecieveBy;
                                        tdresult.ReceiveItemQuantity = transactionquantity;
                                        tdresult.PendingItemQuantity = tqty;
                                        tdresult.DateOfReceive = DateTime.Now.Date;
                                        tresult.ReceiveItemQuantity = transactionquantity;
                                        tresult.PendingItemQuantity = tqty;

                                       
                                        if (flag != 1 && item.TransferId == transferid)
                                        {
                                            var InventoryMaster = new InventoryMaster
                                            {

                                                StoreId = item.ToStoreId,
                                                CreateBy = TransferViewModel.RecieveBy,
                                                CreateDate = DateTime.Now,
                                                TransferId = item.TransferId,
                                                SupplierCompanyId = item.SupplierCompanyId,
                                                SubContractCompanyId = item.SubContractCompanyId
                                            };
                                            unitOfWork.InventoryMasterRepository.Insert(InventoryMaster);
                                            unitOfWork.Save();
                                            flag = 1;
                                            IMID = InventoryMaster.Inv_HD_ID;
                                        }

                                        var InventoryDetail = new InventoryDetail
                                        {
                                            Inv_HD_ID = IMID,
                                            ItemId = item.ItemId,
                                            TransactionQty = transactionquantity,
                                            AvailableQty = transactionquantity,
                                            UnitId = item.UnitId,
                                            MethodId = item.MethodId,



                                            DateOfExpired = item.DateOfExpired,
                                            DateOfNextMaintainance = item.DateOfNextMaintainance,
                                            Barcode = item.Barcode,
                                            Item_Unique_Number = item.Item_Unique_Number,
                                            Chesis_Number = item.Chesis_Number,
                                            Engine_Number = item.Engine_Number,

                                            ProductId = item.ProductId,
                                            CountryId = item.CountryId,
                                            Principle_id = item.Principle_id,
                                            ConditionOfItemId = item.ConditionOfItemId,
                                            WarrantyId = item.WarrantyId,
                                            PO_Price = item.PO_Price,
                                            PO_SubTotal = transactionquantity * item.PO_Price,
                                            CategoryId = item.CategoryId,
                                            SubCategoryId = item.SubCategoryId,
                                            SubSubCategoryId = item.SubSubCategoryId,
                                            SubSubSubCategoryId = item.SubSubSubCategoryId,
                                            SubSubSubSubCategoryId = item.SubSubSubSubCategoryId,
                                            BrandId = item.BrandId,
                                            ModelId = item.MethodId


                                        };
                                        unitOfWork.InventoryDetailRepository.Insert(InventoryDetail);
                                        unitOfWork.Save();
                                    }

                                }

                            }
                            else
                            {

                                return "You Can not transfer more than want!!";

                            }





                        }


                    }
}


            }
            return "Success";
        }

    

 public List<TransferViewModel> GetAllPendingReceiveInfo(int strstoreid)
        {



            var SN = (from s in unitOfWork.StoreRepository.Get()
                      select new
                      {

                          SID = s.StoreId,
                          Name = s.StoreName

                      }).Union(
                                       (from s in unitOfWork.SubStoreRepository.Get()
                                        select new
                                        {

                                            SID = s.SubStoreId,
                                            Name = s.SubStoreName,

                                        })).Union(
                                        (from s in unitOfWork.SubSubStoreRepository.Get()
                                         select new
                                         {

                                             SID = s.SubSubStoreId,
                                             Name = s.SubSubStoreName,

                                         })).Union(
                                        (from s in unitOfWork.SubSubSubStoreRepository.Get()
                                         select new
                                         {

                                             SID = s.SubSubSubStoreId,
                                             Name = s.SubSubSubStoreName,

                                         })).Union(
                                        (from s in unitOfWork.SubSubSubSubStoreRepository.Get()
                                         select new
                                         {

                                             SID = s.SubSubSubSubStoreId,
                                             Name = s.SubSubSubSubStoreName,

                                         }));


            var data = (from s in unitOfWork.TemporaryTransferInformationRepository.Get()


                        join fstore in SN on s.FromStoreId equals fstore.SID
                        join tstore in SN on s.ToStoreId equals tstore.SID
                        join i in unitOfWork.ItemRepository.Get() on s.ItemId equals i.ItemId
                        join u in unitOfWork.UnitRepository.Get() on i.UnitId equals u.UnitId
                        join t in unitOfWork.TransferTypeRepository.Get() on s.TransferTypeId equals t.TransferTypeId
                        join c in unitOfWork.ConditionOfItemRepository.Get() on s.ConditionOfItemId equals c.ConditionOfItemId
                        orderby s.Id ascending
                        where s.Recieve == "Pending" && s.ToStoreId == strstoreid


                        select new TransferViewModel
                        {
                            Id = s.Id,
                            Inv_HD_ID = s.Inv_HD_ID,
                            TransferId = s.TransferId,
                            TransferDetailsId = s.TransferDetailId,
                            FromStoreId = s.FromStoreId,
                            FromStoreName = fstore.Name,
                            ToStoreId = s.ToStoreId,
                            ToStoreName = tstore.Name,
                            PO_Price = s.PO_Price,
                            ItemId = s.ItemId,
                            ItemName = i.ItemName,
                            TransactionQuantity = s.TransactionQty,
                            UnitId = s.UnitId,
                            UnitName = u.UnitName,
                            TransferTypeId = s.TransferTypeId,
                            TransferTypeName = t.TransferTypeName,
                            TransferDate = s.DateOfActualTransfer,
                            TransferOrderDate = s.DateOfTransferOrder,
                            ConditionOfItemId = s.ConditionOfItemId,
                            ConditionOfItemName = c.ConditionOfItemName,
                            Recieve = s.Recieve,
                            MethodId = s.MethodId,
                            SupplierCompanyId = s.SupplierCompanyId,
                            SubContractCompanyId = s.SubContractCompanyId,

                            DateOfExpired = s.DateOfExpired,
                            DateOfNextMaintainance = s.DateOfNextMaintainance,
                            Barcode = s.Barcode,
                            Item_Unique_Number = s.Item_Unique_Number,
                            Chesis_Number = s.Chesis_Number,
                            Engine_Number = s.Engine_Number,

                            ProductId = s.ProductId,
                            CountryId = s.CountryId,
                            Principle_id = s.Principle_id,

                            WarrantyId = s.WarrantyId,
                            CategoryId = s.CategoryId,
                            SubCategoryId = s.SubCategoryId,
                            SubSubCategoryId = s.SubSubCategoryId,
                            SubSubSubCategoryId = s.SubSubSubCategoryId,
                            SubSubSubSubCategoryId = s.SubSubSubSubCategoryId,
                            BrandId = s.BrandId,
                            ModelId = s.MethodId


                        }).ToList();
            return data;
        }



        public IEnumerable<TransferViewModel> GETALLBRANCHSTORE(string strstoreid)
        {
            int storeid = 0;
            int i = Convert.ToInt32(strstoreid);
            if (i >= 1000 && i < 2000)
            {
                storeid = i;

            }
            if (i >= 2000 && i < 3000)
            {
                storeid = Convert.ToInt32(unitOfWork.SubStoreRepository.Get().Where(a => a.SubStoreId == i).Select(a => a.StoreId).Single());

            }

            if (i >= 3000 && i < 4000)
            {

                storeid = unitOfWork.SubSubStoreRepository.Get().Where(a => a.SubSubStoreId == i).Select(a => a.StoreId).Single();
            }

            if (i >= 4000 && i < 5000)
            {

                storeid = unitOfWork.SubSubSubStoreRepository.Get().Where(a => a.SubSubSubStoreId == i).Select(a => a.StoreId).Single();

            }


            if (i >= 5000 && i < 6000)
            {

                storeid = unitOfWork.SubSubSubSubStoreRepository.Get().Where(a => a.SubSubSubSubStoreId == i).Select(a => a.StoreId).Single();
            }


            var data = (from s in unitOfWork.StoreRepository.Get()
                        where s.StoreId == storeid
                        select new TransferViewModel
                        {

                            SID = s.StoreId,
                            Name = s.StoreName

                        }).Union(
                            (from s in unitOfWork.SubStoreRepository.Get()
                             where s.StoreId == storeid
                             select new TransferViewModel
                             {

                                 SID = s.SubStoreId,
                                 Name = s.SubStoreName,

                             })).Union(
                             (from s in unitOfWork.SubSubStoreRepository.Get()
                              where s.StoreId == storeid
                              select new TransferViewModel
                              {

                                  SID = s.SubSubStoreId,
                                  Name = s.SubSubStoreName,

                              })).Union(
                             (from s in unitOfWork.SubSubSubStoreRepository.Get()
                              where s.StoreId == storeid
                              select new TransferViewModel
                              {

                                  SID = s.SubSubSubStoreId,
                                  Name = s.SubSubSubStoreName,

                              })).Union(
                             (from s in unitOfWork.SubSubSubSubStoreRepository.Get()
                              where s.StoreId == storeid
                              select new TransferViewModel
                              {

                                  SID = s.SubSubSubSubStoreId,
                                  Name = s.SubSubSubSubStoreName,

                              }));

            return data;




        }



        public int CountOrderInformattion(string strStoreId)
        {
            var sid = Convert.ToInt32(strStoreId);
            int count = (from x in unitOfWork.TransferMasterRepository.Get() join td in unitOfWork.TransferDetailsRepository.Get() on (int?)x.TransferId equals (int?)td.TransferId where x.FromStoreId == sid select x).Count();
            return count;
        }


        public int CountOrderReceiveInformattion(string strStoreId)
        {
            var sid = Convert.ToInt32(strStoreId);
          
            int count = (from x in unitOfWork.TransferMasterRepository.Get() join td in unitOfWork.TransferDetailsRepository.Get() on x.TransferId equals td.TransferId where x.FromStoreId == sid && td.Recieve == "Received" select x).Count();
            return count;
        }

        public int CountOrderPendingInformattion(string strStoreId)
        {
            var sid = Convert.ToInt32(strStoreId);
            int count = (from x in unitOfWork.TransferMasterRepository.Get() join td in unitOfWork.TransferDetailsRepository.Get() on x.TransferId equals td.TransferId where x.FromStoreId == sid && td.Recieve == "Pending" select x).Count();
            return count;
        }

        public int CountOrderCancelInformattion(string strStoreId)
        {
            var sid = Convert.ToInt32(strStoreId);
            int count = (from x in unitOfWork.TransferMasterRepository.Get() join td in unitOfWork.TransferDetailsRepository.Get() on x.TransferId equals td.TransferId where x.FromStoreId == sid && td.Recieve == "Canceled" select x).Count();
           
            return count;
        }


        public List<TransferViewModel> LoadTransferInformation(string strStoreId)
        {
            var strstoreid = Convert.ToInt32(strStoreId);
            var S = (from s in unitOfWork.StoreRepository.Get()
                     select new
                     {

                         SID = s.StoreId,
                         Name = s.StoreName

                     }).Union(
                               (from s in unitOfWork.SubStoreRepository.Get()
                                select new
                                {

                                    SID = s.SubStoreId,
                                    Name = s.SubStoreName,

                                })).Union(
                                (from s in unitOfWork.SubSubStoreRepository.Get()
                                 select new
                                 {

                                     SID = s.SubSubStoreId,
                                     Name = s.SubSubStoreName,

                                 })).Union(
                                (from s in unitOfWork.SubSubSubStoreRepository.Get()
                                 select new
                                 {

                                     SID = s.SubSubSubStoreId,
                                     Name = s.SubSubSubStoreName,

                                 })).Union(
                                (from s in unitOfWork.SubSubSubSubStoreRepository.Get()
                                 select new
                                 {

                                     SID = s.SubSubSubSubStoreId,
                                     Name = s.SubSubSubSubStoreName,

                                 }));

            

            var data = (from s in unitOfWork.TransferMasterRepository.Get()
                        join td in unitOfWork.TransferDetailsRepository.Get() on s.TransferId equals td.TransferId
                        join to in unitOfWork.TransferOrderRepository.Get() on td.TransferOrderId equals to.TransferOrderId
                        join store in S on s.FromStoreId equals store.SID
                        join tstore in S on td.ToStoreId equals tstore.SID
                        join item in unitOfWork.ItemRepository.Get() on td.ItemId equals item.ItemId
                        join unit in unitOfWork.UnitRepository.Get() on td.UnitId equals unit.UnitId
                        join c in unitOfWork.ConditionOfItemRepository.Get() on td.ConditionOfItemId equals c.ConditionOfItemId
                        where s.FromStoreId == strstoreid
                        select new TransferViewModel
                        {
                            TransferId=s.TransferId,
                            TransferDetailsId=td.TransferDetailId,
                            TransferOrderId = td.TransferOrderId,
                            FromStoreId = s.FromStoreId,
                            FromStoreName = store.Name,
                            ToStoreId = td.ToStoreId,
                            ToStoreName = tstore.Name,
                            ItemId = td.ItemId,
                            ItemName = item.ItemName,
                            TransactionQuantity = td.TransactionQuantity,
                            UnitId = td.UnitId,
                            UnitName = unit.UnitName,
                            TransferTypeId = td.TransferTypeId,
                            TransferTypeName = td.TransferType.TransferTypeName,
                            ConditionOfItemId = td.ConditionOfItemId,
                            ConditionOfItemName = c.ConditionOfItemName,
                            Recieve = td.Recieve,
                            ReceiveItemQuantity = td.ReceiveItemQuantity,
                            PendingItemQuantity = td.PendingItemQuantity,
                            RecieveBy = td.RecieveBy,
                            DateOfReceive = td.DateOfReceive,
                            CreateBy = s.CreateBy,
                            CreateDate = s.CreateDate,
                            DateOfActualTransfer=td.DateOfActualTransfer

                        }).ToList();



            return data;

        }


        public List<TransferViewModel> GetSearchInformation(TransferViewModel transferViewModel)
        {

            var S = (from s in unitOfWork.StoreRepository.Get()
                     select new
                     {

                         SID = s.StoreId,
                         Name = s.StoreName

                     }).Union(
                      (from s in unitOfWork.SubStoreRepository.Get()
                       select new
                       {

                           SID = s.SubStoreId,
                           Name = s.SubStoreName,

                       })).Union(
                       (from s in unitOfWork.SubSubStoreRepository.Get()
                        select new
                        {

                            SID = s.SubSubStoreId,
                            Name = s.SubSubStoreName,

                        })).Union(
                       (from s in unitOfWork.SubSubSubStoreRepository.Get()
                        select new
                        {

                            SID = s.SubSubSubStoreId,
                            Name = s.SubSubSubStoreName,

                        })).Union(
                       (from s in unitOfWork.SubSubSubSubStoreRepository.Get()
                        select new
                        {

                            SID = s.SubSubSubSubStoreId,
                            Name = s.SubSubSubSubStoreName,

                        }));

            var r = (from s in unitOfWork.TransferMasterRepository.Get()
                     join td in unitOfWork.TransferDetailsRepository.Get() on s.TransferId equals td.TransferId
                     join to in unitOfWork.TransferOrderRepository.Get() on td.TransferOrderId equals to.TransferOrderId
                     join store in S on s.FromStoreId equals store.SID
                     join tstore in S on td.ToStoreId equals tstore.SID
                     join item in unitOfWork.ItemRepository.Get() on td.ItemId equals item.ItemId
                     join unit in unitOfWork.UnitRepository.Get() on td.UnitId equals unit.UnitId
                     join c in unitOfWork.ConditionOfItemRepository.Get() on td.ConditionOfItemId equals c.ConditionOfItemId
                     where s.FromStoreId == transferViewModel.FromStoreId
                     select new TransferViewModel
                     {
                         TransferId = s.TransferId,
                         TransferDetailsId = td.TransferDetailId,
                         TransferOrderId = td.TransferOrderId,
                         FromStoreId = s.FromStoreId,
                         FromStoreName = store.Name,
                         ToStoreId = td.ToStoreId,
                         ToStoreName = tstore.Name,
                         ItemId = td.ItemId,
                         ItemName = item.ItemName,
                         TransactionQuantity = td.TransactionQuantity,
                         UnitId = td.UnitId,
                         UnitName = unit.UnitName,
                         TransferTypeId = td.TransferTypeId,
                         TransferTypeName = td.TransferType.TransferTypeName,
                         ConditionOfItemId = td.ConditionOfItemId,
                         ConditionOfItemName = c.ConditionOfItemName,
                         Recieve = td.Recieve,
                         ReceiveItemQuantity = td.ReceiveItemQuantity,
                         PendingItemQuantity = td.PendingItemQuantity,
                         RecieveBy = td.RecieveBy,
                         DateOfReceive = td.DateOfReceive,
                         CreateBy = s.CreateBy,
                         CreateDate = s.CreateDate,
                         DateOfActualTransfer = td.DateOfActualTransfer

                     });

            if (transferViewModel.ToStoreId > 0)
                r = r.Where(p => p.ToStoreId == transferViewModel.ToStoreId);

            if (transferViewModel.ItemId > 0)
                r = r.Where(p => p.ItemId == transferViewModel.ItemId);


            if (transferViewModel.ConditionOfItemId > 0)
                r = r.Where(p => p.ConditionOfItemId == transferViewModel.ConditionOfItemId);

            if (transferViewModel.TransferTypeId > 0)
                r = r.Where(p => p.TransferTypeId == transferViewModel.TransferTypeId);

            if (transferViewModel.TransferCriteria == "Received")
                r = r.Where(p => p.Recieve == "Received");

            if (transferViewModel.TransferCriteria == "Pending")
                r = r.Where(p => p.Recieve == "Pending");

            if (transferViewModel.TransferCriteria == "Canceled")
                r = r.Where(p => p.Recieve == "Canceled");

            if (transferViewModel.FromDate != null && transferViewModel.ToDate != null)
                r = r.Where(p => p.DateOfActualTransfer >= transferViewModel.FromDate && p.DateOfActualTransfer <= transferViewModel.ToDate);

            var data = r.ToList();

            return data;

        }



        public IEnumerable<TransferViewModel> GetTransferInformation(int transDetid)
        {

            var S = (from s in unitOfWork.StoreRepository.Get()
                     select new
                     {

                         SID = s.StoreId,
                         Name = s.StoreName

                     }).Union(
                      (from s in unitOfWork.SubStoreRepository.Get()
                       select new
                       {

                           SID = s.SubStoreId,
                           Name = s.SubStoreName,

                       })).Union(
                       (from s in unitOfWork.SubSubStoreRepository.Get()
                        select new
                        {

                            SID = s.SubSubStoreId,
                            Name = s.SubSubStoreName,

                        })).Union(
                       (from s in unitOfWork.SubSubSubStoreRepository.Get()
                        select new
                        {

                            SID = s.SubSubSubStoreId,
                            Name = s.SubSubSubStoreName,

                        })).Union(
                       (from s in unitOfWork.SubSubSubSubStoreRepository.Get()
                        select new
                        {

                            SID = s.SubSubSubSubStoreId,
                            Name = s.SubSubSubSubStoreName,

                        }));

            var data = (from s in unitOfWork.TransferMasterRepository.Get()
                     join td in unitOfWork.TransferDetailsRepository.Get() on s.TransferId equals td.TransferId
                     join to in unitOfWork.TransferOrderRepository.Get() on td.TransferOrderId equals to.TransferOrderId
                     join store in S on s.FromStoreId equals store.SID
                     join tstore in S on td.ToStoreId equals tstore.SID
                     join item in unitOfWork.ItemRepository.Get() on td.ItemId equals item.ItemId
                     join unit in unitOfWork.UnitRepository.Get() on td.UnitId equals unit.UnitId
                     join c in unitOfWork.ConditionOfItemRepository.Get() on td.ConditionOfItemId equals c.ConditionOfItemId
                     where td.TransferDetailId== transDetid
                     select new TransferViewModel
                     {
                         TransferId = s.TransferId,
                         TransferDetailsId = td.TransferDetailId,
                         TransferOrderId = td.TransferOrderId,
                         FromStoreId = s.FromStoreId,
                         FromStoreName = store.Name,
                         ToStoreId = td.ToStoreId,
                         ToStoreName = tstore.Name,
                         ItemId = td.ItemId,
                         ItemName = item.ItemName,
                         TransactionQuantity = td.TransactionQuantity,
                         UnitId = td.UnitId,
                         UnitName = unit.UnitName,
                         TransferTypeId = td.TransferTypeId,
                         TransferTypeName = td.TransferType.TransferTypeName,
                         ConditionOfItemId = td.ConditionOfItemId,
                         ConditionOfItemName = c.ConditionOfItemName,
                         Recieve = td.Recieve,
                         ReceiveItemQuantity = td.ReceiveItemQuantity,
                         PendingItemQuantity = td.PendingItemQuantity,
                         RecieveBy = td.RecieveBy,
                         DateOfReceive = td.DateOfReceive,
                         CreateBy = s.CreateBy,
                         CreateDate = s.CreateDate,
                         DateOfActualTransfer = td.DateOfActualTransfer

                     }).AsEnumerable();


         

            return data;

        }

    }
}
