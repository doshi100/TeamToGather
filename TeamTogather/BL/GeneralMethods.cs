using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;

namespace BL
{
    public class GeneralMethods
    {
        /// <summary>
        /// sets the DBpath for the DB connection string
        /// </summary>
        static public void SetDBPath(string path)
        {
            Constants c = new Constants(path);

        }


        /// <summary>
        /// Creates DataTime object from a specified year, day and month, used to set Birthday dates and etc'
        /// </summary>
        
        static public DateTime CreateDateTime(int year, int month, int day)
        {
            DateTime dt = new DateTime(year, month, day);
            Console.WriteLine(dt);
            return dt;
        }

        /// <summary>
        /// the method constructs a dictionary from a language DataTable, id - lang,  langname - name of language
        /// </summary>

        public static Dictionary<int, string> GetLang()
        {
            DataTable Langdt = GeneralDB.Returnlanguages();
            if (Langdt == null)
            {
                return null;
            }
            Dictionary<int, string> langDic = new Dictionary<int, string>();
            foreach(DataRow row in Langdt.Rows)
            {
                int langid = (int)row["ID"];
                string LangName = (string)row["LangName"].ToString();
                langDic.Add(langid, LangName);
            }
            return langDic;
        }


        /// <summary>
        /// the method constructs a dictionary from a Countries DataTable, id - lang,  counryname - name of language
        /// </summary>
        public static Dictionary<int, string> GetCountries()
        {
            DataTable countriesdt = GeneralDB.ReturnCountries();
            if (countriesdt == null)
            {
                return null;
            }
            Dictionary<int, string> CountryDic = new Dictionary<int, string>();
            foreach (DataRow row in countriesdt.Rows)
            {
                int countryid = (int)row["ID"];
                string CountryName = (string)row["Name"].ToString();
                CountryDic.Add(countryid, CountryName);
            }
            return CountryDic;
        }


    }
}
