using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Core.Services;
using DPA.Reciclaje.CORE.Infrastructure.Data;
using DPA.Reciclaje.CORE.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _configuration = builder.Configuration;
var _connectionString = _configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ReciclaDbContext>(options =>
{
    options.UseSqlServer(_connectionString);

});

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();

// Register categoria services
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<ICategoriaService, CategoriaService>();

// Register producto services
builder.Services.AddTransient<IProductoRepository, ProductoRepository>();
builder.Services.AddTransient<IProductoService, ProductoService>();

// Register carrito services
builder.Services.AddTransient<ICarritoRepository, CarritoRepository>();
builder.Services.AddTransient<ICarritoService, CarritoService>();

// Register favorito services
builder.Services.AddTransient<IFavoritoRepository, FavoritoRepository>();
builder.Services.AddTransient<IFavoritoService, FavoritoService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
