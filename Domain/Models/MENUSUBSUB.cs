using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
  public  class MENUSUBSUB
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubSubMenuId { get; set; }
        public int SubMenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        //public string ProfileId { get; set; }
        public virtual MENUSUB MENUSUB { get; set; }
      


    }
}
