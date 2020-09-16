using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace TeamTogatherWebUI
{
    public partial class Registration : System.Web.UI.Page
    {
        // the current registration div id
        static int DivID = 1;
        private string username = "";
        private string password = "";
        private string Email = "";
        private DateTime Birthday;
        private int Language;
        private int Country;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void next_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                if (DivID == 1)
                {
                    if (PassReg.Text == ConfiPassReg.Text)
                    {
                        username = UserNameReg.Text;
                        password = PassReg.Text;
                        Email = EmailAddressReg.Text;
                        registrationP1.Visible = false;
                        registrationP2.Visible = true;
                        DivID++;
                        Dictionary<int, string> langdic = GeneralMethods.GetLang();
                        BindDropDown(langDropDown, langdic);
                        Dictionary<int, string> countrydic = GeneralMethods.GetCountries();
                        BindDropDown(CountryDropDown, countrydic);
                        Dictionary<int, int> dic = GetDays();
                        BindDropDown(DropDownDay, dic);
                        dic = GetMonth();
                        BindDropDown(DropDownMonth, dic);
                        dic = GetYear();
                        BindDropDown(DropDownYear, dic);
                    }

                }
                else if (DivID == 2)
                {
                    int year = int.Parse(DropDownYear.SelectedValue);
                    int month = int.Parse(DropDownMonth.SelectedValue);
                    int day = int.Parse(DropDownDay.SelectedValue);
                    Birthday = new DateTime(year, month, day);
                    Language = int.Parse(langDropDown.SelectedValue);
                    Country = int.Parse(CountryDropDown.SelectedValue);
                    registrationP2.Visible = false;
                    registrationP3.Visible = true;
                    DivID++;
                }
                else if (DivID == 3)
                {
                    registrationP3.Visible = false;
                    registrationP4.Visible = true;
                    DivID++;
                    next.Visible = false;
                }

            }
        }
        public static void BindDropDown(DropDownList list, Dictionary<int, string> dic)
        {
            list.DataSource = dic;
            list.DataTextField = "Value";
            list.DataValueField = "Key";
            list.DataBind();
        }

        public static void BindDropDown(DropDownList list, Dictionary<int, int> dic)
        {
            list.DataSource = dic;
            list.DataTextField = "Value";
            list.DataValueField = "Key";
            list.DataBind();
        }

        public static Dictionary<int, int> GetDays()
        {
            const int DAYS = 31;
            Dictionary<int, int> list = new Dictionary<int, int>();
            for (int i = 0; i < DAYS; i++)
            {
                list.Add(i + 1, i + 1);
            }
            return list;
        }

        public static Dictionary<int, int> GetMonth()
        {
            const int MONTH = 12;
            Dictionary<int, int> list = new Dictionary<int, int>();
            for (int i = 0; i < MONTH; i++)
            {
                list.Add(i + 1, i + 1);
            }
            return list;
        }

        public static Dictionary<int, int> GetYear()
        {
            int year = DateTime.Now.Year;
            Dictionary<int, int> list = new Dictionary<int, int>();
            for (int i = year; i > 1940; i--)
            {
                list.Add(i, i);
            }
            return list;
        }
    }
}
