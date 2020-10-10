﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeamTogatherWebUI
{
    public partial class TeamTogatherMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UserID"] != null)
            {
                navbar1.Visible = false;
                navbar2.Visible = true;
            }
        }
    }
}