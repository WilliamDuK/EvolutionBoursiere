using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using EvolutionBoursiere.Core.Settings;
using EvolutionBoursiere.Core.Interfaces;
using EvolutionBoursiere.Infrastructure.Data;
using EvolutionBoursiere.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<StockContext>(opt =>
    opt.UseInMemoryDatabase("Bourse"));
builder.Services.AddDbContext<ArticleContext>(opt =>
    opt.UseInMemoryDatabase("Articles"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Stock API",
        Description = "Un ASP.NET Core Web API pour parcourir les côtes boursières"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBService>();
// builder.Services.AddMvc().AddControllersAsServices(); // Permet d'utiliser tous les controlleurs comme des services
builder.Services.AddTransient<EvolutionBoursiere.Controllers.HttpRequeteController, EvolutionBoursiere.Controllers.HttpRequeteController>();
builder.Services.AddSingleton<IArticlesApiService, ArticlesApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// FIXME: app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("Exécution de l'application");
app.Run();

// TODO: Faire les tests unitaires pour la logique d'affaires et la logique de données.
// TODO: Créer une interface Web CRUD pour les côtes boursières et les requêtes HTTP.
// TODO: Implémenter HttpRequete à partir d'une extension de HttpRequest.
// TODO: /!\ Corriger mon architecture! /!\