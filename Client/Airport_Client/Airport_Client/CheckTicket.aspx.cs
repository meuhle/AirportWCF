using Airport_Client.AirportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Airport_Client
{
    public partial class CheckTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            Utente ut = new Utente();
                ut.Passaporto = Request.Cookies["User"].Value;
            Biglietti[] bp = new List<Biglietti>().ToArray();
            bp =  ac.CheckBigliettiFilter(ut, DateTime.Now.Date, false);
            past.DataSource = bp;
            past.DataBind();

            Biglietti[] bf = new List<Biglietti>().ToArray();
            bf = ac.CheckBigliettiFilter(ut, DateTime.Now.Date, true);
            future.DataSource = bf;
            future.DataBind();
            ac.Close();

        }
        public void backpage(Object sender, EventArgs e)
        {
            Response.Redirect("UserPage.aspx");
        }
    }
}