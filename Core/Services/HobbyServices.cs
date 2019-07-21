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
    public class HobbyServices
    {
        private UnitOfWork unitOfWork;

        public HobbyServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(HobbyViewModel hobbyVM)
        {
            var Hobby = new Hobby
            {

                HobbyName = hobbyVM.HobbyName
            };

            unitOfWork.HobbyRepository.Insert(Hobby);
            unitOfWork.Save();
        }


        public void Update(HobbyViewModel hobbyVM)
        {
            var Hobby = new Hobby
            {
                HobbyId = hobbyVM.HobbyId,
                HobbyName = hobbyVM.HobbyName
            };
            unitOfWork.HobbyRepository.Update(Hobby);
            unitOfWork.Save();
        }

        public HobbyViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.HobbyRepository.Get()
                        where s.HobbyId == id
                        select new HobbyViewModel
                        {
                            HobbyId = s.HobbyId,
                            HobbyName = s.HobbyName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<HobbyViewModel> GetAll()
        {
            var data = (from s in unitOfWork.HobbyRepository.Get()
                        select new HobbyViewModel
                        {

                            HobbyId = s.HobbyId,
                            HobbyName = s.HobbyName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var Hobby = new Hobby
            {
                HobbyId = id
            };

            unitOfWork.HobbyRepository.Delete(Hobby);
            unitOfWork.Save();

        }
    }
}
