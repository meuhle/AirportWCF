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
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
            if (!Page.IsPostBack)
            {
                Airport1.DataSource = CreateDataSource();
                Airport1.DataTextField = "AirportText";
                Airport1.DataValueField = "AirportValue";
                Airport1.DataBind();


                Airport2.DataSource = CreateDataSource();
                Airport2.DataTextField = "AirportText";
                Airport2.DataValueField = "AirportValue";
                Airport2.DataBind();
            }

            //Request.Cookies.Clear();
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

        public void Search(Object sender, EventArgs e)
        {
            HttpCookie SearchMode = new HttpCookie("SearchMode");
            //SearchMode.Expires = DateTime.Now.AddHours(3);
            
            if (oneway.Checked) //one way value 1
            {
                SearchMode.Value = "1";
                Response.SetCookie(SearchMode);

                HttpCookie DepAir = new HttpCookie("DepAir");  //Departure Airport
                DepAir.Value = Airport1.SelectedItem.Value;
                //DepAir.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(DepAir);
                HttpCookie ArrAir = new HttpCookie("ArrAir"); //Arrival Airport
                ArrAir.Value = Airport2.SelectedItem.Value;
                //ArrAir.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(ArrAir);
                HttpCookie DepDate = new HttpCookie("DepDate"); //Departure Date
                DepDate.Value = depart.SelectedDate.ToString();
                //DepDate.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(DepDate);
                
            }
            else if (lucky.Checked)//lucky value 2
            {
                SearchMode.Value = "2";
                Response.SetCookie(SearchMode);

                HttpCookie DepAir = new HttpCookie("DepAir");  //Departure Airport
                DepAir.Value = Airport1.SelectedItem.Value;
                //DepAir.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(DepAir);
                HttpCookie DepDate = new HttpCookie("DepDate"); //Departure Date
                DepDate.Value = depart.SelectedDate.ToString();
                //DepDate.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(DepDate);
            }
            else if (bestprice.Checked)//bestprice value 3
            {
                SearchMode.Value = "3";
                Response.SetCookie(SearchMode);

                HttpCookie DepAir = new HttpCookie("DepAir");  //Departure Airport
                string s1 = Airport1.SelectedItem.Value;
                DepAir.Value = s1;
                //DepAir.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(DepAir);
                HttpCookie ArrAir = new HttpCookie("ArrAir"); //Arrival Airport
                ArrAir.Value = Airport2.SelectedItem.Value;
                //ArrAir.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(ArrAir);
                
            }
            else//normal value 0
            {
                SearchMode.Value = "0";
                Response.SetCookie(SearchMode);

                HttpCookie DepAir = new HttpCookie("DepAir");  //Departure Airport
                DepAir.Value = Airport1.SelectedItem.Value;
                //DepAir.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(DepAir);
                HttpCookie ArrAir = new HttpCookie("ArrAir"); //Arrival Airport
                ArrAir.Value = Airport2.SelectedItem.Value;
                //ArrAir.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(ArrAir);
                HttpCookie DepDate = new HttpCookie("DepDate"); //Departure Date
                DepDate.Value = depart.SelectedDate.ToString();
                //DepDate.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(DepDate);
                HttpCookie RetDate = new HttpCookie("RetDate"); //Return Date
                RetDate.Value = returndate.SelectedDate.ToString();
                //RetDate.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(RetDate);
            }
            string s = Request.Cookies["SearchMode"].Value;
            Response.Redirect("Buy.aspx");
        }
        public void Oneway(Object sender, EventArgs e)
        {
            if (oneway.Checked)
            {
                Console.WriteLine(oneway.Checked.ToString());
                retdiv.Visible = false;

                lucky.Checked = false;
                bestprice.Checked = false;BestPrice(sender, e);Lucky(sender,e);
            }
            else
            {
                Console.WriteLine(oneway.Checked.ToString());
                if (!bestprice.Checked && !lucky.Checked)
                {
                    retdiv.Visible = true;
                }
            }
        }
        public void Lucky(Object sender, EventArgs e)
        {
            if (lucky.Checked)
            {
                Console.WriteLine(lucky.Checked.ToString());
                retdiv.Visible = false;
                Airport2.Visible = false;
                Label1.Visible = false;

                oneway.Checked = false;
                bestprice.Checked = false;BestPrice(sender, e);Oneway(sender,e);
            }
            else
            {
                Console.WriteLine(lucky.Checked.ToString());
                if (!bestprice.Checked && !oneway.Checked)
                {
                    retdiv.Visible = true;
                }
                Airport2.Visible = true;
                Label1.Visible = true;
            }
        }

        public void BestPrice(Object sender, EventArgs e)
        {
            if (bestprice.Checked)
            {
                Console.WriteLine(bestprice.Checked.ToString());
                retdiv.Visible = false;
                DepLabel.Visible = false;
                depart.Visible = false;

                oneway.Checked = false;
                lucky.Checked = false;Lucky(sender, e);Oneway(sender, e);
            }
            else
            {
                Console.WriteLine(bestprice.Checked.ToString());
                if (!oneway.Checked && !lucky.Checked)
                {
                    retdiv.Visible = true;
                }
                DepLabel.Visible = true;
                depart.Visible = true;
            }
        }



       
    }
}