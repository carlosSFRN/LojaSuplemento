using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaSuplemento.Models
{
    [Table("AcessoTipoUsuario")]
    public class AcessoTipoUsuario
    {
        [Display(Name = "Id")]
        [Column("Id")]
        public int Id { get; set; }        

        [Display(Name = "Codigo Acesso")]
        [Column("CodigoAcesso")]
        public string CodigoAcesso { get; set; }

        [Display(Name = "Descrição Acesso")]
        [Column("DescricaoAcesso")]
        public string DescricaoAcesso { get; set; }

        [Display(Name = "TipoAcesso")]
        [ForeignKey("TipoAcesso")]
        public int IdTipoAcesso { get; set; }
        public virtual TipoAcesso TipoAcesso { get; set; }

        [Display(Name = "TipoUsuario")]
        [ForeignKey("TipoUsuario")]
        public int IdTipoUsuario { get; set; }

        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}
