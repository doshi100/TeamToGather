using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL
{
    public class UserDB
    {
        /// <summary>
        /// the function query's a user's username and password from the database, if there is a match, return TRUE otherwise FALSE
        /// </summary>
        public static bool UserAuthintication(string UsNa, string pass)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = "SELECT UserName, Pass FROM Users WHERE UserName = " + UsNa + "AND Pass = " + pass + ";";
            DataTable userTable = helper.GetDataTable(sql);

            if(userTable.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// this function brings the u
        /// </summary>
    }
}
