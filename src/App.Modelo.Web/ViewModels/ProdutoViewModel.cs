using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Modelo.Web.ViewModels
{
    public class ProdutoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é requirido!")]
        [DisplayName("Fornecedor")]
        public Guid FornecedorId { get; set; }

        [Required(ErrorMessage ="O Campo {0} é requirido!")]
        [StringLength(200,ErrorMessage = "O Campo {0} deve possuir entre {2} e {1} caracteres"),MinLength(2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Campo {0} é requirido!")]
        [StringLength(1000, ErrorMessage = "O Campo {0} deve possuir entre {2} e {1} caracteres"), MinLength(2)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
       
        public string Imagem { get; set; }

        [NotMapped]
        public IFormFile ImagemUpload { get; set; }

        [Required(ErrorMessage = "O Campo {0} é requirido!")]
        public decimal Valor { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }        
       
        public FornecedorViewModel Fornecedor { get; set; }
        public IEnumerable<FornecedorViewModel> Fornecedores { get; set; }
    }
}
