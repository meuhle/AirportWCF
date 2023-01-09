using Airport_Client.AirportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Airport_Client
{
    public partial class CheckLavoro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            Utente ut = new Utente();
            ut.Passaporto = Request.Cookies["User"].Value;
            Lavora[] bp = new List<Lavora>().ToArray();
            bp = ac.CheckLavoro(ut);
            work.DataSource = bp;
            work.DataBind();
            ac.Close();

        }
        public void backpage(Object sender, EventArgs e)
        {
            Response.Redirect("CrewPage.aspx");
        }
    }
}