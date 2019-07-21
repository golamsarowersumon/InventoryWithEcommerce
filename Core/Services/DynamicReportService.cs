using Domain;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace Core.Services
{
   public class DynamicReportService
    {
        private UnitOfWork unitOfWork;

        InventoryContext db = new InventoryContext();

        public DynamicReportService(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        //public List<string> GetTable()
        //{
        //    var tabname = db.Database.SqlQuery<string>(@"SELECT name FROM sys.Tables");
        //    return tabname;
        //}

    }
}
