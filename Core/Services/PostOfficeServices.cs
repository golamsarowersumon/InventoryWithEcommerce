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
   public class PostOfficeServices
    {




        private UnitOfWork unitOfWork;

        public PostOfficeServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(PostOfficeViewModel PostOfficeViewModel)
        {
            var PostOffice = new PostOffice
            {


                PostOfficeName = PostOfficeViewModel.PostOfficeName,
                DistrictId = PostOfficeViewModel.DistrictId



            };

            unitOfWork.PostOfficeRepository.Insert(PostOffice);
            unitOfWork.Save();
        }

        public void Update(PostOfficeViewModel PostOfficeViewModel)
        {
            var PostOffice = new PostOffice
            {
                PostOfficeId=PostOfficeViewModel.PostOfficeId,
                PostOfficeName = PostOfficeViewModel.PostOfficeName,
                DistrictId = PostOfficeViewModel.DistrictId


            };


            unitOfWork.PostOfficeRepository.Update(PostOffice);

            unitOfWork.Save();
        }


        public PostOfficeViewModel GetByID(int? id)
        {
            var data = (from s in unitOfWork.PostOfficeRepository.Get()
                        where s.PostOfficeId == id
                        select new PostOfficeViewModel
                        {
                            PostOfficeId = s.PostOfficeId,
                            PostOfficeName = s.PostOfficeName,
                            DistrictId= s.DistrictId,
                            DistrictName = s.District.DistrictName





                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<PostOfficeViewModel> GetAll()
        {
            var data = (from s in unitOfWork.PostOfficeRepository.Get()
                        select new PostOfficeViewModel
                        {
                            PostOfficeId = s.PostOfficeId,
                            PostOfficeName = s.PostOfficeName,
                            DistrictId = s.DistrictId,
                            DistrictName = s.District.DistrictName

                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var PostOffice = new PostOffice
            {

                PostOfficeId = id
            };

            unitOfWork.PostOfficeRepository.Delete(PostOffice);
            unitOfWork.Save();
        }



        public IEnumerable<DropDownViewModel> GetDropDown()
        {
            var data = (from s in unitOfWork.PostOfficeRepository.Get()
                        select new DropDownViewModel
                        {
                            Value = s.PostOfficeId,
                            Text = s.PostOfficeName
                        }).AsEnumerable();

            return data;
        }



    }
}
