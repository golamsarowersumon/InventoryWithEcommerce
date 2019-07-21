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
    public class LanguageServices
    {
        private UnitOfWork unitOfWork;

        public LanguageServices(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public void Create(LanguageViewModel languageVM)
        {
            var Language = new Language
            {

                LanguageName = languageVM.LanguageName
            };

            unitOfWork.LanguageRepository.Insert(Language);
            unitOfWork.Save();
        }


        public void Update(LanguageViewModel languageVM)
        {
            var Language = new Language
            {
                LanguageId = languageVM.LanguageId,
                LanguageName = languageVM.LanguageName
            };
            unitOfWork.LanguageRepository.Update(Language);
            unitOfWork.Save();
        }

        public LanguageViewModel GetById(int id)
        {
            var data = (from s in unitOfWork.LanguageRepository.Get()
                        where s.LanguageId == id
                        select new LanguageViewModel
                        {
                            LanguageId = s.LanguageId,
                            LanguageName = s.LanguageName
                        }).SingleOrDefault();
            return data;
        }
        public IEnumerable<LanguageViewModel> GetAll()
        {
            var data = (from s in unitOfWork.LanguageRepository.Get()
                        select new LanguageViewModel
                        {

                            LanguageId = s.LanguageId,
                            LanguageName = s.LanguageName

                        }).AsEnumerable();
            return data;
        }

        public void Delete(int id)
        {
            var Language = new Language
            {
                LanguageId = id
            };

            unitOfWork.LanguageRepository.Delete(Language);
            unitOfWork.Save();

        }
    }
}
