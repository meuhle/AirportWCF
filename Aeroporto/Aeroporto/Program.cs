using Aeroporto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;


namespace ProvaDB
{
    class Program
    {
        //public class WebServiceHost : System.ServiceModel.ServiceHost
        static void Main(string[] args)
        {

            
            Uri baseAddress = new Uri("http://localhost:8080/");
            
            System.ServiceModel.Web.WebServiceHost svcHost = new System.ServiceModel.Web.WebServiceHost(typeof(Airport), baseAddress);

            try
            {
                svcHost.Open();

                Console.WriteLine("Service is running");
                Console.WriteLine("Press enter to quit...");
                Console.ReadLine();

                svcHost.Close();
            }
            catch (CommunicationException cex)
            {
                Console.WriteLine("An exception occurred: {0}", cex.Message);
                svcHost.Abort();
            }
/*
             WebServiceHost host = new WebServiceHost(typeof(Airport),new Uri("http://localhost:8080/"));
             try
             {
                 //ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IAirport), new WebHttpBinding(), "");
                 host.Open();
                 using (ChannelFactory<IAirport> cf = new ChannelFactory<IAirport>(new WebHttpBinding(), "http://localhost:8080"))
                 {
                     cf.Endpoint.Behaviors.Add(new WebHttpBehavior());

                     IAirport channel = cf.CreateChannel();
                     Aeroporti a = new Aeroporti("MXP", "Italy", "Milan", "Malpensa", 45.632283, 8.724629);
                     Aeroporti b = new Aeroporti("LIN", "Italy", "Milan", "Linate", 45.454187, 9.277645);
                     channel.AddAeroporti(a);
                     channel.AddAeroporti(b);
                     Tratte tr1 = new Tratte(a, b);
                     channel.AddTratta(tr1);

                 }

                 Console.WriteLine("Press <ENTER> to terminate");
                 Console.ReadLine();

                 host.Close();
             }
             catch (CommunicationException cex)
             {
                 Console.WriteLine("An exception occurred: {0}", cex.Message);
                 host.Abort();
             }*/
            /*WebServiceHost host = new WebServiceHost(typeof(Airport));
            //ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IAccesso), new WebHttpBinding(), "");
            ServiceDebugBehavior sdb = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            sdb.HttpHelpPageEnabled = false;
            host.Open();
            Console.WriteLine("Service is running");
            Console.WriteLine("Press enter to quit...");
            Console.ReadLine();


            using (ChannelFactory<IAccesso> cf = new ChannelFactory<IAccesso>(new WebHttpBinding(), "http://localhost:8000")) 

            cf.Endpoint.Behaviors.Add(new WebHttpBehavior()); 
            IAccesso channel = .CreateChannel();
            Aeroporti a = new Aeroporti("MXP", "Italy", "Milan", "Malpensa", 45.632283, 8.724629);

            host.Close();*/


            /*WebServiceHost host = new WebServiceHost(typeof(Flight),new Uri("http://localhost:8000/"));
            try
            {
                //ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IFlight), new WebHttpBinding(), "");
                host.Open();
                using (ChannelFactory<IFlight> cf = new ChannelFactory<IFlight>(new WebHttpBinding(), "http://localhost:8000"))
                {
                    cf.Endpoint.Behaviors.Add(new WebHttpBehavior());

                    IFlight channel = cf.CreateChannel();
                    Aeroporti a = new Aeroporti("MXP", "Italy", "Milan", "Malpensa", 45.632283, 8.724629);
                    Aeroporti b = new Aeroporti("LIN", "Italy", "Milan", "Linate", 45.454187, 9.277645);
                    channel.AddAeroporti(a);
                    channel.AddAeroporti(b);
                    Tratte tr1 = new Tratte(a, b);
                    channel.AddTratta(tr1);

                }

                Console.WriteLine("Press <ENTER> to terminate");
                Console.ReadLine();

                host.Close();
            }
            catch (CommunicationException cex)
            {
                Console.WriteLine("An exception occurred Alex stop: {0}", cex.Message);
                host.Abort();
            }*/


        }
    }

}
