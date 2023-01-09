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
    public partial class AddAirport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        public void Add(Object sender, EventArgs e)
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            Aeroporti ap = new Aeroporti();
            ap.Codice = Request.Form["Code"].ToString();
            ap.Stato = Request.Form["State"].ToString();
            ap.Citta = Request.Form["City"].ToString();
            ap.Nome = Request.Form["Name"].ToString();
            
            ap.Lon = Double.Parse(Request.Form["Lon"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            ap.Lat = Double.Parse(Request.Form["Lat"].ToString(), CultureInfo.InvariantCulture.NumberFormat);

            ac.AddAeroporti(ap);
            ac.Close();
            BindGrid();
        }

        public void BindGrid()
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            Aeroporti[] bp = new List<Aeroporti>().ToArray();
            bp = ac.GetAirports();
            rmairport.DataSource = bp;
            rmairport.DataBind();
            ac.Close();
        }

        public void OnRowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            AirportClient ac = new AirportClient();
            ac.Open();

            string pp = rmairport.Rows[e.RowIndex].Cells[0].Text;
            //string pp = e.ToString();
            ac.RemoveAirport(pp);
            ac.Close();
            BindGrid();
        }
        public void backpage(Object sender, EventArgs e)
        {
            Response.Redirect("AdminPage.aspx");
        }
    }
}