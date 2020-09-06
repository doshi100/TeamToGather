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
        /// the function query's the user's credentials by username and password from the database, if there is a match, it will return a DataTable of that user
        /// </summary>
        public static DataRow UserAuthentication(string UsNa, string pass)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = "SELECT * FROM Users WHERE UserName = '" + UsNa + "' AND Pass = '" + pass + "';";
            DataTable userTable = helper.GetDataTable(sql);
            return userTable.Rows[0];
        }

    }
}
