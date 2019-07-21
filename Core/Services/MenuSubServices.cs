using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Models;
using Domain.ViewModels;

namespace Core.Services
{
   public class MenuSubServices
    {



        private UnitOfWork unitOfWork;

        public MenuSubServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(MenuViewModel MenuViewModel)
        {
            var MENUSUB = new MENUSUB
            {

                MenuName = MenuViewModel.SubMenuName,
                MenuURL = MenuViewModel.SubMenuURL,
                MainMenuId = MenuViewModel.MainMenuId

            };

            unitOfWork.MENUSUBRepository.Insert(MENUSUB);
            unitOfWork.Save();
        }

        public void Update(MenuViewModel MenuViewModel)
        {
            var MENUSUB = new MENUSUB
            {
                SubMenuId = MenuViewModel.SubMenuId,
                MenuName = MenuViewModel.SubMenuName,
                MenuURL = MenuViewModel.SubMenuURL,
                MainMenuId = MenuViewModel.MainMenuId

            };


            unitOfWork.MENUSUBRepository.Update(MENUSUB);

            unitOfWork.Save();
        }


        public MenuViewModel GetByID(int id)
        {
            var data = (from s in unitOfWork.MENUSUBRepository.Get()
                        where s.SubMenuId == id
                        select new MenuViewModel
                        {
                            SubMenuId = s.SubMenuId,
                            SubMenuName = s.MenuName,
                            SubMenuURL = s.MenuURL,
                            MainMenuId = s.MainMenuId




                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<MenuViewModel> GetAll()
        {
            var data = (from s in unitOfWork.MENUSUBRepository.Get()
                        select new MenuViewModel
                        {
                            SubMenuId = s.SubMenuId,
                            SubMenuName = s.MenuName,
                            SubMenuURL = s.MenuURL,
                            MainMenuId = s.MainMenuId


                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var MENUSUB = new MENUSUB
            {

                SubMenuId = id
            };

            unitOfWork.MENUSUBRepository.Delete(MENUSUB);
            unitOfWork.Save();
        }



        public IEnumerable<MenuViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.MENUSUBRepository.Get()
                        select new MenuViewModel
                        {
                            SubMenuId = s.SubMenuId,
                            SubMenuName = s.MenuName
                        }).AsEnumerable();

            return data;
        }


    }
}
