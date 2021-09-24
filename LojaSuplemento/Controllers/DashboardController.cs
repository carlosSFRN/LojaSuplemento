using LojaSuplemento.Models.Dashboard;
using LojaSuplemento.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaSuplemento.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardCardsRepository _context;

        public DashboardController(IDashboardCardsRepository context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {

            DashboardCardsResponse dashboardCardsResponse = _context.GetDashboardCards();

            return View(dashboardCardsResponse);
        }
    }
}
