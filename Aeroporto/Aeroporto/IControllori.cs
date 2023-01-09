using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Aeroporto
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IControllori" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IControllori
    {
        
        [OperationContract]
        bool AddAeroporti(Aeroporti aer);
        [OperationContract]
        bool AddVolo( Voli vo);
        [OperationContract]
        bool AddTratta(Tratte tra);
        [OperationContract]
        bool AddLavoro(Lavora la);
        
    }
}
