using Domain.Repositories;
using Domain.ViewModels;
using Domain.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
   public class EmailServices
    {
        private UnitOfWork unitOfWork;

        public EmailServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        public void Create( EmailViewModel model) {

            var Email = new Email
            {
                FromName = model.FromName,
                FromEmail = model.FromEmail,
                ToEmail = model.ToEmail,
                CC = model.CC,
                Bcc = model.Bcc,
                Subject = model.Subject,
                Message = model.Message,
                ViewMessage = "No"

            };

            unitOfWork.EmailRepository.Insert(Email);
            unitOfWork.Save();

        }


        public int CountAllInEmaill(string InEmail)
        {
            int count = (from x in unitOfWork.EmailRepository.Get() where x.ToEmail == InEmail select x.Message).Count();
           
           
            return count;

        }


        public int CountAllSendEmaill(string SendEmail)
        {
            int count = (from x in unitOfWork.EmailRepository.Get() where x.FromEmail == SendEmail select x.Message).Count();


            return count;

        }

    }
}
