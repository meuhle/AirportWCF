using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Airport_Client
{
    public partial class UserPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void ticket(object sender, EventArgs e)
        {
            Response.Redirect("CheckTicket.aspx");
        }
        public void gohome(object sender, EventArgs e)
        {
            Response.Redirect("Research.aspx");
        }
        public void topup(object sender, EventArgs e)
        {
            Response.Redirect("Ricarica.aspx");
        }
    }
}