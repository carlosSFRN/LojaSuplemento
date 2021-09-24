using LojaSuplemento.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaSuplemento.Repositories.IRepository
{
    public interface IDashboardCardsRepository
    {
        public DashboardCardsResponse GetDashboardCards();
    }
}
