using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
   public class NationalityServices
    {


        private UnitOfWork unitOfWork;

        public NationalityServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(NationalityViewModel nationalityViewModel)
        {
            var Nationality = new Nationality
            {

                NationalityName = nationalityViewModel.NationalityName

            };

            unitOfWork.NationalityRepositoy.Insert(Nationality);
            unitOfWork.Save();
        }

        public void Update(NationalityViewModel nationalityViewModel)
        {
            var Nationality = new Nationality
            {
                NationalityID = nationalityViewModel.NationalityID,
                NationalityName = nationalityViewModel.NationalityName

            };


            unitOfWork.NationalityRepositoy.Update(Nationality);

            unitOfWork.Save();
        }


        public NationalityViewModel GetByID(int? id)
        {
            var data = (from s in unitOfWork.NationalityRepositoy.Get()
                        where s.NationalityID == id
                        select new NationalityViewModel
                        {
                            NationalityID = s.NationalityID,
                            NationalityName = s.NationalityName



                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<NationalityViewModel> GetAll()
        {
            var data = (from s in unitOfWork.NationalityRepositoy.Get()
                        select new NationalityViewModel
                        {
                            NationalityID = s.NationalityID,
                            NationalityName = s.NationalityName


                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var Nationality = new Nationality
            {

                NationalityID = id
            };

            unitOfWork.NationalityRepositoy.Delete(Nationality);
            unitOfWork.Save();
        }



        public IEnumerable<NationalityViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.NationalityRepositoy.Get()
                        select new NationalityViewModel
                        {
                            NationalityID = s.NationalityID,
                            NationalityName = s.NationalityName
                        }).AsEnumerable();

            return data;
        }


        public IEnumerable<DropDownViewModel> GetDropDownValue()
        {
            var data = (from s in unitOfWork.NationalityRepositoy.Get()
                        select new DropDownViewModel
                        {
                            Value = s.NationalityID,
                            Text = s.NationalityName
                        }).AsEnumerable();

            return data;
        }






    }
}
