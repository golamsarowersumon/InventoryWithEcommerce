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
    public class ModelServices
    {
        private UnitOfWork unitOfWork;

        public ModelServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(ModelViewModel ModelViewModel)
        {
            var Model = new Model
            {

                ModelName = ModelViewModel.ModelName
               
            };

            unitOfWork.ModelRepository.Insert(Model);
            unitOfWork.Save();
        }


        public void Update(ModelViewModel ModelViewModel)
        {
            var Model = new Model
            {
                ModelId = ModelViewModel.ModelId,
                ModelName = ModelViewModel.ModelName,
                
            };
            unitOfWork.ModelRepository.Update(Model);
            unitOfWork.Save();
        }

        public ModelViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.ModelRepository.Get()
                        where s.ModelId == id
                        select new ModelViewModel
                        {
                            ModelId = s.ModelId,
                            ModelName = s.ModelName
                           

                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<ModelViewModel> GetAll()
        {
            var data = (from s in unitOfWork.ModelRepository.Get()
                        select new ModelViewModel
                        {

                            ModelId = s.ModelId,
                            ModelName = s.ModelName
                           

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var Model = new Model
            {
                ModelId = id
            };

            unitOfWork.ModelRepository.Delete(Model);
            unitOfWork.Save();

        }
    }
}
