using Domain.Repositories;
using Domain.ViewModels;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class TransferTypeServices
    {
        private UnitOfWork unitOfwork;

        public TransferTypeServices(UnitOfWork _unitOfWork)
        {
            unitOfwork = _unitOfWork;
        }

        public void Create(TransferTypeViewModel transferTypeVM)
        {
            var TransferType = new TransferType
            {
                TransferTypeName = transferTypeVM.TransferTypeName,
            };
            unitOfwork.TransferTypeRepository.Insert(TransferType);
            unitOfwork.Save();
        }

        public void Update(TransferTypeViewModel transferTypeVM)
        {
            var TransferType = new TransferType
            {
                TransferTypeId = transferTypeVM.TransferTypeId,
                TransferTypeName = transferTypeVM.TransferTypeName
            };
        }

        public TransferTypeViewModel GetById(int id)
        {
            var data = (from s in unitOfwork.TransferTypeRepository.Get()
                        where s.TransferTypeId == id
                        select new TransferTypeViewModel
                        {
                            TransferTypeId = s.TransferTypeId,
                            TransferTypeName = s.TransferTypeName
                        }).SingleOrDefault();

            return data;
            
        }

        public IEnumerable<TransferTypeViewModel> GetAll()
        {
            var data = (from s in unitOfwork.TransferTypeRepository.Get()
                        select new TransferTypeViewModel
                        {
                            TransferTypeId = s.TransferTypeId,
                            TransferTypeName = s.TransferTypeName

                        }).AsEnumerable();

            return data;
        }

        public void Delete(int id)
        {
            var TransferType = new TransferType
            {
                TransferTypeId = id
           };

            unitOfwork.TransferTypeRepository.Delete(TransferType);
            unitOfwork.Save();
        }
    }
}
