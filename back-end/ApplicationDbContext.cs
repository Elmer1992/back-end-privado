using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Entidades;
using Microsoft.EntityFrameworkCore;

namespace back_end
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticulosCategorias>()
                .HasKey(x => new { x.ArticulosId, x.CategoriaId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<AArchivados> AArchivados { get; set; }

        public DbSet<Articulos> Articulos { get; set; }
        public DbSet<ArticulosCategorias> ArticulosCategorias { get; set; }
    }
}
