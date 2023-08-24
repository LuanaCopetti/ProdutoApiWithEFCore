using Produtos.Domain.Interfaces;

namespace Produtos.Domain.Entities;

public record Produto : IProdutoEntity
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
}