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
    public class PhoneProviderServices
    {
        private UnitOfWork unitOfWork;
        public PhoneProviderServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(PhoneProviderViewModel phoneProviderVM)
        {
            var PhoneProvider = new PhoneProvider
            {
                PhoneProviderName = phoneProviderVM.PhoneProviderName
            };
            unitOfWork.PhoneProviderRepositoy.Insert(PhoneProvider);
            unitOfWork.Save();
        }

        public void Update(PhoneProviderViewModel phoneProviderVM)
        {
            var PhoneProvider = new PhoneProvider
            {
                PhoneProviderId = phoneProviderVM.PhoneProviderId,
                PhoneProviderName = phoneProviderVM.PhoneProviderName

            };
            unitOfWork.PhoneProviderRepositoy.Update(PhoneProvider);
            unitOfWork.Save();
            
        }

        public PhoneProviderViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.PhoneProviderRepositoy.Get()
                        where s.PhoneProviderId == id
                        select new PhoneProviderViewModel
                        {
                            PhoneProviderId = s.PhoneProviderId,
                            PhoneProviderName = s.PhoneProviderName
                        }).SingleOrDefault();
            return data;
        }

        public IEnumerable<PhoneProviderViewModel> GetAll()
        {
            var data = (from s in unitOfWork.PhoneProviderRepositoy.Get()
                        select new PhoneProviderViewModel
                        {
                            PhoneProviderId = s.PhoneProviderId,
                            PhoneProviderName = s.PhoneProviderName
                        }).AsEnumerable();
            return data;
        }
                      
        public void Delete(int id)
        {
            var PhoneProvider = new PhoneProvider
            {
                PhoneProviderId = id
            };
            unitOfWork.PhoneProviderRepositoy.Delete(PhoneProvider);
            unitOfWork.Save();
               
        }
    }
}
