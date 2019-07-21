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
   public class RegionServices
    {


        public UnitOfWork UnitOfWork;

        public RegionServices(UnitOfWork unitOfWork)
        {

            UnitOfWork = unitOfWork;
        }


        public void Create(RegionViewModel RegionViewModel)
        {


            var Region = new Region
            {

              RegionName = RegionViewModel.RegionName



            };

            UnitOfWork.RegionRepository.Insert(Region);
            UnitOfWork.Save();


        }

        public void Update(RegionViewModel RegionViewModel)
        {


            var Region = new Region
            {
                RegionId = RegionViewModel.RegionId,
                RegionName = RegionViewModel.RegionName



            };

            UnitOfWork.RegionRepository.Update(Region);
            UnitOfWork.Save();


        }



        public IEnumerable<RegionViewModel> GetAll()
        {

            var data = (from s in UnitOfWork.RegionRepository.Get()
                        select new RegionViewModel
                        {
                            RegionId=s.RegionId,
                            RegionName=s.RegionName

                        }).AsEnumerable();

            return data;

        }


        public RegionViewModel GetByID(int id)
        {

            var data = (from s in UnitOfWork.RegionRepository.Get()
                        where s.RegionId == id
                        select new RegionViewModel
                        {

                            RegionId = s.RegionId,
                            RegionName = s.RegionName

                        }).SingleOrDefault();
            return data;



        }


        public void Delete(int id)
        {

            var Region = new Region
            {
                RegionId = id
            };
            UnitOfWork.RegionRepository.Delete(Region);
            UnitOfWork.Save();
        }



        public IEnumerable<DropDownViewModel> GetDropDown()
        {
            var data = (from s in UnitOfWork.RegionRepository.Get()
                        select new DropDownViewModel
                        {
                            Value = s.RegionId,
                            Text = s.RegionName
                        }).AsEnumerable();

            return data;
        }


    }
}
