using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Aeroporto
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAirport" in both code and config file together.
    [ServiceContract]
    public interface IAirport
    {
        // Accesso
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebGet(UriTemplate = "customers/{mail},{password}")]
        Utente Login(string mail, string password);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebGet(UriTemplate = "customers/{mail},{password}")]
        Utente LoginCrew(string mail);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "customers/register", Method = "PUT", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool Register(Utente ut, string password);

        //Clienti
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/buy", Method = "PUT", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool Buy(Biglietti bi);      

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/topup{credito}", Method = "PUT", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool Ricarica(Utente ut, float credito);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/checkticket", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Biglietti> CheckBiglietti(Utente ut);

        /*[OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/checkflight", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Voli> VoliBiglietti(Utente ut);*/

        /*[OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/flightfromticket", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Voli> VolifromBiglietti(Biglietti bi);*/

        //Controllori
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "admin/addairport", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool AddAeroporti(Aeroporti aer);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "admin/addflight", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool AddVolo(Voli vo);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "admin/addpath", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool AddTratta(Tratte tra);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "admin/addwork", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool AddLavoro(Lavora la);

        //Equipaggio
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        // [WebInvoke(UriTemplate = "equip/checkrole", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Lavora> CheckRuolo(Utente ut);
                
        /*[OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        // [WebInvoke(UriTemplate = "admin/chekworkflight", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Lavora> CheckRuoloforVolo(Utente ut, Voli vo);*/



        //last added 
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/checkticket", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Biglietti> CheckBigliettiFilter(Utente ut, DateTime date, bool forward);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/research", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Voli> Ricerca(Aeroporti a1, Aeroporti a2,DateTime date);

       

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/research", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Voli> BestPrice(Aeroporti a1, Aeroporti a2);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/fortune", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Voli> Misentofortutato(Aeroporti a1, DateTime date);
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/fortune", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Voli> MisentofortutatoNoData(Aeroporti a1);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        // [WebInvoke(UriTemplate = "admin/checkwork", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Lavora> CheckLavoro(Utente ut);



        //Get object list
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        // [WebInvoke(UriTemplate = "admin/checkwork", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Tratte> GetTratte();


        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        // [WebInvoke(UriTemplate = "admin/checkwork", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Aeroporti> GetAirports();

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        // [WebInvoke(UriTemplate = "admin/checkwork", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        Utente UserfromPP(string pp);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "customers/register", Method = "PUT", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool AddPP(Utente ut);


        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        // [WebInvoke(UriTemplate = "admin/checkwork", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Utente> GetCrewMember();

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        // [WebInvoke(UriTemplate = "admin/checkwork", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Utente> GetUsers();

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        // [WebInvoke(UriTemplate = "admin/checkwork", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Lavora> GetJobs();

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/fortune", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Voli> GetVoli();

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/fortune", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool RemoveAirport(string code);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/fortune", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool RemovePath(string code);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/fortune", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool RemoveUser(string code);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/fortune", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool RemoveFlight(string code);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[WebInvoke(UriTemplate = "client/fortune", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool RemoveJob(string pp, string volo);


    }
}
