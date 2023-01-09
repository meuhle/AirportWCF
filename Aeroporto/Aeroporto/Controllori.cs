using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Aeroporto
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Controllori" nel codice e nel file di configurazione contemporaneamente.
    public class Controllori : IControllori
    {
        private readonly string connectionstring = "Server = localhost; Database = Aviazione; Trusted_Connection = true;User id=Alex;Password=SQLAlex.22";

        public bool AddAeroporti(Aeroporti aer)   //fatto  // aggiunge aeroporti  // classe ok
        {
            try
            {
                bool ok = false;
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddAeroporti @pCodice = '" + aer.Codice + "',@pStato = '" + aer.Stato + "',@pCitta = '" + aer.Citta + "',@pNome = '" + aer.Nome + "',@plat = '" + aer.Lat + "',@plong = '" + aer.Lon + "' SELECT @responseMessage;";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            if (reader.GetString(0) == "Success")
                            {
                                Console.WriteLine("Aeroporto aggiunto con successo");
                                ok = true;
                            }
                            else
                            {
                                Console.WriteLine(reader.GetString(0));
                                ok = false;
                            }
                        }
                    return ok;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        public bool AddVolo(Voli vo)  //fatto //aggiunge volo   // classe ok
        {
            try
            {
                bool ok = false;
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddVoli @pTratta = '" + vo.Tratta + "',@pData = '" + vo.Data + "',@pPrezzo = '" + vo.Prezzo + "' SELECT @responseMessage;";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            if (reader.GetString(0) == "Success")
                            {
                                Console.WriteLine("Volo aggiunto con successo");
                                ok = true;
                            }
                            else
                            {
                                Console.WriteLine(reader.GetString(0));
                                ok = false;
                            }
                        }
                    return ok;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        public bool AddTratta(Tratte tra)  //fatto // aggiunge tratta //classe ok
        {
            try
            {
                bool ok = false;
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddTratte @pAeroport1 = '" + tra.Partenza + "',@pAeroport2 = '" + tra.Destinazione + "' SELECT @responseMessage;";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            if (reader.GetString(0) == "Success")
                            {
                                Console.WriteLine("Tratta aggiunta con successo");
                                ok = true;
                            }
                            else
                            {
                                Console.WriteLine(reader.GetString(0));
                                ok = false;
                            }
                        }
                    return ok;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        public bool AddLavoro(Lavora la)  //fatto // aggiunge lavoro //classe ok
        {
            try
            {
                bool ok = false;
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddEquipaggio @pVolo = '" + la.Volo + "',@pPassaporto = '" + la.Passaporto + "',@pRuolo = '" + la.Ruolo + "' SELECT @responseMessage;";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            if (reader.GetString(0) == "Success")
                            {
                                Console.WriteLine("Lavoro aggiunto con successo");
                                ok = true;
                            }
                            else
                            {
                                Console.WriteLine(reader.GetString(0));
                                ok = false;
                            }
                        }
                    return ok;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        
        
    }
}
