using Produtos.Domain.Dto;
using Produtos.Domain.Entities;
using Produtos.Domain.Interfaces;

namespace Produtos.Data.Helpers;

public static class ProdutoHelper
{
    public static void Validate(this IProdutoEntity produto)
    {
        if (produto == null) { throw new ArgumentNullException(nameof(produto)); }
        if (string.IsNullOrWhiteSpace(produto.Nome)) { throw new ArgumentException("Nome não pode ser nulo ou vazio", nameof(produto.Nome)); }
        if (produto.Preco <= 0) { throw new ArgumentException("Preço deve ser maior que zero", nameof(produto.Preco)); }
        if (produto.Quantidade <= 0) { throw new ArgumentException("Quantidade deve ser maior que zero", nameof(produto.Quantidade)); }
    }
}