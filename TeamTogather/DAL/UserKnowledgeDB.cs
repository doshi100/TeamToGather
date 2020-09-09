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
    }
}
