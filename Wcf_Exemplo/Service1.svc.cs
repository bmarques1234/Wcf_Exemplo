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
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;
using Wcf_Exemplo.Filters;

namespace Wcf_Exemplo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        ClientesEntities _context = new ClientesEntities();

        public bool CreateClient()
        {
            Clientes cli = new Clientes() { Nome = "Não Identificado" };
            _context.Clientes.Add(cli);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteClient(ClienteBag cliObj)
        {
            //ClienteBag cli = (ClienteBag)cliObj;
            //_context.Clientes.Remove(cli);

            /*Clientes cliente = (Clientes)(from c in _context.Clientes
                          where c.Id == cliObj.Id
                          select c);*/

            //var cli = (Clientes)cliente;
            var cliente = _context.Clientes.Where(c => c.Id == cliObj.Id);
            var cli = cliente.FirstOrDefault<Clientes>();

            _context.Clientes.Remove(cli);
            _context.SaveChanges();
            return true;
        }

        public ClienteBag SearchClientByID(string ID)
        {
            int idnumber = int.Parse(ID);
            var cliente = (from c in _context.Clientes
                           where c.Id == idnumber
                           select new ClienteBag()
                           {
                               Cidade = c.Cidade,
                               Endereco = c.Endereco,
                               Estado = c.Estado,
                               Id = c.Id,
                               Nome = c.Nome,
                               Obs = c.Obs,
                               Telefone = c.Telefone,
                               Contatos = c.Contatos.Select(x => new ContatoBag()
                               {
                                   Cliente = x.Cliente,
                                   Email = x.Email,
                                   Id = x.Id,
                                   Nome = x.Nome,
                                   Telefone = x.Telefone
                               })
                           });
            var cli = cliente.FirstOrDefault<ClienteBag>();
            return cli;
        }

        public List<ClienteBag> SearchClient(string query, string value)
        {
            SearchFilters filters = new SearchFilters();
            try
            {
                List<ClienteBag> clientes = (from c in _context.Clientes
                                            select new ClienteBag()
                                            {
                                                Cidade = c.Cidade,
                                                Endereco = c.Endereco,
                                                Estado = c.Estado,
                                                Id = c.Id,
                                                Nome = c.Nome,
                                                Obs = c.Obs,
                                                Telefone = c.Telefone,
                                                Contatos = c.Contatos.Select(x => new ContatoBag() 
                                                { 
                                                    Cliente = x.Cliente,
                                                    Email = x.Email,
                                                    Id = x.Id,
                                                    Nome = x.Nome,
                                                    Telefone = x.Telefone
                                                })
                                            }).ToList();

                if (value != "" && value != null)
                {
                    if(Regex.IsMatch(query, "cidade"))
                    {
                        clientes = filters.CityFilter(clientes, value);
                    }

                    if (Regex.IsMatch(query, "estado"))
                    {
                        clientes = filters.EstateFilter(clientes, value);
                    }

                    if (Regex.IsMatch(query, "nome"))
                    {
                        clientes = filters.NameFilter(clientes, value);
                    }

                    if (Regex.IsMatch(query, "detectar"))
                    {
                        clientes = filters.AutomaticFilter(clientes, value);
                    }
                }

                if (Regex.IsMatch(query, "obs"))
                {
                    clientes = filters.ObsFilter(clientes);
                }
                if (Regex.IsMatch(query, "contato"))
                {
                    clientes = filters.ContactFilter(clientes);
                }
                
                return clientes;
            }
            catch(CommunicationException e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public bool UpdateClient(ClienteBag cliObj)
        {
            var cliente = _context.Clientes.Where(c => c.Id == cliObj.Id);
            var cli = cliente.FirstOrDefault<Clientes>();
            var contato = _context.Contatos.Where(c => c.Cliente == cliObj.Id).ToList();
            //var con = contato.FirstOrDefault<Contatos>();

            cli.Cidade = cliObj.Cidade;
            cli.Contatos = contato;
            cli.Endereco = cliObj.Endereco;
            cli.Estado = cliObj.Estado;
            cli.Nome = cliObj.Nome;
            cli.Obs = cliObj.Obs;
            cli.Telefone = cliObj.Telefone;

            _context.SaveChanges();
            return true;
        }

        public ContatoBag CreateContact(ClienteBag cliObj)
        {
            //Clientes cli = (Clientes)cliObj;
            Contatos con = new Contatos() { Cliente = cliObj.Id, Nome = "Não identificado" };

            var cliente = _context.Clientes.Where(c => c.Id == cliObj.Id);
            var cli = cliente.FirstOrDefault<Clientes>();

            cli.Contatos.Add(con);
            var contato = new ContatoBag()
                           {
                               Cliente = cliObj.Id,
                               Email = con.Email,
                               Id = con.Id,
                               Nome = con.Nome,
                               Telefone = con.Telefone
                           };
            _context.SaveChanges();
            return contato;
        }

        public bool DeleteContact(ClienteBag cliObj, ContatoBag conObj)
        {
            /*Clientes cli = (Clientes)cliObj;
            Contatos con = (Contatos)conObj;*/

            var cliente = _context.Clientes.Where(c => c.Id == cliObj.Id);
            var cli = cliente.FirstOrDefault<Clientes>();

            var contato = _context.Contatos.Where(c => c.Id == conObj.Id);
            var con = contato.FirstOrDefault<Contatos>();

            //Clientes cli = (Clientes)cliente;
            //Contatos con = (Contatos)contato;
            cli.Contatos.Remove(con);
            _context.SaveChanges();
            return true;
        }

        public List<ContatoBag> SearchContact(ClienteBag cliObj)
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
