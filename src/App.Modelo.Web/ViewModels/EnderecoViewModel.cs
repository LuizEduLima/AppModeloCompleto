using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Modelo.Web.ViewModels
{
    public class EnderecoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é requirido!")]
        [StringLength(200, ErrorMessage = "O Campo {0} deve possuir entre {2} e {1} caracteres"), MinLength(2)]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O Campo {0} é requirido!")]
        [StringLength(50, ErrorMessage = "O Campo {0} deve possuir entre {2} e {1} caracteres"), MinLength(2)]
        [DisplayName("Número")]
        public string Numero { get; set; }


        [Required(ErrorMessage = "O Campo {0} é requirido!")]
        [StringLength(250, ErrorMessage = "O Campo {0} deve possuir entre {2} e {1} caracteres"), MinLength(2)]
        public string Complemento { get; set; }


        [Required(ErrorMessage = "O Campo {0} é requirido!")]
        [StringLength(8, ErrorMessage = "O Campo {0} deve possuir {1} caracteres")]
        public string Cep { get; set; }


        [Required(ErrorMessage = "O Campo {0} é requirido!")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O Campo {0} é requirido!")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O Campo {0} é requirido!")]
        public string Estado { get; set; }

        [HiddenInput]
        public Guid FornecedorId { get; set; }

    }
}
