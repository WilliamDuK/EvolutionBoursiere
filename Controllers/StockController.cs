using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvolutionBoursiere.Infrastructure.Data;
using EvolutionBoursiere.Core.Entities;
using EvolutionBoursiere.Dtos;

namespace EvolutionBoursiere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly StockContext _context;
        private readonly ILogger _logger;
        private readonly HttpRequeteController _http;

        public StockController(StockContext context, ILogger<StockController> logger, HttpRequeteController http)
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
        /// <response code="400">Si le DbSet Stocks est nul</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockDto>>> GetStocks()
        {
            if (!StocksExists())
            {
                _logger.LogCritical("L'ensemble 'StockContext.Stocks' est nul.");
                return Problem("L'ensemble 'StockContext.Stocks' est nul.");
            }

            var stocks = await _context.Stocks
                .Select(x => StockToDto(x))
                .ToListAsync();
            await PostHttpRequete("GET", "/Stock", new {});

            _logger.LogInformation("Obtention de toutes les côtes boursières.");
            return stocks;
        }

        /// <summary>
        /// Obtenir une côte boursière spécifique.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>La côte boursière par l'id spécifié</returns>
        /// <response code="200">Retourne la côte boursière spécifiée</response>
        /// <response code="400">Si le DbSet Stocks est nul</response>
        /// <response code="404">Si la côte boursière spécifiée n'existe pas</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<StockDto>> GetStock(long id)
        {
            if (!StocksExists())
            {
                _logger.LogCritical("L'ensemble 'StockContext.Stocks' est nul.");
                return Problem("L'ensemble 'StockContext.Stocks' est nul.");
            }

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                _logger.LogError($"La côte boursière avec l'id {id} n'existe pas.");
                return NotFound();
            }
            await PostHttpRequete("GET", $"/Stock/{id}", new {});

            _logger.LogInformation($"Obtention de la côte boursière avec l'id {id}.");
            return StockToDto(stock);
        }

        /// <summary>
        /// Modifier une côte boursière spécifique.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stockDto"></param>
        /// <returns></returns>
        /// <response code="204">Retourne succès sans contenu</response>
        /// <response code="400">Si le DbSet Stocks est nul</response>
        /// <response code="404">Si la côte boursière spécifiée n'existe pas</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock(long id, [FromBody] StockDto stockDto)
        {
            if (!StocksExists())
            {
                _logger.LogCritical("L'ensemble 'StockContext.Stocks' est nul.");
                return Problem("L'ensemble 'StockContext.Stocks' est nul.");
            }

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                _logger.LogError($"La côte boursière avec l'id {id} n'existe pas.");
                return NotFound();
            }

            stock.Titre = stockDto.Titre;
            stock.Description = stockDto.Description;

            _context.Entry(stock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
                {
                    _logger.LogError($"La côte boursière avec l'id {id} n'existe pas.");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            await PostHttpRequete("PUT", $"/Stock/{id}", stockDto);

            _logger.LogInformation($"La côte boursière avec l'id {id} a été modifiée.");
            return NoContent();
        }

        /// <summary>
        /// Créer une côte boursière.
        /// </summary>
        /// <param name="stockDto"></param>
        /// <returns>La côte boursière nouvellement créée</returns>
        /// <remarks>
        /// Requête échantillon:
        ///
        ///     POST /Stock
        ///     {
        ///        "Titre": "Titre #1",
        ///        "Description": "Description #1"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retourne la côte boursière nouvellement créée</response>
        /// <response code="400">Si le DbSet Stocks est nul</response>
        [HttpPost]
        public async Task<ActionResult<StockDto>> PostStock([FromBody] StockDto stockDto)
        {
            if (!StocksExists())
            {
                _logger.LogCritical("L'ensemble 'StockContext.Stocks' est nul.");
                return Problem("L'ensemble 'StockContext.Stocks' est nul.");
            }

            var stock = new Stock
            {
                Titre = stockDto.Titre,
                Description = stockDto.Description
            };

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            await PostHttpRequete("POST", $"/Stock", stockDto);

            _logger.LogInformation($"La côte boursière avec l'id {stock.id} a été créée.");
            return CreatedAtAction(nameof(GetStock), new { id = stock.id }, StockToDto(stock));
        }

        /// <summary>
        /// Supprimer toutes les côtes boursières.
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Retourne succès sans contenu</response>
        /// <response code="400">Si le DbSet Stocks est nul</response>
        [HttpDelete]
        public async Task<IActionResult> DeleteStocks()
        {
            if (!StocksExists())
            {
                _logger.LogCritical("L'ensemble 'StockContext.Stocks' est nul.");
                return Problem("L'ensemble 'StockContext.Stocks' est nul.");
            }

            var stocks = await _context.Stocks
                .ToListAsync();
            foreach (var stock in stocks)
            {
                _context.Stocks.Remove(stock);
            }

            await _context.SaveChangesAsync();
            await PostHttpRequete("DELETE", $"/Stock", new {});

            _logger.LogInformation($"Les côtes boursières ont été supprimées.");
            return NoContent();
        }

        /// <summary>
        /// Supprimer une côte boursière spécifique.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Retourne succès sans contenu</response>
        /// <response code="400">Si le DbSet Stocks est nul</response>
        /// <response code="404">Si la côte boursière spécifiée n'existe pas</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(long id)
        {
            if (!StocksExists())
            {
                _logger.LogCritical("L'ensemble 'StockContext.Stocks' est nul.");
                return Problem("L'ensemble 'StockContext.Stocks' est nul.");
            }

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                _logger.LogError($"La côte boursière avec l'id {id} n'existe pas.");
                return NotFound();
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            await PostHttpRequete("DELETE", $"/Stock/{id}", new {});

            _logger.LogInformation($"La côte boursière avec l'id {id} a été supprimée.");
            return NoContent();
        }

        private bool StockExists(long id)
        {
            return (_context.Stocks?.Any(e => e.id == id)).GetValueOrDefault();
        }

        private bool StocksExists()
        {
            return _context.Stocks != null;
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

        private static StockDto StockToDto(Stock stock) => new StockDto
        {
            Titre = stock.Titre,
            Description = stock.Description
        };
    }
}
