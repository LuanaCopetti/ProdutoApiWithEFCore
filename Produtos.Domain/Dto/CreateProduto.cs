using Produtos.Domain.Interfaces;

namespace Produtos.Domain.Dto;

public record CreateProduto : IProdutoEntity
{
    public string Nome { get; set; } = null!;
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
}