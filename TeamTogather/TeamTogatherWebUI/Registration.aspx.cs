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
                    }

                }
                else if (DivID == 2)
                {
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
    }
}