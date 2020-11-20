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
        /// * the sql statment merges the tables Users, ProjectRequests, ProjectPositions and projects and returns a table with -->
        ///   the userID and Projects table fields 
        /// </summary>
        public static DataTable ProjectByUserID(int UserID) 
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = "Select Users.ID, Projects.* FROM " +
                    "(((Users INNER JOIN ProjectRequests ON Users.ID = ProjectRequests.UserID) " +
                    "INNER JOIN ProjectPositions ON ProjectPositions.ID = ProjectRequests.PositionID) INNER JOIN Projects ON ProjectPositions.ProjectID = Projects.ProjectID) WHERE Users.ID = " + UserID + ";";
                DataTable dt = helper.GetDataTable(sql);
                return dt; 
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        /// <summary>
        /// the function gets an ID and a list of chosen professions
        /// and inserts the professions to the Project Positions
        /// on ProjectPositions database table.
        /// returns the number of rows affected by it.
        /// IF the method FAILS it will return -1
        /// </summary>
        public static bool InsertProjPositions(int ProjectID, List<KeyValuePair<int, List<int>>> ProfessionList)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = Build_queryProjectPositions(ProjectID, ProfessionList);
            int outcome = -1;
            outcome = helper.WriteData(sql);
            return outcome != -1;
        }


        /// <summary>
        /// the function gets an ID and a list of chosen professions and mendatory programs for which the user has to have in the project
        /// and inserts the programs to A Project Position mendatory programs
        /// on ProjPositionPrograms database table.
        /// returns the number of rows affected by it.
        /// IF the method FAILS it will return -1
        /// if the user didn't specified any programs, it will return true before executing the sql query.
        /// </summary>
        public static bool InsertPositionPrograms(int ProjectID, List<KeyValuePair<int, List<int>>> ProfessionList)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = Build_queryPositionProgram(ProjectID, ProfessionList);
            string no_programs = "INSERT INTO ProjPositionPrograms (ProjectPID, ProgramID ) " + "SELECT ProjectPID, ProgramID " + "FROM (" + ") AS [add]"; // if the user didn't specified any programs, it will return true before executing the sql query.
            if(sql == no_programs)
            {
                return true;
            }
            int outcome = -1;
            outcome = helper.WriteData(sql);
            return outcome != -1;
        }


        /// <summary>
        /// the function gets an ID of a project and a list of chosen professions and programs needed to that profession that the project needs
        /// and builds a query for the db to insert all of them
        /// to the ProjectPositions database table.
        /// </summary>
        public static string Build_queryProjectPositions(int ProjectID, List<KeyValuePair<int, List<int>>> ProfessionList)
        {
            if (ProfessionList == null)
            {
                return "";
            }
            else
            {
                string sql = $@"INSERT INTO ProjectPositions ( ProjectID, UserID, Profession ) " +
                          @"SELECT ProjectID, UserID, Profession " +
                          "FROM (";
                for (int i = 0; i < ProfessionList.Count; i++)
                {
                    sql += $@"SELECT Projects.ProjectID AS ProjectID, Users.ID AS UserID, Professions.ProfessionID AS Profession FROM Projects, Users, Professions " +
                            $@"WHERE ProjectID = {ProjectID} AND [Users.ID] = 1 AND Professions.ProfessionID = {ProfessionList[i].Key} ";
                    if (i + 1 < ProfessionList.Count) // check if this is not the last run, because the query doesn't need to end with the UNION statement
                    {
                        sql += $@"UNION ALL ";
                    }
                }
                sql += @") AS [add]";
                return sql;
            }
        }

        public static DataTable SelectPositionsProfession(int ProjectID, KeyValuePair<int, List<int>> ProfProgPair)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = $"SELECT ProjectPositions.ID, Profession FROM ProjectPositions WHERE Profession = {ProfProgPair.Key} AND ProjectID={ProjectID};";
            DataTable dt = helper.GetDataTable(sql);
            return dt;
        }  


        public static string Build_queryPositionProgram(int ProjectID, List<KeyValuePair<int, List<int>>> ProfessionList)
        {
            if (ProfessionList == null)
            {
                return "";
            }
            else
            {
                string sql = $@"INSERT INTO ProjPositionPrograms ( ProjectPID, ProgramID ) " +
                          @"SELECT ProjectPID, ProgramID " +
                          "FROM (";
                for (int i = 0; i < ProfessionList.Count; i++)
                {
                    if (ProfessionList[i].Value != null)
                    {
                        DataTable dt = SelectPositionsProfession(ProjectID, ProfessionList[i]);
                        for (int j=0;j<dt.Rows.Count;j++)
                        {
                            DataRow row = dt.Rows[j];
                            List<int> ProgramsList = ProfessionList[i].Value;
                            for (int k=0;k<ProgramsList.Count;k++)
                            {
                                sql += $@"SELECT ProjectPositions.ID AS ProjectPID, Programs.ProgramID AS ProgramID FROM ProjectPositions, Programs  " +
                                        $@"WHERE ProjectPositions.ID = {(int)row["ID"]} AND Programs.ProgramID = {ProgramsList[k]} ";
                                if (k + 1 < ProgramsList.Count || (i < ProfessionList.Count-1) ) // check if this is not the last run, because the query doesn't need to end with the UNION statement
                                {
                                    sql += $@"UNION ALL ";
                                }
                            }
                            try
                            {
                                if (dt.Rows.Count > 1 && j + 1 < dt.Rows.Count)
                                {
                                    i++;
                                }
                            }
                            catch
                            {
                                //nothing
                            }
                        }
                    }
                }
                sql += @") AS [add]";
                return sql;
            }
        }


        /// <summary>
        /// this method inserts a Project to the table, and returns the if it was succesfull or not.
        /// </summary>
        public static bool AddProject(int AdminUSID, int MinAge, int ProjectStatus,
            string ProjectContent, int AdminProfession, List<KeyValuePair<int, List<int>>> ProfessionList)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            int projectID = AddProject_only(AdminUSID, MinAge, ProjectStatus, ProjectContent);
            bool insertPositions = InsertProjPositions(projectID, ProfessionList);
            bool insertprogPositions = InsertPositionPrograms(projectID, ProfessionList);
            bool UpdatePosition = UpdateAdminPosition(AdminProfession, AdminUSID, projectID, helper);
            bool AddAdminRequest = AddAdminProjectRequest(AdminUSID, projectID);
            return UpdatePosition && AddAdminRequest && insertPositions && insertprogPositions || (UpdatePosition && AddAdminRequest && insertPositions && insertprogPositions == false);
        }

        /// <summary>
        /// this method inserts a Project to the table, and returns the id of the project for later use (update of Admin position in the project and etc')
        /// </summary>
        public static int AddProject_only(int AdminUSID, int MinAge, int ProjectStatus, string ProjectContent)
        {
            DateTime dt = DateTime.Now;
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = $"INSERT INTO Projects (AdminUsID, MinAge, ProjectStatus, NumRateVoters, ProjectRate, ProjectContent, DateCreated) " +
                $"VALUES({AdminUSID}, {MinAge}, {ProjectStatus}, 0, 0, '{ProjectContent}', #{dt.ToString("MM/dd/yyyy HH:mm:ss")}#);";
            int ProjectID = helper.InsertWithAutoNumKey(sql);
            return ProjectID;
        }

        /// <summary>
        /// method for testing DELETE LATER
        /// </summary>
        public static bool unite(int AdminProfession, int AdminUSID, int ProjectID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            bool r = UpdateAdminPosition(AdminProfession, AdminUSID, ProjectID, helper);
            bool k = AddAdminProjectRequest(AdminUSID, ProjectID);
            return k && r;
        } 
        /// <summary>
        /// this method updates the AdminPosition on the project in the ProjectPositions db table.
        /// </summary>
        public static bool UpdateAdminPosition(int AdminProfession, int AdminUSID, int ProjectID, DBHelper helper)
        {
            string sql = $"UPDATE(SELECT TOP 1 UserID" +
                         $" FROM ProjectPositions" +
                         $" WHERE Profession = {AdminProfession} AND ProjectID = {ProjectID} ORDER BY ID) AS updateTable" +
                         $" SET ProjectPositions.UserID = {AdminUSID};";
            int updatestate = helper.WriteData(sql);
            return (updatestate != -1);
        }

        /// <summary>
        /// this method adds to a specified project admin a Project Request to the ProjectRequests db table. and returns true of false based of weather it was 
        /// successful or not.
        /// </summary>
        public static bool AddAdminProjectRequest(int userID, int ProjectID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            DateTime dt = DateTime.Now;
            string sql = $"INSERT INTO ProjectRequests (PositionID, UserID, RequestStatus, DateRequested, RequestType)" +
                $"SELECT ProjectPositions.ID, {userID}, 2, #{dt}#, 2" +
                $" FROM ProjectPositions" +
                $" WHERE UserID={userID} AND ProjectPositions.ProjectID = {ProjectID};";
            int insertion = helper.WriteData(sql);
            return (insertion != -1);
        }


        /// <summary>
        /// this method adds to a specified user a Project Request to the ProjectRequests db table to a specified profession in a project.
        /// and returns true of false based of weather it was successful or not.
        /// </summary>
        public static bool AddProjectRequest(int userID, int ProjectID, int RequestStatus, int RequestType, int RequestedProfession)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            DateTime dt = DateTime.Now;
            string sql = $"INSERT INTO ProjectRequests (PositionID, UserID, RequestStatus, DateRequested, RequestType)" +
                $"SELECT ProjectPositions.ID, {userID}, {RequestStatus}, #{dt}#, {RequestType}" +
                $" FROM ProjectPositions" +
                $" WHERE Profession={RequestedProfession} AND ProjectPositions.ProjectID = {ProjectID} AND ProjectPositions.UserID = 1;";
            int insertion = helper.WriteData(sql);
            return (insertion != -1);
        }
    }



}
