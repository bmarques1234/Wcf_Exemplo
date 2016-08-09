using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;

namespace Wcf_Exemplo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        ClientesEntities _context = new ClientesEntities();
        ObservableCollection<Clientes> cliLista = new ObservableCollection<Clientes>();

        public Clientes CreateClient()
        {
            Clientes cli = new Clientes() { Nome = "Não Identificado" };
            _context.Clientes.Add(cli);
            return cli;
        }

        public bool DeleteClient(object cliObj)
        {
            Clientes cli = (Clientes)cliObj;
            _context.Clientes.Remove(cli);
            return true;
        }

        public ObservableCollection<Clientes> SearchClient()
        {
            foreach (var c in _context.Clientes)
            {
                cliLista.Add(c);
            }
            return cliLista;
        }

        public bool UpdateClient()
        {
            _context.SaveChanges();
            return true;
        }

        public Clientes CreateContact(object cliObj)
        {
            Clientes cli = (Clientes)cliObj;
            Contatos con = new Contatos() { Cliente = cli.Id, Nome = "Não identificado" };
            cli.Contatos.Add(con);
            return cli;
        }

        public bool DeleteContact(object cliObj, object conObj)
        {
            Clientes cli = (Clientes)cliObj;
            Contatos con = (Contatos)conObj;
            cli.Contatos.Remove(con);
            return true;
        }

        public List<Contatos> SearchContact(object cliObj)
        {
            Clientes cli = (Clientes)cliObj;
            List<Contatos> ListCon = new List<Contatos>();
            foreach (var c in cli.Contatos)
            {
                ListCon.Add(c);
            }
            return ListCon;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8080/service1");

            // Create the ServiceHost.
            using (ServiceHost host = new ServiceHost(typeof(Service1), baseAddress))
            {
                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();
            }


            /*var uri = new Uri("http://localhost:8888/WcfSelfhostedService");

            var host = new ServiceHost(typeof(Service1), uri);

            //Enable realiable sessions
            var wsHttpBinding = new WSHttpBinding
            {
                ReliableSession = { Enabled = true, Ordered = true }
            };

            //Disable security. Is's ok for now.
            wsHttpBinding.Security.Mode = SecurityMode.None;
            host.AddServiceEndpoint(typeof(IService1), wsHttpBinding, "service");

            //Enable metadata publishing
            var smb = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,
                MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
            };

            //Generate policy information with the metadata that conforms to WS-Policy1.5
            host.Description.Behaviors.Add(smb);

            //Open host
            host.Open();*/
        }
    }
}
