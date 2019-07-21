using Domain.Repositories;
using Domain.ViewModels;
using Domain.Models;

namespace Inventory.Controllers
{
    internal class StoreAssignServices
    {

        private UnitOfWork unitOfWork;

        public StoreAssignServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        //public void Create(StoreAssignViewModel StoreAssignViewModel)
        //{
        //    var LayerAssign = new LayerAssign
        //    {
        //        RoleName = StoreAssignViewModel.RoleName,
        //        LayerId = StoreAssignViewModel.LayerId
        //    };

        //    unitOfWork.LayerAssignRepository.Insert(LayerAssign);
        //    unitOfWork.Save();
        //}


        //public void Update(StoreAssignViewModel StoreAssignViewModel)
        //{
        //    var LayerAssign = new LayerAssign
        //    {
        //        Id = StoreAssignViewModel.Id,
        //        RoleName = StoreAssignViewModel.RoleName,
        //        LayerId = StoreAssignViewModel.LayerId
        //    };
        //    unitOfWork.LayerAssignRepository.Update(LayerAssign);
        //    unitOfWork.Save();
        //}

        //public StoreAssignViewModel GetById(int id)
        //{
        //    var data = (from s in unitOfWork.LayerAssignRepository.Get()
        //                join l in unitOfWork.LayerRepository.Get() on s.LayerId equals l.LayerId
        //                where s.Id == id
        //                select new StoreAssignViewModel
        //                {
        //                    Id = s.Id,
        //                    RoleName = s.RoleName,
        //                    LayerId = s.LayerId,
        //                    LayerName = l.LayerName
        //                }).SingleOrDefault();
        //    return data;
        //}
        //public IEnumerable<StoreAssignViewModel> GetAll()
        //{
        //    var data = (from s in unitOfWork.LayerAssignRepository.Get()
        //                join l in unitOfWork.LayerRepository.Get() on s.LayerId equals l.LayerId
        //                select new StoreAssignViewModel
        //                {
        //                    Id = s.Id,
        //                    RoleName = s.RoleName,
        //                    LayerId = s.LayerId,
        //                    LayerName = l.LayerName

        //                }).AsEnumerable();
        //    return data;
        //}

        //public void Delete(int id)
        //{
        //    var LayerAssign = new LayerAssign
        //    {
        //        Id = id
        //    };

        //    unitOfWork.LayerAssignRepository.Delete(LayerAssign);
        //    unitOfWork.Save();

        //}


    }
}