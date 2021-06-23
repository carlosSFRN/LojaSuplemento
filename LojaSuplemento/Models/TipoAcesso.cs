using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaSuplemento.Models
{
    public class TipoAcesso
    {
        [Display(Name = "Código")]
        [Column("Id")]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [Column("TipoAcesso")]
        public string TipoAcessoDescricao { get; set; }
    }
}
