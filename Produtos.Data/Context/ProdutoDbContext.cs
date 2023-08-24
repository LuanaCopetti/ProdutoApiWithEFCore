using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Produtos.Domain.Entities;

namespace Produtos.Data.Context;

public class ProdutoDbContext : DbContext
{
    private readonly IConfiguration configuration;

    public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options, IConfiguration configuration) : base(options)
    {
        this.configuration = configuration;
    }

    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>().HasKey(p => p.Id);
        modelBuilder.Entity<Produto>().Property(p => p.Nome).IsRequired();
        modelBuilder.Entity<Produto>().Property(p => p.Preco).IsRequired();
        modelBuilder.Entity<Produto>().Property(p => p.Quantidade).IsRequired();
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(configuration.GetConnectionString("ProdutoDb"));
        base.OnConfiguring(optionsBuilder);
    }
}