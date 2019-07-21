using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ReportService
    {
        private UnitOfWork unitOfWork;

        public ReportService(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IEnumerable<ReportViewModel> Getprocureitem()
        {
            var data = (from pm in unitOfWork.ProcurementMasterRepository.Get()
                        join pd in unitOfWork.ProcurementDetailsRepository.Get()
                        on pm.PO_HD_ID equals pd.PO_HD_ID
                        join itm in unitOfWork.ItemRepository.Get()
                        on pd.ItemId equals itm.ItemId

                        group pd by new { pd.ItemId, itm.ItemName } into g

                        select new ReportViewModel
                        {
                            ItemId = g.Key.ItemId,
                            ItemName = g.Key.ItemName
                        }).AsEnumerable();
            return data;

        }
        public IEnumerable<ReportViewModel> Getprocuretype()
        {
            var data = (from pm in unitOfWork.ProcurementMasterRepository.Get()
                        join pd in unitOfWork.ProcurementDetailsRepository.Get()
                        on pm.PO_HD_ID equals pd.PO_HD_ID
                        join pt in unitOfWork.ProcurementTypeRepository.Get()
                        on pm.ProcurementTypeId equals pt.ProcurementTypeId

                        group pm by new { pm.ProcurementTypeId, pt.ProcurementTypeName } into g
                        select new ReportViewModel
                        {
                            ProcurementTypeId = g.Key.ProcurementTypeId,
                            ProcurementTypeName = g.Key.ProcurementTypeName

                        }).AsEnumerable();
            return data;

        }
        public List<ReportViewModel> GetProcurementReport(int itemid, int? procuretypeid, DateTime? Fromdate, DateTime? Todate)
        {
            var data = new List<ReportViewModel>();

            if (itemid > 0)
            {
                if (procuretypeid != null)
                {
                    if (Fromdate != null && Todate != null)
                    {
                        data = (from pm in unitOfWork.ProcurementMasterRepository.Get()
                                join pd in unitOfWork.ProcurementDetailsRepository.Get()
                                on pm.PO_HD_ID equals pd.PO_HD_ID
                                join itm in unitOfWork.ItemRepository.Get()
                                on pd.ItemId equals itm.ItemId
                                join com in unitOfWork.SupplierCompanyRepository.Get()
                                on pm.SupplierCompanyId equals com.SupplierCompanyId
                                join sto in unitOfWork.StoreRepository.Get()
                                on pm.StoreId equals sto.StoreId
                                join pt in unitOfWork.ProcurementTypeRepository.Get()
                                on pm.ProcurementTypeId equals pt.ProcurementTypeId
                                join country in unitOfWork.CountryRepository.Get()
                                on pd.CountryId equals country.CountryId
                                join itmcondition in unitOfWork.ConditionOfItemRepository.Get()
                                on pd.ConditionOfItemId equals itmcondition.ConditionOfItemId 
                                join unit in unitOfWork.UnitRepository.Get()
                                on pd.UnitId equals unit.UnitId
                                join warrantyperiod in unitOfWork.WarrantyRepository.Get()
                                on pd.WarrantyId equals warrantyperiod.WarrantyId into wn
                                from warrantyperiod in wn.DefaultIfEmpty()

                                where pd.ItemId == itemid && pm.ProcurementTypeId == procuretypeid && (pm.DateOfPurchase >= Fromdate && pm.DateOfPurchase <= Todate)
                                select new ReportViewModel
                                {
                                    ItemName = itm.ItemName,
                                    ProcurementTypeName = pt.ProcurementTypeName,
                                    SupplierCompanyName = com.SupplierCompanyName,
                                    WarrantyName = warrantyperiod.WarrantyPeriod,
                                    ConditionOfItemName = itmcondition.ConditionOfItemName,
                                    PO_QTD = pd.PO_QTD,
                                    UnitName = unit.UnitName,
                                    PO_Price = pd.PO_Price,
                                    CountryName = country.CountryName,
                                    DateOfExpired = pd.DateOfExpired,
                                    Item_Unique_Number = pd.Item_Unique_Number,
                                    Chesis_Number = pd.Chesis_Number,
                                    Engine_Number = pd.Engine_Number,
                                    DateOfNextMaintainance = pd.DateOfNextMaintainance,
                                    Barcode = pd.Barcode,
                                    DateOfReceive = pm.DateOfReceive,
                                    DateOfPurchase = pm.DateOfPurchase


                                }).ToList();
                    }
                    else
                    {

                        data = (from pm in unitOfWork.ProcurementMasterRepository.Get()
                                join pd in unitOfWork.ProcurementDetailsRepository.Get()
                                on pm.PO_HD_ID equals pd.PO_HD_ID
                                join itm in unitOfWork.ItemRepository.Get()
                                on pd.ItemId equals itm.ItemId
                                join com in unitOfWork.SupplierCompanyRepository.Get()
                                on pm.SupplierCompanyId equals com.SupplierCompanyId
                                join sto in unitOfWork.StoreRepository.Get()
                                on pm.StoreId equals sto.StoreId
                                join pt in unitOfWork.ProcurementTypeRepository.Get()
                                on pm.ProcurementTypeId equals pt.ProcurementTypeId
                                join country in unitOfWork.CountryRepository.Get()
                                on pd.CountryId equals country.CountryId
                                join itmcondition in unitOfWork.ConditionOfItemRepository.Get()
                                on pd.ConditionOfItemId equals itmcondition.ConditionOfItemId

                                join unit in unitOfWork.UnitRepository.Get()
                                on pd.UnitId equals unit.UnitId
                                join warrantyperiod in unitOfWork.WarrantyRepository.Get()
                                on pd.WarrantyId equals warrantyperiod.WarrantyId into wn
                                from warrantyperiod in wn.DefaultIfEmpty()

                                where pd.ItemId == itemid
                                select new ReportViewModel
                                {
                                    ItemName = itm.ItemName,
                                    ProcurementTypeName = pt.ProcurementTypeName,
                                    SupplierCompanyName = com.SupplierCompanyName,
                                    WarrantyName = warrantyperiod.WarrantyPeriod,
                                    ConditionOfItemName = itmcondition.ConditionOfItemName,
                                    PO_QTD = pd.PO_QTD,
                                    UnitName = unit.UnitName,
                                    PO_Price = pd.PO_Price,
                                    CountryName = country.CountryName,
                                    DateOfExpired = pd.DateOfExpired,
                                    Item_Unique_Number = pd.Item_Unique_Number,
                                    Chesis_Number = pd.Chesis_Number,
                                    Engine_Number = pd.Engine_Number,
                                    DateOfNextMaintainance = pd.DateOfNextMaintainance,
                                    Barcode = pd.Barcode,
                                    DateOfReceive = pm.DateOfReceive,
                                    DateOfPurchase = pm.DateOfPurchase


                                }).ToList();


                    }

                }
                else
                {
                    if (Fromdate != null && Todate != null)
                    {
                        data = (from pm in unitOfWork.ProcurementMasterRepository.Get()
                                join pd in unitOfWork.ProcurementDetailsRepository.Get()
                                on pm.PO_HD_ID equals pd.PO_HD_ID
                                join itm in unitOfWork.ItemRepository.Get()
                                on pd.ItemId equals itm.ItemId
                                join com in unitOfWork.SupplierCompanyRepository.Get()
                                on pm.SupplierCompanyId equals com.SupplierCompanyId
                                join sto in unitOfWork.StoreRepository.Get()
                                on pm.StoreId equals sto.StoreId
                                join pt in unitOfWork.ProcurementTypeRepository.Get()
                                on pm.ProcurementTypeId equals pt.ProcurementTypeId
                                join country in unitOfWork.CountryRepository.Get()
                                on pd.CountryId equals country.CountryId
                                join itmcondition in unitOfWork.ConditionOfItemRepository.Get()
                                on pd.ConditionOfItemId equals itmcondition.ConditionOfItemId
                                join unit in unitOfWork.UnitRepository.Get()
                                on pd.UnitId equals unit.UnitId
                                join warrantyperiod in unitOfWork.WarrantyRepository.Get()
                                on pd.WarrantyId equals warrantyperiod.WarrantyId into wn
                                from warrantyperiod in wn.DefaultIfEmpty()

                                where pd.ItemId == itemid && (pm.DateOfPurchase >= Fromdate && pm.DateOfPurchase <= Todate)
                                select new ReportViewModel
                                {
                                    ItemName = itm.ItemName,
                                    ProcurementTypeName = pt.ProcurementTypeName,
                                    SupplierCompanyName = com.SupplierCompanyName,
                                    WarrantyName = warrantyperiod.WarrantyPeriod,
                                    ConditionOfItemName = itmcondition.ConditionOfItemName,
                                    PO_QTD = pd.PO_QTD,
                                    UnitName = unit.UnitName,
                                    PO_Price = pd.PO_Price,
                                    CountryName = country.CountryName,
                                    DateOfExpired = pd.DateOfExpired,
                                    Item_Unique_Number = pd.Item_Unique_Number,
                                    Chesis_Number = pd.Chesis_Number,
                                    Engine_Number = pd.Engine_Number,
                                    DateOfNextMaintainance = pd.DateOfNextMaintainance,
                                    Barcode = pd.Barcode,
                                    DateOfReceive = pm.DateOfReceive,
                                    DateOfPurchase = pm.DateOfPurchase


                                }).ToList();
                    }
                    else
                    {
                        data = (from pm in unitOfWork.ProcurementMasterRepository.Get()
                                join pd in unitOfWork.ProcurementDetailsRepository.Get()
                                on pm.PO_HD_ID equals pd.PO_HD_ID
                                join itm in unitOfWork.ItemRepository.Get()
                                on pd.ItemId equals itm.ItemId
                                join com in unitOfWork.SupplierCompanyRepository.Get()
                                on pm.SupplierCompanyId equals com.SupplierCompanyId
                                join sto in unitOfWork.StoreRepository.Get()
                                on pm.StoreId equals sto.StoreId
                                join pt in unitOfWork.ProcurementTypeRepository.Get()
                                on pm.ProcurementTypeId equals pt.ProcurementTypeId
                                join country in unitOfWork.CountryRepository.Get()
                                on pd.CountryId equals country.CountryId
                                join itmcondition in unitOfWork.ConditionOfItemRepository.Get()
                                on pd.ConditionOfItemId equals itmcondition.ConditionOfItemId
                                join unit in unitOfWork.UnitRepository.Get()
                                on pd.UnitId equals unit.UnitId
                                join warrantyperiod in unitOfWork.WarrantyRepository.Get()
                                on pd.WarrantyId equals warrantyperiod.WarrantyId into wn
                                from warrantyperiod in wn.DefaultIfEmpty()

                                where pd.ItemId == itemid
                                select new ReportViewModel
                                {
                                    ItemName = itm.ItemName,
                                    ProcurementTypeName = pt.ProcurementTypeName,
                                    SupplierCompanyName = com.SupplierCompanyName,
                                    WarrantyName = warrantyperiod.WarrantyPeriod,
                                    ConditionOfItemName = itmcondition.ConditionOfItemName,
                                    PO_QTD = pd.PO_QTD,
                                    UnitName = unit.UnitName,
                                    PO_Price = pd.PO_Price,
                                    CountryName = country.CountryName,
                                    DateOfExpired = pd.DateOfExpired,
                                    Item_Unique_Number = pd.Item_Unique_Number,
                                    Chesis_Number = pd.Chesis_Number,
                                    Engine_Number = pd.Engine_Number,
                                    DateOfNextMaintainance = pd.DateOfNextMaintainance,
                                    Barcode = pd.Barcode,
                                    DateOfReceive = pm.DateOfReceive,
                                    DateOfPurchase = pm.DateOfPurchase


                                }).ToList();
                    }

                }

            }
            else
            {
                if (procuretypeid != null)
                {
                    if (Fromdate != null && Todate != null)
                    {
                        data = (from pm in unitOfWork.ProcurementMasterRepository.Get()
                                join pd in unitOfWork.ProcurementDetailsRepository.Get()
                                on pm.PO_HD_ID equals pd.PO_HD_ID
                                join itm in unitOfWork.ItemRepository.Get()
                                on pd.ItemId equals itm.ItemId
                                join com in unitOfWork.SupplierCompanyRepository.Get()
                                on pm.SupplierCompanyId equals com.SupplierCompanyId
                                join sto in unitOfWork.StoreRepository.Get()
                                on pm.StoreId equals sto.StoreId
                                join pt in unitOfWork.ProcurementTypeRepository.Get()
                                on pm.ProcurementTypeId equals pt.ProcurementTypeId
                                join country in unitOfWork.CountryRepository.Get()
                                on pd.CountryId equals country.CountryId
                                join itmcondition in unitOfWork.ConditionOfItemRepository.Get()
                                on pd.ConditionOfItemId equals itmcondition.ConditionOfItemId
                                join unit in unitOfWork.UnitRepository.Get()
                                on pd.UnitId equals unit.UnitId
                                join warrantyperiod in unitOfWork.WarrantyRepository.Get()
                                on pd.WarrantyId equals warrantyperiod.WarrantyId into wn
                                from warrantyperiod in wn.DefaultIfEmpty()

                                where pd.ItemId == itemid && pm.ProcurementTypeId == procuretypeid && (pm.DateOfPurchase >= Fromdate && pm.DateOfPurchase <= Todate)
                                select new ReportViewModel
                                {
                                    ItemName = itm.ItemName,
                                    ProcurementTypeName = pt.ProcurementTypeName,
                                    SupplierCompanyName = com.SupplierCompanyName,
                                    WarrantyName = warrantyperiod.WarrantyPeriod,
                                    ConditionOfItemName = itmcondition.ConditionOfItemName,
                                    PO_QTD = pd.PO_QTD,
                                    UnitName = unit.UnitName,
                                    PO_Price = pd.PO_Price,
                                    CountryName = country.CountryName,
                                    DateOfExpired = pd.DateOfExpired,
                                    Item_Unique_Number = pd.Item_Unique_Number,
                                    Chesis_Number = pd.Chesis_Number,
                                    Engine_Number = pd.Engine_Number,
                                    DateOfNextMaintainance = pd.DateOfNextMaintainance,
                                    Barcode = pd.Barcode,
                                    DateOfReceive = pm.DateOfReceive,
                                    DateOfPurchase = pm.DateOfPurchase


                                }).ToList();
                    }
                    else
                    {
                        data = (from pm in unitOfWork.ProcurementMasterRepository.Get()
                                join pd in unitOfWork.ProcurementDetailsRepository.Get()
                                on pm.PO_HD_ID equals pd.PO_HD_ID
                                join itm in unitOfWork.ItemRepository.Get()
                                on pd.ItemId equals itm.ItemId
                                join com in unitOfWork.SupplierCompanyRepository.Get()
                                on pm.SupplierCompanyId equals com.SupplierCompanyId
                                join sto in unitOfWork.StoreRepository.Get()
                                on pm.StoreId equals sto.StoreId
                                join pt in unitOfWork.ProcurementTypeRepository.Get()
                                on pm.ProcurementTypeId equals pt.ProcurementTypeId
                                join country in unitOfWork.CountryRepository.Get()
                                on pd.CountryId equals country.CountryId
                                join itmcondition in unitOfWork.ConditionOfItemRepository.Get()
                                on pd.ConditionOfItemId equals itmcondition.ConditionOfItemId
                                join unit in unitOfWork.UnitRepository.Get()
                                on pd.UnitId equals unit.UnitId
                                join warrantyperiod in unitOfWork.WarrantyRepository.Get()
                                on pd.WarrantyId equals warrantyperiod.WarrantyId into wn
                                from warrantyperiod in wn.DefaultIfEmpty()

                                where pm.ProcurementTypeId == procuretypeid
                                select new ReportViewModel
                                {
                                    ItemName = itm.ItemName,
                                    ProcurementTypeName = pt.ProcurementTypeName,
                                    SupplierCompanyName = com.SupplierCompanyName,
                                    WarrantyName = warrantyperiod.WarrantyPeriod,
                                    ConditionOfItemName = itmcondition.ConditionOfItemName,
                                    PO_QTD = pd.PO_QTD,
                                    UnitName = unit.UnitName,
                                    PO_Price = pd.PO_Price,
                                    CountryName = country.CountryName,
                                    DateOfExpired = pd.DateOfExpired,
                                    Item_Unique_Number = pd.Item_Unique_Number,
                                    Chesis_Number = pd.Chesis_Number,
                                    Engine_Number = pd.Engine_Number,
                                    DateOfNextMaintainance = pd.DateOfNextMaintainance,
                                    Barcode = pd.Barcode,
                                    DateOfReceive = pm.DateOfReceive,
                                    DateOfPurchase = pm.DateOfPurchase


                                }).ToList();
                    }

                }
                else
                {
                    if (Fromdate != null && Todate != null)
                    {
                        data = (from pm in unitOfWork.ProcurementMasterRepository.Get()
                                join pd in unitOfWork.ProcurementDetailsRepository.Get()
                                on pm.PO_HD_ID equals pd.PO_HD_ID
                                join itm in unitOfWork.ItemRepository.Get()
                                on pd.ItemId equals itm.ItemId
                                join com in unitOfWork.SupplierCompanyRepository.Get()
                                on pm.SupplierCompanyId equals com.SupplierCompanyId
                                join sto in unitOfWork.StoreRepository.Get()
                                on pm.StoreId equals sto.StoreId
                                join pt in unitOfWork.ProcurementTypeRepository.Get()
                                on pm.ProcurementTypeId equals pt.ProcurementTypeId
                                join country in unitOfWork.CountryRepository.Get()
                                on pd.CountryId equals country.CountryId
                                join itmcondition in unitOfWork.ConditionOfItemRepository.Get()
                                on pd.ConditionOfItemId equals itmcondition.ConditionOfItemId
                                join unit in unitOfWork.UnitRepository.Get()
                                on pd.UnitId equals unit.UnitId
                                join warrantyperiod in unitOfWork.WarrantyRepository.Get()
                                on pd.WarrantyId equals warrantyperiod.WarrantyId into wn
                                from warrantyperiod in wn.DefaultIfEmpty()


                                where pm.DateOfPurchase >= Fromdate && pm.DateOfPurchase <= Todate
                                select new ReportViewModel
                                {
                                    ItemName = itm.ItemName,
                                    ProcurementTypeName = pt.ProcurementTypeName,
                                    SupplierCompanyName = com.SupplierCompanyName,
                                    WarrantyName = warrantyperiod.WarrantyPeriod,
                                    ConditionOfItemName = itmcondition.ConditionOfItemName,
                                    PO_QTD = pd.PO_QTD,
                                    UnitName = unit.UnitName,
                                    PO_Price = pd.PO_Price,
                                    CountryName = country.CountryName,
                                    DateOfExpired = pd.DateOfExpired,
                                    Item_Unique_Number = pd.Item_Unique_Number,
                                    Chesis_Number = pd.Chesis_Number,
                                    Engine_Number = pd.Engine_Number,
                                    DateOfNextMaintainance = pd.DateOfNextMaintainance,
                                    Barcode = pd.Barcode,
                                    DateOfReceive = pm.DateOfReceive,
                                    DateOfPurchase = pm.DateOfPurchase


                                }).ToList();
                    }
                    else
                    {
                        data = (from pm in unitOfWork.ProcurementMasterRepository.Get()
                                join pd in unitOfWork.ProcurementDetailsRepository.Get()
                                on pm.PO_HD_ID equals pd.PO_HD_ID
                                join itm in unitOfWork.ItemRepository.Get()
                                on pd.ItemId equals itm.ItemId
                                join com in unitOfWork.SupplierCompanyRepository.Get()
                                on pm.SupplierCompanyId equals com.SupplierCompanyId
                                join sto in unitOfWork.StoreRepository.Get()
                                on pm.StoreId equals sto.StoreId
                                join pt in unitOfWork.ProcurementTypeRepository.Get()
                                on pm.ProcurementTypeId equals pt.ProcurementTypeId
                                join country in unitOfWork.CountryRepository.Get()
                                on pd.CountryId equals country.CountryId
                                join itmcondition in unitOfWork.ConditionOfItemRepository.Get()
                                on pd.ConditionOfItemId equals itmcondition.ConditionOfItemId
                                join unit in unitOfWork.UnitRepository.Get()
                                on pd.UnitId equals unit.UnitId
                                join warrantyperiod in unitOfWork.WarrantyRepository.Get()
                                on pd.WarrantyId equals warrantyperiod.WarrantyId into wn
                                from warrantyperiod in wn.DefaultIfEmpty()

                                select new ReportViewModel
                                {

                                    DateOfExpired = pd.DateOfExpired,
                                    Item_Unique_Number = pd.Item_Unique_Number,
                                    Chesis_Number = pd.Chesis_Number,
                                    Engine_Number = pd.Engine_Number,
                                    DateOfNextMaintainance = pd.DateOfNextMaintainance,
                                    Barcode = pd.Barcode,
                                    PO_QTD = pd.PO_QTD,
                                    PO_Price = pd.PO_Price,
                                    DateOfReceive = pm.DateOfReceive,
                                    DateOfPurchase = pm.DateOfPurchase,
                                    ItemName = itm.ItemName,
                                    SupplierCompanyName = com.SupplierCompanyName,
                                    ProcurementTypeName = pt.ProcurementTypeName,
                                    WarrantyName = warrantyperiod.WarrantyPeriod,
                                    ConditionOfItemName = itmcondition.ConditionOfItemName,
                                    UnitName = unit.UnitName,
                                    CountryName = country.CountryName,
                                    StoreName = sto.StoreName


                                }).ToList();
                    }

                }

            }

            return data;

        }





        public IEnumerable<ReportViewModel> GetProduction()
        {
            var data = (from promaster in unitOfWork.ProductionMasterRepository.Get()
                        join product in unitOfWork.ProductRepository.Get()
                        on promaster.ProductId equals product.ProductId

                        //group promaster by new {promaster.ProductId,product.ProductName,promaster.ProductionQuantity, promaster.GrandTotal,promaster.Productiondate} into g

                        select new ReportViewModel
                        {
                            ProductName = product.ProductName,
                            ProductionQuantity = promaster.ProductionQuantity,
                            ProductPrice = promaster.ProductPrice,
                            Productiondate = promaster.Productiondate,
                            ProductionMasterId = promaster.ProductionMasterId

                        }).AsEnumerable();
            return data;

        }

        public List<ReportViewModel> GetProductionwiseItem(int id)
        {
            var data = (from promaster in unitOfWork.ProductionMasterRepository.Get()
                        join prodetails in unitOfWork.ProductionDetailsRepository.Get()
                        on promaster.ProductionMasterId equals prodetails.ProductionMasterId
                        join product in unitOfWork.ProductRepository.Get()
                        on promaster.ProductId equals product.ProductId
                        join item in unitOfWork.ItemRepository.Get()
                        on prodetails.ItemId equals item.ItemId


                        where promaster.ProductionMasterId == id


                        select new ReportViewModel
                        {
                            ProductName = product.ProductName,
                            ProductionQuantity = promaster.ProductionQuantity,
                            ProductPrice = promaster.ProductPrice,
                            Productiondate = promaster.Productiondate,
                            ItemName = item.ItemName,
                            ItemQuantity = prodetails.ItemQuantity,
                            ItemCost = prodetails.ItemCost

                        }).ToList();
            return data;

        }


        public List<DamagedItemViewModel> GetDamageitem()
        {
            var data = (from dam in unitOfWork.DamagedItemRepository.Get()
                        join item in unitOfWork.ItemRepository.Get()
                         on dam.ItemId equals item.ItemId

                        group dam by new { dam.ItemId, item.ItemName } into g
                        select new DamagedItemViewModel
                        {
                            ItemName = g.Key.ItemName,
                            ItemId = g.Key.ItemId,


                        }).ToList();
            return data;

        }
        public List<DamagedItemViewModel> GetDamageProduct()
        {
            var data = (from dam in unitOfWork.DamagedItemRepository.Get()
                        join product in unitOfWork.ProductRepository.Get()
                        on dam.ProductId equals product.ProductId

                        group dam by new { dam.ProductId, product.ProductName } into g
                        select new DamagedItemViewModel
                        {

                            ProductName = g.Key.ProductName,
                            ProductId = g.Key.ProductId

                        }).ToList();
            return data;

        }


        public List<ReportViewModel> GetDamageReport(int? itmid, int? productid, DateTime? fromdate, DateTime? todate)
        {

            var lstdata = new List<ReportViewModel>();

            if (itmid != null)
            {
                if (fromdate != null && todate != null)
                {
                    lstdata = (from dam in unitOfWork.DamagedItemRepository.Get()
                               join item in unitOfWork.ItemRepository.Get()
                               on dam.ItemId equals item.ItemId



                               where dam.ItemId == itmid && (dam.DamageDate >= fromdate && dam.DamageDate <= todate)


                               select new ReportViewModel
                               {
                                   ItemId = item.ItemId,
                                   ItemName = item.ItemName,
                                   DamagedItemType = dam.DamagedItemType,
                                   DamageQuantity = dam.DamageQuantity,
                                   DamageDate = dam.DamageDate,
                                   DateOfPurchase = dam.DateOfPurchase

                               }).ToList();
                }
                else if (itmid > 0)
                {
                    lstdata = (from dam in unitOfWork.DamagedItemRepository.Get()
                               join item in unitOfWork.ItemRepository.Get()
                               on dam.ItemId equals item.ItemId



                               where dam.ItemId == itmid


                               select new ReportViewModel
                               {
                                   ItemId = item.ItemId,
                                   ItemName = item.ItemName,
                                   DamagedItemType = dam.DamagedItemType,
                                   DamageQuantity = dam.DamageQuantity,
                                   DamageDate = dam.DamageDate,
                                   DateOfPurchase = dam.DateOfPurchase

                               }).ToList();
                }
                else
                {
                    lstdata = (from dam in unitOfWork.DamagedItemRepository.Get()
                               join item in unitOfWork.ItemRepository.Get()
                               on dam.ItemId equals item.ItemId
                               select new ReportViewModel
                               {
                                   ItemId = item.ItemId,
                                   ItemName = item.ItemName,
                                   DamagedItemType = dam.DamagedItemType,
                                   DamageQuantity = dam.DamageQuantity,
                                   DamageDate = dam.DamageDate,
                                   DateOfPurchase = dam.DateOfPurchase

                               }).ToList();
                }

            }
            else
            {
                if (fromdate != null && todate != null)
                {
                    lstdata = (from dam in unitOfWork.DamagedItemRepository.Get()
                               join product in unitOfWork.ProductRepository.Get()
                               on dam.ProductId equals product.ProductId



                               where dam.ProductId == productid && (dam.DamageDate >= fromdate && dam.DamageDate <= todate)


                               select new ReportViewModel
                               {
                                   ProductId = product.ProductId,
                                   ProductName = product.ProductName,
                                   DamagedItemType = dam.DamagedItemType,
                                   DamageQuantity = dam.DamageQuantity,
                                   DamageDate = dam.DamageDate,
                                   DateOfPurchase = dam.DateOfPurchase

                               }).ToList();
                }
                else if (productid > 0)
                {
                    lstdata = (from dam in unitOfWork.DamagedItemRepository.Get()
                               join product in unitOfWork.ProductRepository.Get()
                               on dam.ProductId equals product.ProductId



                               where dam.ProductId == productid


                               select new ReportViewModel
                               {
                                   ProductId = product.ProductId,
                                   ProductName = product.ProductName,
                                   DamagedItemType = dam.DamagedItemType,
                                   DamageQuantity = dam.DamageQuantity,
                                   DamageDate = dam.DamageDate,
                                   DateOfPurchase = dam.DateOfPurchase

                               }).ToList();
                }
                else
                {
                    lstdata = (from dam in unitOfWork.DamagedItemRepository.Get()
                               join product in unitOfWork.ProductRepository.Get()
                               on dam.ProductId equals product.ProductId
                               select new ReportViewModel
                               {
                                   ProductId = product.ProductId,
                                   ProductName = product.ProductName,
                                   DamagedItemType = dam.DamagedItemType,
                                   DamageQuantity = dam.DamageQuantity,
                                   DamageDate = dam.DamageDate,
                                   DateOfPurchase = dam.DateOfPurchase

                               }).ToList();
                }

            }

            return lstdata;

        }
    }
}
