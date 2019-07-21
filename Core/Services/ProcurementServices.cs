using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.ViewModels.ProcurementMasterViewModel;

namespace Core.Services
{
    public class ProcurementServices
    {

        public UnitOfWork UnitOfWork;

        public ProcurementServices(UnitOfWork unitOfWork)
        {

            UnitOfWork = unitOfWork;
        }

        public IEnumerable<ProcurementShowViewModel> GetAll()
        {
            var data = (from ph in UnitOfWork.ProcurementMasterRepository.Get()
                        join pd in UnitOfWork.ProcurementDetailsRepository.Get()
                        on ph.PO_HD_ID equals pd.PO_HD_ID
                        join item in UnitOfWork.ItemRepository.Get() on pd.ItemId equals item.ItemId
                        join sto in UnitOfWork.StoreRepository.Get() on ph.StoreId equals sto.StoreId
                        join cat in UnitOfWork.CategoryRepository.Get()
                        on pd.CategoryId equals cat.CategoryId
                        join supp in UnitOfWork.SupplierCompanyRepository.Get() on ph.SupplierCompanyId equals supp.SupplierCompanyId
                        join ptype in UnitOfWork.ProcurementTypeRepository.Get() on ph.ProcurementTypeId equals ptype.ProcurementTypeId

                        select new ProcurementShowViewModel
                        {

                            PO_HD_ID = ph.PO_HD_ID,
                            StoreName = sto.StoreName,
                            CategoryName = cat.CategoryName,
                            ItemName = item.ItemName,
                            PO_Price = pd.PO_Price,
                            PO_QTD = pd.PO_QTD,
                            PO_SubTotal = pd.PO_SubTotal,

                            PO_GRAND_TOTAL = ph.PO_GRAND_TOTAL,
                            SupplierCompanyName = supp.SupplierCompanyName,
                            ProcurementTypeName = ptype.ProcurementTypeName,
                            CreateDate = Convert.ToDateTime(ph.CreateDate)




                        }).AsEnumerable();

            return data;
        }



