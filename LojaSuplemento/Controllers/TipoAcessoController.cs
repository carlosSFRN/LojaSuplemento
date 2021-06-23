using LojaSuplemento.Controllers;
using LojaSuplemento.Data;
using LojaSuplemento.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaSuplemento.Views
{
    public class TipoAcessoController : BaseController
    {

        private readonly ApplicationDbContext _context;

        public TipoAcessoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoAcessoController
        public async Task<IActionResult> Index()
        {
            //var verificaAutorizacao = await IsUserHasAcess("PaginaPerfilUsuario", _context);

            //if (!verificaAutorizacao)
            //{
            //    return RedirectToAction("Index", "Home");
            //}


            return View(await _context.TipoAcesso.ToListAsync());
        }

        // GET: TipoAcessoController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAcesso = await _context.TipoAcesso
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoAcesso == null)
            {
                return NotFound();
            }

            return View(tipoAcesso);
        }

        // GET: TipoAcessoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoAcessoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoAcessoDescricao")] TipoAcesso tipoAcesso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoAcesso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoAcesso);
        }

        // GET: TipoAcessoController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAcesso = await _context.TipoAcesso.FindAsync(id);
            if (tipoAcesso == null)
            {
                return NotFound();
            }
            return View(tipoAcesso);
        }

        // POST: TipoAcessoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoAcessoDescricao")] TipoAcesso tipoAcesso)
        {
            if (id != tipoAcesso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoAcesso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoAcessoExists(tipoAcesso.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoAcesso);
        }

        // GET: TipoAcessoController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAcesso = await _context.TipoAcesso
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoAcesso == null)
            {
                return NotFound();
            }

            return View(tipoAcesso);
        }

        // POST: TipoAcessoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoAcesso = await _context.TipoAcesso.FindAsync(id);
            _context.TipoAcesso.Remove(tipoAcesso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoAcessoExists(int id)
        {
            return _context.TipoAcesso.Any(e => e.Id == id);
        }
    }
}
