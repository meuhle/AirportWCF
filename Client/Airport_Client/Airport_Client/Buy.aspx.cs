using Airport_Client.AirportService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.ServiceModel.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Airport_Client
{
    public partial class Buy : System.Web.UI.Page
    {
        //List<Utente> list = new List<Utente>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["passengers"] = new List<Utente>();
                preparepage();
                economy.Checked = true;
                forme.Checked = true;
                inputview.Visible = false;
                
            }
            BindGrid();
        }
        public void BindGrid()
        {
            passengerview.DataSource = UserList;
            passengerview.DataBind();
        }

        public void preparepage()
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            Aeroporti a1 = new Aeroporti();
            Aeroporti a2 = new Aeroporti();
            DateTime dd = new DateTime();
            DateTime rd = new DateTime();
            Voli[] v1 = new List<Voli>().ToArray();
            Voli[] v2 = new List<Voli>().ToArray();

            var cmd = int.Parse(Request.Cookies["SearchMode"].Value);
            switch (cmd)
            {
                case 0: //normal

                    
                    {a1.Codice = Request.Cookies["DepAir"].Value;}
                    {a2.Codice = Request.Cookies["ArrAir"].Value;}
                    dd = DateTime.Parse(Request.Cookies["DepDate"].Value);
                    rd = DateTime.Parse(Request.Cookies["RetDate"].Value);

                    v1 = ac.Ricerca(a1, a2, dd);
                    v2 = ac.Ricerca(a2, a1, rd);
                    oneway.DataSource = v1;
                    oneway.DataBind();
                    returngrid.DataSource = v2;
                    returngrid.DataBind();

                    break;
                case 1: //oneway
                    returngrid.Visible = false;
                    {
                        a1.Codice = Request.Cookies["DepAir"].Value;
                    }
                    
                    {
                        a2.Codice = Request.Cookies["ArrAir"].Value;
                    }
                    dd = DateTime.Parse(Request.Cookies["DepDate"].Value);
                    v1 = ac.Ricerca(a1, a2, dd);
                    oneway.DataSource = v1;
                    oneway.DataBind();
                    break;
                case 2: //lucky
                    returngrid.Visible = false;
                    {
                        a1.Codice = Request.Cookies["DepAir"].Value;
                    }
                    dd = DateTime.Parse(Request.Cookies["DepDate"].Value);
                    v1 = ac.Misentofortutato(a1,dd);
                    oneway.DataSource = v1;
                    oneway.DataBind();
                    break;
                case 3:  //bestprice
                    returngrid.Visible = false;
                    {
                        a1.Codice = Request.Cookies["DepAir"].Value;
                    }

                    {
                        a2.Codice = Request.Cookies["ArrAir"].Value;
                    }
                    
                    v1 = ac.BestPrice(a1, a2);
                    oneway.DataSource = v1;
                    oneway.DataBind();
                    break;
            }
            ac.Close();
        }

        public void eco(Object sender, EventArgs e)
        {
            if (economy.Checked)
            {
                business.Checked = false; 
                first.Checked = false; fir(sender, e);bus(sender, e);
            }else if(first.Checked==false && business.Checked == false) { economy.Checked = true; }
        }
        public void fir(Object sender, EventArgs e)
        {
            if (first.Checked)
            {
                business.Checked = false; 
                economy.Checked = false; eco(sender, e);bus(sender, e);
            }
            else if (economy.Checked == false && business.Checked == false) { economy.Checked = true; }
        }
        public void bus(Object sender, EventArgs e)
        {
            if (business.Checked)
            {
                economy.Checked = false; 
                first.Checked = false; fir(sender, e);eco(sender, e);
            }
            else if (first.Checked == false && economy.Checked == false) { economy.Checked = true; }
        }
        public void fm(Object sender, EventArgs e)
        {
            if (forme.Checked)
            {
                forother.Checked = false; fo(sender, e);
                if (inputview.Visible == true && UserList.Count==0)
                {
                    inputview.Visible = false;
                }
            }
            else if (forother.Checked == false ) { forme.Checked = true; }
        }
        public void fo(Object sender, EventArgs e)
        {
            if (forother.Checked)
            {
                forme.Checked = false; fm(sender, e);
                if (inputview.Visible == false)
                {
                    inputview.Visible = true;
                }
                
            }
            else if (forme.Checked == false) { forme.Checked = true; }
        }

        public void Buyticket(object sender, EventArgs e)
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            Biglietti bi = new Biglietti();
            if (economy.Checked == true) { bi.Classe = 0; }
            if (business.Checked == true) { bi.Classe = 1; }
            if (first.Checked == true) { bi.Classe = 2; }

            bi.Buyer = Request.Cookies["User"].Value;
            bi.CodVolo = oneway.SelectedRow.Cells[4].Text;

            if (forme.Checked == true){
                bi.Passaporto = Request.Cookies["User"].Value;
                ac.Buy(bi);
            }
            foreach(Utente u in UserList)
            {
                bi.Passaporto = u.Passaporto;
                ac.Buy(bi);
            }
            if (Request.Cookies["SearchMode"].Value == "0")
            {
                bi.CodVolo = returngrid.SelectedRow.Cells[4].Text;
                if (forme.Checked == true)
                {
                    bi.Passaporto = Request.Cookies["User"].Value;
                    ac.Buy(bi);
                }
                foreach (Utente u in UserList)
                {
                    bi.Passaporto = u.Passaporto;
                    ac.Buy(bi);
                }
            }

            ac.Close();
            Response.Redirect("Default.aspx");
        }

        public void AddView(object sender, EventArgs e)
        {
            if (inputview.Visible == false)
            {
                inputview.Visible = true;
            }
        }
        public void AddUser(object sender, EventArgs e)
        {
            AirportClient ac = new AirportClient();
            ac.Open();
            DateTime da = DateTime.Parse(Calendar1.SelectedDate.ToString());
            Utente ut = new Utente();
            ut.Passaporto = Request.Form["passport"].ToString();
            ut.Nome = Request.Form["name"].ToString();
            ut.Cognome = Request.Form["surname"].ToString();
            ut.Nascita = da;
            //list.Add(ut);
            //Console.WriteLine(list);
            UserList.Add(ut);
            ac.AddPP(ut);
            ac.Close();
            BindGrid();


        }


        const string passlist = "passenger";

        public List<Utente> UserList
        {
            get
            {
                // check if not exist to make new (normally before the post back)
                // and at the same time check that you did not use the same viewstate for other object
                if (!(ViewState[passlist] is List<Utente>))
                {
                    // need to fix the memory and added to viewstate
                    ViewState[passlist] = new List<Utente>();
                }
                return (List<Utente>)ViewState[passlist];
            }
        }

        public void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            UserList.RemoveAt(Convert.ToInt32(e.RowIndex));
           
            BindGrid();
        }
    }
}