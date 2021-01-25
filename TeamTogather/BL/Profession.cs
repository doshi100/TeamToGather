using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BL
{
    public class Profession
    {
        public int ProfessionID { get; } // ID of the particular profession in the db
        public string ProfName { get; } // the profession name in db
        public string ProfPath { get; } // the path to the photo of the profession

        /// <summary>
        /// constructor no. 1 takes the properties and builds an profession object
        /// </summary>
        public Profession (int ProfessionID, string ProfName, string ProfPath)
        {
            this.ProfessionID = ProfessionID;
            this.ProfName = ProfName;
            this.ProfPath = ProfPath;
        }

        public Profession (int profID)
        {
            DataRow prof = ProfessionDB.RetProfession(profID);
            if(prof != null)
            {
                this.ProfessionID = (int)prof["ProfessionID"];
                this.ProfName = (string)prof["ProfName"].ToString();
                this.ProfPath = (string)prof["ProfPath"].ToString();
            }
        }

        /// <summary>
        /// the method builds a profession list from a datatable(the table in the db) and returns it
        /// </summary>
        public static List<Profession> GetProfessionList()
        {

            DataTable profTable = ProfessionDB.RetProfessionTable();
            List<Profession> profList = new List<Profession>();
            if (profTable == null)
            {
                return null;
            }
            foreach (DataRow row in profTable.Rows)
            {
                Profession obj = new Profession((int)row["ProfessionID"], (string)row["ProfName"].ToString(), (string)row["ProfPath"].ToString());
                profList.Add(obj);
            }
            return profList;
        }
    }
}
