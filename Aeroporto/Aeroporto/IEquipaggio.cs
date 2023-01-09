using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Aeroporto
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IClienti" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IEquipaggio
    {
        [OperationContract]
        List<Lavora> CheckRuolo(Utente ut);
        [OperationContract]
        List<Voli> CheckLavoro(Utente ut);
        [OperationContract]
        List<Lavora> CheckRuoloforVolo(Utente ut, Voli vo);
    } 
}
