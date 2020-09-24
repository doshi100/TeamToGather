using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BL
{
    public class UserKnowledge:Knowledge
    {
        // ** this Class contains the fields of both ProgKnowledge and Programs tables. (it's a merge between the both)
        // the start of the properties *************************************************************************
        public int PKID { get; set; } // The key ID of the ProgKnowledge table 
        // father propertie programID the ID of the program/skill the user knows
        // father propertie PName the name of the program/skill the user knows
        // father propertie ProgPath the path to the photo of this program/skill (example: knows photoshop ---> path to a picture of the logo of Photoshop)
        // ********end of properties*****************

        /// <summary>
        /// constructor no. 1, it contains all of the UserKnowledge properties
        /// </summary>
        public UserKnowledge(int PKID, int programID, string PName, string ProgPath):base(programID,PName,ProgPath)
        {
            this.PKID = PKID;
        }


        /// <summary>
        /// the method builds a list of userKnowLedge object's by an id of a user given to it.
        /// * the method return the list after it has filled it by all matching skills in the db
        /// </summary>
        public static List<UserKnowledge> GetUserKnowledgeBL(int id)
        {
            try
            {
                List<UserKnowledge> SkillList = new List<UserKnowledge>();
                DataTable SkillTable = UserKnowledgeDB.GetUserKnowledge(id);
                foreach (DataRow USskill in SkillTable.Rows)
                {
                    UserKnowledge UserKnow = new UserKnowledge((int)USskill["PKID"], (int)USskill["ProgramID"], (string)USskill["PName"].ToString(),
                                                                 (string)USskill["ProgPath"].ToString());
                    SkillList.Add(UserKnow);
                }
                return SkillList;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        /// <summary>
        /// the function gets an ID and a list of chosen Knowledges/skills
        /// and inserts the knowledges to the user 
        /// on ProgKnowledge database table.
        /// </summary>
        public static int KnowledgeInsert(int userid, List<int> userKnowledgeList)
        {
            return UserKnowledgeDB.InsertUserKnowledge(userid, userKnowledgeList);
        }



    }
}
