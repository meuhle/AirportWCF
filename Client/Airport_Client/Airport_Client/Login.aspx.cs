using Airport_Client.AirportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Airport_Client
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void Log(Object sender, EventArgs e)
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            
            Utente ut = new Utente();
            ut = ac.Login(Request.Form["mail"].ToString(), Request.Form["password"].ToString());
            HttpCookie cookie = new HttpCookie("User");
            cookie.Value = ut.Passaporto;
            cookie.Expires = DateTime.Now.AddHours(3);
            Response.SetCookie(cookie);
            HttpCookie Ustype= new HttpCookie("UserType");
            Ustype.Value = ut.Tipo.ToString();
            Ustype.Expires = DateTime.Now.AddHours(3);
            Response.SetCookie(Ustype);

            ac.Close();
            if (ut.Tipo == 2)
            {
                Response.Redirect("AdminPage.aspx");
            }
            else
            {
                Response.Redirect("UserPage.aspx");
            }
        }
    }
}