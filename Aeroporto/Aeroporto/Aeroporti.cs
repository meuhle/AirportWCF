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
    public class Aeroporti
    {
        public Aeroporti() { }
         
        public Aeroporti(string codice, string stato, string citta,string nome, double lat, double lon)
        {
            this.Codice = codice;
            this.Stato = stato;
            this.Citta = citta;
            this.Nome = nome;
            this.Lat = lat;
            this.Lon = lon;
        }

        [DataMember]
        public string Codice { get; set; }
        [DataMember]
        public string Stato { get; set; }
        [DataMember]
        public string Citta { get; set; }
        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public double Lat { get; set; }
        [DataMember]
        public double Lon { get; set; }

        public static List<Aeroporti> LAeroporti()
        {
            try
            {
                string connectionstring = "Server = localhost; Database = Aviazione; Trusted_Connection = true;User id=Alex;Password=SQLAlex.22";
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
                                Lon = reader.GetInt32(4),
                                Lat = reader.GetInt32(5),
                            });
                        }
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

    }
}

