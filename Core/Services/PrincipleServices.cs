using Domain.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Core.Services
{
   public class PrincipleServices
    {


        private UnitOfWork unitOfWork;

        public PrincipleServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(PrincipleViewModel PrincipleViewModel)
        {
            var Principle = new Principle
            {

                Principle_name = PrincipleViewModel.Principle_name,
                CountryId = PrincipleViewModel.CountryId


            };

            unitOfWork.PrincipleRepository.Insert(Principle);
            unitOfWork.Save();
        }

        public void Update(PrincipleViewModel PrincipleViewModel)
        {
            var Principle = new Principle
            {
                Principle_id = PrincipleViewModel.Principle_id,
                Principle_name = PrincipleViewModel.Principle_name,
                CountryId = PrincipleViewModel.CountryId


            };


            unitOfWork.PrincipleRepository.Update(Principle);

            unitOfWork.Save();
        }


        public PrincipleViewModel GetByID(int? id)
        {
            var data = (from s in unitOfWork.PrincipleRepository.Get()
                        where s.Principle_id == id
                        select new PrincipleViewModel
                        {
                            Principle_id = s.Principle_id,
                            Principle_name = s.Principle_name,
                            CountryId = s.CountryId,
                            CountryName = s.Country.CountryName
                           
                            



                        }).SingleOrDefault();

            return data;
        }



        public IEnumerable<PrincipleViewModel> princialbycountry( int id)
        {
            var data = (from s in unitOfWork.CountryRepository.Get() join p in unitOfWork.PrincipleRepository.Get()
                        on s.CountryId equals p.CountryId where s.CountryId == id
                        select new PrincipleViewModel
                        {
                            Principle_id = p.Principle_id,
                            Principle_name = p.Principle_name,
                            

                        }).AsEnumerable();

            return data;
        }


        public IEnumerable<PrincipleViewModel> GetAll()
        {
            var data = (from s in unitOfWork.PrincipleRepository.Get()
                        select new PrincipleViewModel
                        {
                            Principle_id = s.Principle_id,
                            Principle_name = s.Principle_name,
                            CountryId=s.CountryId,
                            CountryName = s.Country.CountryName
                            
                           }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var Principle = new Principle
            {

                Principle_id = id
            };

            unitOfWork.PrincipleRepository.Delete(Principle);
            unitOfWork.Save();
        }
 }
}
