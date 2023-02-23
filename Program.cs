using NuGet.Packaging.Signing;
using System.Net.NetworkInformation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using P620231_API.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        //Agregar la información de conexión contra base de datos
        //tenemos varias formas para lograr esto.
        //1. Agregar la información al archivo appsettings.json. 
        //2. Utilizar User Secrets (funciona localmente y por cuenta) 
        //3. Programar técnica personalizada de encriptación de la cadena

        //Obtenemos información de la cadena conexión almacenada en appsettings.json

        var CnnStrBuilder = new SqlConnectionStringBuilder(
            builder.Configuration.GetConnectionString("CNNSTR"));

        //Definimos una variable local que almacene el ccn string
        string cnnStr = CnnStrBuilder.ConnectionString;

        //definir la ccnstring al proyecto antes de iniciarlo
        builder.Services.AddDbContext<P620231_AutoAppoContext>(options => options.UseSqlServer(cnnStr));


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}