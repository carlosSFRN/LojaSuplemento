using LojaSuplemento.Data;
using LojaSuplemento.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaSuplemento.Controllers
{
    public class AcessoTipoUsuarioController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public AcessoTipoUsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: AcessoTipoUsuarios
        public async Task<IActionResult> Index()
        {

            //var temAcesso = await IsUserHasAcess("PaginaAcessoTipoUsuario", _context);

            //if (!temAcesso)
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            var applicationDbContext = _context.AcessoTipoUsuario.Include(a => a.TipoUsuario);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize]
        // GET: AcessoTipoUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acessoTipoUsuario = await _context.AcessoTipoUsuario
                .Include(a => a.TipoUsuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acessoTipoUsuario == null)
            {
                return NotFound();
            }

            return View(acessoTipoUsuario);
        }

        [Authorize]
        // GET: AcessoTipoUsuarios/Create
        public IActionResult Create()
        {
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "Id", "NomeTipoUsuario");
            ViewData["IdTipoAcesso"] = new SelectList(_context.TipoAcesso, "Id", "TipoAcessoDescricao");
            return View();
        }

        // POST: AcessoTipoUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodigoAcesso,DescricaoAcesso,IdTipoAcesso,IdTipoUsuario")] AcessoTipoUsuario acessoTipoUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acessoTipoUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "Id", "NomeTipoUsuario", acessoTipoUsuario.IdTipoUsuario);
            ViewData["IdTipoAcesso"] = new SelectList(_context.TipoAcesso, "Id", "TipoAcessoDescricao", acessoTipoUsuario.IdTipoAcesso);
            return View(acessoTipoUsuario);
        }

        [Authorize]
        // GET: AcessoTipoUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acessoTipoUsuario = await _context.AcessoTipoUsuario.FindAsync(id);
            if (acessoTipoUsuario == null)
            {
                return NotFound();
            }
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "Id", "NomeTipoUsuario", acessoTipoUsuario.IdTipoUsuario);
            ViewData["IdTipoAcesso"] = new SelectList(_context.TipoAcesso, "Id", "TipoAcessoDescricao", acessoTipoUsuario.IdTipoAcesso);
            return View(acessoTipoUsuario);
        }

        // POST: AcessoTipoUsuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodigoAcesso,DescricaoAcesso,IdTipoAcesso,IdTipoUsuario")] AcessoTipoUsuario acessoTipoUsuario)
        {
            if (id != acessoTipoUsuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acessoTipoUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcessoTipoUsuarioExists(acessoTipoUsuario.Id))
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
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "Id", "NomeTipoUsuario", acessoTipoUsuario.IdTipoUsuario);
            ViewData["IdTipoAcesso"] = new SelectList(_context.TipoAcesso, "Id", "TipoAcessoDescricao", acessoTipoUsuario.IdTipoAcesso);
            return View(acessoTipoUsuario);
        }

        [Authorize]
        // GET: AcessoTipoUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acessoTipoUsuario = await _context.AcessoTipoUsuario
                .Include(a => a.TipoUsuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acessoTipoUsuario == null)
            {
                return NotFound();
            }

            return View(acessoTipoUsuario);
        }

        [Authorize]
        // POST: AcessoTipoUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var acessoTipoUsuario = await _context.AcessoTipoUsuario.FindAsync(id);
            _context.AcessoTipoUsuario.Remove(acessoTipoUsuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcessoTipoUsuarioExists(int id)
        {
            return _context.AcessoTipoUsuario.Any(e => e.Id == id);
        }
    }
}
