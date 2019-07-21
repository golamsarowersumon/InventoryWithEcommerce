using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;
using Core.Services;

namespace Core.Services
{
   public class FestivalServices
    {

        private UnitOfWork unitOfWork;

        public FestivalServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(FestivalViewModel festivalViewModel)
        {
            var Festival = new Festival
            {


                FestivalName = festivalViewModel.FestivalName,
                ReligionId = festivalViewModel.ReligionId



            };

            unitOfWork.FestivalRepository.Insert(Festival);
            unitOfWork.Save();
        }

        public void Update(FestivalViewModel festivalViewModel)
        {
            var Festival = new Festival
            {
                FestivalId = festivalViewModel.FestivalId,
                FestivalName = festivalViewModel.FestivalName,
                ReligionId = festivalViewModel.ReligionId


            };


            unitOfWork.FestivalRepository.Update(Festival);

            unitOfWork.Save();
        }


        public FestivalViewModel GetByID(int? id)
        {
            var data = (from s in unitOfWork.FestivalRepository.Get()
                        where s.FestivalId == id
                        select new FestivalViewModel
                        {
                            FestivalId = s.FestivalId,
                            FestivalName = s.FestivalName,
                            ReligionId = s.ReligionId,
                            ReligionName = s.Religion.ReligionName





                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<FestivalViewModel> GetAll()
        {
            var data = (from s in unitOfWork.FestivalRepository.Get()
                        select new FestivalViewModel
                        {
                            FestivalId = s.FestivalId,
                            FestivalName = s.FestivalName,
                            ReligionId = s.ReligionId,
                            ReligionName = s.Religion.ReligionName

                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var Festival = new Festival
            {

                FestivalId = id
            };

            unitOfWork.FestivalRepository.Delete(Festival);
            unitOfWork.Save();
        }

    }
}