        public void Create(ProcurementMasterViewModel ProcurementMasterViewModel, ProcurementDetailsViewModel[] procurementDetailsViewModel, int intStoreId, string strUserName)
        {
            ProcurementMaster ProcurementMaster = new ProcurementMaster();


            ProcurementMaster.DateOfPurchase = ProcurementMasterViewModel.DateOfPurchase;
            ProcurementMaster.DateOfReceive = ProcurementMasterViewModel.DateOfReceive;
            ProcurementMaster.PO_GRAND_TOTAL = ProcurementMasterViewModel.PO_GRAND_TOTAL;
            ProcurementMaster.ProcurementTypeId = ProcurementMasterViewModel.ProcurementTypeId;
            ProcurementMaster.SupplierCompanyId = ProcurementMasterViewModel.SupplierCompanyId;
            ProcurementMaster.SubContractCompanyId = ProcurementMasterViewModel.SubContractCompanyId;
            if (intStoreId >= 1000 && intStoreId < 2000)
            {
                ProcurementMaster.StoreId = intStoreId;
            }
            else if (intStoreId >= 2000 && intStoreId < 3000)
            {
                ProcurementMaster.SubStoreId = intStoreId;
            }
            else if (intStoreId >= 3000 && intStoreId < 4000)
            {
                ProcurementMaster.SubSubStoreId = intStoreId;
            }
            else if (intStoreId >= 4000 && intStoreId < 5000)
            {
                ProcurementMaster.SubSubSubStoreId = intStoreId;
            }
            else if (intStoreId >= 5000 && intStoreId < 6000)
            {
                ProcurementMaster.SubSubSubSubStoreId = intStoreId;
            }
            ProcurementMaster.PurchasedBy_ProcureBy = 1;
            ProcurementMaster.CreateBy = strUserName;
            ProcurementMaster.CreateDate = DateTime.Now;
            ProcurementMaster.UpdateBy = strUserName;
            ProcurementMaster.UpdateDate = DateTime.Now;



            UnitOfWork.ProcurementMasterRepository.Insert(ProcurementMaster);
            UnitOfWork.Save();

            var Pohdid = ProcurementMaster.PO_HD_ID;
            foreach (var items in procurementDetailsViewModel)
            {
                var ProcurementDetails = new ProcurementDetails
                {
                    PO_HD_ID = Pohdid,
                    DateOfExpired = Convert.ToDateTime(items.DateOfExpired),
                    DateOfNextMaintainance = Convert.ToDateTime(items.DateOfNextMaintainance),
                    Barcode = items.Barcode,
                    Item_Unique_Number = items.Item_Unique_Number,
                    Chesis_Number = items.Chesis_Number,
                    Engine_Number = items.Engine_Number,
                    ItemId = items.ItemId,
                    CountryId = items.CountryId,
                    Principle_id = items.Principle_id,
                    ConditionOfItemId = items.ConditionOfItemId,
                    WarrantyId = items.WarrantyId,
                    PO_QTD = items.PO_QTD,
                    PO_Price = items.PO_Price,
                    SO_Price = items.SO_Price,
                    PO_SubTotal = items.PO_SubTotal,
                    CategoryId = items.CategoryId,
                    SubCategoryId = items.SubCategoryId,
                    SubSubCategoryId = items.SubSubCategoryId,
                    SubSubSubCategoryId = items.SubSubSubCategoryId,
                    SubSubSubSubCategoryId = items.SubSubSubSubCategoryId,
                    BrandId = items.BrandId,
                    ModelId = items.ModelId,
                    UnitId = items.UnitId,
                    MethodId = items.MethodId,
                    IsNew = items.IsNew,
                    IsHot = items.IsHot



                };

                UnitOfWork.ProcurementDetailsRepository.Insert(ProcurementDetails);
                UnitOfWork.Save();
            }

            InventoryMaster InventoryMaster = new InventoryMaster();

            InventoryMaster.PO_HD_ID = Pohdid;
            InventoryMaster.DateOfPurchase = ProcurementMasterViewModel.DateOfPurchase;
            InventoryMaster.DateOfReceive = ProcurementMasterViewModel.DateOfReceive;
            InventoryMaster.PO_GRAND_TOTAL = ProcurementMasterViewModel.PO_GRAND_TOTAL;
            InventoryMaster.ProcurementTypeId = ProcurementMasterViewModel.ProcurementTypeId;
            InventoryMaster.SupplierCompanyId = ProcurementMasterViewModel.SupplierCompanyId;
            InventoryMaster.SubContractCompanyId = ProcurementMasterViewModel.SubContractCompanyId;
            if (intStoreId >= 1000 && intStoreId < 2000)
            {
                InventoryMaster.StoreId = intStoreId;
            }
            else if (intStoreId >= 2000 && intStoreId < 3000)
            {
                InventoryMaster.SubStoreId = intStoreId;
            }
            else if (intStoreId >= 3000 && intStoreId < 4000)
            {
                InventoryMaster.SubSubStoreId = intStoreId;
            }
            else if (intStoreId >= 4000 && intStoreId < 5000)
            {
                InventoryMaster.SubSubSubStoreId = intStoreId;
            }
            else if (intStoreId >= 5000 && intStoreId < 6000)
            {
                InventoryMaster.SubSubSubSubStoreId = intStoreId;
            }
            InventoryMaster.PurchasedBy_ProcureBy = 1;
            InventoryMaster.CreateBy = strUserName;
            InventoryMaster.CreateDate = DateTime.Now;
            InventoryMaster.UpdateBy = strUserName;
            InventoryMaster.UpdateDate = DateTime.Now;



            UnitOfWork.InventoryMasterRepository.Insert(InventoryMaster);
            UnitOfWork.Save();




            var invId = InventoryMaster.Inv_HD_ID;


            foreach (var items in procurementDetailsViewModel)
            {


                InventoryDetail InventoryDetails = new InventoryDetail();

                InventoryDetails.Inv_HD_ID = invId;
                InventoryDetails.DateOfExpired = Convert.ToDateTime(items.DateOfExpired);
                InventoryDetails.DateOfNextMaintainance = Convert.ToDateTime(items.DateOfNextMaintainance);
                InventoryDetails.Barcode = items.Barcode;
                InventoryDetails.Item_Unique_Number = items.Item_Unique_Number;
                InventoryDetails.Chesis_Number = items.Chesis_Number;
                InventoryDetails.Engine_Number = items.Engine_Number;
                InventoryDetails.ItemId = items.ItemId;
                InventoryDetails.CountryId = items.CountryId;
                InventoryDetails.Principle_id = items.Principle_id;
                InventoryDetails.ConditionOfItemId = items.ConditionOfItemId;
                InventoryDetails.WarrantyId = items.WarrantyId;
                InventoryDetails.TransactionQty = items.PO_QTD;
                InventoryDetails.PO_Price = items.PO_Price;
                InventoryDetails.SO_Price = items.SO_Price;
                InventoryDetails.PO_SubTotal = items.PO_SubTotal;
                InventoryDetails.CategoryId = items.CategoryId;
                InventoryDetails.SubCategoryId = items.SubCategoryId;
                InventoryDetails.SubSubCategoryId = items.SubSubCategoryId;
                InventoryDetails.SubSubSubCategoryId = items.SubSubSubCategoryId;
                InventoryDetails.SubSubSubSubCategoryId = items.SubSubSubSubCategoryId;
                InventoryDetails.BrandId = items.BrandId;
                InventoryDetails.ModelId = items.ModelId;
                InventoryDetails.UnitId = items.UnitId;
                InventoryDetails.MethodId = items.MethodId;
                InventoryDetails.AvailableQty = items.PO_QTD;
                InventoryDetails.IsNew = items.IsNew;
                InventoryDetails.IsHot = items.IsHot;
                UnitOfWork.InventoryDetailRepository.Insert(InventoryDetails);
                UnitOfWork.Save();
            }
        }


