using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Modelo.Web.ViewModels
{
    public class FornecedorViewModel
    {
        [Key]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "O Campo {0} é requirido!")]
        [StringLength(200, ErrorMessage = "O Campo {0} deve possuir entre {2} e {1} caracteres"), MinLength(2)]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O Campo {0} é requirido!")]
        [StringLength(14, ErrorMessage = "O Campo {0} deve possuir entre {2} e {1} caracteres"), MinLength(11)]
        public string Documento { get; set; }

        [DisplayName("Tipo")]
        public int TipoFornecedor { get; set; }

        public EnderecoViewModel Endereco { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        [NotMapped]
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
    }
}
