using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;

namespace BL
{
    public class GeneralMethods
    {

        static public void SetDBPath(string path)
        {
            Constants c = new Constants(path);

        }

    }
}
