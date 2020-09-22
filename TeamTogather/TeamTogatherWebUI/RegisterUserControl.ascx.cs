using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace TeamTogatherWebUI
{
    public partial class RegisterUserControl : System.Web.UI.UserControl
    {

        public string imgP { get; set; }
        public string fieldName { get; set; }
        public int IDnum { get; set; }
        public string RadioName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            img.Src = imgP;
            nameContainer.InnerText = fieldName;
            labelPro.Attributes.Add("for", RadioName);
        }



    }
}