        public List<ReportViewModel> ItemLoad()
        {



            //var kkk = UnitOfWork.InventoryDetailRepository.Get().Select(s1 => s1.ItemId).ToList().Except(UnitOfWork.SalesElementStupRepoRepository.Get().Select(s2 => s2.ItemId).ToList()).ToList();

            var data = new List<ReportViewModel>();


            var inventoryitemlst = (from pd in UnitOfWork.ProcurementDetailsRepository.Get()
                                    join item in UnitOfWork.ItemRepository.Get() on pd.ItemId equals item.ItemId

                                    group pd by new { pd.ItemId, item.ItemName } into g

                                    select new
                                    {

                                        ItemId = g.Key.ItemId,
                                        ItemName = g.Key.ItemName


                                    }).ToList();


            var salespriceitem = (from pd in UnitOfWork.SalesElementStupRepoRepository.Get()
                                  join item in UnitOfWork.ItemRepository.Get() on pd.ItemId equals item.ItemId

                                  select new
                                  {

                                      ItemId = pd.ItemId,
                                      ItemName = item.ItemName


                                  }).ToList();

            var Uncommon = inventoryitemlst.Except(salespriceitem).ToList();
            foreach (var itms in Uncommon)
            {

                ReportViewModel lstitem = new ReportViewModel();
                lstitem.ItemId = itms.ItemId;
                lstitem.ItemName = itms.ItemName;

                data.Add(lstitem);

            }

            return data;
        }

        public List<SalesElementStupViewModel> SalesAmountView()
        {


            var data = (from sm in UnitOfWork.SalesElementStupRepoRepository.Get()
                        join itm in UnitOfWork.ItemRepository.Get() on sm.ItemId equals itm.ItemId

                        select new SalesElementStupViewModel
                        {
                            SalesElementSetupId = sm.SalesElementSetupId,
                            ItemId = sm.ItemId,
                            ItemName = itm.ItemName,
                            Product_Image = itm.Product_Image,
                            SalesPricePercent = sm.SalesPricePercent,
                            SalesPriceAmount = sm.SalesPriceAmount,
                            DiscountPercent = sm.DiscountPercent,
                            DiscountAmount = sm.DiscountAmount,
                            VatPercent = sm.VatPercent,
                            VarAmount = sm.VarAmount,



                        }).ToList();

            return data;
        }

