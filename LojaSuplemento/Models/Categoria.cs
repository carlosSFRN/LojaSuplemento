using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaSuplemento.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Display(Name = "Categoria")]
        public string NomeCategoria { get; set; }
    }
}
