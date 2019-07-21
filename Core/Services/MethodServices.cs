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
    public class MethodServices
    {
        private UnitOfWork unitOfWork;

        public MethodServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(MethodViewModel methodVM)
        {
            var Method = new Method
            {
                MethodName = methodVM.MethodName
            };
            unitOfWork.MethodRepository.Insert(Method);
            unitOfWork.Save();
            
        }

        public void Update(MethodViewModel methodVM)
        {
            var Method = new Method
            {
                MethodId = methodVM.MethodId,
                MethodName = methodVM.MethodName
            };

            unitOfWork.MethodRepository.Update(Method);
            unitOfWork.Save();

        }

        public MethodViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.MethodRepository.Get()
                        where s.MethodId == id
                        select new MethodViewModel
                        {
                            MethodId = s.MethodId,
                            MethodName = s.MethodName
                        }).SingleOrDefault();
            return data;
        }

        public IEnumerable<MethodViewModel> GetAll()
        {
            var data = (from s in unitOfWork.MethodRepository.Get()
                        select new MethodViewModel
                        {
                            MethodId = s.MethodId,
                            MethodName = s.MethodName
                        }).AsEnumerable();
            return data;
        }


        public void Delete(int id)
        {
            var Method = new Method
            {
                MethodId = id
            };

            unitOfWork.MethodRepository.Delete(Method);
            unitOfWork.Save();
        }
    }
}
