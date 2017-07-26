using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMRModel
{
    public class MenuObj
    {
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public string ActionControllerCode { get; set; }
        public int? ParentMenuId { get; set; }

    }

    public class MenuObjViewModel
    {
        public List<MenuObj> MenuObj { get; set; }
    }
}
