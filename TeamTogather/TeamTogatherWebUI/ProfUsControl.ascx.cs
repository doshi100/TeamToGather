using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace TeamTogatherWebUI
{
    public partial class ProfUsControl : System.Web.UI.UserControl
    {

        public string imgP { get; set; }
        public string profName { get; set; }
        public int profID { get; set; }
        public string RadioName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Profimg.Src = imgP;
            nameContainer.InnerText = profName;
            labelPro.Attributes.Add("for", RadioName);
        }



    }
}