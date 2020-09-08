using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL
{
    public class ProjectDB
    {
        public static DataRow ProjectByUserID(int UserID) 
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = "Select Users.ID, Projects.* FROM " +
                    "(((Users INNER JOIN ProjectRequests ON Users.ID = ProjectRequests.UserID) " +
                    "INNER JOIN ProjectPositions ON ProjectPositions.ID = ProjectRequests.PositionID) INNER JOIN Projects ON ProjectPositions.ProjectID = Projects.ProjectID) WHERE Users.ID = " + 1 + ";";
                DataTable dt = helper.GetDataTable(sql);
                DataRow dr = dt.Rows[0];
                return dr; 
            }
            catch(Exception e)
            {
                return null;
            }
        } 
    }
}
