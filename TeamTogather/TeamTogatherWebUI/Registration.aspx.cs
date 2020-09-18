using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
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
            bool IsPageRefresh = false;
            if (!IsPostBack)
            {
                ViewState["postids"] = System.Guid.NewGuid().ToString();
                Response.Write(ViewState["postids"].ToString());
                Session["postid"] = ViewState["postids"].ToString();
            }
            else
            {
                Response.Write("</br>" + ViewState["postids"].ToString());
                if (ViewState["postids"].ToString() != Session["postid"].ToString())
                {
                    IsPageRefresh = true;
                }
                Session["postid"] = System.Guid.NewGuid().ToString();
                ViewState["postids"] = Session["postid"].ToString();
                Response.Write("</br>" + ViewState["postids"].ToString());
            }
        }

        protected void next_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (DivID == 1)
                {
                    if (PassReg.Text == ConfiPassReg.Text)
                    {
                        // save the username, password and email the user passed to the system
                        username = UserNameReg.Text;
                        password = PassReg.Text;
                        Email = EmailAddressReg.Text;
                        // change from part 1 of the registration to part 2
                        registrationP1.Visible = false;
                        registrationP2.Visible = true;
                        // -------------------------------
                        DivID++; // increment the divID to identify that the user moved to part 2
                        // set the language and countries dropdown menus
                        Dictionary<int, string> langdic = GeneralMethods.GetLang();
                        BindDropDown(langDropDown, langdic);
                        Dictionary<int, string> countrydic = GeneralMethods.GetCountries();
                        BindDropDown(CountryDropDown, countrydic);
                        // ------------------------------------------
                        // sets the Birthday selection dropdowns.
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
                    // save the Birthday Date, Language and country of the user.
                    int year = int.Parse(DropDownYear.SelectedValue);
                    int month = int.Parse(DropDownMonth.SelectedValue);
                    int day = int.Parse(DropDownDay.SelectedValue);
                    Birthday = new DateTime(year, month, day);
                    Language = int.Parse(langDropDown.SelectedValue);
                    Country = int.Parse(CountryDropDown.SelectedValue);
                    // ---------------------------------------------
                    // change from part 2 of the registration to part 3
                    registrationP2.Visible = false;
                    BindProfessions(registrationP3, Page);
                    registrationP3.Visible = true;
                    // increment the divID to identify that the user moved to part 3
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

        protected void UserExist_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (UserInfo.UserExist(UserNameReg.Text))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        public static void BindProfessions(HtmlControl ctrl, Page thispage)
        {
            List<Profession> Plist = Profession.GetProfessionList();
            foreach (Profession p in Plist)
            {
                ProfUsControl userprofession = (ProfUsControl)thispage.LoadControl("~/ProfUsControl.ascx");
                userprofession.imgP = p.ProfPath;
                userprofession.profName = p.ProfName;
                userprofession.profID = p.ProfessionID;
                ctrl.Controls.Add(userprofession);
            }
        }
    }
}
