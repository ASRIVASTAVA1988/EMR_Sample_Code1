using IBussinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMRModel;

namespace BussinessLogic
{
    public class Menu : IMenu
    {


        List<MenuObj> IMenu.GetMenu(int UserId)
        {
            DataLayer.Menu datalayer = new DataLayer.Menu();
            return datalayer.GetMenufromUserId(UserId);
        }
    }
}
