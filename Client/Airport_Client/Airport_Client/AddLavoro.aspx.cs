using Airport_Client.AirportService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Airport_Client
{
    public partial class AddLavoro : System.Web.UI.Page
    {
        string[] roles = { "Captain", "Pilot", "Crew Captain", "Crew Member" };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                flight.DataSource = CreateFLightSource();
                flight.DataTextField = "AirportText";
                flight.DataValueField = "AirportValue";
                flight.DataBind();


                pass.DataSource = CreatePassSource();
                pass.DataTextField = "AirportText";
                pass.DataValueField = "AirportValue";
                pass.DataBind();

                role.DataSource = roles;
                role.DataBind();
            }
            BindGrid();

        }

        ICollection CreatePassSource()
        {

            AirportClient ac = new AirportClient();
            ac.Open();
            AirportService.Utente[] tr = ac.GetCrewMember();
            ac.Close();
            List<AirportService.Utente> tl = new List<Utente>(tr);
            // Create a table to store data for the DropDownList control.
            DataTable dt = new DataTable();

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("AirportText", typeof(String)));
            dt.Columns.Add(new DataColumn("AirportValue", typeof(String)));

            foreach (Utente a in tl)
            {
                dt.Rows.Add(CreateRowUser(a, dt));
            }
            // Create a DataView from the DataTable to act as the data source
            // for the DropDownList control.
            DataView dv = new DataView(dt);
            return dv;
        }
        DataRow CreateRowUser(Utente u, DataTable dt)
        {
            // Create a DataRow using the DataTable defined in the 
            // CreateDataSource method.
            DataRow dr = dt.NewRow();
            string ap = u.Passaporto + "," + u.Nome + "," + u.Cognome + "," + u.Nascita;
            // This DataRow contains the ColorTextField and ColorValueField 
            // fields, as defined in the CreateDataSource method. Set the 
            // fields with the appropriate value. Remember that column 0 
            // is defined as ColorTextField, and column 1 is defined as 
            // ColorValueField.
            dr[0] = ap;
            dr[1] = u.Passaporto;

            return dr;
        }


        ICollection CreateFLightSource()
        {

            AirportClient ac = new AirportClient();
            ac.Open();
            AirportService.Voli[] tr = ac.GetVoli();
            ac.Close();
            List<AirportService.Voli> tl = new List<Voli>(tr);
            // Create a table to store data for the DropDownList control.
            DataTable dt = new DataTable();

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("AirportText", typeof(String)));
            dt.Columns.Add(new DataColumn("AirportValue", typeof(String)));

            foreach (Voli a in tl)
            {
                dt.Rows.Add(CreatFlightUser(a, dt));
            }
            // Create a DataView from the DataTable to act as the data source
            // for the DropDownList control.
            DataView dv = new DataView(dt);
            return dv;
        }

        DataRow CreatFlightUser(Voli u, DataTable dt)
        {
            // Create a DataRow using the DataTable defined in the 
            // CreateDataSource method.
            DataRow dr = dt.NewRow();
            string ap = u.Id_volo + "," + u.Tratta + "," + u.Data ;
            // This DataRow contains the ColorTextField and ColorValueField 
            // fields, as defined in the CreateDataSource method. Set the 
            // fields with the appropriate value. Remember that column 0 
            // is defined as ColorTextField, and column 1 is defined as 
            // ColorValueField.
            dr[0] = ap;
            dr[1] = u.Id_volo;

            return dr;
        }
        public void Add(Object sender, EventArgs e)
        {
            Lavora la = new Lavora();
            la.Volo = flight.SelectedItem.Value;
            la.Passaporto = pass.SelectedItem.Value;
            la.Ruolo = role.SelectedValue.ToString();
            AirportClient ac = new AirportClient();
            ac.Open();
            ac.AddLavoro(la);
            ac.Close();
            BindGrid();
        }

        public void BindGrid()
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            Lavora[] bp = new List<Lavora>().ToArray();
            bp = ac.GetJobs();
            rmgrid.DataSource = bp;
            rmgrid.DataBind();
            ac.Close();
        }

        public void OnRowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            AirportClient ac = new AirportClient();
            ac.Open();

            string pp = rmgrid.Rows[e.RowIndex].Cells[0].Text;
            string volo = rmgrid.Rows[e.RowIndex].Cells[2].Text;
            //string pp = e.ToString();
            ac.RemoveJob(pp, volo);
            ac.Close();
            BindGrid();
        }

        public void backpage(Object sender, EventArgs e)
        {
            Response.Redirect("AdminPage.aspx");
        }

    }
}