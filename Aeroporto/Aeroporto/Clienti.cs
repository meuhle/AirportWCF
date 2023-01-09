using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Aeroporto
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Clienti" nel codice e nel file di configurazione contemporaneamente.
    public class Clienti : IClienti
    {
        private readonly string connectionstring = "Server = localhost; Database = Aviazione; Trusted_Connection = true;User id=Alex;Password=SQLAlex.22";
        public bool Buy(Biglietti bi) //compra il biglietto e aggiorna credito//fatto
        {
            try
            {
                bool ok = false;
                
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddBiglietti @pPasseggero = '" + bi.Passaporto + "',@pVolo = '" + bi.CodVolo + "',@pBuyer = '" + bi.Buyer + "',@pCLasse = '" + bi.Classe+ "',@responseMessage = @responseMessage OUTPUT;SELECT @responseMessage ";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            if (reader.GetString(0) == "Success")
                            {
                                ok= true;
                            }
                            else
                            {
                                ok= false;
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
        public  List<Voli> Ricerca(Aeroporti a1, Aeroporti a2) //da listo voli inserendo 2 aeroporti  //fatto
        {
            try
            {

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Voli> Lista = new List<Voli>();
                    cmd1.CommandText = "SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Tratte ON Voli.Tratta = Tratte.Id_Tratta WHERE Voli.Tratta IN ( SELECT Id_Tratta FROM dbo.Tratte WHERE  Aeroporto1 = '" + a1.Codice + "' AND Aeroporto2 = '" + a2.Codice + "');";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {


                            Lista.Add(new Voli()
                            {
                                Id_volo = reader.GetString(0),
                                Tratta = reader.GetString(1),
                                Data = reader.GetDateTime(2),
                                Prezzo = reader.GetInt16(3),

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
        public  List<Voli> Misentofortnutato(Aeroporti a1) //da listo voli inserendo 1 aeroporto di partenza//FATTO
        {
            try
            {

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Voli> Lista = new List<Voli>();
                    cmd1.CommandText = "SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Tratte ON Voli.Tratta = Tratte.Id_Tratta WHERE Voli.Tratta IN ( SELECT Id_Tratta FROM dbo.Tratte WHERE  Aeroporto1 = '" + a1.Codice + "');";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            Lista.Add(new Voli()
                            {
                                Id_volo = reader.GetString(0),
                                Tratta = reader.GetString(1),
                                Data = reader.GetDateTime(3),
                                Prezzo = reader.GetInt32(4),

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
        public bool Ricarica(Utente ut, float cred)//permette di modificare il credito sia aumentare che sottrarre  //fatto
        {
            try
            {
                bool ok = false;

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "UPDATE Utenti SET Credito=Credito+"+cred+" WHERE Passaporto='"+ut.Passaporto+"'";
                    var result = cmd1.ExecuteNonQuery();
                    if (result == 0)
                    {

                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }
                    
                }
                return ok;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public List<Biglietti> CheckBiglietti(Utente ut) //mostra i bigliette dell' utente //fatto
        {
            try
            {

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Biglietti> Lista = new List<Biglietti>();
                    cmd1.CommandText = "SELECT * FROM dbo.Biglietti WHERE Biglietti.PassaportoPasseggero= '" + ut.Passaporto + "' OR Biglietti.Buyer=  '" + ut.Passaporto + "'; ";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {


                            Lista.Add(new Biglietti()
                            {
                                Passaporto = reader.GetString(0),
                                CodVolo = reader.GetString(1),
                                Buyer = reader.GetString(2),
                                Classe = reader.GetInt16(3),
                                Prezzo = reader.GetFloat(4),

                            });
                        }
                    return Lista;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                List<Biglietti> Lista = new List<Biglietti>();
                return Lista;
            }
        }

        public List<Voli> VoliBiglietti(Utente ut) //mostra i voli dei  biglietti dell' utente//FATTO
        {
            try
            {

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Voli> Lista = new List<Voli>();
                    cmd1.CommandText = "SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Biglietti ON Voli.Tratta = Biglietti .CodVolo WHERE Voli.Tratta IN (SELECT CodVolo FROM dbo.Biglietti WHERE Biglietti.PassaportoPasseggero= '" + ut.Passaporto + "' OR Acquisti.Buyer=  '" + ut.Passaporto + "');";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {


                            Lista.Add(new Voli()
                            {
                                Id_volo = reader.GetString(0),
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

        public List<Voli> VolifromBiglietti(Biglietti bi) //mostra i voli dei  biglietti dell' utente//FATTO
        {
            try
            {

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Voli> Lista = new List<Voli>();
                    cmd1.CommandText = "SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Biglietti ON Voli.Tratta = Biglietti.CodVolo WHERE Biglietti.CodVolo='"+bi.CodVolo+"' AND Biglietti.PassaportoPasseggero='"+bi.Passaporto+"'";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {


                            Lista.Add(new Voli()
                            {
                                Id_volo = reader.GetString(0),
                                Tratta = reader.GetString(1),
                                Data = reader.GetDateTime(2),
                                Prezzo = reader.GetInt16(3),

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
    }

    }

