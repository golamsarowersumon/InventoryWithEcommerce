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
  public  class RightOfAccessService
    {

        private UnitOfWork unitOfWork;

        public RightOfAccessService(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Save(RightOfAccesViewModel rightOfAccesViewModel)
        {


            for (int i = 0; i < rightOfAccesViewModel.rightOfAccesViewModelslist.Count; i++)
            {
                var tempId = rightOfAccesViewModel.rightOfAccesViewModelslist[i].TempId;
                var tmId = rightOfAccesViewModel.rightOfAccesViewModelslist[i].TransferId;
                var tdId = rightOfAccesViewModel.rightOfAccesViewModelslist[i].TransferDetailsId;
                var toId = rightOfAccesViewModel.rightOfAccesViewModelslist[i].TransferOrderId;
                var RightOfAccess = new RightOfAcces
                {
                    TransferId= rightOfAccesViewModel.rightOfAccesViewModelslist[i].TransferId,
                    TransferDetailsId = rightOfAccesViewModel.rightOfAccesViewModelslist[i].TransferDetailsId,
                    TransferOrderId = rightOfAccesViewModel.rightOfAccesViewModelslist[i].TransferOrderId,
                    FromStoreId = rightOfAccesViewModel.rightOfAccesViewModelslist[i].FromStoreId,
                    ToStoreId = rightOfAccesViewModel.rightOfAccesViewModelslist[i].ToStoreId,
                    ItemId = rightOfAccesViewModel.rightOfAccesViewModelslist[i].ItemId,
                    ProductId= rightOfAccesViewModel.rightOfAccesViewModelslist[i].ProductId,
                    ItemQuantity = rightOfAccesViewModel.rightOfAccesViewModelslist[i].ItemQuantity,
                    UnitId = rightOfAccesViewModel.rightOfAccesViewModelslist[i].UnitId,
                    PO_Price = rightOfAccesViewModel.rightOfAccesViewModelslist[i].PO_Price,
                    Remarks = rightOfAccesViewModel.rightOfAccesViewModelslist[i].Remarks,
                    CreateBy = rightOfAccesViewModel.CreateBy,
                    CreateDate =DateTime.Now.Date ,
                 };

               var result = unitOfWork.TemporaryTransferInformationRepository.Get().SingleOrDefault(b => b.Id == tmId);
                if (result != null)
                {
                    result.PendingItemQuantity = result.PendingItemQuantity - rightOfAccesViewModel.rightOfAccesViewModelslist[i].PendingItemQuantity;
                    
                }

                var result2 = unitOfWork.TransferDetailsRepository.Get().SingleOrDefault(b => b.TransferId == tmId && b.TransferDetailId == tdId);
                if (result2 != null)
                {
                    result2.PendingItemQuantity = result2.PendingItemQuantity - rightOfAccesViewModel.rightOfAccesViewModelslist[i].PendingItemQuantity;

                }
                unitOfWork.RightOfAccesRepository.Insert(RightOfAccess);
                unitOfWork.Save();

            }

        }


        public List<RightOfAccesViewModel> LoadRequest(RightOfAccesViewModel rightOfAccesViewModel)
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
            var requestlist = (from p in unitOfWork.TemporaryTransferInformationRepository.Get()
                               join td in unitOfWork.TransferDetailsRepository.Get() on p.TransferDetailId equals td.TransferDetailId
                     join fstore in SN on p.FromStoreId equals fstore.SID
                     join tstore in SN on p.ToStoreId equals tstore.SID
                     join i in unitOfWork.ItemRepository.Get() on p.ItemId equals i.ItemId
                     join u in unitOfWork.UnitRepository.Get() on i.UnitId equals u.UnitId
                     where p.PendingItemQuantity >0
                     select new RightOfAccesViewModel
                     {
                         TempId = p.Id,
                         Inv_HD_ID = p.Inv_HD_ID,
                         TransferId = p.TransferId,
                         TransferDetailsId = p.TransferDetailId,
                         TransferOrderId=td.TransferOrderId,
                         FromStoreId = p.FromStoreId,
                         FromStoreName = fstore.Name,
                         ToStoreId = p.ToStoreId,
                         ToStoreName = tstore.Name,
                         PO_Price = p.PO_Price,
                         ItemId = p.ItemId,
                         ItemName = i.ItemName,
                         ItemQuantity = p.TransactionQty,
                         UnitId = p.UnitId,
                         UnitName = u.UnitName,
                         ProductId = p.ProductId,
                         ReceiveItemQuantity = p.ReceiveItemQuantity,
                         PendingItemQuantity = p.PendingItemQuantity
                     }).ToList();


            return requestlist;

        }




        public List<RightOfAccesViewModel> GetSearchInfo(RightOfAccesViewModel rightOfAccesViewModel)
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
            var r = (from p in unitOfWork.TemporaryTransferInformationRepository.Get()
                     join fstore in SN on p.FromStoreId equals fstore.SID
                     join tstore in SN on p.ToStoreId equals tstore.SID
                     join i in unitOfWork.ItemRepository.Get() on p.ItemId equals i.ItemId
                     join u in unitOfWork.UnitRepository.Get() on i.UnitId equals u.UnitId
                     where p.PendingItemQuantity != null  && p.ItemId == rightOfAccesViewModel.ItemId
                     select new RightOfAccesViewModel
                     {
                         TempId = p.Id,
                         Inv_HD_ID = p.Inv_HD_ID,
                         TransferId = p.TransferId,
                         TransferDetailsId = p.TransferDetailId,
                         FromStoreId = p.FromStoreId,
                         FromStoreName = fstore.Name,
                         ToStoreId = p.ToStoreId,
                         ToStoreName = tstore.Name,
                         PO_Price = p.PO_Price,
                         ItemId = p.ItemId,
                         ItemName = i.ItemName,
                         ItemQuantity = p.TransactionQty,
                         UnitId = p.UnitId,
                         UnitName = u.UnitName,
                         ProductId = p.ProductId,
                         ReceiveItemQuantity= p.ReceiveItemQuantity,
                         PendingItemQuantity= p.PendingItemQuantity
                     });


            if (rightOfAccesViewModel.StoreId >= 1000 && rightOfAccesViewModel.StoreId <= 1999)
                r = r.Where(p => p.ToStoreId == rightOfAccesViewModel.StoreId);

            if (rightOfAccesViewModel.StoreId >= 2000 && rightOfAccesViewModel.StoreId <= 2999)
                r = r.Where(p => p.ToStoreId == rightOfAccesViewModel.StoreId);

            if (rightOfAccesViewModel.StoreId >= 3000 && rightOfAccesViewModel.StoreId <= 3999)
                r = r.Where(p => p.ToStoreId == rightOfAccesViewModel.StoreId);

            if (rightOfAccesViewModel.StoreId >= 4000 && rightOfAccesViewModel.StoreId <= 4999)
                r = r.Where(p => p.ToStoreId == rightOfAccesViewModel.StoreId);

            if (rightOfAccesViewModel.StoreId >= 5000 && rightOfAccesViewModel.StoreId <= 5999)
                r = r.Where(p => p.ToStoreId == rightOfAccesViewModel.StoreId);

               var rightaccesslist = r.ToList();
             return rightaccesslist;

        }


    }
}
