using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
 public class MENUSUB
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubMenuId { get; set; }
        public int MainMenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        //public string ProfileId { get; set; }
        public virtual MENUMAIN MENUMAIN { get; set; }

        public virtual ICollection<MENUSUBSUB> MENUSUBSUB { get; set; }

    }
}
