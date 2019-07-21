using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
   public class StoreAssign
    {
        private UnitOfWork unitOfWork;

        public StoreAssign(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
    }
}
