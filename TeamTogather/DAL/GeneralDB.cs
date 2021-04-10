using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL
{
    public class GeneralDB
    {
        /// <summary>
        /// return the language db table as a dataTable
        /// </summary>
        public static DataTable Returnlanguages()
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = "SELECT ID, LangName FROM Languages";
            DataTable dtLang = helper.GetDataTable(sql);
            return dtLang;
        }

        /// <summary>
        /// returnes all of the countries form the Countrys DB
        /// </summary>
        public static DataTable ReturnCountries()
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = "SELECT ID, Name FROM Countries";
                DataTable dtcountry = helper.GetDataTable(sql);
                return dtcountry;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// retrieves all of the contact websites there is on the DB
        /// </summary>
        public static DataTable ReturnContacts()
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = "SELECT * FROM ContactWebsites";
                DataTable dtContacts = helper.GetDataTable(sql);
                return dtContacts;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// retrieves the most requested profession (ID of it) between a specified time and now.
        /// if the table in null, it will retrieve -1
        /// </summary>
        public static int ReturnMostRequestedProfession(DateTime date)
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = $"SELECT TOP 1 * FROM(SELECT COUNT(ProjectRequests.RequestID) AS NumRequests, ProjectPositions.Profession " +
                    $"FROM(ProjectRequests INNER JOIN ProjectPositions ON ProjectRequests.PositionID = ProjectPositions.ID) INNER JOIN Professions ON ProjectPositions.Profession = Professions.ProfessionID" +
                    $" WHERE ProjectRequests.DateRequested > FORMAT(#{date}#, 'mm / dd / yyyy hh: nn: ss') GROUP BY (ProjectPositions.Profession))  AS newtbl " +
                    $"ORDER BY NumRequests DESC;";
                DataTable dtProfessions = helper.GetDataTable(sql);
                return (int)dtProfessions.Rows[0]["Profession"];
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// returns the number of users logged from a specific time, returns -1 if no users were matched logging.
        /// </summary>
        public static int ReturnLoggedUsers(DateTime date)
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = $"SELECT COUNT(ID) AS LoggedIn FROM Users WHERE Users.LogInDate > FORMAT(#{date}#, 'mm / dd / yyyy hh: nn: ss');";
                DataTable dt = helper.GetDataTable(sql);
                return (int)dt.Rows[0]["LoggedIn"];
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// bans user by his ID
        /// </summary>
        public static void BanUser(int id)
        {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = $"UPDATE Users SET IsBanned = true WHERE [ID] = {id}";
                helper.WriteData(sql);    
        }

        /// <summary>
        /// un bans user by his ID
        /// </summary>
        public static void UnBanUser(int id)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = $"UPDATE Users SET IsBanned = false WHERE [ID] = {id}";
            helper.WriteData(sql);
        }
    }
}

