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
            return View();
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodigoBarras,TituloProduto,DescricaoProduto,PrecoProduto,Quantidade,DataProdutoValidadeInicio,DataProdutoValidadeFim,DataProdutoInclusao,ImageUrl")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                //save image to wwwroot folder
                string wwwrootPath = _hostEnvironment.WebRootPath;
                string FileName = Path.GetFileNameWithoutExtension(produto.ImageUrl.FileName);
                string extension = Path.GetExtension(produto.ImageUrl.FileName);
                FileName = FileName + DateTime.Now.ToString("yymmssfff") + extension;
                produto.ImageUrlPath = FileName;
                string path = Path.Combine(wwwrootPath + "/img/produtos/" + FileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await produto.ImageUrl.CopyToAsync(fileStream);
                }

                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
                ViewBag.ImageUrl = Path.Combine("../../img/produtos/" + produto.ImageUrlPath);
                ViewBag.ImageUrlOld = Path.Combine("/img/produtos/" + produto.ImageUrlPath);
            }
            else
            {
                ViewBag.ImageUrl = Path.Combine("../../img/produtos/placeholder-img.png");
            }
            
            return View(produto);
        }

        // POST: Produtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string ImageUrlOld, [Bind("Id,CodigoBarras,TituloProduto,DescricaoProduto,PrecoProduto,Quantidade,DataProdutoValidadeInicio,DataProdutoValidadeFim,DataProdutoInclusao,ImageUrl,ImageUrlPath")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //delete old img
                    if (ImageUrlOld != null && produto.ImageUrl != null)
                    {
                        FileInfo fi = new FileInfo(_hostEnvironment.WebRootPath + ImageUrlOld);

                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                    }

                    //save image to wwwroot folder
                    if (produto.ImageUrl != null)
                    {
                        string wwwrootPath = _hostEnvironment.WebRootPath;
                        string FileName = Path.GetFileNameWithoutExtension(produto.ImageUrl.FileName);
                        string extension = Path.GetExtension(produto.ImageUrl.FileName);
                        FileName = FileName + DateTime.Now.ToString("yymmssfff") + extension;
                        produto.ImageUrlPath = FileName;
                        string path = wwwrootPath + "/img/produtos/" + FileName;
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await produto.ImageUrl.CopyToAsync(fileStream);
                        }
                    }

                    _context.Update(produto);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
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
    }
}
