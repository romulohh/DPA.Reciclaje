using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Core.Services;
using DPA.Reciclaje.CORE.Infrastructure.Data;
using DPA.Reciclaje.CORE.Infrastructure.Repositories;
using DPA.Reciclaje.CORE.Infrastructure.Shared;
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

// Register campaña services
builder.Services.AddTransient<ICampaniaRepository, CampaniaRepository>();
builder.Services.AddTransient<ICampaniaService, CampaniaService>();

// Register favorito services
builder.Services.AddTransient<IFavoritoRepository, FavoritoRepository>();
builder.Services.AddTransient<IFavoritoService, FavoritoService>();

// Register metodo pago services
builder.Services.AddTransient<IMetodoPagoRepository, MetodoPagoRepository>();
builder.Services.AddTransient<IMetodoPagoService, MetodoPagoService>();

// Register pago services
builder.Services.AddTransient<IPagoRepository, PagoRepository>();
builder.Services.AddTransient<IPagoService, PagoService>();

// Register departamento services
builder.Services.AddTransient<IDepartamentoRepository, DepartamentoRepository>();
builder.Services.AddTransient<IDepartamentoService, DepartamentoService>();

// Register provincia services
builder.Services.AddTransient<IProvinciaRepository, ProvinciaRepository>();
builder.Services.AddTransient<IProvinciaService, ProvinciaService>();

// Register distrito services
builder.Services.AddTransient<IDistritoRepository, DistritoRepository>();
builder.Services.AddTransient<IDistritoService, DistritoService>();

// Register JWTService and authentication
builder.Services.AddSharedInfrastructure(_configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder     // .WithOrigins("URL Frontend")
                .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.UseCors("AllowAll");


app.MapControllers();

app.Run();
