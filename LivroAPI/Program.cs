using LivroAPI.Data;
using LivroAPI.Services.Autor;
using LivroAPI.Services.Livro;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Implement Interface methods in the service.
builder.Services.AddScoped<IAutorInterface, AutorService>();
builder.Services.AddScoped<ILivroInterface, LivroService>();

// Add DbContext to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // Configure the context to use Microsoft SQL Server.
    options.UseSqlServer(
        // Get the connection string from appsettings.json.
        builder.Configuration.GetConnectionString("DefaultConnection")
        );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
