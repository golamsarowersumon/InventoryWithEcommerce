using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.ViewModels;
using Domain.Models;
using System.Globalization;

namespace Core.Services
{
    public class TransferOrderServices
    {
        private UnitOfWork unitOfWork;

        public TransferOrderServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IEnumerable<TransferViewModel> LoadAllStore()
        {
            var data = (from s in unitOfWork.StoreRepository.Get()
                        select new TransferViewModel
                        {

                            SID = s.StoreId,
                            Name = s.StoreName

                        }).Union(
                            (from s in unitOfWork.SubStoreRepository.Get()
                             select new TransferViewModel
                             {

                                 SID = s.SubStoreId,
                                 Name = s.SubStoreName,

                             })).Union(
                             (from s in unitOfWork.SubSubStoreRepository.Get()
                              select new TransferViewModel
                              {

                                  SID = s.SubSubStoreId,
                                  Name = s.SubSubStoreName,

                              })).Union(
                             (from s in unitOfWork.SubSubSubStoreRepository.Get()
                              select new TransferViewModel
                              {

                                  SID = s.SubSubSubStoreId,
                                  Name = s.SubSubSubStoreName,

                              })).Union(
                             (from s in unitOfWork.SubSubSubSubStoreRepository.Get()
                              select new TransferViewModel
                              {

                                  SID = s.SubSubSubSubStoreId,
                                  Name = s.SubSubSubSubStoreName,

                              }));


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


        public int GetInventoryItemInformation(TransferOrderViewModel TransferOrderViewModel)
        {
           
            int count = (from x in unitOfWork.StoreRepository.Get() where x.StoreId == TransferOrderViewModel.ToStoreId select x.StoreId).Count();

            if (count == 1)
            {
                 
                // Calculating the total Available quantity for Transfer Store

                var TotalAvailable = (from IM in unitOfWork.InventoryMasterRepository.Get()
                                      join ID in unitOfWork.InventoryDetailRepository.Get() on IM.Inv_HD_ID equals ID.Inv_HD_ID
                                      where ID.ItemId == TransferOrderViewModel.ItemId && IM.StoreId == TransferOrderViewModel.ToStoreId && IM.SubStoreId == null && IM.SubSubStoreId == null && IM.SubSubSubStoreId == null && IM.SubSubSubSubStoreId == null
                                      select (int?)ID.AvailableQty).Sum() ?? 0;
                return TotalAvailable;
            }

            int count2 = (from x in unitOfWork.SubStoreRepository.Get() where x.SubStoreId == TransferOrderViewModel.ToStoreId select x.SubStoreId).Count();

            if (count2 == 1)
            {
                var storeid = unitOfWork.SubStoreRepository.Get().Where(a => a.SubStoreId == TransferOrderViewModel.ToStoreId).Select(a => a.StoreId).Single();
                // Calculating the total Available quantity for Transfer Store

                var TotalAvailable = (from IM in unitOfWork.InventoryMasterRepository.Get()
                                      join ID in unitOfWork.InventoryDetailRepository.Get() on IM.Inv_HD_ID equals ID.Inv_HD_ID
                                      where ID.ItemId == TransferOrderViewModel.ItemId && IM.StoreId == storeid  && IM.SubStoreId == TransferOrderViewModel.ToStoreId && IM.SubSubStoreId == null && IM.SubSubSubStoreId == null && IM.SubSubSubSubStoreId == null
                                      select (int?)ID.AvailableQty).Sum() ?? 0;
                return TotalAvailable;
            }


            int count3 = (from x in unitOfWork.SubSubStoreRepository.Get() where x.SubSubStoreId == TransferOrderViewModel.ToStoreId select x.SubSubStoreId).Count();

         if (count3 == 1)
            {

                var storeid = unitOfWork.SubSubStoreRepository.Get().Where(a => a.SubSubStoreId == TransferOrderViewModel.ToStoreId).Select(a => a.StoreId).Single();
                var substoreid = unitOfWork.SubSubStoreRepository.Get().Where(a => a.SubSubStoreId == TransferOrderViewModel.ToStoreId).Select(a => a.SubStoreId).Single();

                // Calculating the total Available quantity for Transfer Store

                var TotalAvailable = (from IM in unitOfWork.InventoryMasterRepository.Get()
                                      join ID in unitOfWork.InventoryDetailRepository.Get() on IM.Inv_HD_ID equals ID.Inv_HD_ID
                                      where ID.ItemId == TransferOrderViewModel.ItemId && IM.StoreId == storeid && IM.SubStoreId == substoreid && IM.SubSubStoreId == TransferOrderViewModel.ToStoreId && IM.SubSubSubStoreId == null && IM.SubSubSubSubStoreId == null
                                      select (int?)ID.AvailableQty).Sum() ?? 0;
                return TotalAvailable;
            }



            int count4 = (from x in unitOfWork.SubSubSubStoreRepository.Get() where x.SubSubSubStoreId == TransferOrderViewModel.ToStoreId select x.SubSubSubStoreId).Count();

            if (count4 == 1)
            {

                var storeid = unitOfWork.SubSubSubStoreRepository.Get().Where(a => a.SubSubSubStoreId == TransferOrderViewModel.ToStoreId).Select(a => a.StoreId).Single();
                var substoreid = unitOfWork.SubSubSubStoreRepository.Get().Where(a => a.SubSubSubStoreId == TransferOrderViewModel.ToStoreId).Select(a => a.SubStoreId).Single();
                var subsubstoreid = unitOfWork.SubSubSubStoreRepository.Get().Where(a => a.SubSubSubStoreId == TransferOrderViewModel.ToStoreId).Select(a => a.SubStoreId).Single();

                // Calculating the total Available quantity for Transfer Store

                var TotalAvailable = (from IM in unitOfWork.InventoryMasterRepository.Get()
                                      join ID in unitOfWork.InventoryDetailRepository.Get() on IM.Inv_HD_ID equals ID.Inv_HD_ID
                                      where ID.ItemId == TransferOrderViewModel.ItemId && IM.StoreId == storeid && IM.SubStoreId == substoreid && IM.SubSubStoreId == subsubstoreid && IM.SubSubSubStoreId == TransferOrderViewModel.ToStoreId && IM.SubSubSubSubStoreId == null
                                      select (int?)ID.AvailableQty).Sum() ?? 0;
                return TotalAvailable;
            }




            int count5 = (from x in unitOfWork.SubSubSubSubStoreRepository.Get() where x.SubSubSubSubStoreId == TransferOrderViewModel.ToStoreId select x.SubSubSubSubStoreId).Count();

            if (count5 == 1)
            {

                var storeid = unitOfWork.SubSubSubSubStoreRepository.Get().Where(a => a.SubSubSubSubStoreId == TransferOrderViewModel.ToStoreId).Select(a => a.StoreId).Single();
                var substoreid = unitOfWork.SubSubSubSubStoreRepository.Get().Where(a => a.SubSubSubSubStoreId == TransferOrderViewModel.ToStoreId).Select(a => a.SubStoreId).Single();
                var subsubstoreid = unitOfWork.SubSubSubSubStoreRepository.Get().Where(a => a.SubSubSubSubStoreId == TransferOrderViewModel.ToStoreId).Select(a => a.SubStoreId).Single();
                var subsubsubstoreid = unitOfWork.SubSubSubSubStoreRepository.Get().Where(a => a.SubSubSubSubStoreId == TransferOrderViewModel.ToStoreId).Select(a => a.SubStoreId).Single();

                // Calculating the total Available quantity for Transfer Store

                var TotalAvailable = (from IM in unitOfWork.InventoryMasterRepository.Get()
                                      join ID in unitOfWork.InventoryDetailRepository.Get() on IM.Inv_HD_ID equals ID.Inv_HD_ID
                                      where ID.ItemId == TransferOrderViewModel.ItemId && IM.StoreId == storeid && IM.SubStoreId == substoreid && IM.SubSubStoreId == subsubstoreid && IM.SubSubSubStoreId == subsubsubstoreid  && IM.SubSubSubSubStoreId == TransferOrderViewModel.ToStoreId
                                      select (int?)ID.AvailableQty).Sum() ?? 0;
                return TotalAvailable;
            }

            return 0;

        }

        public void Create(TransferOrderViewModel[] TransferOrderViewModel, string storeid,string strUserName)
        {
            var sid = Convert.ToInt32(storeid);
            foreach (var items in TransferOrderViewModel)
            {
                var TransferOrder = new TransferOrder
                {
                    ToStoreId = items.ToStoreId,
                    FromStoreId = sid,
                    ItemId = items.ItemId,
                    TransactionQuantity = items.TransactionQuantity,
                    UnitId = items.UnitId,
                    TransferTypeId = items.TransferTypeId,
                    ConditionOfItemId = items.ConditionOfItemId,
                    TransferOrderdate = DateTime.Now.Date,
                    TransferOrderDeliverydate = items.TransferOrderDeliverydate,
                    OrderRecieve = "Pending",
                    TransferOrderSent = "No",
                    CreateBy = strUserName,
                    CreateDate = DateTime.Now.Date,
                  

                };

                unitOfWork.TransferOrderRepository.Insert(TransferOrder);
                unitOfWork.Save();




            }




        }


        public int LoadOrderPending(string strStoreId)
        {
            var sid = Convert.ToInt32(strStoreId);
            int count = (from x in unitOfWork.TransferOrderRepository.Get() where x.OrderRecieve == "Pending" && x.ToStoreId == sid select x.OrderRecieve).Count();
            return count;

        }


        public List<TransferOrderViewModel> LoadOrderPendingInformation(string strStoreId)
        {
            var sid = Convert.ToInt32(strStoreId);


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

            var data = (from s in unitOfWork.TransferOrderRepository.Get()

                        join item in unitOfWork.ItemRepository.Get() on s.ItemId equals item.ItemId
                        join unit in unitOfWork.UnitRepository.Get() on item.UnitId equals unit.UnitId
                        join transfertypename in unitOfWork.TransferTypeRepository.Get() on s.TransferTypeId equals transfertypename.TransferTypeId
                        join sn in S on s.ToStoreId equals sn.SID
                        join fsn in S on s.FromStoreId equals fsn.SID
                        where s.OrderRecieve == "Pending" && s.ToStoreId == sid

                        select new TransferOrderViewModel
                        {
                            TransferOrderId = s.TransferOrderId,
                            FromStoreId = s.FromStoreId,
                            FromStoreName = fsn.Name,
                            ToStoreId = s.ToStoreId,
                            ToStoreName = sn.Name,

                            ItemId = s.ItemId,
                            ItemName = item.ItemName,
                            TransactionQuantity = s.TransactionQuantity,
                            UnitId = s.UnitId,
                            UnitName = unit.UnitName,
                            TransferTypeId = s.TransferTypeId,
                            TransferTypeName = transfertypename.TransferTypeName,
                            TransferOrderdate = s.TransferOrderdate,
                            OrderRecieve = s.OrderRecieve


                        }).ToList();

            return data;


        }



        public void Receivetransferorder(TransferOrderViewModel TransferOrderViewModel)
        {



            for (int i = 0; i < TransferOrderViewModel.TransferOrderList.Count; i++)
            {
                var id = TransferOrderViewModel.TransferOrderList[i].TransferOrderId;

                var result = unitOfWork.TransferOrderRepository.Get().SingleOrDefault(b => b.TransferOrderId == id);
                if (result != null)
                {
                    result.OrderRecieve = "Received";
                    result.TransferOrderReceiveby = TransferOrderViewModel.TransferOrderReceiveby;
                    result.TransferOrderReceiveDate = DateTime.Now.Date;

                    unitOfWork.Save();
                }


            }

        }

        public void Canceltransferorder(TransferOrderViewModel TransferOrderViewModel)
        {



            for (int i = 0; i < TransferOrderViewModel.TransferOrderList.Count; i++)
            {
              
                var id = TransferOrderViewModel.TransferOrderList[i].TransferOrderId;

                var result = unitOfWork.TransferOrderRepository.Get().SingleOrDefault(b => b.TransferOrderId == id);
                if (result != null)
                {
                    result.OrderRecieve = "Canceled";
                    result.TransferOrderReceiveby = TransferOrderViewModel.TransferOrderReceiveby;
                    result.TransferOrderReceiveDate =DateTime.Now.Date;

                    unitOfWork.Save();
                }


            }

        }


        public IEnumerable<TransferOrderViewModel> GetTransferOrderInformation(string strstoreid)
        {
            var sid = Convert.ToInt32(strstoreid);
            var S = (from s in unitOfWork.StoreRepository.Get()
                     select new TransferViewModel
                     {

                         SID = s.StoreId,
                         Name = s.StoreName

                     }).Union(
                          (from s in unitOfWork.SubStoreRepository.Get()
                           select new TransferViewModel
                           {

                               SID = s.SubStoreId,
                               Name = s.SubStoreName,

                           })).Union(
                           (from s in unitOfWork.SubSubStoreRepository.Get()
                            select new TransferViewModel
                            {

                                SID = s.SubSubStoreId,
                                Name = s.SubSubStoreName,

                            })).Union(
                           (from s in unitOfWork.SubSubSubStoreRepository.Get()
                            select new TransferViewModel
                            {

                                SID = s.SubSubSubStoreId,
                                Name = s.SubSubSubStoreName,

                            })).Union(
                           (from s in unitOfWork.SubSubSubSubStoreRepository.Get()
                            select new TransferViewModel
                            {

                                SID = s.SubSubSubSubStoreId,
                                Name = s.SubSubSubSubStoreName,

                            }));

            var data = (from s in unitOfWork.TransferOrderRepository.Get()
                        join i in unitOfWork.ItemRepository.Get() on s.ItemId equals i.ItemId
                        join u in unitOfWork.UnitRepository.Get() on i.UnitId equals u.UnitId
                        join T in unitOfWork.TransferTypeRepository.Get() on s.TransferTypeId equals T.TransferTypeId
                        join st in S on s.FromStoreId equals st.SID
                        join ts in S on s.ToStoreId equals ts.SID

                        where s.OrderRecieve == "Received" && s.ToStoreId == sid && s.TransferOrderSent == "No"

                        select new TransferOrderViewModel
                        {
                            TransferOrderId = s.TransferOrderId,
                            FromStoreId = s.FromStoreId,
                            FromStoreName = st.Name,
                            ToStoreId = s.ToStoreId,
                            ToStoreName = ts.Name,
                            ItemId = s.ItemId,
                            ItemName = i.ItemName,
                            TransactionQuantity = s.TransactionQuantity,
                            UnitId = s.UnitId,
                            UnitName = u.UnitName,
                            TransferTypeId = s.TransferTypeId,
                            TransferTypeName = T.TransferTypeName,
                            TransferOrderdate = s.TransferOrderdate,
                            TransferOrderDeliverydate = s.TransferOrderDeliverydate,
                            OrderRecieve = s.OrderRecieve


                        }).ToList();

            return data;
        }

        public IEnumerable<TransferOrderViewModel> GetOrderInfo(int id)
        {

            var SId = unitOfWork.TransferOrderRepository.Get().Where(x => x.TransferOrderId == id).Select(x => x.FromStoreId).SingleOrDefault();

            int a = Convert.ToInt32(SId);

            if (a >= 1000 && a <= 1999)
            {


                var data = (from s in unitOfWork.TransferOrderRepository.Get()
                            join store in unitOfWork.StoreRepository.Get() on s.FromStoreId equals store.StoreId
                            join item in unitOfWork.ItemRepository.Get() on s.ItemId equals item.ItemId
                            join unit in unitOfWork.UnitRepository.Get() on item.UnitId equals unit.UnitId
                            join c in unitOfWork.ConditionOfItemRepository.Get() on s.ConditionOfItemId equals c.ConditionOfItemId
                            where s.TransferOrderId == id && s.FromStoreId == a
                            select new TransferOrderViewModel
                            {
                                TransferOrderId = s.TransferOrderId,
                                ToStoreId = s.FromStoreId,
                                ToStoreName = store.StoreName,
                                ItemId = s.ItemId,
                                ItemName = item.ItemName,
                                TransactionQuantity = s.TransactionQuantity,
                                UnitId = s.UnitId,
                                UnitName = unit.UnitName,
                                TransferTypeId = s.TransferTypeId,
                                TransferTypeName = s.TransferType.TransferTypeName,
                                TransferOrderdate = s.TransferOrderdate,
                                TransferOrderDeliverydate = s.TransferOrderDeliverydate,
                                ConditionOfItemId=s.ConditionOfItemId,
                                ConditionOfItemName= c.ConditionOfItemName


                            }).AsEnumerable();



                return data;



            }

            else if (a >= 2000 && a <= 2999)
            {

                var data = (from s in unitOfWork.TransferOrderRepository.Get()
                            join substore in unitOfWork.SubStoreRepository.Get() on s.FromStoreId equals substore.SubStoreId
                            join store in unitOfWork.StoreRepository.Get() on substore.StoreId equals store.StoreId
                            join item in unitOfWork.ItemRepository.Get() on s.ItemId equals item.ItemId
                            join unit in unitOfWork.UnitRepository.Get() on item.UnitId equals unit.UnitId
                            join c in unitOfWork.ConditionOfItemRepository.Get() on s.ConditionOfItemId equals c.ConditionOfItemId
                            where s.TransferOrderId == id && s.FromStoreId == a
                            select new TransferOrderViewModel
                            {
                                TransferOrderId = s.TransferOrderId,
                                ToSubStoreId = s.FromStoreId,
                                ToSubStoreName = substore.SubStoreName,
                                ToStoreId = substore.SubStoreId,
                                ToStoreName = store.StoreName,

                                ItemId = s.ItemId,
                                ItemName = item.ItemName,
                                TransactionQuantity = s.TransactionQuantity,
                                UnitId = s.UnitId,
                                UnitName = unit.UnitName,
                                TransferTypeId = s.TransferTypeId,
                                TransferTypeName = s.TransferType.TransferTypeName,
                                TransferOrderdate = s.TransferOrderdate,
                                TransferOrderDeliverydate = s.TransferOrderDeliverydate,
                                   ConditionOfItemId = s.ConditionOfItemId,
                                ConditionOfItemName = c.ConditionOfItemName


                            }).AsEnumerable();



                return data;

            }

            else if (a >= 3000 && a <= 3999)
            {

                var data = (from s in unitOfWork.TransferOrderRepository.Get()
                            join subsubstore in unitOfWork.SubSubStoreRepository.Get() on s.FromStoreId equals subsubstore.SubSubStoreId
                            join substore in unitOfWork.SubStoreRepository.Get() on subsubstore.SubStoreId equals substore.SubStoreId
                            join store in unitOfWork.StoreRepository.Get() on subsubstore.StoreId equals store.StoreId
                            join item in unitOfWork.ItemRepository.Get() on s.ItemId equals item.ItemId
                            join unit in unitOfWork.UnitRepository.Get() on item.UnitId equals unit.UnitId
                            join c in unitOfWork.ConditionOfItemRepository.Get() on s.ConditionOfItemId equals c.ConditionOfItemId
                            where s.TransferOrderId == id && s.FromStoreId == a
                            select new TransferOrderViewModel
                            {
                                TransferOrderId = s.TransferOrderId,
                                ToSubSubStoreId = s.FromStoreId,
                                ToSubSubStoreName = subsubstore.SubSubStoreName,
                                ToSubStoreId = subsubstore.SubStoreId,
                                ToSubStoreName = substore.SubStoreName,
                                ToStoreId = subsubstore.StoreId,
                                ToStoreName = store.StoreName,
                                ItemId = s.ItemId,
                                ItemName = item.ItemName,
                                TransactionQuantity = s.TransactionQuantity,
                                UnitId = s.UnitId,
                                UnitName = unit.UnitName,
                                TransferTypeId = s.TransferTypeId,
                                TransferTypeName = s.TransferType.TransferTypeName,
                                TransferOrderdate = s.TransferOrderdate,
                                TransferOrderDeliverydate = s.TransferOrderDeliverydate,
                                ConditionOfItemId = s.ConditionOfItemId,
                                ConditionOfItemName = c.ConditionOfItemName



                            }).AsEnumerable();



                return data;

            }

            else if (a >= 4000 && a <= 4999)
            {

                var data = (from s in unitOfWork.TransferOrderRepository.Get()
                            join subsubsubstore in unitOfWork.SubSubSubStoreRepository.Get() on s.FromStoreId equals subsubsubstore.SubSubSubStoreId
                            join subsubstore in unitOfWork.SubSubStoreRepository.Get() on subsubsubstore.SubSubStoreId equals subsubstore.SubSubStoreId
                            join substore in unitOfWork.SubStoreRepository.Get() on subsubsubstore.SubStoreId equals substore.SubStoreId
                            join store in unitOfWork.StoreRepository.Get() on subsubsubstore.StoreId equals store.StoreId
                            join item in unitOfWork.ItemRepository.Get() on s.ItemId equals item.ItemId
                            join unit in unitOfWork.UnitRepository.Get() on item.UnitId equals unit.UnitId
                            join c in unitOfWork.ConditionOfItemRepository.Get() on s.ConditionOfItemId equals c.ConditionOfItemId
                            where s.TransferOrderId == id && s.FromStoreId == a
                            select new TransferOrderViewModel
                            {
                                TransferOrderId = s.TransferOrderId,
                                ToSubSubSubStoreId = s.FromStoreId,
                                ToSubSubSubStoreName = subsubsubstore.SubSubSubStoreName,
                                ToSubSubStoreId = subsubsubstore.SubSubStoreId,
                                ToSubSubStoreName = substore.SubStoreName,
                                ToSubStoreId = subsubsubstore.SubStoreId,
                                ToSubStoreName = substore.SubStoreName,
                                ToStoreId = subsubsubstore.StoreId,
                                ToStoreName = store.StoreName,
                                ItemId = s.ItemId,
                                ItemName = item.ItemName,
                                TransactionQuantity = s.TransactionQuantity,
                                UnitId = s.UnitId,
                                UnitName = unit.UnitName,
                                TransferTypeId = s.TransferTypeId,
                                TransferTypeName = s.TransferType.TransferTypeName,
                                TransferOrderdate = s.TransferOrderdate,
                                TransferOrderDeliverydate = s.TransferOrderDeliverydate,
                                ConditionOfItemId = s.ConditionOfItemId,
                                ConditionOfItemName = c.ConditionOfItemName



                            }).AsEnumerable();



                return data;

            }

            else if (a >= 5000 && a <= 5999)
            {

                var data = (from s in unitOfWork.TransferOrderRepository.Get()
                            join subsubsubsubstore in unitOfWork.SubSubSubSubStoreRepository.Get() on s.FromStoreId equals subsubsubsubstore.SubSubSubSubStoreId
                            join subsubsubstore in unitOfWork.SubSubSubStoreRepository.Get() on subsubsubsubstore.SubSubSubStoreId equals subsubsubstore.SubSubSubStoreId
                            join subsubstore in unitOfWork.SubSubStoreRepository.Get() on subsubsubsubstore.SubSubStoreId equals subsubstore.SubSubStoreId
                            join substore in unitOfWork.SubStoreRepository.Get() on subsubsubsubstore.SubStoreId equals substore.SubStoreId
                            join store in unitOfWork.StoreRepository.Get() on subsubsubsubstore.StoreId equals store.StoreId
                            join item in unitOfWork.ItemRepository.Get() on s.ItemId equals item.ItemId
                            join unit in unitOfWork.UnitRepository.Get() on item.UnitId equals unit.UnitId
                            join c in unitOfWork.ConditionOfItemRepository.Get() on s.ConditionOfItemId equals c.ConditionOfItemId
                            where s.TransferOrderId == id && s.FromStoreId == a
                            select new TransferOrderViewModel
                            {
                                TransferOrderId = s.TransferOrderId,
                                ToSubSubSubSubStoreId = s.FromStoreId,
                                ToSubSubSubSubStoreName = subsubsubsubstore.SubSubSubSubStoreName,
                                ToSubSubSubStoreId = subsubsubsubstore.SubSubSubStoreId,
                                ToSubSubSubStoreName = subsubsubstore.SubSubSubStoreName,
                                ToSubSubStoreId = subsubsubsubstore.SubSubStoreId,
                                ToSubSubStoreName = subsubstore.SubSubStoreName,
                                ToSubStoreId = subsubsubsubstore.SubStoreId,
                                ToSubStoreName = substore.SubStoreName,

                                ToStoreId = subsubsubsubstore.StoreId,
                                ToStoreName = store.StoreName,

                                ItemId = s.ItemId,
                                ItemName = item.ItemName,
                                TransactionQuantity = s.TransactionQuantity,
                                UnitId = s.UnitId,
                                UnitName = unit.UnitName,
                                TransferTypeId = s.TransferTypeId,
                                TransferTypeName = s.TransferType.TransferTypeName,
                                TransferOrderdate = s.TransferOrderdate,
                                TransferOrderDeliverydate = s.TransferOrderDeliverydate,
                                ConditionOfItemId = s.ConditionOfItemId,
                                ConditionOfItemName = c.ConditionOfItemName



                            }).AsEnumerable();



                return data;

            }

            return null;



        }


        public int CountOrderInformattion(string strStoreId)
        {
            var sid = Convert.ToInt32(strStoreId);
            int count = (from x in unitOfWork.TransferOrderRepository.Get() where x.FromStoreId == sid select x).Count();
            return count;
        }


        public int CountOrderReceiveInformattion(string strStoreId)
        {
            var sid = Convert.ToInt32(strStoreId);
            int count = (from x in unitOfWork.TransferOrderRepository.Get() where x.OrderRecieve == "Received" && x.ToStoreId == sid select x).Count();
            return count;
        }

        public int CountOrderPendingInformattion(string strStoreId)
        {
            var sid = Convert.ToInt32(strStoreId);
            int count = (from x in unitOfWork.TransferOrderRepository.Get() where x.OrderRecieve == "Pending" && x.FromStoreId == sid select x).Count();
            return count;
        }

        public int CountOrderCancelInformattion(string strStoreId)
        {
            var sid = Convert.ToInt32(strStoreId);
            int count = (from x in unitOfWork.TransferOrderRepository.Get() where x.OrderRecieve == "Canceled" && x.ToStoreId == sid select x).Count();
            return count;
        }



        public List<TransferOrderViewModel> LoadOrderInformation(string strStoreId) {
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

            var data = (from s in unitOfWork.TransferOrderRepository.Get()
                        join store in S on s.FromStoreId equals store.SID
                        join tstore in S on s.ToStoreId equals tstore.SID
                        join item in unitOfWork.ItemRepository.Get() on s.ItemId equals item.ItemId
                        join unit in unitOfWork.UnitRepository.Get() on s.UnitId equals unit.UnitId
                        join c in unitOfWork.ConditionOfItemRepository.Get() on s.ConditionOfItemId equals c.ConditionOfItemId
                        where s.FromStoreId == strstoreid
                        select new TransferOrderViewModel
                        {
                            TransferOrderId = s.TransferOrderId,
                            FromStoreId = s.FromStoreId,
                            FromStoreName = store.Name,
                            ToStoreId = s.ToStoreId,
                            ToStoreName = tstore.Name,
                            ItemId = s.ItemId,
                            ItemName = item.ItemName,
                            TransactionQuantity = s.TransactionQuantity,
                            UnitId = s.UnitId,
                            UnitName = unit.UnitName,
                            TransferTypeId = s.TransferTypeId,
                            TransferTypeName = s.TransferType.TransferTypeName,
                            TransferOrderdate = s.TransferOrderdate,
                            TransferOrderDeliverydate = s.TransferOrderDeliverydate,
                            ConditionOfItemId = s.ConditionOfItemId,
                            ConditionOfItemName = c.ConditionOfItemName,
                            OrderRecieve =s.OrderRecieve

                        }).ToList();



            return data;

        }


        public List<TransferOrderViewModel> GetSearchInformation(TransferOrderViewModel transferOrderViewModel)
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

            var r = (from s in unitOfWork.TransferOrderRepository.Get()
                        join store in S on s.FromStoreId equals store.SID
                        join tstore in S on s.ToStoreId equals tstore.SID
                        join item in unitOfWork.ItemRepository.Get() on s.ItemId equals item.ItemId
                        join unit in unitOfWork.UnitRepository.Get() on s.UnitId equals unit.UnitId
                        join c in unitOfWork.ConditionOfItemRepository.Get() on s.ConditionOfItemId equals c.ConditionOfItemId
                        where s.FromStoreId == transferOrderViewModel.FromStoreId
                        select new TransferOrderViewModel
                        {
                            TransferOrderId = s.TransferOrderId,
                            FromStoreId = s.FromStoreId,
                            FromStoreName = store.Name,
                            ToStoreId = s.ToStoreId,
                            ToStoreName = tstore.Name,
                            ItemId = s.ItemId,
                            ItemName = item.ItemName,
                            TransactionQuantity = s.TransactionQuantity,
                            UnitId = s.UnitId,
                            UnitName = unit.UnitName,
                            TransferTypeId = s.TransferTypeId,
                            TransferTypeName = s.TransferType.TransferTypeName,
                            TransferOrderdate = s.TransferOrderdate,
                            TransferOrderDeliverydate = s.TransferOrderDeliverydate,
                            ConditionOfItemId = s.ConditionOfItemId,
                            ConditionOfItemName = c.ConditionOfItemName,
                            OrderRecieve =s.OrderRecieve


                        });

            if (transferOrderViewModel.ToStoreId > 0)
                r = r.Where(p => p.ToStoreId == transferOrderViewModel.ToStoreId);

            if (transferOrderViewModel.ItemId > 0)
                r = r.Where(p => p.ItemId == transferOrderViewModel.ItemId);
            

            if (transferOrderViewModel.ConditionOfItemId > 0)
                r = r.Where(p => p.ConditionOfItemId == transferOrderViewModel.ConditionOfItemId);

            if (transferOrderViewModel.TransferTypeId > 0)
                r = r.Where(p => p.TransferTypeId == transferOrderViewModel.TransferTypeId);

            //if (transferOrderViewModel.FromDate!=null &&  transferOrderViewModel.ToDate!= null)
            //     r =r.Where(p =>Convert.ToDateTime( p.TransferOrderdate) >= Convert.ToDateTime(transferOrderViewModel.FromDate)  && Convert.ToDateTime(p.TransferOrderdate) <= Convert.ToDateTime(transferOrderViewModel.ToDate));

            //if (transferOrderViewModel.FromDate!=null && transferOrderViewModel.ToDate != null)
            //   r =r.Where(p => DateTime.ParseExact(p.TransferOrderdate, "MM/dd/yyyy", CultureInfo.InvariantCulture) >= transferOrderViewModel.FromDate && DateTime.ParseExact(p.TransferOrderdate, "MM/dd/yyyy", CultureInfo.InvariantCulture) <= transferOrderViewModel.ToDate);

            //if (transferOrderViewModel.FromDate!=null)
            //    r = r.Where(p => Convert.ToDateTime(p.TransferOrderdate) <= transferOrderViewModel.ToDate);


            if (transferOrderViewModel.TransferCriteria == "Received")
                r = r.Where(p => p.OrderRecieve == "Received");

            if (transferOrderViewModel.TransferCriteria == "Pending")
                r = r.Where(p => p.OrderRecieve == "Pending");

            if (transferOrderViewModel.TransferCriteria == "Canceled")
               r = r.Where(p => p.OrderRecieve == "Canceled");

            if (transferOrderViewModel.FromDate != null && transferOrderViewModel.ToDate != null)
                r = r.Where(p => p.TransferOrderdate >= transferOrderViewModel.FromDate && p.TransferOrderdate <= transferOrderViewModel.ToDate);

            var data = r.ToList();

            return data;

        }


