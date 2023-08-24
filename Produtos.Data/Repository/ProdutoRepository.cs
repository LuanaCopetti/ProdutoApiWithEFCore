using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Produtos.Data.Context;
using Produtos.Data.Helpers;
using Produtos.Domain.Entities;
using Produtos.Domain.Dto;
using Produtos.Domain.Interfaces;

namespace Produtos.Data.Repository;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ProdutoDbContext _context;

    public ProdutoRepository(ProdutoDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Produto>> GetAllAsync() => 
        await _context.Produtos.ToListAsync();

    public async Task<IList<Produto>> FilterByAsync(
        Expression<Func<Produto, bool>> predicate) => 
        await _context.Produtos.Where(predicate).ToListAsync();

    public async Task<Produto?> GetByIdAsync(int id) =>
        await _context.Produtos.FindAsync(id) ?? null;

    public async Task<Produto> CreateAsync(CreateProduto produto)
    {
        produto.Validate();
        var newProduto = new Produto
        {
            Nome = produto.Nome,
            Preco = produto.Preco,
            Quantidade = produto.Quantidade
        };
        var entity = await _context.Produtos.AddAsync(newProduto);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<Produto> UpdateAsync(int id, UpdateProduto produto)
    {
        produto.Validate();
        var entity = await GetByIdAsync(id);
        if (entity == null) { throw new ArgumentException("Produto não encontrado", nameof(id)); }
        entity.Nome = produto.Nome;
        entity.Preco = produto.Preco;
        entity.Quantidade = produto.Quantidade;
        _context.Produtos.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) { throw new ArgumentException("Produto não encontrado", nameof(id)); }
        _context.Produtos.Remove(entity);
        await _context.SaveChangesAsync();
    }
}