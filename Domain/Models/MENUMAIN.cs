using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Domain.Models
{
  public  class MENUMAIN
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MainMenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public string RoleId { get; set; }
        //public string ProfileId { get; set; }
        
        
        public virtual ICollection<MENUSUB> MENUSUB { get; set; }
       

    }
}
