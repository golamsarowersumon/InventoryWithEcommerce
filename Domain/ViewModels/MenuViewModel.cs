using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class MenuViewModel
    {
        public int MainMenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuURL { get; set; }
        public int SubSubMenuId { get; set; }
        public string SubSubMenuName { get; set; }
        public string SubSubMenuURL { get; set; }
       
    }
}
