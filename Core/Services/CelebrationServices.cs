using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;

namespace Core.Services
{
   public class CelebrationServices
    {
        public UnitOfWork UnitOfWork;

        public CelebrationServices(UnitOfWork unitOfWork) {

            UnitOfWork = unitOfWork;
        }


        public void Create(CelebrationViewModel CelebrationViewModel)
        {


            var Celebration = new Celebration
            {

                CelebrationName = CelebrationViewModel.CelebrationName,
                CelebrationType=CelebrationViewModel.CelebrationType,
               




            };

            UnitOfWork.CelebrationRepository.Insert(Celebration);
            UnitOfWork.Save();


        }



        public void Update(CelebrationViewModel CelebrationViewModel)
        {


            var Celebration = new Celebration
            {
                CelebrationId=CelebrationViewModel.CelebrationId,
                CelebrationName = CelebrationViewModel.CelebrationName,
                CelebrationType = CelebrationViewModel.CelebrationType,
               



            };

            UnitOfWork.CelebrationRepository.Update(Celebration);
            UnitOfWork.Save();


        }




        public IEnumerable<CelebrationViewModel> GetAll()
        {

            var data = (from s in UnitOfWork.CelebrationRepository.Get()
                        select new CelebrationViewModel
                        {
                            CelebrationId = s.CelebrationId,
                            CelebrationName = s.CelebrationName,
                            CelebrationType = s.CelebrationType
                          

                        }).AsEnumerable();

            return data;

        }


        public CelebrationViewModel GetByID(int id)
        {

            var data = (from s in UnitOfWork.CelebrationRepository.Get()
                        where s.CelebrationId == id
                        select new CelebrationViewModel
                        {
                            CelebrationId = s.CelebrationId,
                            CelebrationName = s.CelebrationName,
                            CelebrationType = s.CelebrationType



                        }).SingleOrDefault();
            return data;



        }


        public void Delete(int id)
        {

            var Celebration = new Celebration
            {
                CelebrationId = id
            };
            UnitOfWork.CelebrationRepository.Delete(Celebration);
            UnitOfWork.Save();
        }

    }
}
