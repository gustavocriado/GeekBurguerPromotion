using GeekBurguer_Promotion_Service.Contracts.Input;
using GeekBurguer_Promotion_Service.Contracts.Output;
using GeekBurguer_Promotion_Service.Promotion.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurguer_Promotion.Controllers
{
    [ApiController]
    [Route("Promotion/api/[controller]")]
    public class PromotionController : ControllerBase
    {
        private readonly ILogger<PromotionController> _logger;
        public PromotionController(ILogger<PromotionController> logger)
        {
            _logger = logger;
        }

        // GET promotion/StoreName
        /// <summary>
        /// Retorna a promoção por nome da loja.
        /// </summary>
        /// <returns>uma Promoção baseado no nome da loja</returns>
        /// <param name="storeName"></param>
        [HttpGet("{storeName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PromotionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromServices] IPromotionService promotionService, [FromRoute] string storeName)
        {
            _logger.LogInformation($"Trying GetByStoreName {storeName}");

            try
            {
                var response = await promotionService.GetByStoreName(storeName);
                if (response == null)
                    return NoContent();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Erro para GetByStoreName{ex}");
                return Problem($"Falha para obter por Nome da loja {storeName}");
            }
        }

        // GET promotion/all
        /// <summary>
        /// lista todas as promoções.
        /// </summary>
        /// <returns>lista promoções</returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<PromotionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromServices] IPromotionService promotionService)
        {
            _logger.LogInformation($"Trying GetAll");
            try
            {
                var response = await promotionService.GetAll();

                if (!response.Any())
                    return NoContent();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Erro para GetAll{ex}");
                return Problem("Falha para obter todos os Índices");
            }
        }

        // GET promotion/crate
        /// <summary>
        /// cria uma promoção.
        /// </summary>
        /// <returns>Um novo item criado</returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromServices] IPromotionService promotionService, [FromBody] PromotionRequest model)
        {
            _logger.LogInformation($"Trying create {model}");
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await promotionService.Create(model);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Erro para criar{ex}");
                return Problem("Falha para criar promoção");
            }
        }

        // GET promotion/update
        /// <summary>
        /// atauliza uma promoção.
        /// </summary>
        /// <returns>Um item atualizado</returns>
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromServices] IPromotionService promotionService, [FromBody] PromotionRequest model)
        {
            _logger.LogInformation($"Trying update {model}");
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await promotionService.Update(model);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Erro para atualizar{ex}");
                return Problem("Falha para atualizar promoção");
            }
        }

        // GET promotion/delete
        /// <summary>
        /// deleta uma promoção.
        /// </summary>
        /// <returns>Um item deletado</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromServices] IPromotionService promotionService, [FromRoute] Guid id)
        {
            _logger.LogInformation($"Trying delete {id}");
            try
            {
                await promotionService.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Erro para deletar{ex}");
                return Problem("Falha para deletar promoção");
            }
        }
    }
}
