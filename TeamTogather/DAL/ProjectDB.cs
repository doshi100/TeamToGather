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
                string sql = "Select DISTINCT Users.ID, Projects.* FROM " +
                    "(((Users INNER JOIN ProjectRequests ON Users.ID = ProjectRequests.UserID) " +
                    "INNER JOIN ProjectPositions ON ProjectPositions.ID = ProjectRequests.PositionID) INNER JOIN Projects ON ProjectPositions.ProjectID = Projects.ProjectID) WHERE Users.ID = " + UserID + " AND Projects.ProjectStatus <> 3;";
                DataTable dt = helper.GetDataTable(sql);
                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static DataTable ProjectsByUserID(int UserID, DateTime indexDate)
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = "Select DISTINCT TOP 10 Projects.* FROM " +
                    "(((Users INNER JOIN ProjectRequests ON Users.ID = ProjectRequests.UserID) " +
                    "INNER JOIN ProjectPositions ON ProjectPositions.ID = ProjectRequests.PositionID) INNER JOIN Projects ON ProjectPositions.ProjectID = Projects.ProjectID) WHERE (ProjectPositions.UserID = " + UserID + " OR Projects.AdminUsID = " + UserID + ") AND Projects.ProjectStatus <> 3" +
                    $"AND Projects.DateCreated< FORMAT(#{indexDate}#, 'mm / dd / yyyy hh: nn: ss') ORDER BY Projects.DateCreated DESC;";
                DataTable dt = helper.GetDataTable(sql);
                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        //public static DataTable ProjectsByUserID(int UserID, DateTime indexDate)
        //{
        //    try
        //    {
        //        DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
        //        string sql = "Select DISTINCT TOP 10 Projects.* FROM " +
        //            "Projects WHERE Projects.AdminUsID = " + UserID + " AND Projects.ProjectStatus <> 3" +
        //            $"AND Projects.DateCreated< FORMAT(#{indexDate}#, 'mm / dd / yyyy hh: nn: ss') ORDER BY Projects.DateCreated DESC;";
        //        DataTable dt = helper.GetDataTable(sql);
        //        return dt;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //        return null;
        //    }
        //}

        public static DataRow ReturnProject(int ProjectID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT Projects.* FROM Projects WHERE ProjectID = {ProjectID};";
            DataTable dt = helper.GetDataTable(query);
            return dt.Rows[0];
        }

        /// <summary>
        /// this method returns a list of Projects Positions.
        /// <summary>
        public static DataTable ReturnProjectPositions(int ProjectID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT ProjectPositions.* FROM ProjectPositions WHERE ProjectPositions.ProjectID = {ProjectID} AND ProjectPositions.IsDeleted = 1;";
            DataTable dt = helper.GetDataTable(query);
            return dt;
        }

        /// <summary>
        /// the function gets an ID and a list of chosen professions
        /// and inserts the professions to the Project Positions
        /// on ProjectPositions database table.
        /// returns the number of rows affected by it.
        /// IF the method FAILS it will return -1
        /// </summary>
        public static int InsertProjPositions(int ProjectID, List<KeyValuePair<int, List<int>>> ProfessionList)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = Build_queryProjectPositions(ProjectID, ProfessionList);
            int outcome = -1;
            outcome = helper.WriteDataWithAutoNumKey(sql);
            return outcome;
        }


        /// <summary>
        /// the function gets an ID and a list of chosen professions and mendatory programs for which the user has to have in the project
        /// and inserts the programs to A Project Position mendatory programs
        /// on ProjPositionPrograms database table.
        /// returns the number of rows affected by it.
        /// IF the method FAILS it will return -1
        /// if the user didn't specified any programs, it will return true before executing the sql query.
        /// </summary>
        public static bool InsertPositionPrograms(int ProjectID, List<KeyValuePair<int, List<int>>> ProfessionList, int ID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = Build_queryPositionProgram(ProjectID, ProfessionList, ID);
            string no_programs = "INSERT INTO ProjPositionPrograms (ProjectPID, ProgramID ) " + "SELECT ProjectPID, ProgramID " + "FROM (" + ") AS [add]"; // if the user didn't specified any programs, it will return true before executing the sql query.
            if (sql == no_programs)
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
                string sql = $@"INSERT INTO ProjectPositions ( ProjectID, UserID, Profession, IsDeleted ) " +
                          @"SELECT ProjectID, UserID, Profession, 1 " +
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

        public static DataTable SelectPositionsProfession(int ProjectID, KeyValuePair<int, List<int>> ProfProgPair, int ID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = $"SELECT ProjectPositions.ID, Profession FROM ProjectPositions WHERE Profession = {ProfProgPair.Key} AND ProjectID={ProjectID} AND ProjectPositions.ID > {ID};";
            DataTable dt = helper.GetDataTable(sql);
            return dt;
        }


        public static string Build_queryPositionProgram(int ProjectID, List<KeyValuePair<int, List<int>>> ProfessionList, int ID)
        {
            if (ProfessionList == null)
            {
                return "";
            }
            else
            {
                string sql = $@"INSERT INTO ProjPositionPrograms ( ProjectPID, ProgramID, IsDeleted ) " +
                          @"SELECT ProjectPID, ProgramID, 1 " +
                          "FROM (";
                bool HasExecuted = false;
                for (int i = 0; i < ProfessionList.Count; i++) //+1 every time ListProject insertion ends, and a new one is getting added, 
                {
                    //if (ProfessionList[i].Value.Count != 0)
                    //{
                    DataTable dt = SelectPositionsProfession(ProjectID, ProfessionList[i], ID-ProfessionList.Count);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        DataRow row = dt.Rows[j];
                        if (i < ProfessionList.Count)
                        {

                            List<int> ProgramsList = ProfessionList[i].Value;
                            if(ProgramsList.Count != 0)
                            {
                                for (int k = 0; k < ProgramsList.Count; k++)
                                {
                                    if (k == 0 && HasExecuted)
                                    {
                                        sql += $@"UNION ALL SELECT ProjectPositions.ID AS ProjectPID, Programs.ProgramID AS ProgramID FROM ProjectPositions, Programs  " +
                                            $@"WHERE ProjectPositions.ID = {(int)row["ID"]} AND Programs.ProgramID = {ProgramsList[k]} ";
                                    }
                                    else if (k == 0)
                                    {
                                        sql += $@"SELECT ProjectPositions.ID AS ProjectPID, Programs.ProgramID AS ProgramID FROM ProjectPositions, Programs " +
                                            $@"WHERE ProjectPositions.ID = {(int)row["ID"]} AND Programs.ProgramID = {ProgramsList[k]} ";
                                    }
                                    else
                                    {
                                        sql += $@"UNION ALL SELECT ProjectPositions.ID AS ProjectPID, Programs.ProgramID AS ProgramID FROM ProjectPositions, Programs " +
                                            $@"WHERE ProjectPositions.ID = {(int)row["ID"]} AND Programs.ProgramID = {ProgramsList[k]} ";
                                    }
                                    HasExecuted = true;
                                    //if (k + 1 < ProgramsList.Count || (i < ProfessionList.Count - 1)) // check if this is not the last run, because the query doesn't need to end with the UNION statement
                                    //{
                                    //    if(!((i + 1 == ProfessionList.Count - 1) && (ProfessionList[i + 1].Value.Count != 0)))
                                    //    {
                                    //        sql += $@"UNION ALL ";
                                    //    }
                                    //}
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
                    //}
                }
                sql += @") AS [add]";
                return sql;
            }
        }

        /// <summary>
        /// checks if the admin of the project is already in the chosen position or if he is not. (this method is used when updating the project)
        /// </summary>
        public static bool CheckProfessionPositionAtpos(int projectid, int userid, int profession)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = $"SELECT * FROM ProjectPositions WHERE ProjectID = {projectid} AND UserID = {userid} AND Profession = {profession} AND IsDeleted <> 2;";
            DataTable dt = helper.GetDataTable(sql);
            return dt.Rows.Count == 1;
        }

        /// <summary>
        /// this method inserts a Project to the table, and returns the if it was succesfull or not.
        /// </summary>
        public static bool AddProject(int AdminUSID, int MinAge, int ProjectStatus,
            string ProjectContent, int AdminProfession, List<KeyValuePair<int, List<int>>> ProfessionList)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            int projectID = AddProject_only(AdminUSID, MinAge, ProjectStatus, ProjectContent);
            int insertPositions = InsertProjPositions(projectID, ProfessionList);
            bool insertprogPositions = InsertPositionPrograms(projectID, ProfessionList, insertPositions);
            bool UpdatePosition = UpdateAdminPosition(AdminProfession, AdminUSID, projectID, helper);
            bool AddAdminRequest = AddAdminProjectRequest(AdminUSID, projectID);
            return UpdatePosition && AddAdminRequest && insertPositions > 0 && insertprogPositions || (UpdatePosition && AddAdminRequest && insertPositions > 0 && insertprogPositions == false);
        }

        public static bool UpdatePageAdminPos(int AdminUSID, int AdminProfession, int ProjectID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            bool UpdatePosition = UpdateAdminPosition2(AdminProfession, AdminUSID, ProjectID, helper);
            bool AddAdminRequest = AddAdminProjectRequestByProf(AdminUSID, ProjectID, AdminProfession);
            return UpdatePosition && AddAdminRequest;
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
                         $" WHERE Profession = {AdminProfession} AND ProjectID = {ProjectID} AND" +
                         $" ID NOT IN (SELECT ProjectPID FROM ProjPositionPrograms) ORDER BY ID) AS updateTable" +
                         $" SET ProjectPositions.UserID = {AdminUSID};";
            int updatestate = helper.WriteData(sql);
            return (updatestate != -1);
        }

        /// <summary>
        /// this method updates the AdminPosition on the project in the ProjectPositions db table.
        /// </summary>
        public static bool UpdateAdminPosition2(int AdminProfession, int AdminUSID, int ProjectID, DBHelper helper)
        {
            string sql = $"UPDATE(SELECT TOP 1 UserID" +
                         $" FROM ProjectPositions" +
                         $" WHERE Profession = {AdminProfession} AND ProjectID = {ProjectID} AND IsDeleted <> 2 AND UserID = 1 AND" +
                         $" ID NOT IN (SELECT ProjectPID FROM ProjPositionPrograms) ORDER BY ID) AS updateTable" +
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
                $" WHERE UserID={userID} AND ProjectPositions.ProjectID = {ProjectID} AND ProjectPositions.IsDeleted <> 2;";
            int insertion = helper.WriteData(sql);
            return (insertion != -1);
        }


        /// <summary>
        /// this method adds to a specified project admin a Project Request to the ProjectRequests db table. and returns true of false based of wether it was.
        /// successful or not.
        /// by prof of the userAdmin
        /// </summary>
        public static bool AddAdminProjectRequestByProf(int userID, int ProjectID, int profession)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            DateTime dt = DateTime.Now;
            string sql = $"INSERT INTO ProjectRequests (PositionID, UserID, RequestStatus, DateRequested, RequestType)" +
                $"SELECT ProjectPositions.ID, {userID}, 2, #{dt}#, 2" +
                $" FROM ProjectPositions" +
                $" WHERE UserID={userID} AND ProjectPositions.ProjectID = {ProjectID} AND ProjectPositions.IsDeleted <> 2 AND ProjectPositions.Profession = {profession};";
            int insertion = helper.WriteData(sql);
            return (insertion != -1);
        }

        /// <summary>
        /// OPTION 1 -DON'T USE IN THE MEANTIME, IF YOU USE OPTION 2, DELETE THIS
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


        /// <summary>
        /// this function shows the user Projects he can join to, based on Age, Time, Project Rate and Profession restriction he choose
        /// </summary>
        /// <param name="UserID">paramater that passed in order to know what user is requesting the projects, and show some of them if he has the right conditions</param>
        /// <param name="ProjectIndexShow">takes only the projects the user hasn't loaded NEED TO BE CHANGED EVERYTIME</param>
        /// <param name="allPara">all parameters defaults are 0, except UserID and date(date default is current month)<param>
        /// <returns>the method returns the table from the indexShow offset, in order to load the user projects he hasn't saw.</returns>
        public static DataTable ShowProjects(int UserID, int AgeFilter, DateTime DateFilter, int RateFilter, int ProjectIndexShow)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT DISTINCT TOP 10 Projects.*" +
                $" FROM(UserProf INNER JOIN ProjectPositions ON UserProf.ProfID = ProjectPositions.Profession) INNER JOIN Projects ON ProjectPositions.ProjectID = Projects.ProjectID" +
                $" WHERE ProjectPositions.UserID = 1 AND UserProf.UserID = {UserID} AND Projects.ProjectID > {ProjectIndexShow} AND Projects.MinAge >= {AgeFilter}" +
                $" AND Projects.ProjectStatus <> 3 AND ProjectPositions.IsDeleted <> 2 AND Projects.DateCreated >= #{DateFilter.ToString("MM/dd/yyyy HH:mm:ss")}# AND (Projects.ProjectRate / IIF(Projects.NumRateVoters = 0, 1, Projects.NumRateVoters) >= {RateFilter})";
            query += $" ORDER BY Projects.ProjectID;";
            DataTable dt = helper.GetDataTable(query);
            return dt;
        }

        /// <summary>
        /// OPTION 2 USE THIS.
        /// </summary>
        public static bool AddProjectReqeust(int PositionID, int userID, int RequestStatus, int RequestType)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            DateTime DateRequested = DateTime.Now;
            string query = $"INSERT INTO ProjectRequests ( PositionID, UserID, RequestStatus, DateRequested, RequestType ) " +
                $"VALUES({PositionID}, {userID}, {RequestStatus}, #{DateRequested.ToString("MM/dd/yyyy HH:mm:ss")}#, {RequestType});";
            int succeeded = helper.InsertWithAutoNumKey(query);
            return succeeded != -1;
        }


        /// <summary>
        /// returns the num rate voters of a project, if the project id is false, it will return -1
        /// </summary>
        public static int GetNumRateVoters(int ProjectID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT Projects.ProjectRate, Projects.NumRateVoters FROM Projects WHERE Projects.ProjectID = {ProjectID};";
            DataTable dt = helper.GetDataTable(query);
            if (dt != null)
            {
                DataRow row = dt.Rows[0];
                int numRateVoters = (int)row["NumRateVoters"];
                return numRateVoters;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// updates the project's rate, rate should be sum + new rate, if it is a new rate which wasn't rated before newRate bool need to be set to 'true'
        /// </summary>
        public static bool updateProjectRate(int ProjectID, int rate, bool newRate)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query;
            if (newRate)
            {
                query = $"UPDATE Projects SET NumRateVoters = NumRateVoters + 1, ProjectRate = {rate} WHERE Projects.ProjectID = {ProjectID};";
            }
            else
            {
                query = $"UPDATE Projects SET NumRateVoters = NumRateVoters, ProjectRate = {rate} WHERE Projects.ProjectID = {ProjectID};";
            }
            int updateSucceeded = helper.WriteData(query);
            return updateSucceeded != -1;
        }

        /// <summary>
        /// updates the user rate of a specific project that he is rating.
        /// </summary>
        public static bool updateUserRateAtProject(int ProjectID, int userID, int rate)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"UPDATE ProjectRate SET ProjectRate.ProjectRate = {rate} WHERE ProjectID = {ProjectID} AND UserID = {userID};";
            int updateSucceeded = helper.WriteData(query);
            return updateSucceeded != -1;
        }

        /// <summary>
        /// returns weather or not a particular user has requested a position.  
        /// </summary>
        public static bool CheckUserAtProjectPos(int positionID, int userID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT RequestID FROM ProjectRequests WHERE PositionID = {positionID} AND UserID = {userID} AND RequestType = 1;";
            DataTable dt = helper.GetDataTable(query);
            return dt.Rows.Count != 0;
        }

        /// <summary>
        /// gets the programs/knowledge of a specified position and return a table of their Symbol Path
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProgramsAtPosition(int PosID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT Programs.ProgPath FROM Programs INNER JOIN ProjPositionPrograms ON " +
                $"Programs.ProgramID = ProjPositionPrograms.ProgramID WHERE ProjPositionPrograms.ProjectPID = {PosID};";
            DataTable dt = helper.GetDataTable(query);
            return dt;
        }

        public static DataRow GetPosition(int PosID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT * FROM ProjectPositions WHERE ID = {PosID};";
            DataTable dt = helper.GetDataTable(query);
            if (dt != null)
            {
                return dt.Rows[0];
            }
            return null;
        }
        /// <summary>
        /// this method inserts a project rate record into the ProjectRate db table.
        /// project rates are from 1 to 5.
        /// </summary>
        public static bool InsertProjectRate_Rec(int projectID, int userID, int projectRate)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"INSERT INTO ProjectRate (ProjectID, UserID, ProjectRate)" +
                            $"VALUES({projectID}, {userID}, {projectRate});";
            int exe_status = helper.InsertWithAutoNumKey(query);
            return exe_status != -1;
        }

        /// <summary>
        /// returns the user rate vote on a specific project.
        /// </summary>
        public static DataRow ReturnUserRateAtProject(int projectID, int userID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT * FROM ProjectRate WHERE ProjectRate.ProjectID = {projectID} AND ProjectRate.UserID = {userID};";
            DataTable dt = helper.GetDataTable(query);
            if (dt.Rows.Count != 0)
            {
                return dt.Rows[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// checks if the user has already voted for that project
        /// </summary>
        public static bool CheckUserRate_Project(int projectID, int userID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT * FROM ProjectRate WHERE ProjectRate.ProjectID = {projectID} AND ProjectRate.UserID = {userID};";
            DataTable dt = helper.GetDataTable(query);
            return dt.Rows.Count == 1;
        }


        /// <summary>
        /// method for removing users from a pos (by entering userID = 1) or add them to position (by entering their userID)
        /// </summary>
        public static bool AddOrRemoveUserFromPos(int userID, int PosID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"UPDATE ProjectPositions SET UserID = {userID} WHERE ProjectPositions.ID = {PosID};";
            int affected = helper.WriteData(query);
            return affected > 0;
        }

        /// <summary>
        /// method for updating request status (1 - requested | 2 - accepted  | 3 - rejected)
        /// </summary>
        public static bool UpdateRequestStatus(int requestStatus, int requestID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"UPDATE ProjectRequests SET RequestStatus = {requestStatus} WHERE ProjectRequests.RequestID = {requestID};";
            int affected = helper.WriteData(query);
            return affected > 0;
        }

        public static bool UpdateRequestStatusByPos(int positionID, int userID, int statusRe)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"UPDATE ProjectRequests SET RequestStatus = {statusRe} WHERE ProjectRequests.PositionID = {positionID} AND ProjectRequests.UserID = {userID};";
            int affected = helper.WriteData(query);
            return affected > 0;
        }

        /// <summary>
        /// update Min Age, Project Status and Project Content of a specific Project by its ID (positions are updated differently)
        /// </summary>
        public static bool UpdateProject(int minage, int projectStatus, string ProjectContent, int projectID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"UPDATE Projects SET MinAge = {minage}, ProjectStatus = {projectStatus}, ProjectContent = '{ProjectContent}' WHERE ProjectID = {projectID};";
            int affected = helper.WriteData(query);
            return affected > 0;
        }

        public static bool DeletePos(int positionID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"UPDATE ProjectPositions SET IsDeleted = 2, DateDeleted = FORMAT(Now(), 'mm / dd / yyyy hh: nn: ss') WHERE ProjectPositions.ID = {positionID};";
            int affected = helper.WriteData(query);
            return affected > 0;
        }

        public static bool neutralizePositionsRequests(int positionID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"UPDATE ProjectRequests SET RequestStatus = 3 WHERE PositionID = {positionID};";
            int affected = helper.WriteData(query);
            return affected > 0;
        }

        public static List<int> returnProjAdminProf(int ProjectID, int AdminID)
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string query = $"SELECT Profession FROM ProjectPositions WHERE ProjectID = {ProjectID} AND UserID = {AdminID} AND IsDeleted <> 2;";
                DataTable dt = helper.GetDataTable(query);
                List<int> professionIDs = new List<int>();
                if(dt.Rows.Count > 0)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        int professionID = (int)row["Profession"];
                        professionIDs.Add(professionID);
                    }
                }
                
                return professionIDs;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable returnProjectHeadLines(int userid)
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string query = $"SELECT ProjectID, ProjectContent FROM Projects WHERE ProjectStatus <> 3 AND AdminUsID = {userid} " +
                    $"ORDER BY DateCreated DESC; ";
                DataTable dt = helper.GetDataTable(query);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// checks if an invitation was already sent by the user to a specific position in a project
        /// </summary>
        /// <returns></returns>
        public static bool CheckRequestInvitation(int positionID, int userID)
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string query = $"SELECT * FROM ProjectRequests WHERE PositionID = {positionID} AND UserID = {userID} AND RequestType = 2 AND RequestStatus = 1;";
                DataTable dt = helper.GetDataTable(query);
                return dt.Rows.Count == 1;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// returns the DataTable of top of the 10 projects(max) from a specified time until now. otherwise it returns null
        public static DataTable ReturnTopProjects(DateTime date)
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string query = $"SELECT TOP 10 * " +
                    $"FROM(SELECT(Projects.ProjectRate / IIF(Projects.NumRateVoters = 0, 1, Projects.NumRateVoters)) AS total, *FROM Projects WHERE Projects.DateCreated > FORMAT(#{date}#, 'mm / dd / yyyy hh: nn: ss'))" +
                    $" AS newtbl " +
                    $"WHERE total <> 0 ORDER BY total DESC; ";
                DataTable dt = helper.GetDataTable(query);
                return dt;
            }
            catch
            {
                return null;
            }
        }
    }


}
