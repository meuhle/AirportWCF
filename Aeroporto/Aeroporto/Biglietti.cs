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
    public class Biglietti
    {
        public Biglietti() { }

        public Biglietti(Utente ut, Voli volo, Utente buy, int classe, double price)
        {
            this.Passaporto = ut.Passaporto;
            this.CodVolo = volo.Id_volo;
            this.Buyer = buy.Passaporto;
            this.Classe = classe;
            this.Prezzo = price;
        }
        public Biglietti(Utente ut, Voli volo, Utente buy, int classe)
        {
            this.Passaporto = ut.Passaporto;
            this.CodVolo = volo.Id_volo;
            this.Buyer = buy.Passaporto;
            this.Classe = classe;
        }
        [DataMember]
        public string Passaporto { get; set; }
        [DataMember]
        public string CodVolo { get; set; }

        [DataMember]
        public string Buyer { get; set; }
        [DataMember]
        public int Classe { get; set; }
        [DataMember]
        public double Prezzo { get; set; }

        public static List<Biglietti> LBiglietti()
        {
            try
            {
                string connectionstring = "Server = localhost; Database = Aviazione; Trusted_Connection = true;User id=Alex;Password=SQLAlex.22";
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    List<Biglietti> Lista = new List<Biglietti>();
                    cmd1.CommandText = "SELECT * FROM dbo.Biglietti";
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                        while (reader.Read())
                        {


                            Lista.Add(new Biglietti()
                            {
                                Passaporto = reader.GetString(0),
                                CodVolo = reader.GetString(1),
                                Buyer = reader.GetString(2),
                                Classe = reader.GetInt32(3),
                                Prezzo = reader.GetDouble(4),
                                
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
    }
}
