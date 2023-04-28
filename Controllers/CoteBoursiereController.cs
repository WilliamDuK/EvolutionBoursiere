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

namespace EvolutionBoursiere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoteBoursiereController : ControllerBase
    {
        private readonly CoteContext _context;
        private readonly ILogger _logger;
        private readonly HttpRequeteController _http;

        public CoteBoursiereController(CoteContext context, ILogger<CoteBoursiereController> logger, HttpRequeteController http)
        {
            _context = context;
            _logger = logger;
            _http = http;
        }

        /// <summary>
        /// Obtenir toutes les côtes boursières.
        /// </summary>
        /// <returns>Toutes les côtes boursières</returns>
        /// <response code="200">Retourne toutes les côtes boursières</response>
        /// <response code="400">Si le DbSet CotesBoursieres est nul</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoteBoursiereDto>>> GetCotesBoursieres()
        {
            if (!CotesBoursieresExists())
            {
                _logger.LogCritical("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
                return Problem("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
            }

            var cotesBoursieres = await _context.CotesBoursieres
                .Select(x => CoteToDto(x))
                .ToListAsync();
            await PostHttpRequete("GET", "/CoteBoursiere", new {});

            _logger.LogInformation("Obtention de toutes les côtes boursières.");
            return cotesBoursieres;
        }

        /// <summary>
        /// Obtenir une côte boursière spécifique.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>La côte boursière par l'id spécifié</returns>
        /// <response code="200">Retourne la côte boursière spécifiée</response>
        /// <response code="400">Si le DbSet CotesBoursieres est nul</response>
        /// <response code="404">Si la côte boursière spécifiée n'existe pas</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<CoteBoursiereDto>> GetCoteBoursiere(long id)
        {
            if (!CotesBoursieresExists())
            {
                _logger.LogCritical("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
                return Problem("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
            }

            var coteBoursiere = await _context.CotesBoursieres.FindAsync(id);
            if (coteBoursiere == null)
            {
                _logger.LogError($"La côte boursière avec l'id {id} n'existe pas.");
                return NotFound();
            }
            await PostHttpRequete("GET", $"/CoteBoursiere/{id}", new {});

            _logger.LogInformation($"Obtention de la côte boursière avec l'id {id}.");
            return CoteToDto(coteBoursiere);
        }

        /// <summary>
        /// Modifier une côte boursière spécifique.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coteBoursiereDto"></param>
        /// <returns></returns>
        /// <response code="204">Retourne succès sans contenu</response>
        /// <response code="400">Si le DbSet CotesBoursieres est nul</response>
        /// <response code="404">Si la côte boursière spécifiée n'existe pas</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoteBoursiere(long id, CoteBoursiereDto coteBoursiereDto)
        {
            if (!CotesBoursieresExists())
            {
                _logger.LogCritical("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
                return Problem("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
            }

            var coteBoursiere = await _context.CotesBoursieres.FindAsync(id);
            if (coteBoursiere == null)
            {
                _logger.LogError($"La côte boursière avec l'id {id} n'existe pas.");
                return NotFound();
            }

            coteBoursiere.Titre = coteBoursiereDto.Titre;
            coteBoursiere.Description = coteBoursiereDto.Description;

            _context.Entry(coteBoursiere).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoteBoursiereExists(id))
                {
                    _logger.LogError($"La côte boursière avec l'id {id} n'existe pas.");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            await PostHttpRequete("PUT", $"/CoteBoursiere/{id}", coteBoursiereDto);

            _logger.LogInformation($"La côte boursière avec l'id {id} a été modifiée.");
            return NoContent();
        }

        /// <summary>
        /// Créer une côte boursière.
        /// </summary>
        /// <param name="coteBoursiereDto"></param>
        /// <returns>La côte boursière nouvellement créée</returns>
        /// <remarks>
        /// Requête échantillon:
        ///
        ///     POST /CoteBoursiere
        ///     {
        ///        "Titre": "Titre #1",
        ///        "Description": "Description #1"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retourne la côte boursière nouvellement créée</response>
        /// <response code="400">Si le DbSet CotesBoursieres est nul</response>
        [HttpPost]
        public async Task<ActionResult<CoteBoursiereDto>> PostCoteBoursiere(CoteBoursiereDto coteBoursiereDto)
        {
            if (!CotesBoursieresExists())
            {
                _logger.LogCritical("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
                return Problem("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
            }

            var coteBoursiere = new CoteBoursiere
            {
                Titre = coteBoursiereDto.Titre,
                Description = coteBoursiereDto.Description
            };

            _context.CotesBoursieres.Add(coteBoursiere);
            await _context.SaveChangesAsync();
            await PostHttpRequete("POST", $"/CoteBoursiere", coteBoursiereDto);

            _logger.LogInformation($"La côte boursière avec l'id {coteBoursiere.id} a été créée.");
            return CreatedAtAction(nameof(GetCoteBoursiere), new { id = coteBoursiere.id }, CoteToDto(coteBoursiere));
        }

        /// <summary>
        /// Supprimer une côte boursière spécifique.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Retourne succès sans contenu</response>
        /// <response code="400">Si le DbSet CotesBoursieres est nul</response>
        /// <response code="404">Si la côte boursière spécifiée n'existe pas</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoteBoursiere(long id)
        {
            if (!CotesBoursieresExists())
            {
                _logger.LogCritical("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
                return Problem("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
            }

            var coteBoursiere = await _context.CotesBoursieres.FindAsync(id);
            if (coteBoursiere == null)
            {
                _logger.LogError($"La côte boursière avec l'id {id} n'existe pas.");
                return NotFound();
            }

            _context.CotesBoursieres.Remove(coteBoursiere);
            await _context.SaveChangesAsync();
            await PostHttpRequete("DELETE", $"/CoteBoursiere/{id}", new {});

            _logger.LogInformation($"La côte boursière avec l'id {id} a été supprimée.");
            return NoContent();
        }

        private bool CoteBoursiereExists(long id)
        {
            return (_context.CotesBoursieres?.Any(e => e.id == id)).GetValueOrDefault();
        }

        private bool CotesBoursieresExists()
        {
            return _context.CotesBoursieres != null;
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

        private static CoteBoursiereDto CoteToDto(CoteBoursiere coteBoursiere) => new CoteBoursiereDto
        {
            Titre = coteBoursiere.Titre,
            Description = coteBoursiere.Description
        };
    }
}
