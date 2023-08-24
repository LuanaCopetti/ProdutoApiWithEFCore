using System.Runtime.InteropServices;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Produtos.Data.Context;
using Produtos.Api.Extensions;
using Produtos.Domain.Dto;
using Produtos.Domain.Entities;
using Produtos.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiProblemDetails();
builder.Services.AddDbContext<ProdutoDbContext>(options =>
{
    options.UseSqlite(config.GetConnectionString("ProdutoDb"));
    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging()
            .EnableDetailedErrors();
});
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseProblemDetails();
app.UseHttpsRedirection();

// API Endpoints

app.MapGet("/produto", async ([FromServices] IProdutoRepository repository) => await repository.GetAllAsync()) 
.WithName("GetProdutos")
.Produces(StatusCodes.Status200OK, typeof(List<Produto>));

app.MapGet("/produto/{id}", async ([FromServices] IProdutoRepository repository, int id) =>
{
    var p = await repository.GetByIdAsync(id);
    return p != null ? Results.Ok(p) : Results.NotFound();
})
.WithName("GetProduto")
.Produces(StatusCodes.Status200OK, typeof(Produto))
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/produto", async ([FromServices] IProdutoRepository repository, CreateProduto produto) =>
{
    var p = await repository.CreateAsync(produto);
    return Results.Created($"GetProduto/{p.Id}", p);
})
.WithName("CreateProduto")
.Produces(StatusCodes.Status201Created, typeof(Produto))
.Produces(StatusCodes.Status400BadRequest);

app.MapPut("/produto/{id}", async ([FromServices] IProdutoRepository repository, int id, UpdateProduto produto) =>
{
    var p = await repository.UpdateAsync(id, produto);
    return Results.Ok(p);
})
.WithName("UpdateProduto")
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status404NotFound);

app.MapDelete("/produto/{id}", async ([FromServices] IProdutoRepository repository, int id) =>
{
    await repository.DeleteAsync(id);
    return Results.NoContent();
})
.WithName("DeleteProduto")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound);

app.Run();
