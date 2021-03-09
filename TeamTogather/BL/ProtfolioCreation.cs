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

        public static int AddProtfolioCreation(string creationPath, int userID)
        {
            return UserDB.AddProtfolioCreation(creationPath, userID);
        }

        public static bool DeleteCreation(int creationID)
        {
            return UserDB.DeleteCreation(creationID);
        }

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
