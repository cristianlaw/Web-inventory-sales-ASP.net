using Microsoft.EntityFrameworkCore;
using MisMinistritosAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//Aquí se realiza la inyección a SQL por medio del ContextDb / referencia al ContextDb
builder.Services.AddDbContext<ContextDb>      //Se trae el constructor
    (option => option.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"))); //LLave de la cadena en appsettings.json
//Se creara una carpeta Migration despues de aplicar Add-Migration Nombre en la consola/ son cambios graduales a SQL 





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCaching();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();



app.Run();

 