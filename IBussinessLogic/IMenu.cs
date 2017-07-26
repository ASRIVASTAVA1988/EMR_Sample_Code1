using EMRModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBussinessLogic
{
    public interface IMenu
    {
        List<MenuObj> GetMenu(int UserId);
    }
}
