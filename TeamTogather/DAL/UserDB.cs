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
            catch(Exception)
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
        public static bool AddUser(string UserName, string Pass, string Email, DateTime Birthday, int NativeLang, int Country
                        , int Profession, int WeeklyFreeTime, DateTime RegistrationDate
                        )
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = "INSERT INTO Users(UserName, Pass, Email, Birthday, NativeLang, Country, Profession, WeeklyFreeTime, NumRateVoters, UserRate, IsBanned, ProjectsSum, RegistrationDate, Type)" +
                         " VALUES( '" + UserName + "', '" + Pass + "', '" + Email + "', #" + Birthday.ToString("MM/dd/yyyy") + "#, " + NativeLang + ", " + Country + ", " + Profession + ", " + WeeklyFreeTime + ", " + 0 + ", " + 0 + ", " + "false" + ", " + 0
                         + ", #" + RegistrationDate.ToString("MM/dd/yyyy HH:mm:ss") + "#, " + 1+ ");";
            int IsAdded = helper.WriteData(sql);
            return (IsAdded == 1); // if one row was affected(added) it return true(the insert was succesful) if not it will return false
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
    }
}
