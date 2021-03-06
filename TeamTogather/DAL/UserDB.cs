﻿using System;
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
        /// the function query's the user's credentials by username and password from the database, if there is a match, it will return a DataRow of that user
        /// </summary>
        public static DataRow UserAuthentication(string UsNa, string pass)
        {
            try
            {

                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = "SELECT * FROM Users WHERE UserName = '" + UsNa + "' AND Pass = '" + pass + "';";
                DataTable userTable = helper.GetDataTable(sql);
                return userTable.Rows[0];
            }
            catch (Exception)
            {
                return null;
            }

        }
        /// <summary>
        /// this method takes an id of a user and returns his column in the table "users"
        /// </summary>
        /// <returns>a DataRow that contains the user credentials</returns>
        public static DataRow GetUserByID(int id)
        {
            try
            {

                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = "SELECT * FROM Users WHERE ID = " + id + ";";
                DataTable userTable = helper.GetDataTable(sql);
                return userTable.Rows[0];
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// Receives the ID of a user(after he logged in) and updates his loginDate on the database 
        /// </summary>
        public static void UpdateLoginDate(int ID)
        {
            DateTime dt = DateTime.Now;
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = "UPDATE Users SET [LogInDate] = #" + dt.ToString("MM/dd/yyyy HH:mm:ss") + "# WHERE ID = " + ID + ";";
            helper.WriteData(sql);
        }


        /// <summary>
        /// inserts a new user to db
        /// </summary>
        /// <returns> return true if the user has been added to the table, false otherwise </returns>
        public static int AddUser(string UserName, string Pass, string Email, DateTime Birthday, int NativeLang, int Country
                        , int WeeklyFreeTime, DateTime RegistrationDate
                        )
        {
            int IsAdded = -1;
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = "INSERT INTO Users(UserName, Pass, Email, Birthday, NativeLang, Country, WeeklyFreeTime, NumRateVoters, UserRate, IsBanned, RegistrationDate, UserType)" +
                         " VALUES( '" + UserName + "', '" + Pass + "', '" + Email + "', #" + Birthday.ToString("MM/dd/yyyy") + "#, " + NativeLang + ", " + Country + ", " + WeeklyFreeTime + ", " + 0 + ", " + 0 + ", " + "false" + ", "
                         + "#" + RegistrationDate.ToString("MM/dd/yyyy HH:mm:ss") + "#, " + 1 + ");";
            IsAdded = helper.InsertWithAutoNumKey(sql);
            return (IsAdded); // if one row was affected(added) the method return the id of the user(the insert was succesful) if not it will return -1
        }

        /// <summary>
        /// return true or false based on weather the user exist on the system, gets a string of the username
        /// </summary>
        public static bool UserExist(string usN)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = "SELECT UserName FROM Users WHERE UserName = '" + usN + "';";
            DataTable dt = helper.GetDataTable(sql);
            return dt.Rows.Count == 1;
        }


        /// <summary>
        /// add a full user into the database, with his chosen Programs and Professions.
        /// </summary>
        public static bool GeneralAddUser(string UserName, string Pass, string Email, DateTime Birthday, int NativeLang, int Country
                        , int WeeklyFreeTime, DateTime RegistrationDate, List<int> ProfessionList, List<int> ProgramsList
                        )
        {
            int userID = AddUser(UserName, Pass, Email, Birthday, NativeLang, Country, WeeklyFreeTime, RegistrationDate);
            if (userID == -1)
            {
                return false;
            }
            UserKnowledgeDB.InsertUserKnowledge(userID, ProgramsList);
            int countProfession = ProfessionDB.InsertUserProfessions(userID, ProfessionList);
            if (countProfession < 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// checkes if a user is an Admin returns true if he is, and false if he is not.
        /// </summary>
        public static bool CheckAdmin(int id)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = $"SELECT UserType FROM Users WHERE ID = {id} AND UserType = 2";
            DataTable ifAdmin = helper.GetDataTable(sql);
            if (ifAdmin.Rows.Count == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// gets the user professions (profession table is only providing the profname).
        /// </summary>
        public static DataTable GetUserProfessions(int id)
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = $"SELECT UserProf.ProfID, Professions.ProfName FROM UserProf " +
                    $"INNER JOIN Professions ON UserProf.ProfID = Professions.ProfessionID " +
                    $"WHERE UserProf.UserID = {id};";
                DataTable dt = helper.GetDataTable(sql);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// gets the user professions (profession table as a whole).
        /// </summary>
        public static DataTable GetUserProfessions2(int id)
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = $"SELECT Professions.* FROM UserProf " +
                    $"INNER JOIN Professions ON UserProf.ProfID = Professions.ProfessionID " +
                    $"WHERE UserProf.UserID = {id};";
                DataTable dt = helper.GetDataTable(sql);
                return dt;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// this method returns Users from the database, but it can filter them by
        /// 1. profession 2. age 3. the weekly free time they chose 4. their native language 5. their year of birth(their age)
        /// </summary>
        /// <param name="profID">default is -1</param>
        /// <param name="age">default year is 1940 in order to show all users(without date age filter)</param>
        /// <param name="langID">if unfiltered must be set to -1</param>
        /// <param name="WeeklyFreeTime">default is 0</param>
        /// <param name="userRate">default is 0</param>
        /// <param name="IndexUserID">should be updated in order to load more users from the table</param>
        /// <returns>a datatable that contains the user's credentials. indexUserID needs to be loaded to viewstate in order to load more users</returns>
        public static DataTable ShowUsers(int profID, DateTime age, int langID, int WeeklyFreeTime, int userRate,
                                          int IndexUserID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT DISTINCT TOP 10 Users.* " +
                $"FROM Users INNER JOIN UserProf ON Users.ID = UserProf.UserID " +
                $"WHERE Users.Birthday <= #{age.ToString("MM/dd/yyyy HH:mm:ss")}# " +
                $"AND Users.WeeklyFreeTime >= {WeeklyFreeTime} AND Users.ID > {IndexUserID} AND (Users.UserRate / IIF(Users.NumRateVoters = 0, 1, Users.NumRateVoters) >= {userRate})";
            if (langID != -1)
            {
                query += $" AND Users.NativeLang = {langID} ";
            }
            if (profID != -1)
            {
                query += $" AND UserProf.ProfID = {profID} ";
            }
            query += $"ORDER BY Users.ID; ";
            DataTable dt = helper.GetDataTable(query);
            return dt;
        }

        /// <summary>
        /// this method returns the user's Contacts in a datatable(by his ID)
        /// </summary>
        public static DataTable GetUserContacts(int userID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT UserContacts.*, ContactWebsites.WebSiteName AS ContactWebSite, ContactWebsites.SymbolPath " +
                $"FROM UserContacts INNER JOIN ContactWebSites ON ContactWebSites.WebsiteID = UserContacts.WebsiteID " +
                $"WHERE UserID = {userID} AND IsDeleted = 1;";
            DataTable dt = helper.GetDataTable(query);
            return dt;
        }

        /// <summary>
        /// this method sets the delete flag (value=2) on a contact.
        /// </summary>
        public static bool DeleteContact(int contactID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"UPDATE UserContacts SET IsDeleted = 2, DateDeleted = FORMAT(Now(), 'mm/dd/yyyy hh:nn:ss') " +
                $"WHERE UserContacts.[ID] = {contactID};";
            int affected = helper.WriteData(query);
            return affected != 0;
        }

        /// <summary>
        /// add a contact to the user by the user to websites, such as artstation, instagram, facebook and etc'.
        /// </summary>
        /// <param name="websiteName"></param>
        /// <param name="contactLink"></param>
        public static int AddContact(int userID, string websiteName, string contactLink, int websiteID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"INSERT INTO UserContacts ( UserID, WebSiteName, ContactLink, WebsiteID, IsDeleted) " +
                $"VALUES({userID}, '{websiteName}', '{contactLink}', {websiteID}, 1);";
            int ID = helper.InsertWithAutoNumKey(query);
            return ID;
        }


        /// <summary>
        /// this method returns the user's Creations in a datatable(by his ID)
        /// </summary>
        public static DataTable GetUserCreations(int userID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT ProtfolioCreations.* FROM ProtfolioCreations WHERE UserID = {userID} AND IsDeleted = 1;";
            DataTable dt = helper.GetDataTable(query);
            return dt;
        }

        /// <summary>
        /// this method sets the delete flag (value=2) on a Creation.
        /// </summary>
        public static bool DeleteCreation(int creationID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"UPDATE ProtfolioCreations SET IsDeleted = 2, DateDeleted = FORMAT(Now(), 'mm / dd / yyyy hh: nn: ss') " +
                $"WHERE ProtfolioCreations.[CreationID] = {creationID};";
            int affected = helper.WriteData(query);
            return affected != 0;
        }

        /// <summary>
        /// adds a protfolio creation to the table (provide path and user id)
        /// </summary>
        public static int AddProtfolioCreation(string creationPath, int userID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"INSERT INTO ProtfolioCreations ( CreationPath, UserID, IsDeleted ) VALUES('{creationPath}', {userID}, 1);";
            int ID = helper.InsertWithAutoNumKey(query);
            return ID;
        }


        /// <summary>
        /// gets the requests the user got from other users who want to be in his project, specify the user you wish to get the requests from and IndexDateTime
        /// </summary>
        public static DataTable GetProjectsUserRequest(int ProjectAdminID, DateTime projectDate)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT TOP 10 ProjectRequests.*, Projects.ProjectContent, ProjectPositions.Profession " +
                $"FROM(Projects INNER JOIN ProjectPositions ON Projects.ProjectID = ProjectPositions.ProjectID) " +
                $"INNER JOIN ProjectRequests ON ProjectPositions.ID = ProjectRequests.PositionID WHERE ProjectRequests.RequestStatus = 1 " +
                $" AND ProjectRequests.RequestType = 1 AND Projects.AdminUsID = {ProjectAdminID} AND ProjectRequests.DateRequested < FORMAT(#{projectDate}#, 'mm/dd/yyyy hh:nn:ss') " +
                $" ORDER BY ProjectRequests.DateRequested DESC;";
            DataTable dt = helper.GetDataTable(query);
            return dt;
        }

        /// <summary>
        /// gets the requests the user got from project's Admins, specify the user you wish to get the requests from and IndexDateTime
        /// </summary>
        public static DataTable GetUserRequestsToProjects(int UserID, DateTime projectDate)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT TOP 10 ProjectRequests.*, Projects.*, ProjectPositions.Profession " +
                $"FROM(Projects INNER JOIN ProjectPositions ON Projects.ProjectID = ProjectPositions.ProjectID) INNER JOIN ProjectRequests ON ProjectPositions.ID = ProjectRequests.PositionID " +
                $"WHERE ProjectRequests.RequestStatus = 1 AND ProjectRequests.RequestType = 2 AND ProjectRequests.UserID = {UserID} " +
                $"AND ProjectRequests.DateRequested < FORMAT(#{projectDate}#, 'mm/dd/yyyy hh:nn:ss') " +
                "ORDER BY ProjectRequests.DateRequested DESC;";
            DataTable dt = helper.GetDataTable(query);
            return dt;
        }

        /// <summary>
        /// gets all of the Projects which are finished - (has the status of 3) 
        /// </summary>
        public static DataTable GetUserDoneProjects(int AdminUserID, DateTime projectDate)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT TOP 10 Projects.* " +
                $"FROM Projects " +
                $"WHERE AdminUsID = {AdminUserID} AND ProjectStatus = 3 AND " +
                $"Projects.DateCreated < FORMAT(#{projectDate}#, 'mm/dd/yyyy hh:nn:ss') ORDER BY Projects.DateCreated DESC;";
            DataTable dt = helper.GetDataTable(query);
            return dt;
        }

        /// <summary>
        /// returns the user rate by his id.
        /// </summary>
        public static int ReturnUserRateAtUserNum(int UserRatedID, int userID)
        {
            DataRow userRow = ReturnUserRateAtUser(UserRatedID, userID);
            if (userRow != null)
            {
                return (int)userRow["Rate"];
            }
            return 0;
        }

        /// <summary>
        /// rates a user by who is rating him, who is rated(second argument) and the rate.
        /// </summary>
        public static bool RateUser(int UserRatedID, int userID, int UserRate)
        {
            DataRow userRow = ReturnUserRateAtUser(UserRatedID, userID);
            if (userRow == null)
            {

                InsertUserRate_Rec(UserRatedID, userID, UserRate);
                int rate = ReturnUserRate(userID);
                rate = rate + UserRate;
                return updateUserRate(userID, rate, true);
            }
            else
            {
                int rate = ReturnUserRate(userID);
                int PreviousRatedNum = (int)userRow["Rate"];
                rate = (UserRate - PreviousRatedNum) + rate;
                UpdatePersonalUserRate(UserRatedID, userID, UserRate);
                bool success = updateUserRate(userID, rate, false);
                return success;
            }
        }


        /// <summary>
        /// this method inserts a user rate record into the UserRate db table.
        /// user rates are from 1 to 5.
        /// </summary>
        public static bool UpdatePersonalUserRate(int UserRatedID, int userID, int UserRate)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"UPDATE UserRate SET UserRate.Rate = {UserRate} WHERE UserRatedID = {UserRatedID} AND UserID = {userID};";
            int exe_status = helper.InsertWithAutoNumKey(query);
            return exe_status != -1;
        }

        /// <summary>
        /// inserts a new rate record to the user rates table.
        /// </summary>
        public static bool InsertUserRate_Rec(int UserRatedID, int userID, int UserRate)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"INSERT INTO UserRate ( UserRatedID, UserID, Rate ) VALUES({UserRatedID}, {userID}, {UserRate});";
            int exe_status = helper.InsertWithAutoNumKey(query);
            return exe_status != -1;
        }

        /// <summary>
        /// returns the user rate vote on a specific user.
        /// </summary>
        public static DataRow ReturnUserRateAtUser(int UserRated, int userID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT * FROM UserRate WHERE UserRate.UserRatedID = {UserRated} AND UserRate.UserID = {userID};";
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

        public static string ReturnUserProfilePath(int userID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query = $"SELECT ProfilePath FROM Users WHERE ID = {userID};";
            DataTable dt = helper.GetDataTable(query);
            if (dt.Rows[0]["ProfilePath"] is DBNull)
            {
                return "";
            }
            return (string)dt.Rows[0]["ProfilePath"];
        }


        /// <summary>
        /// updates the users's rate, rate should be sum + new rate, if it is a new rate which wasn't rated before newRate bool need to be set to 'true'
        /// </summary>
        public static bool updateUserRate(int UserID, int rate, bool newRate)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string query;
            if (newRate)
            {
                query = $"UPDATE Users SET NumRateVoters = NumRateVoters + 1, UserRate = {rate} WHERE Users.ID = {UserID};";
            }
            else
            {
                query = $"UPDATE Users SET NumRateVoters = NumRateVoters, UserRate = {rate} WHERE Users.ID = {UserID};";
            }
            int updateSucceeded = helper.WriteData(query);
            return updateSucceeded != -1;
        }

        /// <summary>
        /// returns userrate by his id.
        /// </summary>
        public static int ReturnUserRate(int UserID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = $"SELECT Users.UserRate FROM Users WHERE ID = {UserID};";
            DataTable dt = helper.GetDataTable(sql);
            DataRow user = dt.Rows[0];
            int rate = 0;
            if (user != null)
            {
                rate = (int)user["UserRate"];
                return rate;
            }
            else
            {
                return rate;
            }
        }

        /// <summary>
        /// returnes Language name by its ID.
        /// </summary>
        public static string ReturnLangByID(int langID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = $"SELECT Languages.LangName FROM Languages WHERE ID = {langID};";
            DataTable dt = helper.GetDataTable(sql);
            DataRow user;
            string lang = "";
            try
            {
                user = dt.Rows[0];
            }
            catch
            {
                user = null;
            }
            if (user != null)
            {
                lang = (string)user["LangName"];
                return lang;
            }
            else
            {
                return lang;
            }
        }


        /// <summary>
        /// updates user's profile photo by a path an his id.
        /// </summary>
        public static void UpdateProfilePhoto(string path, int id)
        {
            DateTime dt = DateTime.Now;
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = $"UPDATE Users SET [ProfilePath] = '{path}' WHERE ID = {id};";
            helper.WriteData(sql);
        }

        /// <summary>
        /// retrieves the status of the requests the user has sent to the admin of a project. the date provided sets how old does the requests returned will be
        /// (exmple. 3 weeks ago will get all of the requests sent during the last 3 weeks)
        /// </summary>
        public static DataTable returnSentUserReq(int userID, DateTime ReqFromDate)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = $"SELECT DISTINCT ProjectPositions.ID, Projects.AdminUsID, Projects.ProjectID, Projects.ProjectContent, ProjectPositions.Profession, ProjectRequests.* " +
                $"FROM(ProjectRequests INNER JOIN ProjectPositions ON ProjectRequests.PositionID = ProjectPositions.ID) INNER JOIN Projects ON ProjectPositions.ProjectID = Projects.ProjectID " +
                $"WHERE ProjectRequests.UserID = {userID} AND ProjectPositions.IsDeleted = 1 AND ProjectRequests.RequestType = 1 AND ProjectRequests.DateRequested > FORMAT(#{ReqFromDate}#, 'mm / dd / yyyy hh: nn: ss') ORDER BY ProjectRequests.DateRequested DESC;";
            DataTable dt = helper.GetDataTable(sql);
            return dt;
        }

        /// <summary>
        /// retrieves the status of the invitation the user has sent to the other users as the Project's Admin. the date provided sets how old does the requests returned will be
        /// (exmple. 3 weeks ago will get all of the requests sent during the last 3 weeks)
        /// </summary>
        public static DataTable returnSentUserInv(int userID, DateTime ReqFromDate)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = $"SELECT DISTINCT ProjectPositions.ID, Projects.AdminUsID, Projects.ProjectID, Projects.ProjectContent, ProjectPositions.Profession, ProjectRequests.* FROM (Projects INNER JOIN ProjectPositions  " +
                $"ON Projects.ProjectID = ProjectPositions.ProjectID) INNER JOIN ProjectRequests ON ProjectPositions.ID = ProjectRequests.PositionID WHERE Projects.AdminUsID = {userID} AND ProjectRequests.RequestType = 2 " +
                $"AND ProjectPositions.IsDeleted <> 2 AND ProjectRequests.DateRequested > FORMAT(#{ReqFromDate}#, 'mm / dd / yyyy hh: nn: ss') ORDER BY ProjectRequests.DateRequested DESC;";
            DataTable dt = helper.GetDataTable(sql);
            return dt;
        }


        /// <summary>
        /// the function query's users credentials by username and email from the database, if there is a match, it will return a DataTable of all users matched
        /// </summary>
        public static DataTable RetrieveUsersByCredentials(string UsNa, string email)
        {
            try
            {

                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                //string sql = $"SELECT * FROM Users WHERE UserName LIKE '{UsNa}*' AND Email LIKE '{email}*' ORDER BY (ID);";
                string sql = $"SELECT * FROM Users WHERE UserName LIKE '{UsNa}%' AND Email LIKE '{email}%' ORDER BY (ID);"; 
                DataTable userTable = helper.GetDataTable(sql);
                return userTable;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// gets a user by his username and email.
        /// </summary>
        public static DataTable RetrieveUserTableByCredentials(string UsNa, string email, string country, string lang)
        {
            try
            {

                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = $"SELECT Users.ID, Users.UserName, Users.Pass, Users.Email, Users.Birthday, Languages.LangName, " +
                    $"Countries.Name AS Country, Users.WeeklyFreeTime, Users.NumRateVoters, Users.UserRate, Users.IsBanned, Users.RegistrationDate, Users.LogInDate, Users.UserType " +
                    $"FROM((Users " +
                    $"INNER JOIN Languages ON Users.NativeLang = Languages.ID) " +
                    $"INNER JOIN Countries ON Users.Country = Countries.ID) " +
                    $"WHERE UserName LIKE '{UsNa}%' AND Email LIKE '{email}%' AND Countries.Name LIKE '{country}%' AND Languages.LangName LIKE '{lang}%' ORDER BY(Users.ID); ";
                DataTable userTable = helper.GetDataTable(sql);
                return userTable;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
