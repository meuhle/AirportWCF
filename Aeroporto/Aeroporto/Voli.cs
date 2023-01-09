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
    public class Voli
    {
        public Voli() { }

        public Voli(string tratta,DateTime data, double price)
        {
            this.Tratta = tratta;
            this.Data = data;
            this.Prezzo = price;

            this.Id_volo = this.Tratta + this.Data.ToString("yyyyMMdd");

        }

        [DataMember]
        public string Id_volo { get; set; }
        [DataMember]
        public DateTime Data { get; set; }
        [DataMember]
        public double Prezzo { get; set; }
        [DataMember]
        public string Tratta { get; set; }

        public static List<Voli> LVoli()
        {
            try
            {
                string connectionstring = "Server = localhost; Database = Aviazione; Trusted_Connection = true;User id=Alex;Password=SQLAlex.22";
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Voli> Lista = new List<Voli>();
                    cmd1.CommandText = "SELECT * FROM dbo.Voli3";
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

    }
}
