using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class StoreAssignViewModel
    {


        public int Id { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string UserName { get; set; }


    }
}
