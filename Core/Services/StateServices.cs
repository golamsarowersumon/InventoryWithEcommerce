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
   public class StateServices
    {


        private UnitOfWork unitOfWork;

        public StateServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(StateViewModel StateViewModel)
        {
            var State = new State
            {


                StateName = StateViewModel.StateName,
                CountryID = StateViewModel.CountryID
                


            };

            unitOfWork.StateRepository.Insert(State);
            unitOfWork.Save();
        }

        public void Update(StateViewModel StateViewModel)
        {
            var State = new State
            {
                StateID = StateViewModel.StateID,
                StateName = StateViewModel.StateName,
                CountryID = StateViewModel.CountryID


            };


            unitOfWork.StateRepository.Update(State);

            unitOfWork.Save();
        }


        public StateViewModel GetByID(int? id)
        {
            var data = (from s in unitOfWork.StateRepository.Get()
                        where s.StateID == id
                        select new StateViewModel
                        {
                            StateID = s.StateID,
                            StateName = s.StateName,
                            CountryID = s.CountryID,
                            CountryName = s.Country.CountryName





                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<StateViewModel> GetAll()
        {
            var data = (from s in unitOfWork.StateRepository.Get()
                        select new StateViewModel
                        {
                            StateID = s.StateID,
                            StateName = s.StateName,
                            CountryID = s.CountryID,
                            CountryName = s.Country.CountryName

                        }).AsEnumerable();

            return data;
        }


        public void Delete(int id)
        {
            var State = new State
            {

                StateID = id
            };

            unitOfWork.StateRepository.Delete(State);
            unitOfWork.Save();
        }


    }
}
