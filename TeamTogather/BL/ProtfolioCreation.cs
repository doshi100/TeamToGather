using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;

namespace BL
{
    public class ProtfolioCreation
    {
        public int CreationID { get; set; }
        public string CreationPath { get; set; }
        public int UserID { get; set; }

        /// <summary>
        /// no. 1 empty constructor
        /// </summary>
        public ProtfolioCreation()
        {

        }

        /// <summary>
        /// no. 2 builds a user Protfolio Creation object by a DataRow
        /// </summary>
        public ProtfolioCreation(DataRow UserContact)
        {
            this.CreationID = (int)UserContact["CreationID"];
            this.CreationPath = (string)UserContact["CreationPath"];
            this.UserID = (int)UserContact["UserID"];
        }

        /// <summary>
        /// adds a protfolio creation by path and userID.
        /// </summary>
        public static int AddProtfolioCreation(string creationPath, int userID)
        {
            return UserDB.AddProtfolioCreation(creationPath, userID);
        }

        /// <summary>
        /// deletes a creation by its ID (signs in the table that it is deleted by marking '2')
        /// </summary>
        public static bool DeleteCreation(int creationID)
        {
            return UserDB.DeleteCreation(creationID);
        }


        /// <summary>
        /// gets all of the protfolio creation of the user. (a list of objects)
        /// </summary>
        public static List<ProtfolioCreation> GetUserCreations(int userID)
        {
            DataTable protfoliodt = UserDB.GetUserCreations(userID);
            List<ProtfolioCreation> protfolioList = new List<ProtfolioCreation>();
            foreach (DataRow row in protfoliodt.Rows)
            {
                ProtfolioCreation creation = new ProtfolioCreation(row);
                protfolioList.Add(creation);
            }
            return protfolioList;
        }
    }
}
