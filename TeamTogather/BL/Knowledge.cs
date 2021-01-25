using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BL
{
    public class Knowledge
    {
        public int ProgramID { get; set; } // the ID of the program/skill the user knows
        public string PName { get; set; } // the name of the program/skill the user knows
        public string ProgPath { get; set; } // the path to the photo of this program/skill (example: knows photoshop ---> path to a picture of the logo of Photoshop)
         


        /// <summary>
        /// constructor no.1 builds a knowledge object from all of the properties.
        /// </summary>
        public Knowledge(int ProgramID, string PName, string ProgPath)
        {
            this.ProgramID = ProgramID;
            this.PName = PName;
            this.ProgPath = ProgPath;
        }

        /// <summary>
        /// constructor no.2 builds an empty knowledge object
        /// </summary>
        public Knowledge()
        {
        }


        /// <summary>
        /// returns a list of Knowledge objects that contains the db "programs"
        /// </summary>
        public static List<Knowledge> RetKnowledgeList()
        {
            DataTable knowledgeTable = KnowledgeDB.GetProgramsTable();
            List<Knowledge> knowledgeList = new List<Knowledge>();
            if (knowledgeTable == null)
            {
                return null;
            }
            foreach (DataRow row in knowledgeTable.Rows)
            {
                Knowledge obj = new Knowledge((int)row["ProgramID"], (string)row["PName"].ToString(), (string)row["ProgPath"].ToString());
                knowledgeList.Add(obj);
            }
            return knowledgeList;
        }
    }
}
