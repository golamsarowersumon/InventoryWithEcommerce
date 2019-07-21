using Domain;
using System.Web.Mvc;
using Domain.Repositories;
using Core.Services;
using System.Linq;
using System.Reflection.Emit;
using System;
using System.Collections.Generic;

namespace Inventory.Controllers
{
    public class DynamicReportController : Controller
    {
        InventoryContext db = new InventoryContext();
        private UnitOfWork unitOfWork;
        private DynamicReportService dynamicReportService;

        public object Program { get; private set; }

        public DynamicReportController()
        {
            unitOfWork = new UnitOfWork();
            dynamicReportService = new DynamicReportService(unitOfWork);
        }
        // GET: DynamicReport
        public ActionResult Index()
        {

            return View();
        }

        public JsonResult GetTableName()
        {
            var tabname = db.Database.SqlQuery<string>(@"SELECT name FROM sys.Tables").ToList();

            return Json(tabname, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetColumn(string tabname)
        {
            var columnname = db.Database.SqlQuery<string>(@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '" + tabname + "'").ToList();

            return Json(columnname, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetValue(string tabname, string columnname)
        {
            var table = new List<object>();
            var ctx = new InventoryContext();
            var cmd = ctx.Database.Connection.CreateCommand();
            ctx.Database.Connection.Open();
            cmd.CommandText = "select " + columnname + " from " + tabname + "";
            var reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                object row = new object();
              
                for (int i = 0; i < reader.FieldCount; i++)

                    row = reader[i];
                table.Add(row);
            }
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReport(string tabname, string column1,string Column2,string value1,string value2)
        {
            var table = new List<object>();
            var ctx = new InventoryContext();
            var cmd = ctx.Database.Connection.CreateCommand();
            ctx.Database.Connection.Open();
            cmd.CommandText = "select * from "+ tabname + " where "+ column1 +"="+value1+"";
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                object row = new object();

                for (int i = 0; i < reader.FieldCount; i++)

                    row = reader[i];
                table.Add(row);
            }
            return Json(table, JsonRequestBehavior.AllowGet);
        }

    }
}