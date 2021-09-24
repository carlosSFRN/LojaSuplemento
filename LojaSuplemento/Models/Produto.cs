using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaSuplemento.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "'Categoria' é obrigatório")]
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }

        [Required(ErrorMessage = "'Código de barras' é obrigatório")]
        [Display(Name = "Código de Barras")]
        public string CodigoBarras { get; set; }

        [Required(ErrorMessage = "'Título do produto' é obrigatório")]
        [Display(Name = "Titulo do Produto")]
        public string TituloProduto { get; set; }

        [Required(ErrorMessage = "'Descrição do produto' é obrigatório")]
        [Display(Name = "Descrição do Produto")]
        public string DescricaoProduto { get; set; }

        [Required(ErrorMessage = "'Preço do produto' é obrigatório")]
        [Display(Name = "Preço do Produto")]
        public decimal PrecoProduto { get; set; }

        [Required(ErrorMessage = "'Estoque do produto' é obrigatório")]
        [Display(Name = "Estoque")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "'Data de validade início' é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Validade Inicio")]
        public DateTime DataProdutoValidadeInicio { get; set; }

        [Required(ErrorMessage = "'Data de validade Fim' é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Validade Fim")]
        public DateTime DataProdutoValidadeFim { get; set; }

        [Required(ErrorMessage = "'Data de inclusão' é obrigatório")]
        [Display(Name = "Data Inclusão")]
        public DateTime DataProdutoInclusao { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "'Imagem do Produto' é obrigatório")]
        [Display(Name = "Imagem do Produto")]
        public IFormFile ImageUrl { get; set; }


        [Display(Name = "Imagem do Produto")]
        public string ImageUrlPath { get; set; }

        public bool Desativado { get; set; }
    }
}
