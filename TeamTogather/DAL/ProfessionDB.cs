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


        /// <summary>
        /// the function gets an ID and a list of chosen Profession
        /// and inserts the Profession to the user 
        /// on UserProf database table.
        /// returns the number of rows affected by it.
        /// IF the method FAILS it will return -1
        /// </summary>
        public static int InsertUserProfessions(int userid, List<int> professionValues)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = Build_queryProfession(userid, professionValues);
            int outcome = -1;
            outcome = helper.WriteData(sql);
            return outcome;
        }



        /// <summary>
        /// the function gets an ID of a user and a list of chosen Profession/skills
        /// and builds a query for the db to insert all of them
        /// to the UserProf database table.
        /// </summary>
        public static string Build_queryProfession(int userid, List<int> userprofessionList)
        {
            if (userprofessionList == null)
            {
                return "";
            }
            else
            {
                string sql = $@"INSERT INTO UserProf ( UserID, ProfID ) " +
                          @"SELECT UserID, ProfID " +
                          "FROM (";
                for (int i = 0; i < userprofessionList.Count; i++)
                {
                    sql += $@"SELECT Users.ID AS UserID, Professions.ProfessionID AS ProfID FROM Users, Professions " +
                            $@"WHERE Professions.ProfessionID = {userprofessionList[i]} AND Users.ID = {userid} ";
                    if (i + 1 < userprofessionList.Count) // check if this is not the last run, because the query doesn't need to end with the UNION statement
                    {
                        sql += $@"UNION ALL ";
                    }
                }
                sql += @") AS [add]";
                return sql;
            }
        }
    }
}

