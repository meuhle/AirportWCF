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
    public class Tratte
    {
        public Tratte() { }

        public Tratte(Aeroporti part, Aeroporti dest,float dist)
        {
            this.Partenza = part.Codice;
            this.Destinazione = dest.Codice;

            this.Distance = dist;
            this.Id_tratta = this.Partenza + this.Destinazione;
        }
        public Tratte(Aeroporti part, Aeroporti dest)
        {
            this.Partenza = part.Codice;
            this.Destinazione = dest.Codice;
            double R = 6371e3;
            double x1 = part.Lat * Math.PI / 180;
            double x2 = dest.Lat * Math.PI / 180;
            double phi1 = (part.Lat - dest.Lat) * Math.PI / 180;
            double phi2 = (part.Lon - dest.Lon) * Math.PI / 180;
            double a = Math.Pow(Math.Sin(phi1 / 2), 2) + Math.Pow(Math.Sin(phi2 / 2), 2) * Math.Cos(x1) * Math.Cos(x2);
            double c = 2 * Math.Atan2(Math.Sqrt(a),Math.Sqrt(1-a));
            float dist = (float)(c * R)/1000; //in km
            this.Distance = dist;
            this.Id_tratta = this.Partenza + this.Destinazione;
        }
        public Tratte(String codice,String a1,String a2,float distance)
        {
            this.Id_tratta = codice;
            this.Partenza = a1;
            this.Destinazione = a2;
            this.Distance = distance;
            
        }

        [DataMember]
        public string Id_tratta { get; set; }
        [DataMember]
        public string Partenza { get; set; }
        [DataMember]
        public string Destinazione { get; set; }
        [DataMember]
        public float Distance { get; set; }
        public static List<Tratte> ListTratte()
        {
            try
            {
                string connectionstring = "Server = localhost; Database = Aviazione; Trusted_Connection = true;User id=Alex;Password=SQLAlex.22";
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
                                Distance = reader.GetFloat(3),

                            });
                        }
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

    }
}
