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

        [Display(Name = "Código de Barras")]
        public string CodigoBarras { get; set; }

        [Display(Name = "Titulo do Produto")]
        public string TituloProduto { get; set; }

        [Display(Name = "Descrição do Produto")]
        public string DescricaoProduto { get; set; }

        [Column(TypeName = "Money")]
        [Display(Name = "Preço do Produto")]
        public decimal PrecoProduto { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Validade Inicio")]
        public DateTime DataProdutoValidadeInicio { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Validade Fim")]
        public DateTime DataProdutoValidadeFim { get; set; }

        [Display(Name = "Data Inclusão")]
        public DateTime DataProdutoInclusao { get; set; }
    }
}
