using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeamTogatherWebUI
{
    public partial class JobsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JobsWS.JobsWS proxy = new JobsWS.JobsWS();
            NumOffers.Text = proxy.countOffers().ToString();
            JobOffersGrid.DataSource = proxy.ReturnJobOffers();
            JobOffersGrid.DataBind();
        }
    }
}