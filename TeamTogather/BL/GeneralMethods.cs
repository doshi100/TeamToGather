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
        /// <summary>
        /// sets the DBpath for the DB connection string
        /// </summary>
        static public void SetDBPath(string path)
        {
            Constants c = new Constants(path);

        }


        /// <summary>
        /// Creates DataTime object from a specified year, day and month, used to set Birthday dates and etc'
        /// </summary>
        
        static public DateTime CreateDateTime(int year, int month, int day)
        {
            DateTime dt = new DateTime(year, month, day);
            Console.WriteLine(dt);
            return dt;
        }

    }
}
