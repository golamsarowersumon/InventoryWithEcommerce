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
    public class SeasonServices
    {
        private UnitOfWork unitOfWork;

        public SeasonServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(SeasonViewModel seasonVM)
        {
            var Season = new Season
            {

                SeasonName = seasonVM.SeasonName
            };

            unitOfWork.SeasonRepository.Insert(Season);
            unitOfWork.Save();
        }


        public void Update(SeasonViewModel seasonVM)
        {
            var Season = new Season
            {
                SeasonId = seasonVM.SeasonId,
                SeasonName = seasonVM.SeasonName
            };
            unitOfWork.SeasonRepository.Update(Season);
            unitOfWork.Save();
        }

        public SeasonViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.SeasonRepository.Get()
                        where s.SeasonId == id
                        select new SeasonViewModel
                        {
                            SeasonId = s.SeasonId,
                            SeasonName = s.SeasonName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<SeasonViewModel> GetAll()
        {
            var data = (from s in unitOfWork.SeasonRepository.Get()
                        select new SeasonViewModel
                        {

                            SeasonId = s.SeasonId,
                            SeasonName = s.SeasonName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var Season = new Season
            {
                SeasonId = id
            };

            unitOfWork.SeasonRepository.Delete(Season);
            unitOfWork.Save();

        }
    }
}
