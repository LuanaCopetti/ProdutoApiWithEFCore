namespace Produtos.Domain.Interfaces;

public interface IProdutoEntity
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
}