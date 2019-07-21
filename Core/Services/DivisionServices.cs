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
   public class DivisionServices
    {


        private UnitOfWork unitOfWork;

        public DivisionServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(DivisionViewModel divisionViewModel)
        {
            var Division = new Division
            {

                DivisionName = divisionViewModel.DivisionName

            };

            unitOfWork.DivisionRepository.Insert(Division);
            unitOfWork.Save();
        }

        public void Update(DivisionViewModel divisionViewModel)
        {
            var Division = new Division
            {
                DivisionId = divisionViewModel.DivisionId,
                DivisionName = divisionViewModel.DivisionName

            };


            unitOfWork.DivisionRepository.Update(Division);

            unitOfWork.Save();
        }


        public DivisionViewModel GetByID(int id)
        {
            var data = (from s in unitOfWork.DivisionRepository.Get()
                        where s.DivisionId == id
                        select new DivisionViewModel
                        {
                            DivisionId = s.DivisionId,
                            DivisionName = s.DivisionName



                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<DivisionViewModel> GetAll()
        {
            var data = (from s in unitOfWork.DivisionRepository.Get()
                        select new DivisionViewModel
                        {
                            DivisionId = s.DivisionId,
                            DivisionName = s.DivisionName


                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var Division = new Division
            {

                DivisionId = id
            };

            unitOfWork.DivisionRepository.Delete(Division);
            unitOfWork.Save();
        }



        public IEnumerable<DivisionViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.DivisionRepository.Get()
                        select new DivisionViewModel
                        {
                            DivisionId = s.DivisionId,
                            DivisionName = s.DivisionName
                        }).AsEnumerable();

            return data;
        }

        public IEnumerable<DropDownViewModel> GetDropDownValue()
        {
            var data = (from s in unitOfWork.DivisionRepository.Get()
                        select new DropDownViewModel
                        {
                            Value = s.DivisionId,
                            Text = s.DivisionName
                        }).AsEnumerable();

            return data;
        }


    }
}
