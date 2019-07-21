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
   public class ReligionServices
    {


        private UnitOfWork unitOfWork;

        public ReligionServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(ReligionViewModel religionViewModel)
        {
            var Religion = new Religion
            {

                ReligionName = religionViewModel.ReligionName

            };

            unitOfWork.ReligionRepository.Insert(Religion);
            unitOfWork.Save();
        }

        public void Update(ReligionViewModel religionViewModel)
        {
            var Religion = new Religion
            {
                ReligionId = religionViewModel.ReligionId,
                ReligionName = religionViewModel.ReligionName

            };


            unitOfWork.ReligionRepository.Update(Religion);

            unitOfWork.Save();
        }


        public ReligionViewModel GetByID(int id)
        {
            var data = (from s in unitOfWork.ReligionRepository.Get()
                        where s.ReligionId == id
                        select new ReligionViewModel
                        {
                            
                            ReligionId=s.ReligionId,
                            ReligionName=s.ReligionName


                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<ReligionViewModel> GetAll()
        {
            var data = (from s in unitOfWork.ReligionRepository.Get()
                        select new ReligionViewModel
                        {
                            ReligionId=s.ReligionId,
                            ReligionName=s.ReligionName


                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var Religion = new Religion
            {

                ReligionId = id
            };

            unitOfWork.ReligionRepository.Delete(Religion);
            unitOfWork.Save();
        }



        public IEnumerable<DropDownViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.ReligionRepository.Get()
                        select new DropDownViewModel
                        {
                            Value=s.ReligionId,
                            Text=s.ReligionName
                            
                        }).AsEnumerable();

            return data;
        }


    }
}