        public SalesElementStupViewModel Saleselementedit(int id)
        {


            var data = (from sm in UnitOfWork.SalesElementStupRepoRepository.Get()
                        join itm in UnitOfWork.ItemRepository.Get() on sm.ItemId equals itm.ItemId
                        where sm.SalesElementSetupId == id

                        select new SalesElementStupViewModel
                        {
                            SalesElementSetupId = sm.SalesElementSetupId,
                            ItemId = sm.ItemId,
                            ItemName = itm.ItemName,
                            Product_Image = itm.Product_Image,
                            SalesPricePercent = sm.SalesPricePercent,
                            SalesPriceAmount = sm.SalesPriceAmount,
                            DiscountPercent = sm.DiscountPercent,
                            DiscountAmount = sm.DiscountAmount,
                            VatPercent = sm.VatPercent,
                            VarAmount = sm.VarAmount,
                            BrandId = sm.BrandId,
                            ModelId = sm.ModelId,
                            PurchasePrice = sm.PurchasePrice

                        }).FirstOrDefault();

            return data;
        }
        public ReportViewModel Itemwiseelementload(int id)
        {


            var data = (from pd in UnitOfWork.InventoryDetailRepository.Get()
                        join item in UnitOfWork.ItemRepository.Get() on pd.ItemId equals item.ItemId
                        into it
                        from item in it.DefaultIfEmpty()
                        join brand in UnitOfWork.BrandRepository.Get() on pd.BrandId equals brand.BrandId
                          into br
                        from brand in br.DefaultIfEmpty()
                        join model in UnitOfWork.ModelRepository.Get() on pd.ModelId equals model.ModelId
                         into mo
                        from model in mo.DefaultIfEmpty()

                        where pd.ItemId == id

                        group pd by new { pd.ItemId, item.ItemName, item.Product_Image, brand.BrandName, pd.BrandId, pd.ModelId, model.ModelName, item.MethodId } into g


                        select new 
                        {

                            ItemId = g.Key.ItemId,
                            ItemName = g.Key.ItemName,
                            Product_Image = g.Key.Product_Image,
                            BrandId = g.Key.BrandId,
                            BrandName = g.Key.BrandName,
                            ModelId = g.Key.ModelId,
                            ModelName = g.Key.ModelName,
                            MethodId = g.Key.MethodId,
                            PO_Price = g.Sum(p => p.PO_Price)

                        }).FirstOrDefault();

            ReportViewModel ItemwisePrice = new ReportViewModel();

            ItemwisePrice.Product_Image = data.Product_Image;
            ItemwisePrice.BrandName = data.BrandName;
            ItemwisePrice.ModelName = data.ModelName;
            ItemwisePrice.ModelId = data.ModelId;
            ItemwisePrice.BrandId = data.BrandId;
            if (data.MethodId == 1)
            {
                ItemwisePrice.PO_Price = Convert.ToDecimal(UnitOfWork.InventoryDetailRepository.Get().Where(l => l.ItemId == data.ItemId).OrderBy(l => l.PO_Price).Select(h => h.PO_Price).FirstOrDefault());
            }
            else if (data.MethodId == 2)
            {
                ItemwisePrice.PO_Price = Convert.ToDecimal(UnitOfWork.InventoryDetailRepository.Get().Where(l => l.ItemId == data.ItemId).OrderByDescending(l => l.PO_Price).Select(h => h.PO_Price).FirstOrDefault());

            }

            else if (data.MethodId == 3)
            {
                ItemwisePrice.PO_Price = data.PO_Price;
            }

            return ItemwisePrice;
        }

        public void Create(SalesElementStupViewModel salesElementStupViewModel)
        {
            if(salesElementStupViewModel.SalesElementSetupId >0)
            {
                var saleselementsetup = new SalesElementStup
                {
                    SalesElementSetupId = salesElementStupViewModel.SalesElementSetupId,
                    ItemId = salesElementStupViewModel.ItemId,
                    SalesPricePercent = salesElementStupViewModel.SalesPricePercent,
                    SalesPriceAmount = salesElementStupViewModel.SalesPriceAmount,
                    DiscountPercent = salesElementStupViewModel.DiscountPercent,
                    DiscountAmount = salesElementStupViewModel.DiscountAmount,
                    VatPercent = salesElementStupViewModel.VatPercent,
                    VarAmount = salesElementStupViewModel.VarAmount,
                    BrandId = salesElementStupViewModel.BrandId,
                    ModelId = salesElementStupViewModel.ModelId,
                    PurchasePrice = salesElementStupViewModel.PurchasePrice


                };

                UnitOfWork.SalesElementStupRepoRepository.Update(saleselementsetup);
                UnitOfWork.Save();
            }
            else
            {
                var saleselementsetup = new SalesElementStup
                {

                    ItemId = salesElementStupViewModel.ItemId,
                    SalesPricePercent = salesElementStupViewModel.SalesPricePercent,
                    SalesPriceAmount = salesElementStupViewModel.SalesPriceAmount,
                    DiscountPercent = salesElementStupViewModel.DiscountPercent,
                    DiscountAmount = salesElementStupViewModel.DiscountAmount,
                    VatPercent = salesElementStupViewModel.VatPercent,
                    VarAmount = salesElementStupViewModel.VarAmount,
                    BrandId = salesElementStupViewModel.BrandId,
                    ModelId = salesElementStupViewModel.ModelId,
                    PurchasePrice = salesElementStupViewModel.PurchasePrice


                };

                UnitOfWork.SalesElementStupRepoRepository.Insert(saleselementsetup);
                UnitOfWork.Save();
                
            }
           
        }


    }

}

