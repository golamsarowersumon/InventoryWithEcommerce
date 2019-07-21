using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class ProductionServices
    {
        private UnitOfWork unitOfWork;

        public ProductionServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        public void Create(ProductionMasterViewModel ProductionMasterViewModel, ProductionDetailsViewModel[] productionDetailsViewModels, int? intStoreId)
        {
            int? StorId = null;
            int? SubStorId = null;
            int? SubSubStorId = null;
            int? SubSubSubStorId = null;
            int? SubSubSubSubStorId = null;
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



            ProductionMaster Producionheader = new ProductionMaster();

            Producionheader.ProductId = ProductionMasterViewModel.ProductId;
            Producionheader.ProductionQuantity = ProductionMasterViewModel.ProductionQuantity;
            Producionheader.ProductPrice = ProductionMasterViewModel.ProductPrice;
            Producionheader.Productiondate = DateTime.Now;
            Producionheader.StoreId = StorId;
            Producionheader.SubStoreId = SubStorId;
            Producionheader.SubSubStoreId = SubSubStorId;
            Producionheader.SubSubSubStoreId = SubSubSubStorId;
            Producionheader.SubSubSubSubStoreId = SubSubSubSubStorId;

            unitOfWork.ProductionMasterRepository.Insert(Producionheader);
            unitOfWork.Save();

            var Productionhdid = Producionheader.ProductionMasterId;
            foreach (var items in productionDetailsViewModels)
            {
                var productionDetails = new ProductionDetails
                {
                    ProductionMasterId = Productionhdid,
                    ItemId = items.ItemId,
                    ItemQuantity = items.ItemQuantity,
                    ItemCost = items.ItemCost,
                    SubTotal = items.SubTotal
                };
                unitOfWork.ProductionDetailsRepository.Insert(productionDetails);
                unitOfWork.Save();


                var avalableqty = unitOfWork.InventoryMasterRepository.Get().Join(unitOfWork.InventoryDetailRepository.Get(),
                (invd => invd.Inv_HD_ID),
                (invh => invh.Inv_HD_ID),
                ((invd, invh) => new { Inventorymaster = invd, inventorydetails = invh })).Where(k => k.Inventorymaster.StoreId == StorId || k.Inventorymaster.SubStoreId== SubStorId || k.Inventorymaster.SubSubStoreId == SubSubStorId || k.Inventorymaster.SubSubSubStoreId== SubSubSubStorId || k.Inventorymaster.SubSubSubSubStoreId== SubSubSubSubStorId).Where(i => i.inventorydetails.ItemId == items.ItemId).Select(q => q.inventorydetails.AvailableQty).Sum();
                var itemqty = Convert.ToDecimal(items.ItemQuantity);




                var invdetails = (from p in unitOfWork.InventoryDetailRepository.Get()
                                  join m in unitOfWork.InventoryMasterRepository.Get() on p.Inv_HD_ID equals m.Inv_HD_ID
                                  where p.AvailableQty > 0 && p.ItemId == items.ItemId && (m.StoreId == StorId || m.SubStoreId== SubStorId || m.SubSubStoreId== SubSubStorId || m.SubSubSubStoreId== SubSubSubStorId||m.SubSubSubSubStoreId== SubSubSubSubStorId)
                                  select new
                                  {
                                      InvDetailsId = p.Inv_Details_ID,


                                  }).ToList();






                foreach (var invid in invdetails)
                {

                    if (itemqty > 0)
                    {

                        var invdetailidqty = unitOfWork.InventoryDetailRepository.Get().Where(m => m.Inv_Details_ID == invid.InvDetailsId).Select(l => l.AvailableQty).SingleOrDefault();

                        if (itemqty <= invdetailidqty)
                        {
                            var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_Details_ID == invid.InvDetailsId);
                            if (result != null)
                            {
                                result.AvailableQty = invdetailidqty - itemqty;
                                unitOfWork.Save();
                                itemqty = Convert.ToDecimal(items.ItemQuantity) - itemqty;


                            }
                        }
                        else
                        {

                            var result = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.Inv_Details_ID == invid.InvDetailsId);
                            if (result != null)
                            {
                                result.AvailableQty = result.AvailableQty - invdetailidqty;
                                unitOfWork.Save();
                                itemqty = itemqty - invdetailidqty;


                            }
                        }

                    }






                }
            }


            var InventoryMaster = new InventoryMaster
            {
                ProductionMasterId = Productionhdid,
                StoreId = ProductionMasterViewModel.StoreId,
                CreateDate = DateTime.Now

            };
            unitOfWork.InventoryMasterRepository.Insert(InventoryMaster);
            unitOfWork.Save();

            var invId = InventoryMaster.Inv_HD_ID;

            InventoryDetail inventoryDetail = new InventoryDetail();
            inventoryDetail.Inv_HD_ID = invId;
            inventoryDetail.ProductId = ProductionMasterViewModel.ProductId;
            inventoryDetail.TransactionQty = ProductionMasterViewModel.ProductionQuantity;
            inventoryDetail.AvailableQty = ProductionMasterViewModel.ProductionQuantity;
            inventoryDetail.PO_Price = ProductionMasterViewModel.ProductPrice / ProductionMasterViewModel.ProductionQuantity;

            unitOfWork.InventoryDetailRepository.Insert(inventoryDetail);
            unitOfWork.Save();





            //foreach (var items in productionDetailsViewModels)
            //{
            //    //InventoryDetail lst = new InventoryDetail();

            //    var ProductionQty = items.ItemQuantity;

            //    var inventoryQty = unitOfWork.InventoryDetailRepository.Get().Where(m => m.ItemId == items.ItemId).Select(k => k.TransactionQty).Sum();

            //    var updateqty = inventoryQty - ProductionQty;

            //    if (inventoryQty > ProductionQty)
            //    {
            //        //var jj = (from p in unitOfWork.InventoryDetailRepository.Get() join m in unitOfWork.InventoryMasterRepository.Get() on p.Inv_HD_ID equals m.Inv_HD_ID  where p.ItemId == items.ItemId && m.StoreId == ProductionMasterViewModel.StoreId select p).SingleOrDefault();

            //        var kk = unitOfWork.InventoryDetailRepository.Get().SingleOrDefault(b => b.ItemId == items.ItemId);
            //        //foreach (var f in jj)
            //        //{
            //        //    lst.Inv_Details_ID = f.Inv_Details_ID;
            //        //    lst.Inv_HD_ID = f.Inv_HD_ID;
            //        //    lst.ItemId = f.ItemId;
            //        //    lst.UnitId = f.UnitId;
            //        //    lst.MethodId = f.MethodId;
            //        //    lst.DateOfExpired = f.DateOfExpired;
            //        //    lst.DateOfNextMaintainance = f.DateOfNextMaintainance;
            //        //    lst.TransactionQty = inventoryQty - ProductionQty;

            //        //}
            //        kk.AvailableQty = updateqty;

            //        unitOfWork.Save();
            //    }


            //}




        }












        public List<ProductDetailsViewModel> GetById(int id, int? intStoreId)
        {
            int? StorId =null;
            int? SubStorId =null;
            int? SubSubStorId=null;
            int? SubSubSubStorId =null;
            int? SubSubSubSubStorId =null;
            if (intStoreId >=1000 && intStoreId <2000)
            {
                StorId =intStoreId;
            }
            else if(intStoreId >= 2000 && intStoreId < 3000)
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



            var data = (from s in unitOfWork.ProductDetailsRepository.Get()
                        join itm in unitOfWork.ItemRepository.Get()
                        on s.ItemId equals itm.ItemId
                        join inv in unitOfWork.InventoryDetailRepository.Get()
                        on itm.ItemId equals inv.ItemId
                        join invh in unitOfWork.InventoryMasterRepository.Get()
                        on inv.Inv_HD_ID equals invh.Inv_HD_ID
                        join uni in unitOfWork.UnitRepository.Get() on inv.UnitId equals uni.UnitId
           
                        where s.ProductId == id && (invh.StoreId == StorId || invh.SubStoreId == SubStorId || invh.SubSubStoreId == SubSubStorId || invh.SubSubSubStoreId== SubSubSubStorId || invh.SubSubSubSubStoreId== SubSubSubSubStorId)

                        
                        group inv by new { s.ItemId, itm.ItemName, s.ItemQuantity, s.ProductQuantity, inv.MethodId, uni.UnitId, uni.UnitName } into g

                        select new
                        {

                            ItemId = g.Key.ItemId,
                            ItemName = g.Key.ItemName,
                            ItemQuantity = g.Key.ItemQuantity,
                            ProductQuantity = g.Key.ProductQuantity,
                            UnitId = g.Key.UnitId,
                            UnitName = g.Key.UnitName,
                            PO_PriceA = g.Average(a => a.PO_Price),
                            MethodId = g.Key.MethodId,
                            invdetailsqty = g.Sum(j => j.AvailableQty),



                        }).ToList();

            var inventoryitemidbystore = (unitOfWork.InventoryMasterRepository.Get().Join(unitOfWork.InventoryDetailRepository.Get(),
                (invd => invd.Inv_HD_ID),
                (invh => invh.Inv_HD_ID),
                ((invd, invh) => new { Inventorymaster = invd, inventorydetails = invh })).Where(k => k.Inventorymaster.StoreId == StorId || k.Inventorymaster.SubStoreId== SubStorId || k.Inventorymaster.SubSubStoreId== SubSubStorId || k.Inventorymaster.SubSubSubStoreId == SubSubSubStorId || k.Inventorymaster.SubSubSubSubStoreId== SubSubSubSubStorId).Select(q => q.inventorydetails.ItemId)).ToArray();

            var stditemid = (unitOfWork.ProductDetailsRepository.Get().Join(unitOfWork.ProductRepository.Get(),
               (invd => invd.ProductId),
               (invh => invh.ProductId),
               ((invd, invh) => new { productdetails = invd, product = invh })).Where(k => k.product.ProductId == id).Select(q => q.productdetails.ItemId)).ToArray();






            var dd = new List<ProductDetailsViewModel>();
            var Diffrray = inventoryitemidbystore.Intersect(stditemid).ToArray();

            int[] ascOrderedstdarray= (from i in stditemid orderby i ascending select i).ToArray();
            int[] ascOrdereddiffarray = (from i in Diffrray orderby i ascending select i).ToArray();

            bool DifferArray = ascOrderedstdarray.SequenceEqual(ascOrdereddiffarray);
            
            //bool DifferArray = Diffrray.Zip(stditemid, (a, b) => (a == b)).All(p => p);
            //bool DifferArray = Array.Equals(stditemid, Diffrray);


            if (DifferArray == false)
            {
                ProductDetailsViewModel msg = new ProductDetailsViewModel();
                msg.Msg = "Requirement Item Are Not Procure In Inventory Please Procure Item";
                dd.Add(msg);

            }
            else
            {
                if(data.Count()>0)
                {
                    foreach (var itms in data)
                    {

                        ProductDetailsViewModel lstprod = new ProductDetailsViewModel();
                        lstprod.ItemId = itms.ItemId;
                        lstprod.ItemName = itms.ItemName;
                        lstprod.ItemQuantity = itms.ItemQuantity;
                        lstprod.ProductQuantity = itms.ProductQuantity;
                        lstprod.UnitId = itms.UnitId;
                        lstprod.UnitName = itms.UnitName;
                        lstprod.AvailableQty = itms.invdetailsqty;
                        if (itms.MethodId == 1)
                        {
                            lstprod.PO_Price = Convert.ToDecimal(unitOfWork.InventoryDetailRepository.Get().Where(l => l.ItemId == itms.ItemId).OrderBy(l => l.PO_Price).Select(h => h.PO_Price).FirstOrDefault());
                        }
                        else if (itms.MethodId == 2)
                        {
                            lstprod.PO_Price = Convert.ToDecimal(unitOfWork.InventoryDetailRepository.Get().Where(l => l.ItemId == itms.ItemId).OrderByDescending(l => l.PO_Price).Select(h => h.PO_Price).FirstOrDefault());
                        }

                        else if (itms.MethodId == 3)
                        {
                            lstprod.PO_Price = itms.PO_PriceA;
                        }


                        dd.Add(lstprod);

                    }
                }
                else
                {
                    ProductDetailsViewModel msg = new ProductDetailsViewModel();
                    msg.Msg = "Production Standard Are Not Setup";
                    dd.Add(msg);
                }
                
            }
            return dd;
        }



        public IEnumerable<ProductionMasterViewModel> GetAll()
        {
            var data = (from s in unitOfWork.ProductRepository.Get()


                        select new ProductionMasterViewModel
                        {


                            ProductId = s.ProductId,
                            ProductName = s.ProductName

                        }).AsEnumerable();
            return data;
        }


    }
}
