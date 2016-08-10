using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wcf_Exemplo.DTO
{
    public class ClienteBag
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public string Obs { get; set; }
        public IEnumerable<ContatoBag> Contatos { get; set; }
    }
}