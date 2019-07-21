using Core.Services;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class ItemController : Controller
    {
        private UnitOfWork unitOfWork;
        private ItemServices itemServices;
        private CategoryServices categoryServices;
        private SubCategoryServices subCategoryServices;
        private SubSubCategoryServices subSubCategoryServices;
        private SubSubSubCategoryServices subSubSubCategoryServices;
        private SubSubSubSubCategoryServices subSubSubSubCategoryServices;
        private CompanyServices companyServices;
        private UnitServices unitServices;
        private StoreServices storeServices;
        private SubStoreServices subStoreServices;
        private SubSubStoreServices subSubStoreServices;
        private SubSubSubStoreServices subSubSubStoreServices;
        private SubSubSubSubStoreServices subSubSubSubStoreServices;
        private MethodServices MethodServices;
        private BrandServices brandServices;
        private ModelServices modelServices;
       

        public ItemController()
        {
            unitOfWork = new UnitOfWork();
            itemServices = new ItemServices(unitOfWork);
            categoryServices = new CategoryServices(unitOfWork);
            subCategoryServices = new SubCategoryServices(unitOfWork);
            subSubCategoryServices = new SubSubCategoryServices(unitOfWork);
            subSubSubCategoryServices = new SubSubSubCategoryServices(unitOfWork);
            subSubSubSubCategoryServices = new SubSubSubSubCategoryServices(unitOfWork);
            companyServices = new CompanyServices(unitOfWork);
            unitServices = new UnitServices(unitOfWork);
            storeServices = new StoreServices(unitOfWork);
            subStoreServices = new SubStoreServices(unitOfWork);
            subSubStoreServices = new SubSubStoreServices(unitOfWork);
            subSubSubStoreServices = new SubSubSubStoreServices(unitOfWork);
            subSubSubSubStoreServices = new SubSubSubSubStoreServices(unitOfWork);
            MethodServices = new MethodServices(unitOfWork);
            brandServices = new BrandServices(unitOfWork);
            modelServices = new ModelServices(unitOfWork);
        }


        // GET: Item
        public ActionResult Index()
        {
            var data=itemServices.GetAll();
            return View(data);
        }

        public ActionResult Details(int id)
        {
            var data = itemServices.GetById(id);
            return View(data);
        }

        public ActionResult Create()
        {
            loadAll();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ItemViewModel itemVM)
        {
            if (ModelState.IsValid)
            {
                if (itemVM.File_Image != null)
                {
                    string ext = Path.GetExtension(itemVM.File_Image.FileName).ToString();
                    if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".png" ||
                        ext.ToLower() == ".gif" || ext.ToLower() == ".pdf")
                    {
                        string fileName = Path.GetFileNameWithoutExtension(itemVM.File_Image.FileName) + Guid.NewGuid() +
                                          ext;
                        itemVM.Product_Image = "/AppFiles/Images/" + fileName;
                        itemVM.File_Image.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images"), fileName));
                    }

                }

                if (itemVM.File_Image1 != null)
                {
                    string ext = Path.GetExtension(itemVM.File_Image1.FileName).ToString();
                    if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".png" ||
                        ext.ToLower() == ".gif" || ext.ToLower() == ".pdf")
                    {
                        string fileName = Path.GetFileNameWithoutExtension(itemVM.File_Image1.FileName) + Guid.NewGuid() + ext;
                        itemVM.Product_Image1 = "/AppFiles/Images/" + fileName;
                        itemVM.File_Image1.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images"), fileName));
                    }

                }
                if (itemVM.File_Image2 != null)
                {
                    string ext = Path.GetExtension(itemVM.File_Image2.FileName).ToString();
                    if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".png" ||
                        ext.ToLower() == ".gif" || ext.ToLower() == ".pdf")
                    {
                        string fileName = Path.GetFileNameWithoutExtension(itemVM.File_Image2.FileName) + Guid.NewGuid() + ext;
                        itemVM.Product_Image2 = "/AppFiles/Images/" + fileName;
                        itemVM.File_Image2.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images"), fileName));
                    }

                }

                itemServices.Create(itemVM);
                loadAll();
                ModelState.Clear();
                return RedirectToAction("Index");
            }
            loadAll();
            ModelState.Clear();
            return View();
        }


        public ActionResult Edit(int id)
        {
            loadAll();
            var data = itemServices.GetById(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(ItemViewModel itemVM)
        {
            if (ModelState.IsValid)
            {
                if (itemVM.File_Image != null)
                {
                    string ext = Path.GetExtension(itemVM.File_Image.FileName).ToString();
                    if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".png" ||
                        ext.ToLower() == ".gif")
                    {
                        string fileName = Path.GetFileNameWithoutExtension(itemVM.File_Image.FileName) + Guid.NewGuid() +
                                          ext;
                        itemVM.Product_Image = "/AppFiles/Images/" + fileName;
                        itemVM.File_Image.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images"), fileName));
                    }

                }
                if (itemVM.File_Image1 != null)
                {
                    string ext = Path.GetExtension(itemVM.File_Image1.FileName).ToString();
                    if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".png" ||
                        ext.ToLower() == ".gif" || ext.ToLower() == ".pdf")
                    {
                        string fileName = Path.GetFileNameWithoutExtension(itemVM.File_Image1.FileName) + Guid.NewGuid() + ext;
                        itemVM.Product_Image1 = "/AppFiles/Images/" + fileName;
                        itemVM.File_Image1.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images"), fileName));
                    }

                }
                if (itemVM.File_Image2 != null)
                {
                    string ext = Path.GetExtension(itemVM.File_Image2.FileName).ToString();
                    if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".png" ||
                        ext.ToLower() == ".gif" || ext.ToLower() == ".pdf")
                    {
                        string fileName = Path.GetFileNameWithoutExtension(itemVM.File_Image2.FileName) + Guid.NewGuid() + ext;
                        itemVM.Product_Image2 = "/AppFiles/Images/" + fileName;
                        itemVM.File_Image2.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images"), fileName));
                    }

                }

                itemServices.Update(itemVM);

                return RedirectToAction("Index");
            }

            return View();

        }

        public ActionResult Delete(int id)
        {
            itemServices.Delete(id);
            return RedirectToAction("Index");
        }

        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        itemServices.Delete(id);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
            
        //}
           
        public void loadAll()
        {
            

            var CategoryList = categoryServices.GetAll();
            ViewBag.categorylist = new SelectList(CategoryList, "CategoryId", "CategoryName");

            var SubCategoryList = subCategoryServices.GetAll();
            ViewBag.subCategorylist = new SelectList(SubCategoryList, "SubCategoryId", "SubCategoryName");

            var SubSubCategoryList = subSubCategoryServices.GetAll();
            ViewBag.subSubCategoryList = new SelectList(SubSubCategoryList, "SubSubCategoryId", "SubSubCategoryName");

            var SubSubSubCategoryList = subSubSubCategoryServices.GetAll();
            ViewBag.subSubSubCategoryList = new SelectList(SubSubSubCategoryList, "SubSubSubCategoryId", "SubSubSubCategoryName");

            var SubSubSubSubCategoryList = subSubSubSubCategoryServices.GetAll();
            ViewBag.subSubSubSubCategoryList = new SelectList(SubSubSubSubCategoryList, "SubSubSubSubCategoryId", "SubSubSubSubCategoryName");

            var CompanyList = companyServices.GetAll();
            ViewBag.companyList = new SelectList(CompanyList, "CompanyId", "CompanyName");

            var UnitList = unitServices.GetAll();
            ViewBag.unitList = new SelectList(UnitList, "UnitId", "UnitName");

            var StoreList = storeServices.GetAll();
            ViewBag.storeList = new SelectList(StoreList, "StoreId", "StoreName");

            var SubStoreList = subStoreServices.GetAll();
            ViewBag.subStoreList = new SelectList(SubStoreList, "SubStoreId", "SubStoreName");

            var SubSubStoreList = subSubStoreServices.GetAll();
            ViewBag.subSubStoreList = new SelectList(SubSubStoreList, "SubSubStoreId", "SubSubStoreName");

            var SubSubSubStoreList = subSubSubStoreServices.GetAll();
            ViewBag.subSubSubStoreList = new SelectList(SubSubSubStoreList, "SubSubSubStoreId", "SubSubSubStoreName");

            var SubSubSubSubStoreList = subSubSubSubStoreServices.GetAll();
            ViewBag.subSubSubSubStoreList = new SelectList(SubSubSubSubStoreList, "SubSubSubSubStoreId", "SubSubSubSubStoreName");

            var Methodlist = MethodServices.GetAll();
            ViewBag.methodlst = new SelectList(Methodlist, "MethodId", "MethodName");

            var BrandList = brandServices.GetAll();
            ViewBag.brandlst = new SelectList(BrandList, "BrandId", "BrandName");

            var ModelList = modelServices.GetAll();
            ViewBag.modellst = new SelectList(ModelList, "ModelId", "ModelName");
        }
        

    }

}