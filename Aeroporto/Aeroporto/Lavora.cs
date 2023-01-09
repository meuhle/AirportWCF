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
    public class Lavora
    {
        public Lavora() { }

        public Lavora(Voli volo, Utente ut, string ruolo, DateTime data)
        {
            this.Volo = volo.Id_volo;
            this.Passaporto = ut.Passaporto;
            this.Ruolo = ruolo;
            this.Data = data;
        }
        public Lavora(Voli volo, Utente ut, string ruolo)
        {
            this.Volo = volo.Id_volo;
            this.Passaporto = ut.Passaporto;
            this.Ruolo = ruolo;
        }

        [DataMember]
        public string Volo { get; set; }
        [DataMember]
        public string Passaporto { get; set; }
        [DataMember]
        public string Ruolo { get; set; }
        [DataMember]
        public DateTime Data { get; set; }
        public static List<Lavora> ListLavora()
        {
            try
            {
                string connectionstring = "Server = localhost; Database = Aviazione; Trusted_Connection = true;User id=Alex;Password=SQLAlex.22";
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Lavora> Lista = new List<Lavora>();
                    cmd1.CommandText = "SELECT * FROM dbo.Equipaggio";
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
