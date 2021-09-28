using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppBascioAspNet.Models
{
    public class Fornecedor : Entity
    {
      

        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoFornecedor TipoFornecedor { get; set; }

        //public Guid EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public bool Ativo { get; set; }

        /* EF RELATIONS*/
        
        public IEnumerable<Produto> Produtos { get; set; }

    }
}
