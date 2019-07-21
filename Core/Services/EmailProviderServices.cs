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
    public class EmailProviderServices
    {
        private UnitOfWork unitOfWork;

        public EmailProviderServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(EmailProviderViewModel emailProviderVM)
        {
            var EmailProvider = new EmailProvider
            {

                EmailProviderName = emailProviderVM.EmailProviderName
            };

            unitOfWork.EmailProviderRepository.Insert(EmailProvider);
            unitOfWork.Save();
        }


        public void Update(EmailProviderViewModel emailProviderVM)
        {
            var EmailProvider = new EmailProvider
            {
                EmailProviderId = emailProviderVM.EmailProviderId,
                EmailProviderName = emailProviderVM.EmailProviderName
            };
            unitOfWork.EmailProviderRepository.Update(EmailProvider);
            unitOfWork.Save();
        }

        public EmailProviderViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.EmailProviderRepository.Get()
                        where s.EmailProviderId == id
                        select new EmailProviderViewModel
                        {
                            EmailProviderId = s.EmailProviderId,
                            EmailProviderName = s.EmailProviderName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<EmailProviderViewModel> GetAll()
        {
            var data = (from s in unitOfWork.EmailProviderRepository.Get()
                        select new EmailProviderViewModel
                        {

                            EmailProviderId = s.EmailProviderId,
                            EmailProviderName = s.EmailProviderName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var EmailProvider = new EmailProvider
            {
                EmailProviderId = id
            };

            unitOfWork.EmailProviderRepository.Delete(EmailProvider);
            unitOfWork.Save();

        }
    }
}
