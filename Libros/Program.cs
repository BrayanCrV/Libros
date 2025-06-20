using Libros.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Si está vacía o nula, entonces la leemos de la variable de entorno "MY_SQL_CONNECTION"
if (string.IsNullOrEmpty(connectionString))
{
    connectionString = Environment.GetEnvironmentVariable("MY_SQL_CONNECTION");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new Exception("No se encontró la cadena de conexión en appsettings.json ni en la variable de entorno MY_SQL_CONNECTION");
    }
}

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

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
