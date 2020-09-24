using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL
{
    public class KnowledgeDB
    {
        /// <summary>
        /// the method returns the db table "Programs" as a dataTable
        /// </summary>
        
        static public DataTable GetProgramsTable()
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = "SELECT * FROM Programs;";
                DataTable proSkillsDT = helper.GetDataTable(sql);
                return proSkillsDT;
            }
            catch(Exception)
            {
                return null;
            }
        }

    }
}
