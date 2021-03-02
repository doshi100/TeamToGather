using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL
{
    public class GeneralDB
    {
        /// <summary>
        /// return the language db table as a dataTable
        /// </summary>
        public static DataTable Returnlanguages()
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = "SELECT ID, LangName FROM Languages";
            DataTable dtLang = helper.GetDataTable(sql);
            return dtLang;
        }


        public static DataTable ReturnCountries()
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = "SELECT ID, Name FROM Countries";
                DataTable dtcountry = helper.GetDataTable(sql);
                return dtcountry;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable ReturnContacts()
        {
            try
            {
                DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
                string sql = "SELECT * FROM ContactWebsites";
                DataTable dtContacts = helper.GetDataTable(sql);
                return dtContacts;
            }
            catch
            {
                return null;
            }
        }
    }
}
