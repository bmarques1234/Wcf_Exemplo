using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wcf_Exemplo.DTO
{
    public class ContatoBag
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int? Cliente { get; set; }
    }
}