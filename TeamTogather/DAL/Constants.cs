using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Constants
    /// </summary>
    public class Constants
    {
        public static string PROVIDER = @"Microsoft.ACE.OLEDB.12.0";
        public static string PATH;
        public  Constants(string path)
        {
            PATH = path;
        }
    }
}
