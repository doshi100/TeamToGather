using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL
{
    public class ProfessionDB
    {

        /// <summary>
        /// returns a the db Profession table as a DataTable 
        /// </summary>
        
        public static DataTable RetProfessionTable()
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = "SELECT * FROM Professions";
                DataTable dtprof = helper.GetDataTable(sql);
                return dtprof;
            }
            catch(Exception)
            {
                return null;
            }

        }
    }
}
