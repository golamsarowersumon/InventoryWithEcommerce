using Domain.Repositories;
using Domain.ViewModels;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class MenuMainServices
    {



        private UnitOfWork unitOfWork;

        public MenuMainServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(MenuViewModel MenuViewModel)
        {
            var MENUMAIN = new MENUMAIN
            {

                MenuName = MenuViewModel.MenuName,
                MenuURL = MenuViewModel.MenuURL

            };

            unitOfWork.MENUMAINRepository.Insert(MENUMAIN);
            unitOfWork.Save();
        }

        public void Update(MenuViewModel MenuViewModel)
        {
            var MENUMAIN = new MENUMAIN
            {
                MainMenuId = MenuViewModel.MainMenuId,
                MenuName = MenuViewModel.MenuName,
                MenuURL = MenuViewModel.MenuURL

            };


            unitOfWork.MENUMAINRepository.Update(MENUMAIN);

            unitOfWork.Save();
        }


        public MenuViewModel GetByID(int? id)
        {
            var data = (from s in unitOfWork.MENUMAINRepository.Get()
                        where s.MainMenuId == id
                        select new MenuViewModel
                        {
                            MainMenuId = s.MainMenuId,
                            MenuName = s.MenuName,
                            MenuURL = s.MenuURL



                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<MenuViewModel> GetAll()
        {
            var data = (from s in unitOfWork.MENUMAINRepository.Get()
                        select new MenuViewModel
                        {
                            MainMenuId = s.MainMenuId,
                            MenuName = s.MenuName,
                            MenuURL = s.MenuURL


                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var MENUMAIN = new MENUMAIN
            {

                MainMenuId = id
            };

            unitOfWork.MENUMAINRepository.Delete(MENUMAIN);
            unitOfWork.Save();
        }



        public IEnumerable<MenuViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.MENUMAINRepository.Get()
                        select new MenuViewModel
                        {
                            MainMenuId = s.MainMenuId,
                            MenuName = s.MenuName
                        }).AsEnumerable();

            return data;
        }

    }
}
