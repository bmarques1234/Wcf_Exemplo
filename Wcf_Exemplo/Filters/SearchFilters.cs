using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wcf_Exemplo.DTO;

namespace Wcf_Exemplo.Filters
{
    public class SearchFilters
    {
        public List<ClienteBag> ObsFilter(List<ClienteBag> clients)
        {
            return (from c in clients
                       where (c.Obs != null) && (c.Obs != "")
                       select c).ToList();
        }

        public List<ClienteBag> ContactFilter(List<ClienteBag> clients)
        {
            return (from c in clients
                    where (c.Contatos.Count() > 0)
                    select c).ToList();
        }

        public List<ClienteBag> CityFilter(List<ClienteBag> clients, string name)
        {
            return (from c in clients
                    where (c.Cidade == name)
                    select c).ToList();
        }

        public List<ClienteBag> EstateFilter(List<ClienteBag> clients, string name)
        {
            return (from c in clients
                    where (c.Estado == name)
                    select c).ToList();
        }

        public List<ClienteBag> NameFilter(List<ClienteBag> clients, string name)
        {
            return (from c in clients
                    where (c.Nome == name)
                    select c).ToList();
        }

        public List<ClienteBag> AutomaticFilter(List<ClienteBag> clients, string name)
        {
            return (from c in clients
                    where (c.Nome == name) || (c.Estado == name) || (c.Cidade == name)
                    select c).ToList();
        }
    }
}