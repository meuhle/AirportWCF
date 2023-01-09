using System;
using Airport_Client.AirportService;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Airport_Client
{
    public partial class AddPath : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dep.DataSource = CreateDataSource();
                dep.DataTextField = "AirportText";
                dep.DataValueField = "AirportValue";
                dep.DataBind();


                arr.DataSource = CreateDataSource();
                arr.DataTextField = "AirportText";
                arr.DataValueField = "AirportValue";
                arr.DataBind();
            }
            BindGrid();


        }

        ICollection CreateDataSource()
        {

            AirportClient ac = new AirportClient();
            ac.Open();
            AirportService.Aeroporti[] tr = ac.GetAirports();
            ac.Close();
            List<AirportService.Aeroporti> tl = new List<Aeroporti>(tr);
            // Create a table to store data for the DropDownList control.
            DataTable dt = new DataTable();

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("AirportText", typeof(String)));
            dt.Columns.Add(new DataColumn("AirportValue", typeof(String)));

            foreach (Aeroporti a in tl)
            {
                dt.Rows.Add(CreateRow(a, dt));
            }
            // Create a DataView from the DataTable to act as the data source
            // for the DropDownList control.
            DataView dv = new DataView(dt);
            return dv;
        }

        DataRow CreateRow(Aeroporti u, DataTable dt)
        {
            // Create a DataRow using the DataTable defined in the 
            // CreateDataSource method.
            DataRow dr = dt.NewRow();
            string ap = u.Codice + "," + u.Nome + "," + u.Citta;
            // This DataRow contains the ColorTextField and ColorValueField 
            // fields, as defined in the CreateDataSource method. Set the 
            // fields with the appropriate value. Remember that column 0 
            // is defined as ColorTextField, and column 1 is defined as 
            // ColorValueField.
            dr[0] = ap;
            dr[1] = u.Codice;

            return dr;
        }

        public void Add(Object sender, EventArgs e)
        {
            string a1 = dep.SelectedItem.Value;
            string s2 = dep.SelectedItem.Value; 
            Aeroporti ap1 = new Aeroporti();
            string a2 = arr.SelectedItem.Value;
            Aeroporti ap2 = new Aeroporti();

            if (a1 != a2)
            {
                AirportClient ac = new AirportClient();
                ac.Open();
                AirportService.Aeroporti[] tr = ac.GetAirports();
                foreach (Aeroporti a in tr)
                {
                    if (a.Codice == a1) { ap1 = a; }
                    if (a.Codice == a2) { ap2 = a; }
                }
                Tratte t = new Tratte()
                {
                    Partenza = a1,
                    Destinazione = a2
                };

                ac.AddTratta(t);
                ac.Close();
                BindGrid();
            }

        }
        public void BindGrid()
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            Tratte[] bp = new List<Tratte>().ToArray();
            bp = ac.GetTratte();
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
            ac.RemovePath(pp);
            ac.Close();
            BindGrid();
        }
        public void backpage(Object sender, EventArgs e)
        {
            Response.Redirect("AdminPage.aspx");
        }




    }
}