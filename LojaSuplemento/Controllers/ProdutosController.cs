using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaSuplemento.Data;
using LojaSuplemento.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace LojaSuplemento.Views
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProdutosController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [Authorize]
        // GET: Produtoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produto.ToListAsync());
        }

        [Authorize]
        // GET: Produtoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [Authorize]
        // GET: Produtoes/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "NomeCategoria");
            return View();
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodigoBarras,TituloProduto,DescricaoProduto,PrecoProduto,Quantidade,DataProdutoValidadeInicio,DataProdutoValidadeFim,DataProdutoInclusao,IdCategoria,Desativado,ImageUrl")] Produto produto)
        {
            if (ModelState.IsValid)
            {

                var imagePrefix = Guid.NewGuid() + "_";

                if (!await UploadFile(produto.ImageUrl, imagePrefix))
                {
                    return View(produto);
                }

                produto.ImageUrlPath = imagePrefix + produto.ImageUrl.FileName;

                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "NomeCategoria", produto.IdCategoria);
            return View(produto);
        }

        [Authorize]
        // GET: Produtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            if (produto.ImageUrlPath != null) 
            {
                ViewBag.ImageUrlPathOld = produto.ImageUrlPath;
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "NomeCategoria");
            return View(produto);
        }

        // POST: Produtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string ImageUrlPathOld, [Bind("Id,CodigoBarras,TituloProduto,DescricaoProduto,PrecoProduto,Quantidade,DataProdutoValidadeInicio,DataProdutoValidadeFim,DataProdutoInclusao,IdCategoria,ImageUrl,ImageUrlPath,ImageUrlPathOld,Desativado")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Caso houver mudanca de imagem
                    DeleteFileOld(ImageUrlPathOld);

                    var imagePrefix = Guid.NewGuid() + "_";

                    if (!await UploadFile(produto.ImageUrl, imagePrefix))
                    {
                        return View(produto);
                    }

                    produto.ImageUrlPath = imagePrefix + produto.ImageUrl.FileName;

                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "NomeCategoria", produto.IdCategoria);
            return View(produto);
        }

        [Authorize]
        // GET: Produtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [Authorize]
        // POST: Produtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.Id == id);
        }

        private async Task<bool> UploadFile(IFormFile file, string imagePrefix)
        {
            if (file.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/produtos", imagePrefix + file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }

        private string RenameFilename(IFormFile file) 
        {
            string extension = Path.GetExtension(file.FileName);
            string fileName = $"monster_nutrition_{DateTime.Now:yyyy'-'MM'-'dd'T'HH':'mm':'ss}{extension}";

            return fileName;
        }

        private void DeleteFileOld(string fileName)
        {
            if (fileName != null) 
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/produtos", fileName);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }
    }
}
