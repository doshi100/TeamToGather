using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using BL;

namespace TeamTogatherWebUI
{
    public partial class Registration : System.Web.UI.Page
    {
        private bool IsPageRefresh;
        private int Selection { get; set; }
        private string username = "";
        private string password = "";
        private string Email = "";
        private bool CredentialsFlag = true;
        private DateTime Birthday;
        private int Language;
        private int Country;


        protected void Page_Load(object sender, EventArgs e)
        {
            IsPageRefresh = false;
            if (!IsPostBack)
            {
                ViewState["DivID"] = 1;
                ViewState["postids"] = System.Guid.NewGuid().ToString();
                Session["postid"] = ViewState["postids"].ToString();
            }
            else
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString())
                {
                    IsPageRefresh = true;
                }
                Session["postid"] = System.Guid.NewGuid().ToString();
                ViewState["postids"] = Session["postid"].ToString();
            }
            if (int.Parse(ViewState["DivID"].ToString()) == 3)
            {
                BindProfessions(radios, Page);
            }

        }

        // if the next button is clicked, make the necessary changes.
        protected void next_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) // checkes if the page is valid to proceed or to start the form.
            {
                if (IsPageRefresh)
                {
                    ViewState["DivID"] = int.Parse(ViewState["DivID"].ToString()) - 1; // if the page was refreshed, make sure to keep the user at the same stage.
                }

                if (int.Parse(ViewState["DivID"].ToString()) == 1)
                {
                    if (PassReg.Text == ConfiPassReg.Text)
                    {
                        // save the username, password and email the user passed to the system
                        ViewState["username"] = UserNameReg.Text;
                        ViewState["password"] = PassReg.Text;
                        ViewState["Email"] = EmailAddressReg.Text;
                        // -------------------------------
                        // change from part 1 of the registration to part 2
                        registrationP1.Visible = false;
                        registrationP2.Visible = true;
                        // -------------------------------
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
                    else
                    {
                        CredentialsFlag = true;
                    }

                }
                else if (int.Parse(ViewState["DivID"].ToString()) == 2)
                {
                    // save the Birthday Date, Language and country of the user.
                    ViewState["year"] = int.Parse(DropDownYear.SelectedValue);
                    ViewState["month"] = int.Parse(DropDownMonth.SelectedValue);
                    ViewState["day"] = int.Parse(DropDownDay.SelectedValue);
                    ViewState["Language"] = int.Parse(langDropDown.SelectedValue);
                    ViewState["Country"] = int.Parse(CountryDropDown.SelectedValue);
                    // ---------------------------------------------
                    // change from part 2 of the registration to part 3
                    registrationP2.Visible = false;
                    BindProfessions(radios, Page);
                    registrationP3.Visible = true;
                    radios.Visible = true;
                }
                else if (int.Parse(ViewState["DivID"].ToString()) == 3)
                {
                    // change from part 3 of the registration to part 4
                    ViewState["Profid"] = CheckRadio(radios);
                    registrationP3.Visible = false;
                    registrationP4.Visible = true;
                    // ---------------------------------------------
                    next.Visible = false;
                }
                if(CredentialsFlag)
                {
                    ViewState["DivID"] = int.Parse(ViewState["DivID"].ToString()) + 1; ; // increment the divID to identify that the user moved to the next stage
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
                HtmlInputRadioButton rd_button = new HtmlInputRadioButton();
                rd_button.Value = p.ProfessionID.ToString();
                const string GROUP_NAME = "Professions";
                rd_button.Name = GROUP_NAME;
                string LinkID = "P" + p.ProfessionID.ToString();
                rd_button.Attributes["id"] = LinkID;
                ProfUsControl userprofession = (ProfUsControl)thispage.LoadControl("~/ProfUsControl.ascx");
                userprofession.imgP = p.ProfPath;
                userprofession.profName = p.ProfName;
                userprofession.profID = p.ProfessionID;
                userprofession.RadioName = LinkID;
                userprofession.EnableViewState = true;
                ctrl.Controls.Add(rd_button);
                ctrl.Controls.Add(userprofession);
            }
        }

        // Takes a Control which has RadioButton and checks if one of them is checked, if anything went wrong it returns -1;
        public static int CheckRadio(HtmlControl ctrl)
        {
            try
            {
                int counter = 0;
                int id = -1;
                foreach (Control rdButton in ctrl.Controls)
                {
                    if (rdButton is HtmlInputRadioButton)
                    {
                        HtmlInputRadioButton bu = (HtmlInputRadioButton)rdButton;
                        if (bu.Checked)
                        {
                            counter++;
                            id = int.Parse(bu.Value);
                        }
                    }
                }
                if (counter > 1)
                {
                    return -1;
                }

                return id;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

    }
}
