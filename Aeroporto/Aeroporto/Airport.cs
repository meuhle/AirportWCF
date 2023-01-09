using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Aeroporto
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Airport" in both code and config file together.
    public class Airport : IAirport
    {
        private readonly string connectionstring = "Server = localhost; Database = Aviazione; Trusted_Connection = true;MultipleActiveResultSets=True;User id=Alex;Password=SQLAlex.22";

        public Utente Login(string mail, string password)
        {
            try
            {
                Utente u = new Utente();
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DECLARE @responseMessage NVARCHAR(250) DECLARE @userID NVARCHAR(9) EXEC dbo.Accesso @pMail = '" + mail + "',@pPassword = '" + password + "',@userID=@userID OUTPUT,@responseMessage = @responseMessage OUTPUT SELECT @responseMessage,@userID;";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            switch (reader.GetString(0))
                            {
                                case "1":
                                    Console.WriteLine("User successfully logged in");
                                    string passport = reader.GetString(1);
                                    using (SqlCommand cmd2 = conn.CreateCommand())
                                    {
                                        cmd2.CommandText = "SELECT * FROM Utenti WHERE Passaporto= '" + passport + "';";
                                        using (SqlDataReader reader2 = cmd2.ExecuteReader())

                                            while (reader2.Read())
                                            {
                                                Console.WriteLine(reader2.GetValue(0));
                                                Console.WriteLine(reader2.GetValue(1));
                                                Console.WriteLine(reader2.GetValue(2));
                                                Console.WriteLine(reader2.GetValue(3));
                                                Console.WriteLine(reader2.GetValue(4));
                                                Console.WriteLine(reader2.GetValue(5));
                                                Console.WriteLine(reader2.GetValue(6));
                                                Console.WriteLine(reader2.GetValue(7));
                                                Console.WriteLine(reader2.GetValue(8));
                                                u.Passaporto = reader2.GetString(0);
                                                u.Nome = reader2.GetString(1);
                                                u.Cognome = reader2.GetString(2);
                                                u.Nascita = reader2.GetDateTime(3);
                                                u.Mail = reader2.GetString(4);
                                                u.Tipo = reader2.GetInt32(7);
                                                u.Credito = reader2.GetDouble(8);
                                            }
                                    }
                                    break;
                                case "2":
                                    Console.WriteLine("Invalid login");
                                    break;
                                case "3":
                                    Console.WriteLine("Incorrect password");
                                    break;
                            }
                        }
                    Console.WriteLine("utente loggato: " + u.Passaporto + u.Nome);
                    return u;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public bool Register(Utente ut, string password)
        {
            try
            {
                bool ok = false;
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddUtenti @pPassaporto = '" + ut.Passaporto + "',@pNome = '" + ut.Nome + "',@pCognome = '" + ut.Cognome + "',@pData = '" + ut.Nascita + "',@pMail = '" + ut.Mail + "',@pPassword = '" + password + "',@pTipo = " + ut.Tipo + ",@pCredito = " + ut.Credito + " ,@responseMessage = @responseMessage OUTPUT SELECT @responseMessage; ";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            if (reader.GetString(0) == "Success")
                            {
                                ok = true;
                            }
                            else
                            {
                                Console.WriteLine(reader.GetString(0));
                                ok = false;
                            }
                        }
                }
                Console.WriteLine("utente registrato: " + ok);
                return ok;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public bool Buy(Biglietti bi) //compra il biglietto e aggiorna credito//fatto
        {
            try
            {
                bool ok = false;

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddBiglietti @pPasseggero = '" + bi.Passaporto + "',@pVolo = '" + bi.CodVolo + "',@pBuyer = '" + bi.Buyer + "',@pCLasse = '" + bi.Classe + "',@responseMessage = @responseMessage OUTPUT;SELECT @responseMessage ";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            if (reader.GetString(0) == "Success")
                            {
                                Console.WriteLine("Biglietto acquistato per " + bi.Passaporto);
                                ok = true;
                            }
                            else
                            {
                                Console.WriteLine(reader.GetString(0));
                                ok = false;
                            }
                        }
                    Console.WriteLine("Biglietto comprato: " + ok);
                    return ok;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;

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
                    cmd1.CommandText = "UPDATE Utenti SET Credito=Credito+" + cred + " WHERE Passaporto='" + ut.Passaporto + "'";
                    var result = cmd1.ExecuteNonQuery();
                    if (result == 1)
                    {

                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }

                }
                Console.WriteLine("Ricarica: " +cred+" "+ ok);
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
                            string Passaporto = reader.GetString(0);
                            string CodVolo = reader.GetString(1);
                            string Buyer = reader.GetString(2);
                            int Classe = reader.GetInt32(3);
                            double Prezzo = reader.GetDouble(4);

                            Lista.Add(new Biglietti()
                            {
                                Passaporto = reader.GetString(0),
                                CodVolo = reader.GetString(1),
                                Buyer = reader.GetString(2),
                                Classe = reader.GetInt32(3),
                                Prezzo = reader.GetDouble(4),

                            });
                        }
                    Console.WriteLine("Lista checkbiglietti count: " + Lista.Count);
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

        /*public List<Voli> VoliBiglietti(Utente ut) //mostra i voli dei  biglietti dell' utente//FATTO
        {
            try
            {

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Voli> Lista = new List<Voli>();
                    cmd1.CommandText = "SELECT * FROM dbo.Voli WHERE Id_volo IN (SELECT CodVolo FROM dbo.Biglietti WHERE Biglietti.PassaportoPasseggero= '" + ut.Passaporto + "' OR Biglietti.Buyer=  '" + ut.Passaporto + "');";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {


                            Lista.Add(new Voli()
                            {
                                Id_volo = reader.GetString(0),
                                Tratta = reader.GetString(1),
                                Data = reader.GetDateTime(2),
                                Prezzo = reader.GetDouble(3),

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
        */
       /* public List<Voli> VolifromBiglietti(Biglietti bi) //mostra i voli dei  biglietti dell' utente//FATTO
        {
            try
            {

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Voli> Lista = new List<Voli>();
                    cmd1.CommandText = "SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Biglietti ON Voli.Id_volo = Biglietti.CodVolo WHERE Biglietti.CodVolo='" + bi.CodVolo + "' AND Biglietti.Buyer='" + bi.Passaporto + "'";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            Lista.Add(new Voli()
                            {
                                Id_volo = reader.GetString(0),
                                Tratta = reader.GetString(1),
                                Data = reader.GetDateTime(2),
                                Prezzo = reader.GetDouble(3),
                            });
                        }
                    Console.WriteLine("Lista volifrombiglietti count: " + Lista.Count);
                    return Lista;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                List<Voli> Lista = new List<Voli>();
                return Lista;
            }
        }*/
        public bool AddAeroporti(Aeroporti aer)   //fatto  // aggiunge aeroporti  // classe ok
        {
            try
            {
                string var = aer.Lat.ToString().Replace(',', '.');
                bool ok = false;
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddAeroporti @pCodice = '" + aer.Codice + "',@pStato = '" + aer.Stato + "',@pCitta = '" + aer.Citta + "',@pNome = '" + aer.Nome + "',@plat = '" + aer.Lat.ToString().Replace(',', '.') + "',@plong = '" + aer.Lon.ToString().Replace(',', '.') + "',@responseMessage = @responseMessage OUTPUT SELECT @responseMessage;";
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
                    Console.WriteLine("aeroporto aggiunto: " + ok);
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
                    cmd1.CommandText = "DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddVoli @pTratta = '" + vo.Tratta + "',@pData = '" + vo.Data + "',@pPrezzo = '" + vo.Prezzo + "',@responseMessage = @responseMessage OUTPUT SELECT @responseMessage;";
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
                    Console.WriteLine("volo aggiunto: " + ok);
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
                    cmd1.CommandText = "DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddTratte @pAeroport1 = '" + tra.Partenza + "',@pAeroport2 = '" + tra.Destinazione + "',@responseMessage = @responseMessage OUTPUT SELECT @responseMessage;";
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
                    Console.WriteLine("tratta aggiunto: " + ok);
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
                    cmd1.CommandText = "DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddEquipaggio @pVolo = '" + la.Volo + "',@pPassaporto = '" + la.Passaporto + "',@pRuolo = '" + la.Ruolo + "',@responseMessage = @responseMessage OUTPUT SELECT @responseMessage;";
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
                    Console.WriteLine("lavoro aggiunto: " + ok);
                    return ok;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public List<Lavora> CheckRuolo(Utente ut)  //controlla ruolo dei voli su cui lavora//FATTO
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Lavora> Lista = new List<Lavora>();
                    cmd1.CommandText = "SELECT * FROM dbo.Equipaggio WHERE Equipaggio.Passaporto= '" + ut.Passaporto + "';";
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
                    Console.WriteLine("Lista checkruolo count: " + Lista.Count);
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
        /*public List<Lavora> CheckRuoloforVolo(Utente ut, Voli vo)  //controlla ruolo dei voli su cui lavora//FATTO
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Lavora> Lista = new List<Lavora>();
                    cmd1.CommandText = "SELECT * FROM dbo.Equipaggio WHERE Equipaggio.Passaporto= '" + ut.Passaporto + "' AND Equipaggio.Volo = '" + vo.Id_volo + "';";
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
        */

        //check
        public List<Voli> Ricerca(Aeroporti a1, Aeroporti a2, DateTime date) //da listo voli inserendo 2 aeroporti  //fatto
        {
            try
            {

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Voli> Lista = new List<Voli>();
                    cmd1.CommandText = "SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Tratte ON Voli.Tratta = Tratte.Id_Tratta WHERE Voli.Data='" + date + "' AND Voli.Tratta IN ( SELECT Id_Tratta FROM dbo.Tratte WHERE  Aeroporto1 = '" + a1.Codice + "' AND Aeroporto2 = '" + a2.Codice + "' OR  Aeroporto1 = '" + a2.Codice + "' AND Aeroporto2 = '" + a1.Codice + "');";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {


                            Lista.Add(new Voli()
                            {
                                Id_volo = reader.GetString(0),
                                Tratta = reader.GetString(1),
                                Data = reader.GetDateTime(2),
                                Prezzo = reader.GetDouble(3),

                            });
                        }
                    Console.WriteLine("Lista Ricerca count: " + Lista.Count);
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



        public List<Voli> Misentofortutato(Aeroporti a1, DateTime date) //da listo voli inserendo 1 aeroporto di partenza//FATTO
        {
            try
            {

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Voli> Lista = new List<Voli>();
                    cmd1.CommandText = "SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Tratte ON Voli.Tratta = Tratte.Id_Tratta WHERE Voli.Data='" + date + "' AND Voli.Tratta IN ( SELECT Id_Tratta FROM dbo.Tratte WHERE  Aeroporto1 = '" + a1.Codice + "' OR Aeroporto2 = '" + a1.Codice + "');";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            Lista.Add(new Voli()
                            {
                                Id_volo = reader.GetString(0),
                                Tratta = reader.GetString(1),
                                Data = reader.GetDateTime(2),
                                Prezzo = reader.GetDouble(3),

                            });
                        }
                    Console.WriteLine("Lista fortunato count: " + Lista.Count);
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

        public List<Voli> MisentofortutatoNoData(Aeroporti a1) //da listo voli inserendo 1 aeroporto di partenza//FATTO
        {
            try
            {

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Voli> Lista = new List<Voli>();
                    cmd1.CommandText = "SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Tratte ON Voli.Tratta = Tratte.Id_Tratta WHERE Voli.Tratta IN ( SELECT Id_Tratta FROM dbo.Tratte WHERE  Aeroporto1 = '" + a1.Codice + "') ORDER BY Voli.Data ASC;";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            Lista.Add(new Voli()
                            {
                                Id_volo = reader.GetString(0),
                                Tratta = reader.GetString(1),
                                Data = reader.GetDateTime(2),
                                Prezzo = reader.GetDouble(3),

                            });
                        }
                    Console.WriteLine("Lista fortunatonodata count: " + Lista.Count);
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


        public List<Lavora> CheckLavoro(Utente ut) //controlla i voli su cui lavora //FATTO
        {
            try
            {

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Lavora> Lista = new List<Lavora>();
                    cmd1.CommandText = "SELECT Voli.Id_volo,Equipaggio.Passaporto,Equipaggio.Ruolo,Voli.Data  FROM dbo.Voli INNER JOIN dbo.Equipaggio ON Voli.Id_volo = Equipaggio.Volo WHERE Voli.Id_volo IN (SELECT Volo FROM dbo.Equipaggio WHERE Equipaggio.Passaporto = '" + ut.Passaporto + "'); ";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            Lista.Add(new Lavora()
                            {
                                Volo = reader.GetString(0),
                                Passaporto = reader.GetString(1),
                                Ruolo = reader.GetString(2),
                                Data = reader.GetDateTime(3),

                            });
                        }
                    Console.WriteLine("Lista checklavoro count: " + Lista.Count);
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

        public List<Biglietti> CheckBigliettiFilter(Utente ut, DateTime date, bool forward)
        {
            try
            {
                String d = date.Date.ToString();
                String da = date.Day + "/" + date.Month + "/" + date.Year;
                DateTime days = date.Date;
                //Console.WriteLine(d);
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Biglietti> Lista = new List<Biglietti>();
                    if (forward)
                    {
                        cmd1.CommandText = "SELECT * FROM Biglietti INNER JOIN Voli ON Voli.Id_volo = Biglietti.CodVolo  WHERE Voli.Data >= '" + date + "' AND Biglietti.Buyer ='" + ut.Passaporto + "' OR Voli.Data >='" + date + "' AND Biglietti.PassaportoPasseggero= '" + ut.Passaporto + "' ORDER BY Voli.Data ASC ";
                    }
                    else
                    {
                        cmd1.CommandText = "SELECT * FROM Biglietti INNER JOIN Voli ON Voli.Id_volo = Biglietti.CodVolo  WHERE Voli.Data <= '" + date + "' AND Biglietti.Buyer ='" + ut.Passaporto + "' OR Voli.Data <='" + date + "' AND Biglietti.PassaportoPasseggero= '" + ut.Passaporto + "' ORDER BY Voli.Data ASC ";
                    }
                    //cmd1.Parameters.AddWithValue("@pDate", d);
                    //cmd1.Parameters.AddWithValue("@Passport", ut.Passaporto);
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            string Passaporto = reader.GetString(0);
                            string CodVolo = reader.GetString(1);
                            string Buyer = reader.GetString(2);
                            int Classe = reader.GetInt32(3);
                            double Prezzo = reader.GetDouble(4);

                            Lista.Add(new Biglietti()
                            {
                                Passaporto = reader.GetString(0),
                                CodVolo = reader.GetString(1),
                                Buyer = reader.GetString(2),
                                Classe = reader.GetInt32(3),
                                Prezzo = reader.GetDouble(4),

                            });
                        }
                    Console.WriteLine("Lista checkbiglietti count: " + Lista.Count);
                    return Lista;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                List<Biglietti> Lista = new List<Biglietti>();
                return Lista;
            }
        }  //non va

        public List<Voli> BestPrice(Aeroporti a1, Aeroporti a2)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Voli> Lista = new List<Voli>();
                    cmd1.CommandText = "SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Tratte ON Voli.Tratta = Tratte.Id_Tratta WHERE Voli.Tratta IN ( SELECT Id_Tratta FROM dbo.Tratte WHERE  Aeroporto1 = '" + a1.Codice + "' AND Aeroporto2 = '" + a2.Codice + "' OR  Aeroporto1 = '" + a2.Codice + "' AND Aeroporto2 = '" + a1.Codice + "') ORDER BY Voli.Prezzo ASC;";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {


                            Lista.Add(new Voli()
                            {
                                Id_volo = reader.GetString(0),
                                Tratta = reader.GetString(1),
                                Data = reader.GetDateTime(2),
                                Prezzo = reader.GetDouble(3),

                            });
                        }
                    Console.WriteLine("LIsta bestprice count: " + Lista.Count);
                    return Lista;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                List<Voli> Lista = new List<Voli>();
                return Lista;
            }
        }//FATTO





        //get object list
        public List<Tratte> GetTratte()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Tratte> Lista = new List<Tratte>();
                    cmd1.CommandText = "SELECT * FROM dbo.Tratte";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {


                            Lista.Add(new Tratte()
                            {
                                Id_tratta = reader.GetString(0),
                                Partenza = reader.GetString(1),
                                Destinazione = reader.GetString(2),
                                //Distance = reader.GetFloat(3),

                            });
                        }
                    Console.WriteLine("LIsta tratte count: " + Lista.Count);
                    return Lista;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                List<Tratte> Lista = new List<Tratte>();
                return Lista;
            }
        }

        public List<Aeroporti> GetAirports()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Aeroporti> Lista = new List<Aeroporti>();
                    cmd1.CommandText = "SELECT Codice,Stato,Citta,Nome,Coord.Long AS Long,Coord.Lat AS Lat FROM dbo.Aeroporti";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {


                            Lista.Add(new Aeroporti()
                            {
                                Codice = reader.GetString(0),
                                Stato = reader.GetString(1),
                                Citta = reader.GetString(2),
                                Nome = reader.GetString(3),
                                Lon = reader.GetDouble(4),
                                Lat = reader.GetDouble(5),
                            });
                        }
                    Console.WriteLine("Lista aeroporti count: " + Lista.Count);
                    return Lista;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                List<Aeroporti> Lista = new List<Aeroporti>();
                return Lista;
            }
        }

        public Utente UserfromPP(string pp)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Aeroporti> Lista = new List<Aeroporti>();
                    Utente ut = new Utente();
                    cmd1.CommandText = "SELECT * FROM Utenti WHERE Passaporto='" + pp + "'";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            ut.Passaporto = reader.GetString(0);
                            ut.Nome = reader.GetString(1);
                            ut.Cognome = reader.GetString(2);
                            ut.Nascita = reader.GetDateTime(3);
                            ut.Mail = reader.GetString(4);
                            ut.Tipo = reader.GetInt16(7);
                            ut.Credito = reader.GetFloat(8);
                        }
                    Console.WriteLine("Utente da pp " + ut.Passaporto + ut.Nome);
                    return ut;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Utente ut = new Utente();
                return ut;
            }
        }

        public bool AddPP(Utente ut)
        {
            try
            {
                bool ok = false;
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "INSERT INTO Utenti(Passaporto,Nome,Cognome,Datanascita,Tipo) VALUES ('" + ut.Passaporto + "','" + ut.Nome + "','" + ut.Cognome + "','" + ut.Nascita + "',3)";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            if (reader.GetString(0) == "Success")
                            {
                                ok = true;
                            }
                            else
                            {
                                Console.WriteLine(reader.GetString(0));
                                ok = false;
                            }
                        }
                }
                Console.WriteLine("Aggiunta passeggero " + ok);
                return ok;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public List<Utente> GetCrewMember()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Utente> Lista = new List<Utente>();
                    cmd1.CommandText = "SELECT * FROM Utenti WHERE Tipo = 1";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {


                            Lista.Add(new Utente()
                            {
                                Passaporto = reader.GetString(0),
                                Nome = reader.GetString(1),
                                Cognome = reader.GetString(2),
                                Nascita = reader.GetDateTime(3),
                                Mail = reader.GetString(4),
                                Tipo = reader.GetInt32(7),
                                Credito = reader.GetDouble(8),
                            });
                        }
                    Console.WriteLine("Lista equipaggio count: " + Lista.Count);
                    return Lista;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                List<Utente> Lista = new List<Utente>();
                return Lista;
            }

        }

        public List<Utente> GetUsers()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Utente> Lista = new List<Utente>();
                    cmd1.CommandText = "SELECT * FROM Utenti";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            Utente u = new Utente();
                            u.Passaporto = reader.GetString(0);
                            u.Nome = reader.GetString(1);
                            u.Cognome = reader.GetString(2);
                            u.Nascita = reader.GetDateTime(3);
                            if (reader.IsDBNull(4))
                            {
                                u.Mail = "";
                            }
                            else
                            {
                                u.Mail = reader.GetString(4);
                            }
                            u.Tipo = reader.GetInt32(7);
                            if (reader.IsDBNull(4))
                            {
                                u.Credito = 0;
                            }
                            else
                            {
                                u.Credito = reader.GetDouble(8);
                            }
                            


                            Lista.Add(u);
                        }
                    Console.WriteLine("LIsta utenti count: " + Lista.Count);
                    return Lista;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                List<Utente> Lista = new List<Utente>();
                return Lista;
            }

        }

        public List<Lavora> GetJobs()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Lavora> Lista = new List<Lavora>();
                    cmd1.CommandText = "SELECT * FROM Equipaggio";
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
                    Console.WriteLine("LIsta lavori count: " + Lista.Count);
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


        public List<Voli> GetVoli()
        {
            try
            {

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Voli> Lista = new List<Voli>();
                    cmd1.CommandText = "SELECT * FROM Voli";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            Lista.Add(new Voli()
                            {
                                Id_volo = reader.GetString(0),
                                Tratta = reader.GetString(1),
                                Data = reader.GetDateTime(2),
                                Prezzo = reader.GetDouble(3),

                            });
                        }
                    Console.WriteLine("LIsta voli count: "+ Lista.Count);
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

        public Utente LoginCrew(string passport)
        {
            try
            {
                Utente u = new Utente();
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();

                using (SqlCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = "SELECT * FROM Utenti WHERE Passaporto= '" + passport + "' AND Tipo=1;";
                    using (SqlDataReader reader2 = cmd2.ExecuteReader())

                        while (reader2.Read())
                        {
                            u.Passaporto = reader2.GetString(0);
                            u.Nome = reader2.GetString(1);
                            u.Cognome = reader2.GetString(2);
                            u.Nascita = reader2.GetDateTime(3);
                            u.Mail = reader2.GetString(4);
                            u.Tipo = reader2.GetInt32(7);
                            u.Credito = reader2.GetDouble(8);
                        }
                }
                Console.WriteLine("Crew logged: "+u.Passaporto);
                return u;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public bool RemoveAirport(string code)
        {
            try
            {
                bool ok = false;

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DELETE FROM Aeroporti WHERE Codice='" + code + "' AND Codice NOT IN(SELECT Aeroporto1 FROM Tratte ) AND Codice NOT IN (SELECT Aeroporto2 FROM Tratte )";
                    var result = cmd1.ExecuteNonQuery();
                    if (result == 1)
                    {

                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }

                }
                Console.WriteLine("Aeroporto rimosso " + ok);
                return ok;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        public bool RemovePath(string code)
        {
            try
            {
                bool ok = false;

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DELETE FROM Tratte WHERE Id_Tratta='" + code + "' AND Id_Tratta NOT IN(SELECT Tratta FROM Voli)";
                    var result = cmd1.ExecuteNonQuery();
                    if (result == 1)
                    {

                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }

                }
                Console.WriteLine("Tratta rimossa " + ok);
                return ok;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public bool RemoveUser(string code)
        {
            try
            {
                bool ok = false;

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DELETE FROM Utenti WHERE Passaporto = '" + code + "' AND Passaporto NOT IN(SELECT PassaportoPasseggero FROM Biglietti) AND Passaporto NOT IN (SELECT Buyer FROM  Biglietti)";
                    var result = cmd1.ExecuteNonQuery();
                    if (result == 1)
                    {

                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }

                }
                Console.WriteLine("Utente rimosso " + ok);
                return ok;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        public bool RemoveFlight(string code)
        {
            try
            {
                bool ok = false;

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DELETE FROM Voli WHERE Id_volo ='" + code + "' AND Id_volo NOT IN(SELECT CodVolo FROM Biglietti)";
                    var result = cmd1.ExecuteNonQuery();
                    if (result == 1)
                    {

                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }

                }
                Console.WriteLine("Volo rimosso " + ok);
                return ok;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public bool RemoveJob(string pp, string volo)
        {
            try
            {
                bool ok = false;

                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DELETE FROM Equipaggio WHERE Passaporto = '" + pp + "' AND Volo = '" + volo + "'";
                    var result = cmd1.ExecuteNonQuery();
                    if (result == 1)
                    {

                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }

                }
                Console.WriteLine("Lavoro rimosso " + ok);
                return ok;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

    }

}
