using Domain.Repositories;
using Domain.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DamagedItemServices
    {
        private UnitOfWork unitOfWork;

        public DamagedItemServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        public List<ProcurementShowViewModel> Damageproductshow(int id)
        {
            var data = (from invh in unitOfWork.InventoryMasterRepository.Get()
                        join invd in unitOfWork.InventoryDetailRepository.Get() on invh.Inv_HD_ID equals invd.Inv_HD_ID
                        join prd in unitOfWork.ProductRepository.Get() on invd.ProductId equals prd.ProductId
                        join sto in unitOfWork.StoreRepository.Get() on invh.StoreId equals sto.StoreId
                         into st
                        from sto in st.DefaultIfEmpty()

                        where invd.Inv_Details_ID == id 

                        select new ProcurementShowViewModel
                        {
                            Inv_Details_ID = invd.Inv_Details_ID,
                            ProductId = invd.ProductId,
                            ProductName = prd.ProductName,
                            PO_GRAND_TOTAL = invd.PO_Price,
                            CreateDate = invh.CreateDate,
                            Item_Unique_Number = invd.Item_Unique_Number,
                            StoreId = invh.StoreId,
                            StoreName = sto.StoreName,
                            AvailableQty =invd.AvailableQty
                            
                            


                        }).ToList();

          

            return data;
        }

        public List<ProcurementShowViewModel> DamageItemShow(int id)
        {
            var data = (from invh in unitOfWork.InventoryMasterRepository.Get()
                        join invd in unitOfWork.InventoryDetailRepository.Get() on invh.Inv_HD_ID equals invd.Inv_HD_ID
                        join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                        join sto in unitOfWork.StoreRepository.Get() on invh.StoreId equals sto.StoreId
                         into st from sto in st.DefaultIfEmpty()
                        where invd.Inv_Details_ID == id 

                        select new ProcurementShowViewModel
                        {
                            Inv_Details_ID = invd.Inv_Details_ID,
                            ItemId = itm.ItemId,
                            ItemName = itm.ItemName,
                            PO_Price = invd.PO_Price,
                            DateOfPurchase = invh.DateOfPurchase,
                            Item_Unique_Number = invd.Item_Unique_Number,
                            StoreId = invh.StoreId,
                            StoreName = sto.StoreName,
                            AvailableQty = invd.AvailableQty



                        }).ToList();
            
            return data;
        }

        public List<ProcurementShowViewModel> Productshowfordamage(int id)
        {
            var data = (from invh in unitOfWork.InventoryMasterRepository.Get()
                        join invd in unitOfWork.InventoryDetailRepository.Get() on invh.Inv_HD_ID equals invd.Inv_HD_ID
                        join prd in unitOfWork.ProductRepository.Get() on invd.ProductId equals prd.ProductId
                        join sto in unitOfWork.StoreRepository.Get() on invh.StoreId equals sto.StoreId
                        into st from sto in st.DefaultIfEmpty()

                        where invd.ProductId == id && invd.AvailableQty>0

                        select new ProcurementShowViewModel
                        {
                            Inv_Details_ID = invd.Inv_Details_ID,
                            ProductName = prd.ProductName,
                            PO_GRAND_TOTAL = invd.PO_Price,
                            CreateDate = invh.CreateDate,
                            Item_Unique_Number = invd.Item_Unique_Number,
                             StoreId = invh.StoreId,
                            StoreName = sto.StoreName



                        }).ToList();

            return data;
        }

        public List<ProcurementShowViewModel> Itemshowfordamage(int id)
        {
            var data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                        join invh in unitOfWork.InventoryMasterRepository.Get() on invd.Inv_HD_ID equals invh.Inv_HD_ID
                        join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                       
                        join sto in unitOfWork.StoreRepository.Get() on invh.StoreId equals sto.StoreId
                        join warr in unitOfWork.WarrantyRepository.Get() on invd.WarrantyId equals warr.WarrantyId into wr
                        from warr in wr.DefaultIfEmpty()
                        where invd.ItemId == id && invd.AvailableQty>0

                        select new ProcurementShowViewModel
                        {
                            Inv_Details_ID = invd.Inv_Details_ID,
                            ItemName = itm.ItemName,
                            WarrantyPeriod = warr.WarrantyPeriod,
                            PO_Price = invd.PO_Price,
                            DateOfPurchase  =invh.DateOfPurchase,
                            Item_Unique_Number = invd.Item_Unique_Number,
                            StoreId = invh.StoreId,
                            StoreName = sto.StoreName




                        }).ToList();

            return data;
        }


        public void Create(DamagedItemViewModel[] damagedItemVM)
        {

            foreach(var itm in damagedItemVM)
            {

                var DamagedItem = new DamagedItem
                {
                    ItemId = itm.ItemId,
                    ProductId = itm.ProductId,
                    DamagedItemType = itm.DamagedItemType,
                    DamageDate = DateTime.Now,
                    DamageQuantity = itm.DamageQuantity,
                    StoreId = itm.StoreId,
                    PO_Price = itm.PO_Price,
                    Item_Unique_Number = itm.Item_Unique_Number,
                    DateOfPurchase = itm.DateOfPurchase


                };
                unitOfWork.DamagedItemRepository.Insert(DamagedItem);
                unitOfWork.Save();


                var availablequantity = itm.AvailableQty - itm.DamageQuantity;

            var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_Details_ID == itm.Inv_Details_ID);
                if (result != null)
                {
                    result.AvailableQty = availablequantity;
                    unitOfWork.Save();
                }

            }
        }


    }
}
