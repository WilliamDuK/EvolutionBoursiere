using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvolutionBoursiere.Infrastructure.Data;
using EvolutionBoursiere.Core.Entities;
using EvolutionBoursiere.Dtos;
using MongoDB.Bson;
using MongoDB.Driver;
using EvolutionBoursiere.Infrastructure.Services;

namespace EvolutionBoursiere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpRequeteController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;
        private readonly ILogger _logger;

        public HttpRequeteController(MongoDBService mongoDBService, ILogger<HttpRequeteController> logger)
        {
            _mongoDBService = mongoDBService;
            _logger = logger;
        }

        /// <summary>
        /// Obtenir toutes les requêtes HTTP.
        /// </summary>
        /// <returns>Toutes les requêtes HTTP</returns>
        /// <response code="200">Retourne toutes les requêtes HTTP</response>
        [HttpGet]
        public async Task<List<HttpRequete>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        /// <summary>
        /// Créer une requête HTTP.
        /// </summary>
        /// <param name="httpRequete"></param>
        /// <returns>La requête HTTP nouvellement créée</returns>
        /// <remarks>
        /// Requête échantillon:
        ///
        ///     POST /HttpRequete
        ///     {
        ///        "Method" = "POST",
        ///        "Path" = "/CoteBoursiere",
        ///        "Host" = "Localhost",
        ///        "Body" = "{"Titre":"Titre #1","Description":"Description #1"}",
        ///        "CreatedAt" = "2023-04-28T18:45:08.599+00:00"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retourne la requête HTTP nouvellement créée</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HttpRequete httpRequete)
        {
            await _mongoDBService.CreateAsync(httpRequete);
            return CreatedAtAction(nameof(Get), new { id = httpRequete.Id }, httpRequete);
        }

        /// <summary>
        /// Supprimer une requête HTTP spécifique.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Retourne succès sans contenu</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }
    }
}
