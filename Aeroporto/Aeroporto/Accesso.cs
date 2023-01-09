using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Aeroporto
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Accesso" nel codice e nel file di configurazione contemporaneamente.
    public class Accesso : IAccesso
    {
        private readonly string connectionstring = "Server = localhost; Database = Aviazione; Trusted_Connection = true;User id=Alex;Password=SQLAlex.22";

        public Utente Login(string mail, string password)
        {
            try
            {
                Utente u = null;
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DECLARE @responseMessage NVARCHAR(250) DECLARE @userID NVARCHAR(9) EXEC dbo.Accesso @pMail = '" + mail + "',@pPassword = '" + password + "',@userID=@userID OUTPUT,@responseMessage = @responseMessage OUTPUT SELECT @responseMessage,@userID;";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {
                            switch (reader.GetInt32(0))
                            {
                                case 1:
                                    Console.WriteLine("User successfully logged in");
                                    String passport = reader.GetString(1);
                                    using (SqlCommand cmd2 = conn.CreateCommand())
                                    {
                                        cmd2.CommandText = "SELECT * FROM Utenti WHERE Passaporto= '" + passport + "';";
                                        using (SqlDataReader reader2 = cmd2.ExecuteReader())
                                            while (reader2.Read())
                                            {
                                                u.Passaporto = reader2.GetString(0);
                                                u.Nome = reader2.GetString(1);
                                                u.Cognome = reader2.GetString(2);
                                                u.Nascita = reader2.GetDateTime(3);
                                                u.Mail = reader2.GetString(4);
                                                u.Tipo = reader2.GetInt16(7);
                                                u.Credito = reader2.GetFloat(8);
                                            }

                                    }
                                    
                                    break;
                                case 2:
                                    Console.WriteLine("Invalid login");
                                    break;
                                case 3:
                                    Console.WriteLine("Incorrect password");
                                    break;
                            }
                        }
                    return u;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null ;
            }
        }

        public bool Register(Utente ut, String password)
        {
            try
            {
                bool ok = false;
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddUtenti @pPassaporto = '" + ut.Passaporto + "',@pNome = '" + ut.Nome + "',@pCognome = '" + ut.Cognome + "',@pData = '" + ut.Nascita + "',@pMail = '" + ut.Mail + "',@pPassword = '" + password + "',@pTipo = " + ut.Tipo + ",@responseMessage = @responseMessage OUTPUT; ";
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
