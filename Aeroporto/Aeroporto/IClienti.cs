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
    public interface IClienti
    {
        [OperationContract]
        bool Buy(Biglietti bi);
        
        [OperationContract]
        List<Voli> Ricerca(Aeroporti a1, Aeroporti a2);

        [OperationContract]
        List<Voli> Misentofortnutato(Aeroporti a1);

        [OperationContract]
        bool Ricarica(Utente ut, float credito);

        [OperationContract]
        List<Biglietti> CheckBiglietti(Utente ut);

        [OperationContract]
        List<Voli> VoliBiglietti(Utente ut);

        [OperationContract]
        List<Voli> VolifromBiglietti(Biglietti bi);


    }
}
