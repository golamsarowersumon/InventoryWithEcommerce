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
  public  class LayerServices
    {
        private UnitOfWork unitOfWork;

        public LayerServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(LayerViewModel LayerViewModel)
        {
            var Layer = new Layer
            {

                LayerName = LayerViewModel.LayerName
            };

            unitOfWork.LayerRepository.Insert(Layer);
            unitOfWork.Save();
        }


        public void Update(LayerViewModel LayerViewModel)
        {
            var Layer = new Layer
            {
                LayerId = LayerViewModel.LayerId,
                LayerName = LayerViewModel.LayerName
            };
            unitOfWork.LayerRepository.Update(Layer);
            unitOfWork.Save();
        }

        public LayerViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.LayerRepository.Get()
                        where s.LayerId == id
                        select new LayerViewModel
                        {
                            LayerId = s.LayerId,
                            LayerName = s.LayerName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<LayerViewModel> GetAll()
        {
            var data = (from s in unitOfWork.LayerRepository.Get()
                        select new LayerViewModel
                        {

                            LayerId = s.LayerId,
                            LayerName = s.LayerName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var Layer = new Layer
            {
                LayerId = id
            };

            unitOfWork.LayerRepository.Delete(Layer);
            unitOfWork.Save();

        }

    }
}
