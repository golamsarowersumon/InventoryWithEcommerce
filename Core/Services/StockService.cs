using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class StockService
    {
        private UnitOfWork unitOfWork;

        public StockService(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        public IEnumerable<StockViewModel> GetAll()
        {

            var data = (from s in unitOfWork.StoreRepository.Get()
                        select new StockViewModel
                        {
                            StoreId = s.StoreId,
                            StoreName = s.StoreName

                        });

            return data;


        }

        public List<StockViewModel> Getstockbyid(int id)
        {

            var stklst = (from invh in unitOfWork.InventoryMasterRepository.Get()
                          join invd in unitOfWork.InventoryDetailRepository.Get()
                           on invh.Inv_HD_ID equals invd.Inv_HD_ID
                          join item in unitOfWork.ItemRepository.Get() on invd.ItemId equals item.ItemId
                          join sto in unitOfWork.StoreRepository.Get() on invh.StoreId equals sto.StoreId
                          where sto.StoreId == id
                          group invd by new {item.ItemName, sto.StoreName} into g
                          select new StockViewModel
                          {
                              ItemName = g.Key.ItemName,
                              StoreName = g.Key.StoreName,
                              PO_QTD = g.Sum(t=>t.TransactionQty)

                          }).ToList();


            return stklst;


        }


        public List<StockViewModel> GetAllStock(int intStoreId)
        {
            int StorId = 0;
            int SubStorId = 0;
            int SubSubStorId = 0;
            int SubSubSubStorId = 0;
            int SubSubSubSubStorId = 0;
            if (intStoreId >= 1000 && intStoreId < 2000)
            {
                StorId = intStoreId;
            }
            else if (intStoreId >= 2000 && intStoreId < 3000)
            {
                SubStorId = intStoreId;
            }
            else if (intStoreId >= 2000 && intStoreId < 3000)
            {
                SubSubStorId = intStoreId;
            }
            else if (intStoreId >= 2000 && intStoreId < 3000)
            {
                SubSubSubStorId = intStoreId;
            }
            else if (intStoreId >= 2000 && intStoreId < 3000)
            {
                SubSubSubSubStorId = intStoreId;
            }



            var stklst = (from invh in unitOfWork.InventoryMasterRepository.Get()
                          join invd in unitOfWork.InventoryDetailRepository.Get()
                           on invh.Inv_HD_ID equals invd.Inv_HD_ID
                          join item in unitOfWork.ItemRepository.Get() on invd.ItemId equals item.ItemId
                         
                          join sto in unitOfWork.StoreRepository.Get() on invh.StoreId equals sto.StoreId
                            into st
                          from sto in st.DefaultIfEmpty()
                          join substo in unitOfWork.SubStoreRepository.Get() on invh.SubStoreId equals substo.SubStoreId
                           into subst
                          from substo in subst.DefaultIfEmpty()
                          join subsubsto in unitOfWork.SubSubStoreRepository.Get() on invh.SubSubStoreId equals subsubsto.SubSubStoreId
                           into subsubst
                          from subsubsto in subsubst.DefaultIfEmpty()
                          join subsubsubsto in unitOfWork.SubSubSubStoreRepository.Get() on invh.SubSubSubStoreId equals subsubsubsto.SubSubSubStoreId
                           into subsubsubst
                          from subsubsubsto in subsubsubst.DefaultIfEmpty()
                          join subsubsubsubsto in unitOfWork.SubSubSubSubStoreRepository.Get() on invh.SubSubSubSubStoreId equals subsubsubsubsto.SubSubSubSubStoreId
                           into subsubsubsubst
                          from subsubsubsubsto in subsubsubsubst.DefaultIfEmpty()
                          join unitname in unitOfWork.UnitRepository.Get() on invd.UnitId equals unitname.UnitId

                          where  invh.StoreId == StorId || invh.SubStoreId == SubStorId || invh.SubSubStoreId == SubSubStorId || invh.SubSubSubStoreId == SubSubSubStorId || invh.SubSubSubSubStoreId == SubSubSubSubStorId
                          group invd by new { item.ItemName, sto.StoreName,substo.SubStoreName,subsubsto.SubSubStoreName,subsubsubsto.SubSubSubStoreName,subsubsubsubsto.SubSubSubSubStoreName, unitname.UnitName } into g
                          
                          select new StockViewModel
                          {

                              ItemName = g.Key.ItemName,
                              StoreName = g.Key.StoreName,
                              SubStoreName = g.Key.SubStoreName,
                              SubSubStoreName = g.Key.SubSubStoreName,
                              SubSubSubStoreName = g.Key.SubSubSubStoreName,
                              SubSubSubSubStoreName = g.Key.SubSubSubSubStoreName,
                              UnitName = g.Key.UnitName,
                              PO_QTD = g.Sum(t => t.AvailableQty)

                          }).ToList();


            return stklst;


        }
    }
}
