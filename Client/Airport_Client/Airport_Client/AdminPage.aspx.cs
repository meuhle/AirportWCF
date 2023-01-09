using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Airport_Client
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void ra(object sender, EventArgs e)
        {
            Response.Redirect("AddAirport.aspx");
        }
        public void rp(object sender, EventArgs e)
        {
            Response.Redirect("AddPath.aspx");
        }
        public void rf(object sender, EventArgs e)
        {
            Response.Redirect("AddFlight.aspx");
        }
        public void ru(object sender, EventArgs e)
        {
            Response.Redirect("AddUser.aspx");
        }
        public void rj(object sender, EventArgs e)
        {
            Response.Redirect("AddLavoro.aspx");
        }
    }
}