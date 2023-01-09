using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto
{
    [DataContract]
    public class Utente
    {
        public Utente() { }

        public Utente(string passaporto, string nome, string cognome, DateTime nascita, string mail, int type, double credito)
        {
            this.Passaporto = passaporto;
            this.Nome = nome;
            this.Cognome = cognome;
            this.Nascita = nascita;
            this.Mail = mail;                        
            this.Tipo = type;
            this.Credito = credito;
        }
        [DataMember]
        public string Passaporto { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Cognome { get; set; }
        [DataMember]
        public DateTime Nascita { get; set; }
        [DataMember]
        public string Mail { get; set; }
        [DataMember]
        public int Tipo { get; set; }
        [DataMember]
        public double Credito {  get; set; }


        public static List<Utente> LUtente()
        {
            try
            {
                string connectionstring = "Server = localhost; Database = Aviazione; Trusted_Connection = true;User id=Alex;Password=SQLAlex.22";
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Utente> Lista = new List<Utente>();
                    cmd1.CommandText = "SELECT * FROM dbo.Utenti";
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
                                Tipo = reader.GetInt16(7),
                                Credito = reader.GetFloat(8),

                            });
                        }
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
    }
}
