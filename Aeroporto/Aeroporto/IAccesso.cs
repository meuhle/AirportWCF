using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Aeroporto
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IAccesso" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IAccesso
    {

        [OperationContract]
        [WebGet(UriTemplate = "login/{mail},{password}")]
        Utente Login(string mail, string password);

        [OperationContract]
        [WebInvoke(UriTemplate = "customers/{id}", Method = "PUT", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool Register(Utente ut, string password);
    }
}
