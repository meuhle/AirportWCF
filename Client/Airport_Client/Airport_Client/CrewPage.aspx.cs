using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Airport_Client
{
    public partial class CrewPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void jobs(object sender, EventArgs e)
        {
            Response.Redirect("CheckLavoro.aspx");
        }
    }
}