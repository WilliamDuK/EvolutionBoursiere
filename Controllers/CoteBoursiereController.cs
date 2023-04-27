using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvolutionBoursiere.Models;

namespace EvolutionBoursiere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoteBoursiereController : ControllerBase
    {
        private readonly CoteContext _context;
        private readonly ILogger _logger;

        public CoteBoursiereController(CoteContext context, ILogger<CoteBoursiereController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtenir toutes les côtes boursières.
        /// </summary>
        /// <returns>Toutes les côtes boursières</returns>
        /// <response code="200">Retourne toutes les côtes boursières</response>
        /// <response code="400">Si le DbSet CotesBoursieres est nul</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoteBoursiereDTO>>> GetCotesBoursieres()
        {
            _logger.LogInformation("Obtention de toutes les côtes boursières.");

            if (!CotesBoursieresExists())
            {
                return Problem("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
            }

            return await _context.CotesBoursieres
                .Select(x => CoteToDTO(x))
                .ToListAsync();
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
        public async Task<ActionResult<CoteBoursiereDTO>> GetCoteBoursiere(long id)
        {
            if (!CotesBoursieresExists())
            {
                return Problem("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
            }

            var coteBoursiere = await _context.CotesBoursieres.FindAsync(id);
            if (coteBoursiere == null)
            {
                return NotFound();
            }

            return CoteToDTO(coteBoursiere);
        }

        /// <summary>
        /// Modifier une côte boursière spécifique.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coteBoursiereDTO"></param>
        /// <returns></returns>
        /// <response code="204">Retourne succès sans contenu</response>
        /// <response code="400">Si le DbSet CotesBoursieres est nul</response>
        /// <response code="404">Si la côte boursière spécifiée n'existe pas</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoteBoursiere(long id, CoteBoursiereDTO coteBoursiereDTO)
        {
            if (!CotesBoursieresExists())
            {
                return Problem("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
            }

            var coteBoursiere = await _context.CotesBoursieres.FindAsync(id);
            if (coteBoursiere == null)
            {
                return NotFound();
            }

            coteBoursiere.Titre = coteBoursiereDTO.Titre;
            coteBoursiere.Description = coteBoursiereDTO.Description;

            _context.Entry(coteBoursiere).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoteBoursiereExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Créer une côte boursière.
        /// </summary>
        /// <param name="coteBoursiereDTO"></param>
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
        public async Task<ActionResult<CoteBoursiereDTO>> PostCoteBoursiere(CoteBoursiereDTO coteBoursiereDTO)
        {
            if (!CotesBoursieresExists())
            {
                return Problem("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
            }

            var coteBoursiere = new CoteBoursiere
            {
                Titre = coteBoursiereDTO.Titre,
                Description = coteBoursiereDTO.Description
            };

            _context.CotesBoursieres.Add(coteBoursiere);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCoteBoursiere), new { id = coteBoursiere.id }, CoteToDTO(coteBoursiere));
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
                return Problem("L'ensemble 'CoteContext.CotesBoursieres' est nul.");
            }

            var coteBoursiere = await _context.CotesBoursieres.FindAsync(id);
            if (coteBoursiere == null)
            {
                return NotFound();
            }

            _context.CotesBoursieres.Remove(coteBoursiere);
            await _context.SaveChangesAsync();

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

        private static CoteBoursiereDTO CoteToDTO(CoteBoursiere coteBoursiere) => new CoteBoursiereDTO
        {
            Titre = coteBoursiere.Titre,
            Description = coteBoursiere.Description
        };
    }
}