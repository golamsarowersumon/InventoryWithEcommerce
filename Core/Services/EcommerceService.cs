using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EcommerceService
    {
        private UnitOfWork unitOfWork;


        public EcommerceService(UnitOfWork _unitOfWork)
        {

            unitOfWork = _unitOfWork;
        }

        public List<ReportViewModel> GetAll(string itmtype)
        {
            var data = new List<ReportViewModel>();
            if (itmtype != "old")
            {
                data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                        join cat in unitOfWork.CategoryRepository.Get() on invd.CategoryId equals cat.CategoryId

                        where invd.IsNew == true
                        group cat by new { invd.CategoryId, cat.CategoryName } into c
                        select new ReportViewModel
                        {
                            CategoryId = c.Key.CategoryId,
                            CategoryName = c.Key.CategoryName

                        }).ToList();
            }
            else
            {
                data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                        join cat in unitOfWork.CategoryRepository.Get() on invd.CategoryId equals cat.CategoryId

                        where invd.IsNew == false

                        group cat by new { invd.CategoryId, cat.CategoryName } into c
                        select new ReportViewModel
                        {
                            CategoryId = c.Key.CategoryId,
                            CategoryName = c.Key.CategoryName

                        }).ToList();
            }


            return data;


        }
        public List<ReportViewModel> Catagorywiseview(int id, string itmtype)
        {
            var data = new List<ReportViewModel>();
            if (itmtype == "old")
            {
                if (id > 0)
                {
                    data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                            join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                            into it
                            from itm in it.DefaultIfEmpty()
                            join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                             into br
                            from brand in br.DefaultIfEmpty()
                            join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                            into mo
                            from model in mo.DefaultIfEmpty()
                            join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId
                            into sa
                            from salessetup in sa.DefaultIfEmpty()

                            where invd.CategoryId == id && invd.IsNew == false
                            group invd by new { salessetup.ItemId, itm.ItemName, itm.Product_Image, brand.BrandName, model.ModelName, salessetup.SalesPriceAmount } into g
                            select new ReportViewModel
                            {
                                ItemId = g.Key.ItemId,
                                ItemName = g.Key.ItemName,
                                Product_Image = g.Key.Product_Image,
                                SalesPriceAmount = g.Key.SalesPriceAmount,
                                BrandName = g.Key.BrandName,
                                ModelName = g.Key.ModelName




                            }).ToList();
                }
                else
                {
                    data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                            join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                            into it
                            from itm in it.DefaultIfEmpty()
                            join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                             into br
                            from brand in br.DefaultIfEmpty()
                            join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                            into mo
                            from model in mo.DefaultIfEmpty()
                            join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId


                            where invd.IsNew == false
                            group invd by new { salessetup.ItemId, itm.ItemName, itm.Product_Image, brand.BrandName, model.ModelName, salessetup.SalesPriceAmount } into g
                            select new ReportViewModel
                            {
                                ItemId = g.Key.ItemId,
                                ItemName = g.Key.ItemName,
                                Product_Image = g.Key.Product_Image,
                                SalesPriceAmount = g.Key.SalesPriceAmount,
                                BrandName = g.Key.BrandName,
                                ModelName = g.Key.ModelName




                            }).ToList();
                }
            }
            else
            {
                if (id > 0)
                {

                    data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                            join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                            into it
                            from itm in it.DefaultIfEmpty()
                            join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                             into br
                            from brand in br.DefaultIfEmpty()
                            join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                            into mo
                            from model in mo.DefaultIfEmpty()
                            join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId
                            into sa
                            from salessetup in sa.DefaultIfEmpty()

                            where invd.CategoryId == id && invd.IsNew == true
                            group invd by new { salessetup.ItemId, itm.ItemName, itm.Product_Image, brand.BrandName, model.ModelName, salessetup.SalesPriceAmount } into g
                            select new ReportViewModel
                            {
                                ItemId = g.Key.ItemId,
                                ItemName = g.Key.ItemName,
                                Product_Image = g.Key.Product_Image,
                                SalesPriceAmount = g.Key.SalesPriceAmount,
                                BrandName = g.Key.BrandName,
                                ModelName = g.Key.ModelName




                            }).ToList();

                }
                else
                {
                    data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                            join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                            into it
                            from itm in it.DefaultIfEmpty()
                            join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                             into br
                            from brand in br.DefaultIfEmpty()
                            join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                            into mo
                            from model in mo.DefaultIfEmpty()
                            join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId


                            where invd.IsNew == true
                            group invd by new { salessetup.ItemId, itm.ItemName, itm.Product_Image, brand.BrandName, model.ModelName, salessetup.SalesPriceAmount } into g
                            select new ReportViewModel
                            {
                                ItemId = g.Key.ItemId,
                                ItemName = g.Key.ItemName,
                                Product_Image = g.Key.Product_Image,
                                SalesPriceAmount = g.Key.SalesPriceAmount,
                                BrandName = g.Key.BrandName,
                                ModelName = g.Key.ModelName




                            }).ToList();



                }
            }




            return data;


        }

        public List<ReportViewModel> BrandView(int id)
        {
            var data = (from invd in unitOfWork.InventoryDetailRepository.Get()

                        join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId

                        where invd.CategoryId == id && invd.IsNew == true

                        group brand by new { brand.BrandId, brand.BrandName, invd.CategoryId } into b
                        select new ReportViewModel
                        {

                            BrandId = b.Key.BrandId,
                            BrandName = b.Key.BrandName,
                            CategoryId = b.Key.CategoryId



                        }).ToList();
            return data;
        }
        public List<ReportViewModel> Modelview(int? id)
        {
            var data = new List<ReportViewModel>();
            if (id != null)
            {
                data = (from invd in unitOfWork.InventoryDetailRepository.Get()

                        join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId

                        where invd.BrandId == id && invd.IsNew == true

                        group model by new { model.ModelId, model.ModelName } into b
                        select new ReportViewModel
                        {

                            ModelId = b.Key.ModelId,
                            ModelName = b.Key.ModelName



                        }).ToList();
            }
            else
            {
                data = (from invd in unitOfWork.InventoryDetailRepository.Get()

                        join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId

                        where invd.BrandId == id && invd.IsNew == true

                        group model by new { model.ModelId, model.ModelName } into b
                        select new ReportViewModel
                        {

                            ModelId = b.Key.ModelId,
                            ModelName = b.Key.ModelName



                        }).ToList();
            }

            return data;
        }


        public List<ReportViewModel> FilteredView(int? catid, int? braid, int? modid, decimal? fromprice, decimal? toprice, string itemtype)
        {
            var data = new List<ReportViewModel>();


            if (itemtype == "old")
            {
                if (fromprice != null && toprice != null)
                {
                    if (fromprice != null && toprice != null && modid == null && braid == null && catid == null)
                    {
                        data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                                join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                                into it
                                from itm in it.DefaultIfEmpty()
                                join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                                 into br
                                from brand in br.DefaultIfEmpty()
                                join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                                into mo
                                from model in mo.DefaultIfEmpty()
                                join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId
                                into sa
                                from salessetup in sa.DefaultIfEmpty()

                                where salessetup.SalesPriceAmount >= fromprice && salessetup.SalesPriceAmount <= toprice && invd.IsNew == false

                                group invd by new { salessetup.ItemId, itm.ItemName, itm.Product_Image, brand.BrandName, model.ModelName, salessetup.SalesPriceAmount } into g
                                select new ReportViewModel
                                {
                                    ItemId = g.Key.ItemId,
                                    ItemName = g.Key.ItemName,
                                    Product_Image = g.Key.Product_Image,
                                    SalesPriceAmount = g.Key.SalesPriceAmount,
                                    BrandName = g.Key.BrandName,
                                    ModelName = g.Key.ModelName




                                }).ToList();


                    }
                    else
                    {
                        if (modid != null)
                        {
                            data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                                    join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                                    into it
                                    from itm in it.DefaultIfEmpty()
                                    join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                                     into br
                                    from brand in br.DefaultIfEmpty()
                                    join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                                    into mo
                                    from model in mo.DefaultIfEmpty()
                                    join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId
                                    into sa
                                    from salessetup in sa.DefaultIfEmpty()

                                    where invd.CategoryId == catid && invd.BrandId == braid && invd.ModelId == modid && (salessetup.SalesPriceAmount >= fromprice && salessetup.SalesPriceAmount <= toprice) && invd.IsNew == false

                                    group invd by new { salessetup.ItemId, itm.ItemName, itm.Product_Image, brand.BrandName, model.ModelName, salessetup.SalesPriceAmount } into g
                                    select new ReportViewModel
                                    {
                                        ItemId = g.Key.ItemId,
                                        ItemName = g.Key.ItemName,
                                        Product_Image = g.Key.Product_Image,
                                        SalesPriceAmount = g.Key.SalesPriceAmount,
                                        BrandName = g.Key.BrandName,
                                        ModelName = g.Key.ModelName




                                    }).ToList();

                        }
                        else
                        {

                            data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                                    join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                                    into it
                                    from itm in it.DefaultIfEmpty()
                                    join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                                     into br
                                    from brand in br.DefaultIfEmpty()
                                    join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                                    into mo
                                    from model in mo.DefaultIfEmpty()
                                    join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId
                                    into sa
                                    from salessetup in sa.DefaultIfEmpty()

                                    where invd.CategoryId == catid && salessetup.SalesPriceAmount >= fromprice && salessetup.SalesPriceAmount <= toprice && invd.IsNew == false

                                    group invd by new { salessetup.ItemId, itm.ItemName, itm.Product_Image, brand.BrandName, model.ModelName, salessetup.SalesPriceAmount } into g
                                    select new ReportViewModel
                                    {
                                        ItemId = g.Key.ItemId,
                                        ItemName = g.Key.ItemName,
                                        Product_Image = g.Key.Product_Image,
                                        SalesPriceAmount = g.Key.SalesPriceAmount,
                                        BrandName = g.Key.BrandName,
                                        ModelName = g.Key.ModelName




                                    }).ToList();


                        }
                    }

                }
                else
                {
                    if (modid != null)
                    {
                        data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                                join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                                into it
                                from itm in it.DefaultIfEmpty()
                                join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                                 into br
                                from brand in br.DefaultIfEmpty()
                                join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                                into mo
                                from model in mo.DefaultIfEmpty()
                                join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId
                                into sa
                                from salessetup in sa.DefaultIfEmpty()

                                where invd.CategoryId == catid && invd.BrandId == braid && invd.ModelId == modid && invd.IsNew == false

                                group invd by new { salessetup.ItemId, itm.ItemName, itm.Product_Image, brand.BrandName, model.ModelName, salessetup.SalesPriceAmount } into g
                                select new ReportViewModel
                                {
                                    ItemId = g.Key.ItemId,
                                    ItemName = g.Key.ItemName,
                                    Product_Image = g.Key.Product_Image,
                                    SalesPriceAmount = g.Key.SalesPriceAmount,
                                    BrandName = g.Key.BrandName,
                                    ModelName = g.Key.ModelName




                                }).ToList();


                    }
                    else
                    {
                        data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                                join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                                into it
                                from itm in it.DefaultIfEmpty()
                                join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                                 into br
                                from brand in br.DefaultIfEmpty()
                                join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                                into mo
                                from model in mo.DefaultIfEmpty()
                                join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId
                                into sa
                                from salessetup in sa.DefaultIfEmpty()

                                where invd.CategoryId == catid && invd.BrandId == braid && invd.IsNew == false

                                group invd by new { salessetup.ItemId, itm.ItemName, itm.Product_Image, brand.BrandName, model.ModelName, salessetup.SalesPriceAmount } into g
                                select new ReportViewModel
                                {
                                    ItemId = g.Key.ItemId,
                                    ItemName = g.Key.ItemName,
                                    Product_Image = g.Key.Product_Image,
                                    SalesPriceAmount = g.Key.SalesPriceAmount,
                                    BrandName = g.Key.BrandName,
                                    ModelName = g.Key.ModelName




                                }).ToList();


                    }
                }
            }
            else
            {
                if (fromprice != null && toprice != null)
                {
                    if (fromprice != null && toprice != null && modid == null && braid == null && catid == null)
                    {
                        data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                                join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                                into it
                                from itm in it.DefaultIfEmpty()
                                join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                                 into br
                                from brand in br.DefaultIfEmpty()
                                join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                                into mo
                                from model in mo.DefaultIfEmpty()
                                join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId
                                into sa
                                from salessetup in sa.DefaultIfEmpty()

                                where salessetup.SalesPriceAmount >= fromprice && salessetup.SalesPriceAmount <= toprice && invd.IsNew == true

                                group invd by new { salessetup.ItemId, itm.ItemName, itm.Product_Image, brand.BrandName, model.ModelName, salessetup.SalesPriceAmount } into g
                                select new ReportViewModel
                                {
                                    ItemId = g.Key.ItemId,
                                    ItemName = g.Key.ItemName,
                                    Product_Image = g.Key.Product_Image,
                                    SalesPriceAmount = g.Key.SalesPriceAmount,
                                    BrandName = g.Key.BrandName,
                                    ModelName = g.Key.ModelName




                                }).ToList();


                    }
                    else
                    {
                        if (modid != null)
                        {

                            data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                                    join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                                    into it
                                    from itm in it.DefaultIfEmpty()
                                    join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                                     into br
                                    from brand in br.DefaultIfEmpty()
                                    join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                                    into mo
                                    from model in mo.DefaultIfEmpty()
                                    join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId
                                    into sa
                                    from salessetup in sa.DefaultIfEmpty()

                                    where invd.CategoryId == catid && invd.BrandId == braid && invd.ModelId == modid && (salessetup.SalesPriceAmount >= fromprice && salessetup.SalesPriceAmount <= toprice) && invd.IsNew == true

                                    group invd by new { salessetup.ItemId, itm.ItemName, itm.Product_Image, brand.BrandName, model.ModelName, salessetup.SalesPriceAmount } into g
                                    select new ReportViewModel
                                    {
                                        ItemId = g.Key.ItemId,
                                        ItemName = g.Key.ItemName,
                                        Product_Image = g.Key.Product_Image,
                                        SalesPriceAmount = g.Key.SalesPriceAmount,
                                        BrandName = g.Key.BrandName,
                                        ModelName = g.Key.ModelName




                                    }).ToList();


                        }
                        else
                        {
                            data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                                    join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                                    into it
                                    from itm in it.DefaultIfEmpty()
                                    join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                                     into br
                                    from brand in br.DefaultIfEmpty()
                                    join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                                    into mo
                                    from model in mo.DefaultIfEmpty()
                                    join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId
                                    into sa
                                    from salessetup in sa.DefaultIfEmpty()

                                    where invd.CategoryId == catid && salessetup.SalesPriceAmount >= fromprice && salessetup.SalesPriceAmount <= toprice && invd.IsNew == true

                                    group invd by new { salessetup.ItemId, itm.ItemName, itm.Product_Image, brand.BrandName, model.ModelName, salessetup.SalesPriceAmount } into g
                                    select new ReportViewModel
                                    {
                                        ItemId = g.Key.ItemId,
                                        ItemName = g.Key.ItemName,
                                        Product_Image = g.Key.Product_Image,
                                        SalesPriceAmount = g.Key.SalesPriceAmount,
                                        BrandName = g.Key.BrandName,
                                        ModelName = g.Key.ModelName




                                    }).ToList();

                        }
                    }

                }
                else
                {
                    if (modid != null)
                    {

                        data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                                join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                                into it
                                from itm in it.DefaultIfEmpty()
                                join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                                 into br
                                from brand in br.DefaultIfEmpty()
                                join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                                into mo
                                from model in mo.DefaultIfEmpty()
                                join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId
                                into sa
                                from salessetup in sa.DefaultIfEmpty()

                                where invd.CategoryId == catid && invd.BrandId == braid && invd.ModelId == modid && invd.IsNew == true

                                group invd by new { salessetup.ItemId, itm.ItemName, itm.Product_Image, brand.BrandName, model.ModelName, salessetup.SalesPriceAmount } into g
                                select new ReportViewModel
                                {
                                    ItemId = g.Key.ItemId,
                                    ItemName = g.Key.ItemName,
                                    Product_Image = g.Key.Product_Image,
                                    SalesPriceAmount = g.Key.SalesPriceAmount,
                                    BrandName = g.Key.BrandName,
                                    ModelName = g.Key.ModelName




                                }).ToList();


                    }
                    else
                    {
                        data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                                join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                                into it
                                from itm in it.DefaultIfEmpty()
                                join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                                 into br
                                from brand in br.DefaultIfEmpty()
                                join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                                into mo
                                from model in mo.DefaultIfEmpty()
                                join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId
                                into sa
                                from salessetup in sa.DefaultIfEmpty()

                                where invd.CategoryId == catid && invd.BrandId == braid && invd.IsNew == true

                                group invd by new { salessetup.ItemId, itm.ItemName, itm.Product_Image, brand.BrandName, model.ModelName, salessetup.SalesPriceAmount } into g
                                select new ReportViewModel
                                {
                                    ItemId = g.Key.ItemId,
                                    ItemName = g.Key.ItemName,
                                    Product_Image = g.Key.Product_Image,
                                    SalesPriceAmount = g.Key.SalesPriceAmount,
                                    BrandName = g.Key.BrandName,
                                    ModelName = g.Key.ModelName




                                }).ToList();

                    }
                }
            }





            return data;
        }

        public ReportViewModel SingleItemDetails(int id)
        {

            var data = (from invd in unitOfWork.InventoryDetailRepository.Get()
                        join itm in unitOfWork.ItemRepository.Get() on invd.ItemId equals itm.ItemId
                         into it
                        from itm in it.DefaultIfEmpty()
                        join brand in unitOfWork.BrandRepository.Get() on invd.BrandId equals brand.BrandId
                         into br
                        from brand in br.DefaultIfEmpty()
                        join model in unitOfWork.ModelRepository.Get() on invd.ModelId equals model.ModelId
                        into mo
                        from model in mo.DefaultIfEmpty()
                        join salessetup in unitOfWork.SalesElementStupRepoRepository.Get() on itm.ItemId equals salessetup.ItemId
                        into sa
                        from salessetup in sa.DefaultIfEmpty()
                        where invd.ItemId == id
                        group invd by new
                        {
                            salessetup.ItemId,
                            itm.ItemName,
                            itm.Product_Image,
                            itm.Product_Image1,
                            itm.Product_Image2,
                            brand.BrandName,
                            model.ModelName,
                            salessetup.SalesPriceAmount,
                            invd.CategoryId,
                            itm.Category.CategoryName,
                            invd.UnitId,
                            itm.Unit.UnitName,
                            itm.ItemDetails,
                        } into g
                        select new ReportViewModel
                        {
                            ItemId = g.Key.ItemId,
                            ItemName = g.Key.ItemName,
                            Product_Image = g.Key.Product_Image,
                            Product_Image1 = g.Key.Product_Image1,
                            Product_Image2 = g.Key.Product_Image2,
                            SalesPriceAmount = g.Key.SalesPriceAmount,
                            CategoryId = g.Key.CategoryId,
                            CategoryName = g.Key.CategoryName,
                            AvailableQty = g.Sum(q => q.AvailableQty),
                            UnitId = g.Key.UnitId,
                            UnitName = g.Key.UnitName,
                            ItemDetails = g.Key.ItemDetails,

                            BrandName = g.Key.BrandName,
                            ModelName = g.Key.ModelName




                        }).SingleOrDefault();
            return data;

        }

        public List<ReportViewModel> CountryList()
        {
            var data = (from country in unitOfWork.CountryRepository.Get()


                        select new ReportViewModel
                        {
                            CountryId = country.CountryId,
                            CountryName = country.CountryName

                        }).ToList();
            return data;
        }
        public List<DistrictViewModel> CountrywiseDist(int id)
        {
            var data = (from dist in unitOfWork.DistrictRepository.Get()

                        where dist.CountryId == id
                        select new DistrictViewModel
                        {
                            DistrictId = dist.DistrictId,
                            DistrictName = dist.DistrictName


                        }).ToList();
            return data;
        }
        public DistrictViewModel Distwiseshipping(int id)
        {
            var data = (from dist in unitOfWork.DistrictRepository.Get()

                        where dist.DistrictId == id
                        select new DistrictViewModel
                        {
                            ShippingCharge = dist.ShippingCharge

                        }).SingleOrDefault();
            return data;
        }

    }
}
