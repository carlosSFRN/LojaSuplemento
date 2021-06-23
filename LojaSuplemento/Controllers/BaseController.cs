using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaSuplemento.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LojaSuplemento.Controllers
{
    public class BaseController : Controller
    {
        public async Task<bool> IsUserHasAcess(string codigoAcesso, ApplicationDbContext _context)
        {

            var usuario = User.Identity.Name;

            var temAcesso = await (from TP in _context.TipoUsuario
                                   join AT in _context.AcessoTipoUsuario on TP.Id equals AT.IdTipoUsuario
                                   join PF in _context.PerfilUsuario on TP.Id equals PF.IdTipoUsuario
                                   join US in _context.Usuario on PF.UserId equals US.Id
                                   where AT.CodigoAcesso == codigoAcesso && US.Email == usuario
                                   select new
                                   {
                                       TP.Id
                                   }).AnyAsync();

            return temAcesso;

        }
    }
}
