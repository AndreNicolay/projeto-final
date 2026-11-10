using Api.Models;
using Dapper;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// 1. Connection string (vem do .env, pelo Docker)
var conexao = builder.Configuration.GetConnectionString("Padrao")
    ?? throw new InvalidOperationException(
        "Connection string 'Padrao' não encontrada.");

// 2. CORS: autoriza o Vue a chamar a API
builder.Services.AddCors(opcoes =>
    opcoes.AddDefaultPolicy(politica => politica
        .WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()));

var app = builder.Build();
app.UseCors();

// Rota de teste
app.MapGet("/", () => "API do projeto final no ar!");

// GET /api/produtos: lista todos
app.MapGet("/api/produtos", async () =>
{
    await using var con = new MySqlConnection(conexao);
    return await con.QueryAsync<Produto>(
        "SELECT id, nome, preco FROM produtos ORDER BY nome");
});

// GET /api/produtos/{id}: busca um
app.MapGet("/api/produtos/{id:int}", async (int id) =>
{
    await using var con = new MySqlConnection(conexao);
    var produto = await con.QuerySingleOrDefaultAsync<Produto>(
        "SELECT id, nome, preco FROM produtos WHERE id = @id", new { id });
    return produto is null ? Results.NotFound() : Results.Ok(produto);
});

// POST /api/produtos: cadastra
app.MapPost("/api/produtos", async (Produto produto) =>
{
    await using var con = new MySqlConnection(conexao);
    produto.Id = await con.ExecuteScalarAsync<int>(
        "INSERT INTO produtos (nome, preco) VALUES (@Nome, @Preco);" +
        "SELECT LAST_INSERT_ID();", produto);
    return Results.Created($"/api/produtos/{produto.Id}", produto);
});

// PUT /api/produtos/{id}: altera
app.MapPut("/api/produtos/{id:int}", async (int id, Produto produto) =>
{
    await using var con = new MySqlConnection(conexao);
    var linhas = await con.ExecuteAsync(
        "UPDATE produtos SET nome = @Nome, preco = @Preco WHERE id = @id",
        new { produto.Nome, produto.Preco, id });
    return linhas == 0 ? Results.NotFound() : Results.NoContent();
});

// DELETE /api/produtos/{id}: exclui
app.MapDelete("/api/produtos/{id:int}", async (int id) =>
{
    await using var con = new MySqlConnection(conexao);
    var linhas = await con.ExecuteAsync(
        "DELETE FROM produtos WHERE id = @id", new { id });
    return linhas == 0 ? Results.NotFound() : Results.NoContent();
});

app.Run();