        public IEnumerable<TransferOrderViewModel> GetOrderInformation(int id)
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

        var data =(  from s in unitOfWork.TransferOrderRepository.Get()
                     join store in S on s.FromStoreId equals store.SID
                     join tstore in S on s.ToStoreId equals tstore.SID
                     join item in unitOfWork.ItemRepository.Get() on s.ItemId equals item.ItemId
                     join unit in unitOfWork.UnitRepository.Get() on s.UnitId equals unit.UnitId
                     join c in unitOfWork.ConditionOfItemRepository.Get() on s.ConditionOfItemId equals c.ConditionOfItemId
                     where s.TransferOrderId==id
                     select new TransferOrderViewModel
                     {
                         TransferOrderId = s.TransferOrderId,
                         FromStoreId = s.FromStoreId,
                         FromStoreName = store.Name,
                         ToStoreId = s.ToStoreId,
                         ToStoreName = tstore.Name,
                         ItemId = s.ItemId,
                         ItemName = item.ItemName,
                         TransactionQuantity = s.TransactionQuantity,
                         UnitId = s.UnitId,
                         UnitName = unit.UnitName,
                         TransferTypeId = s.TransferTypeId,
                         TransferTypeName = s.TransferType.TransferTypeName,
                         TransferOrderdate = s.TransferOrderdate,
                         TransferOrderDeliverydate = s.TransferOrderDeliverydate,
                         ConditionOfItemId = s.ConditionOfItemId,
                         ConditionOfItemName = c.ConditionOfItemName,
                         OrderRecieve = s.OrderRecieve,
                         TransferOrderby=s.TransferOrderby


                     }).AsEnumerable();




            return data;









        }






    }
}
