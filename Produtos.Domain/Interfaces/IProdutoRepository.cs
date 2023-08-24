using System.Linq.Expressions;
using Produtos.Domain.Dto;
using Produtos.Domain.Entities;

namespace Produtos.Domain.Interfaces;

public interface IProdutoRepository
{
    Task<IList<Produto>> GetAllAsync();
    Task<IList<Produto>> FilterByAsync(Expression<Func<Produto, bool>> predicate);
    Task<Produto?> GetByIdAsync(int id);
    Task<Produto> CreateAsync(CreateProduto produto);
    Task<Produto> UpdateAsync(int id, UpdateProduto produto);
    Task DeleteAsync(int id);
}