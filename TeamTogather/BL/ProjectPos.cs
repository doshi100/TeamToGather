using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
namespace BL
{
    public class ProjectPos
    {
        public int id { get; set; }
        public int userID { get; set; }
        public Profession profession { get; set; }
        public List<Knowledge> Programs { get; set; }

        public ProjectPos(int id, int userID, int profession)
        {
            this.id = id;
            this.profession = new Profession(profession);
            this.userID = userID;
            DataTable programsTable = ProjectDB.GetProgramsAtPosition(id);
            if (programsTable != null)
            {
                List<Knowledge> Programs = new List<Knowledge>();
                foreach(DataRow row in programsTable.Rows)
                {
                    Knowledge program = new Knowledge();
                    program.ProgPath = (string)row["ProgPath"];
                    Programs.Add(program);
                }
                this.Programs = Programs;
            }
        }

        public ProjectPos(int id)
        {
            DataRow posFields = ProjectDB.GetPosition(id);
            this.id = id;
            this.userID = (int)posFields["UserID"];
            Profession pos_prof = new Profession((int)posFields["Profession"], "none", "none");
            this.profession = pos_prof;
        }

        public bool CheckFilled()
        {
            return (this.userID == 1);
        }

        /// <summary>
        /// checks if this position has been requested already by a user, returns true if it is, and false if it isn't
        /// </summary>
        public bool CheckUserAtProjectPos(int userID)
        {
            return ProjectDB.CheckUserAtProjectPos(this.id, userID);
        }

        

    }
}
