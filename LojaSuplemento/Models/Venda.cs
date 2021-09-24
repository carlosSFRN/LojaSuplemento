using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaSuplemento.Models
{
    public class Venda
    {
        [Key]
        public int IdVenda { get; set; }
        
        [ForeignKey("Produto")]
        public int IdProduto { get; set; }
        
        public virtual Produto Produto { get; set; }
        
        public decimal Preco { get; set; }
        
        public int Quantidade { get; set; }
        
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }

        //TODO -- Referenciar quando houver a tabela de TipoPagamento  
        //public int IdTipoPagamento { get; set; }
        public DateTime DataVenda { get; set; }
    }
}
