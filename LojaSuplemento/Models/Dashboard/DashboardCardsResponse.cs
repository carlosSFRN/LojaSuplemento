using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaSuplemento.Models.Dashboard
{
    public class DashboardCardsResponse
    {
        public int Estoque { get; set; }
        public decimal VendasDia { get; set; }
        public decimal VendasMes { get; set; }
        public decimal VendasAno { get; set; }
    }
}
