using Microsoft.EntityFrameworkCore;
using Produtos.Data.Context;
using Produtos.Data.Repository;
using Produtos.Domain.Interfaces;

namespace Produtos.Api.Extensions;

public static class ServicesExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
    }
}