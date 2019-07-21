using Domain.Repositories;
using Domain.Models;
using Domain.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
  public  class LayerAssignServices
    {
        private UnitOfWork unitOfWork;

        public LayerAssignServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        public void Create(LayerAssignViewModel LayerAssignViewModel)
        {
            var LayerAssign = new LayerAssign
            {
                RoleName= LayerAssignViewModel.RoleName,
                LayerId = LayerAssignViewModel.LayerId
            };

            unitOfWork.LayerAssignRepository.Insert(LayerAssign);
            unitOfWork.Save();
        }


        public void Update(LayerAssignViewModel LayerAssignViewModel)
        {
            var LayerAssign = new LayerAssign
            {
                Id = LayerAssignViewModel.Id,
                RoleName=LayerAssignViewModel.RoleName,
                LayerId = LayerAssignViewModel.LayerId
            };
            unitOfWork.LayerAssignRepository.Update(LayerAssign);
            unitOfWork.Save();
        }

        public LayerAssignViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.LayerAssignRepository.Get()
                        join l in unitOfWork.LayerRepository.Get() on s.LayerId equals l.LayerId
                        where s.Id == id
                        select new LayerAssignViewModel
                        {
                            Id=s.Id,
                            RoleName = s.RoleName,
                            LayerId = s.LayerId,
                            LayerName=l.LayerName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<LayerAssignViewModel> GetAll()
        {
            var data = (from s in unitOfWork.LayerAssignRepository.Get()
                        join l in unitOfWork.LayerRepository.Get() on s.LayerId equals l.LayerId
                        select new LayerAssignViewModel
                        {
                            Id = s.Id,
                            RoleName = s.RoleName,
                            LayerId = s.LayerId,
                            LayerName = l.LayerName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var LayerAssign = new LayerAssign
            {
                Id = id
            };

            unitOfWork.LayerAssignRepository.Delete(LayerAssign);
            unitOfWork.Save();

        }


    }
}
