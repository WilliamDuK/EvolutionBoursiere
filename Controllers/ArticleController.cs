using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvolutionBoursiere.Infrastructure.Data;
using EvolutionBoursiere.Core.Entities;
using EvolutionBoursiere.Core.Interfaces;
using EvolutionBoursiere.Core.Entities.Articles.Models;
using EvolutionBoursiere.Core.Entities.Articles.Api;

namespace EvolutionBoursiere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleContext _context;
        private readonly ILogger _logger;
        private readonly HttpRequeteController _http;
        private readonly IArticlesApiService _service;

        public ArticleController(ArticleContext context, ILogger<ArticleController> logger, HttpRequeteController http, IArticlesApiService service)
        {
            _context = context;
            _logger = logger;
            _http = http;
            _service = service;
        }

        /// <summary>
        /// Obtenir tous les articles.
        /// </summary>
        /// <returns>Tous les articles</returns>
        /// <response code="204">Retourne succès sans contenu</response>
        /// <response code="400">Si le DbSet Articles est nul</response>
        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            if (!ArticlesExists())
            {
                _logger.LogCritical("L'ensemble 'ArticleContext.Articles' est nul.");
                return Problem("L'ensemble 'ArticleContext.Articles' est nul.");
            }

            // TODO: La configuration doit être obtenu via l'interface frontend.
            ArticlesApiConfiguration config = new ArticlesApiConfiguration()
            {
                q = "recall OR \"safety concern*\"",
                title = "tesla OR TSLA OR \"General M?tors\" OR GM",
                from = "2023-03-01",
                paywall = false,
                size = 123,
                showReprints = true,
                personWikidataId = new List<string>() { "123", "456", "789" },
                personName = new List<string>() { "abc", "def", "ghi" },
                maxDistance = 999,
                lat = (float)134.45E-2f,
                lon = (float)10.020f
            };

            ArticlesApiResponse articlesResponse = await _service.GetArticles(config);
            foreach (var article in articlesResponse.articles)
            {
                _context.Articles.Add(article);
            }
            await _context.SaveChangesAsync();
            await PostHttpRequete("GET", "/Article", config);

            _logger.LogInformation("Obtention de tous les articles.");
            return NoContent();
        }

        /// <summary>
        /// Supprimer tous les articles.
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Retourne succès sans contenu</response>
        /// <response code="400">Si le DbSet Articles est nul</response>
        [HttpDelete]
        public async Task<IActionResult> DeleteArticles()
        {
            if (!ArticlesExists())
            {
                _logger.LogCritical("L'ensemble 'ArticleContext.Articles' est nul.");
                return Problem("L'ensemble 'ArticleContext.Articles' est nul.");
            }

            // FIXME: System.InvalidOperationException: No suitable constructor was found for entity type 'Category'. The following constructors had parameters that could not be bound to properties of the entity type: Cannot bind 'value' in 'Category(string value). Note that only mapped properties can be bound to constructor parameters. Navigations to related entities, including references to owned types, cannot be bound.
            var articles = await _context.Articles
                .ToListAsync();
            foreach (var article in articles)
            {
                _context.Articles.Remove(article);
            }

            await _context.SaveChangesAsync();
            await PostHttpRequete("DELETE", $"/Article", new {});

            _logger.LogInformation($"Les articles ont été supprimées.");
            return NoContent();
        }

        private bool ArticlesExists()
        {
            return _context.Articles != null;
        }

        private async Task PostHttpRequete(string method, string path, Object body)
        {
            await _http.Post(new HttpRequete
            {
                Method = method,
                Path = path,
                Host = HttpContext.Request.Host.Host,
                Body = Newtonsoft.Json.JsonConvert.SerializeObject(body),
                CreatedAt = DateTime.Now
            });
        }
    }
}