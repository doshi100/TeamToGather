using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL
{
    public class UserKnowledgeDB
    {
        /// <summary>
        /// the function gets an ID of a user and return his knowledge
        /// * The sql line that excutes merges the 3 tables *Users, ProgKnowledge and Programs* and returns a table -->
        ///   with the user's ID,PKID, ProgramIDs and the program/skill Name and photopath on the server.
        /// </summary>
        public static DataTable GetUserKnowledge(int ID)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = "SELECT Users.ID, ProgKnowledge.PKID, ProgKnowledge.ProgramID, Programs.PName, Programs.ProgPath FROM " +
                        "((Users INNER JOIN ProgKnowledge " +
                        "ON Users.ID = ProgKnowledge.UserID) " +
                        "INNER JOIN Programs ON ProgKnowledge.ProgramID = Programs.ProgramID) " +
                        "WHERE Users.ID= " + ID +";";
            DataTable dt = helper.GetDataTable(sql);
            return dt;
        }


        /// <summary>
        /// the function gets an ID and a list of chosen Knowledges/skills
        /// and inserts the knowledges to the user 
        /// on ProgKnowledge database table.
        /// returns the number of rows affected by it.
        /// IF the method FAILS it will return -1
        /// </summary>
        public static int InsertUserKnowledge(int userid, List<int> userKnowledgeList)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = Build_queryKnowledge(userid, userKnowledgeList);
            int outcome = -1;
            outcome = helper.WriteData(sql);
            return outcome;
        }

        /// <summary>
        /// the function gets an ID of a user and a list of chosen Knowledges/skills
        /// and builds a query for the db to insert all of them
        /// to the ProgKnowledge database table.
        /// </summary>
        public static string Build_queryKnowledge(int userid, List<int> userKnowledgeList)
        {
            if(userKnowledgeList == null)
            {
                return "";
            }
            else
            {
                string sql = $@"INSERT INTO ProgKnowledge ( UserID, ProgramID ) " +
                          @"SELECT UserID, ProgramID " +
                          "FROM (";
                for (int i = 0; i < userKnowledgeList.Count; i++)
                {
                    sql += $@"SELECT Users.ID AS UserID, Programs.ProgramID FROM Users, Programs " +
                            $@"WHERE Programs.ProgramID = {userKnowledgeList[i]} AND Users.ID = {userid} ";
                    if (i + 1 < userKnowledgeList.Count) // check if this is not the last run, because the query doesn't need to end with the UNION statement
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
