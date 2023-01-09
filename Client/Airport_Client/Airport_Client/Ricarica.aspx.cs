using Airport_Client.AirportService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Airport_Client
{
    public partial class Ricarica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void Topup(Object sender, EventArgs e)
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            AirportService.Utente ut = new AirportService.Utente();
            float c = float.Parse(Request.Form["money"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            ac.Ricarica(ut, c);
            ac.Close();
        }

        public void backpage(Object sender, EventArgs e)
        {
            Response.Redirect("UserPage.aspx");
        }
    }
}