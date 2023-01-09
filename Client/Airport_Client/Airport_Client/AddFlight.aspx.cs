using Airport_Client.AirportService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Airport_Client
{
    public partial class AddFlight : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                path.DataSource = CreateDataSource();
                path.DataTextField = "AirportText";
                path.DataValueField = "AirportValue";
                path.DataBind();
            }
            BindGrid();
        }

        ICollection CreateDataSource()
        {

            AirportClient ac = new AirportClient();
            ac.Open();
            AirportService.Tratte[] tr = ac.GetTratte();
            ac.Close();
            List<AirportService.Tratte> tl = new List<Tratte>(tr);
            // Create a table to store data for the DropDownList control.
            DataTable dt = new DataTable();

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("AirportText", typeof(String)));
            dt.Columns.Add(new DataColumn("AirportValue", typeof(String)));

            foreach (Tratte a in tl)
            {
                dt.Rows.Add(CreateRow(a, dt));
            }
            // Create a DataView from the DataTable to act as the data source
            // for the DropDownList control.
            DataView dv = new DataView(dt);
            return dv;
        }

        DataRow CreateRow(Tratte u, DataTable dt)
        {
            // Create a DataRow using the DataTable defined in the 
            // CreateDataSource method.
            DataRow dr = dt.NewRow();
            string ap = u.Id_tratta + ",Part:" + u.Partenza + ",Dest:" + u.Destinazione+ ",Distance:"+u.Distance+" km";
            // This DataRow contains the ColorTextField and ColorValueField 
            // fields, as defined in the CreateDataSource method. Set the 
            // fields with the appropriate value. Remember that column 0 
            // is defined as ColorTextField, and column 1 is defined as 
            // ColorValueField.
            dr[0] = ap;
            dr[1] = u.Id_tratta;

            return dr;
        }

        public void Add(Object sender, EventArgs e)
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            DateTime da = DateTime.Parse(depart.SelectedDate.ToString());
            Voli vo = new Voli();
            vo.Tratta = path.SelectedItem.Value;
            vo.Data = da;
            vo.Prezzo = float.Parse(Request.Form["money"].ToString(), CultureInfo.InvariantCulture.NumberFormat);



            ac.AddVolo(vo);
            ac.Close();
            BindGrid();

        }

        public void BindGrid()
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            Voli[] bp = new List<Voli>().ToArray();
            bp = ac.GetVoli();
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
            ac.RemoveFlight(pp);
            ac.Close();
            BindGrid();
        }
        public void backpage(Object sender, EventArgs e)
        {
            Response.Redirect("AdminPage.aspx");
        }
    }
}