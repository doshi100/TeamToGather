﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace TeamTogatherWebUI
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                LoginMessage.Text = "Logged in!";
            }

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string userName = UserNameBox.Text;
            string password = PassBox.Text;
            UserInfo user = UserInfo.Authentication(userName, password);
            if(user != null)
            {
                if(user.Password == password && user.UserName == userName)
                {
                    Session["UserID"] = user.ID;
                    user.UpdateLoginDate();
                    Response.Redirect("HomePage.aspx");
                }
            }
            else
            {
                LoginMessage.Visible = true;
                LoginMessage.Text = "the login failed";
            }
        }
    }
}