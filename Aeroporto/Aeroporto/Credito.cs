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
    public class Credito
    {
        public Credito() { }

        public Credito(string passport, int credit)
        {
            this.Proprietario = passport;
            this.Saldo = credit;
        }
        [DataMember]
        public string Proprietario { get; set; }
        [DataMember]
        public int Saldo { get; set; }

        public static List<Credito> LCredito()
        {
            try
            {
                string connectionstring = "Server = localhost; Database = Aviazione; Trusted_Connection = true;User id=Alex;Password=SQLAlex.22";
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Credito> Lista = new List<Credito>();
                    cmd1.CommandText = "SELECT * FROM dbo.Credito";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {


                            Lista.Add(new Credito()
                            {
                                Proprietario = reader.GetString(0),
                                Saldo = reader.GetInt32(1),
                               

                            });
                        }
                    return Lista;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                List<Credito> Lista = new List<Credito>();
                return Lista;
            }
        }
    }
}
