using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Text;

namespace Aeroporto
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Clienti" nel codice e nel file di configurazione contemporaneamente.
    public class Equipaggio : IEquipaggio
    {

        private readonly string connectionstring = "Server = localhost; Database = Aviazione; Trusted_Connection = true;User id=Alex;Password=SQLAlex.22";
        public  List<Voli> CheckLavoro(Utente ut) //controlla i voli su cui lavora //FATTO
        {
            try
            {

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Voli> Lista = new List<Voli>();
                    cmd1.CommandText = "SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Equipaggio ON Voli.Id_volo = Equipaggio.Volo WHERE Voli.Volo IN (SELECT Volo FROM dbo.Equipaggio WHERE Equipaggio.Passaporto= '" + ut.Passaporto + "');";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            Lista.Add(new Voli()
                            {
                                Tratta = reader.GetString(1),
                                Data = reader.GetDateTime(2),
                                Prezzo = reader.GetInt32(3),

                            });
                        }
                    return Lista;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                List<Voli> Lista = new List<Voli>();
                return Lista;
            }
        }
        public  List<Lavora> CheckRuolo(Utente ut)  //controlla ruolo dei voli su cui lavora//FATTO
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Lavora> Lista = new List<Lavora>();
                    cmd1.CommandText = "SELECT * FROM dbo.Equipaggio WHERE Equipaggio.Passaporto= '" + ut.Passaporto + "');";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            Lista.Add(new Lavora()
                            {
                                Volo = reader.GetString(0),
                                Passaporto = reader.GetString(1),
                                Ruolo = reader.GetString(2),
                                

                            });

                        }
                    return Lista;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                List<Lavora> Lista = new List<Lavora>();
                return Lista;
            }
        }
        public List<Lavora> CheckRuoloforVolo(Utente ut,Voli vo)  //controlla ruolo dei voli su cui lavora//FATTO
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Lavora> Lista = new List<Lavora>();
                    cmd1.CommandText = "SELECT * FROM dbo.Equipaggio WHERE Equipaggio.Passaporto= '" + ut.Passaporto + "' AND Equipaggio.Volo = '"+vo.Id_volo+"';";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            Lista.Add(new Lavora()
                            {
                                Volo = reader.GetString(0),
                                Passaporto = reader.GetString(1),
                                Ruolo = reader.GetString(2),


                            });

                        }
                    return Lista;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                List<Lavora> Lista = new List<Lavora>();
                return Lista;
            }
        }
    }
}
