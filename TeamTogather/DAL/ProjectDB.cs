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
        /// <summary>
        /// Return a datatable that lists all of the projects the user is in, the method receives the userID
        /// </summary>
        public static DataTable ProjectByUserID(int UserID) 
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = "Select Users.ID, Projects.* FROM " +
                    "(((Users INNER JOIN ProjectRequests ON Users.ID = ProjectRequests.UserID) " +
                    "INNER JOIN ProjectPositions ON ProjectPositions.ID = ProjectRequests.PositionID) INNER JOIN Projects ON ProjectPositions.ProjectID = Projects.ProjectID) WHERE Users.ID = " + 1 + ";";
                DataTable dt = helper.GetDataTable(sql);
                return dt; 
            }
            catch(Exception e)
            {
                return null;
            }
        } 
    }
}
