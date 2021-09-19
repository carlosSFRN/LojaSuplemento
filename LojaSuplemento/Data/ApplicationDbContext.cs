using System;
using System.Collections.Generic;
using System.Text;
using LojaSuplemento.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LojaSuplemento.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<AcessoTipoUsuario> AcessoTipoUsuario { get; set; }
        public DbSet<PerfilUsuario> PerfilUsuario { get; set; }
        public DbSet<TipoAcesso> TipoAcesso { get; set; }
        public DbSet<IdentityUser> Usuario { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Produto> Produto { get; set; }
    }
}
