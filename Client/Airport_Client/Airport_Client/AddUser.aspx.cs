using Airport_Client.AirportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Airport_Client
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
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
            ut4.Credito = 0.0;
            if (crew.Checked) { ut4.Tipo = 1; }
            else
            {
                if (admin.Checked) { ut4.Tipo = 2; }
                else { ut4.Tipo = 0; }
            }
            Console.WriteLine(ut4.ToString());
            ac.Register(ut4, Request.Form["password"].ToString());
            ac.Close();
            BindGrid();
        }


        public void Admin(Object sender, EventArgs e)
        {
            if (admin.Checked)
            {
                crew.Checked = false;
            }
        }

        public void Crew(Object sender, EventArgs e)
        {
            if (crew.Checked)
            {
                admin.Checked = false;
            }
        }

        public void BindGrid()
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            Utente[] bp = new List<Utente>().ToArray();
            bp = ac.GetUsers();
            rmgrid.DataSource = bp;
            rmgrid.DataBind();
            ac.Close();
        }

        public void OnRowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            AirportClient ac = new AirportClient();
            ac.Open();

            string pp = rmgrid.Rows[e.RowIndex].Cells[0].Text;
            //string pp = e.ToString();
            ac.RemoveUser(pp);
            ac.Close();
            BindGrid();
        }

        public void backpage(Object sender, EventArgs e)
        {
            Response.Redirect("AdminPage.aspx");
        }
    }
}