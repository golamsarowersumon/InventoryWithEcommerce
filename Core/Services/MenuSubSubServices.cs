using Domain.Repositories;
using Domain.Models;
using Domain.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Services
{
  public  class MenuSubSubServices
    {




        private UnitOfWork unitOfWork;

        public MenuSubSubServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(MenuViewModel MenuViewModel)
        {
            var MENUSUBSUB = new MENUSUBSUB
            {

                MenuName = MenuViewModel.SubSubMenuName,
                MenuURL = MenuViewModel.SubSubMenuURL,

                SubMenuId = MenuViewModel.SubMenuId

            };

            unitOfWork.MENUSUBSUBRepository.Insert(MENUSUBSUB);
            unitOfWork.Save();
        }

        public void Update(MenuViewModel MenuViewModel)
        {
            var MENUSUBSUB = new MENUSUBSUB
            {
                SubSubMenuId = MenuViewModel.SubSubMenuId,
                MenuName = MenuViewModel.SubSubMenuName,
                MenuURL = MenuViewModel.SubSubMenuURL,

                SubMenuId = MenuViewModel.SubMenuId

            };


            unitOfWork.MENUSUBSUBRepository.Update(MENUSUBSUB);

            unitOfWork.Save();
        }


        public MenuViewModel GetByID(int? id)
        {
            var data = (from s in unitOfWork.MENUSUBSUBRepository.Get()
                        where s.SubSubMenuId == id
                        select new MenuViewModel
                        {
                            SubSubMenuId = s.SubSubMenuId,
                            SubSubMenuName = s.MenuName,
                            SubSubMenuURL = s.MenuURL,
                            SubMenuId = s.SubMenuId,
                            SubMenuName=s.MENUSUB.MenuName
                            



                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<MenuViewModel> GetAll()
        {
            var data = (from s in unitOfWork.MENUSUBSUBRepository.Get()
                        select new MenuViewModel
                        {
                            SubSubMenuId = s.SubSubMenuId,
                            SubSubMenuName = s.MenuName,
                            SubSubMenuURL = s.MenuURL,

                            SubMenuId = s.SubMenuId


                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var MENUSUBSUB = new MENUSUBSUB
            {

                SubSubMenuId = id
            };

            unitOfWork.MENUSUBSUBRepository.Delete(MENUSUBSUB);
            unitOfWork.Save();
        }



        public IEnumerable<MenuViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.MENUSUBSUBRepository.Get()
                        select new MenuViewModel
                        {
                            SubSubMenuId = s.SubSubMenuId,
                            SubSubMenuName = s.MenuName
                        }).AsEnumerable();

            return data;
        }




    }
}
