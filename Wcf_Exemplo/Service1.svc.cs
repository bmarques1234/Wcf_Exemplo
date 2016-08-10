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
using Wcf_Exemplo.DTO;

namespace Wcf_Exemplo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        ClientesEntities _context = new ClientesEntities();

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

        public List<ClienteBag> SearchClient()
        {
            try
            {
                var clientes = (from c in _context.Clientes
                                select new ClienteBag()
                                {
                                    Cidade = c.Cidade,
                                    Endereco = c.Endereco,
                                    Estado = c.Estado,
                                    Id = c.Id,
                                    Nome = c.Nome,
                                    Obs = c.Obs,
                                    Telefone = c.Telefone/*,
                                    Contatos = c.Contatos.Select(x => new ContatoBag() 
                                    { 
                                        Cliente = x.Cliente,
                                        Email = x.Email,
                                        Id = x.Id,
                                        Nome = x.Nome,
                                        Telefone = x.Telefone
                                    })*/
                                }).ToList();
                return clientes;
            }
            catch(CommunicationException e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public bool UpdateClient()
        {
            _context.SaveChanges();
            return true;
        }

        public ContatoBag CreateContact(object cliObj)
        {
            Clientes cli = (Clientes)cliObj;
            Contatos con = new Contatos() { Cliente = cli.Id, Nome = "Não identificado" };
            cli.Contatos.Add(con);
            var contato = new ContatoBag()
                           {
                               Cliente = cli.Id,
                               Email = con.Email,
                               Id = con.Id,
                               Nome = con.Nome,
                               Telefone = con.Telefone
                           };
            return contato;
        }

        public bool DeleteContact(object cliObj, object conObj)
        {
            Clientes cli = (Clientes)cliObj;
            Contatos con = (Contatos)conObj;
            cli.Contatos.Remove(con);
            return true;
        }

        public List<ContatoBag> SearchContact(object cliObj)
        {
            ClienteBag cli = (ClienteBag)cliObj;
            var contatos = (from c in _context.Contatos
                            where c.Cliente == cli.Id
                            select new ContatoBag()
                            {
                                Cliente = c.Cliente,
                                Email = c.Email,
                                Id = c.Id,
                                Nome = c.Nome,
                                Telefone = c.Telefone
                            }).ToList();
            return contatos;
        }
    }
}
