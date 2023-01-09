using Airport_Client.AirportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Airport_Client
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void Registra(Object sender, EventArgs e)
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            DateTime da = DateTime.Parse(birthdate.SelectedDate.ToString());
            //DateTime da = DateTime.Parse(Request.Form["birthdate"].ToString());
            AirportService.Utente ut4 = new AirportService.Utente();
            ut4.Passaporto = Request.Form["passport"].ToString();
            ut4.Nome = Request.Form["name"].ToString();
            ut4.Cognome = Request.Form["surname"].ToString();
            ut4.Nascita = da;
            ut4.Mail = Request.Form["mail"].ToString();
            ut4.Tipo = 0;
            ut4.Credito = 0.0;
            Console.WriteLine(ut4.ToString());
            ac.Register(ut4, Request.Form["password"].ToString());
            ac.Close();
        }
    }
}