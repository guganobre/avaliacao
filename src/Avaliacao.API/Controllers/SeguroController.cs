using Avaliacao.Domain.DTOs;
using Avaliacao.Application.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Avaliacao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguroController : ControllerBase
    {
        private readonly CriarSeguroUseCase _criarSeguroUseCase;
        private readonly ObterSeguroUseCase _obterSeguroUseCase;
        private readonly ListarSegurosUseCase _listarSegurosUseCase;
        private readonly ObterRelatorioMediasUseCase _obterRelatorioMediasUseCase;

        public SeguroController(
            CriarSeguroUseCase criarSeguroUseCase,
            ObterSeguroUseCase obterSeguroUseCase,
            ListarSegurosUseCase listarSegurosUseCase,
            ObterRelatorioMediasUseCase obterRelatorioMediasUseCase)
        {
            _criarSeguroUseCase = criarSeguroUseCase;
            _obterSeguroUseCase = obterSeguroUseCase;
            _listarSegurosUseCase = listarSegurosUseCase;
            _obterRelatorioMediasUseCase = obterRelatorioMediasUseCase;
        }

        /// <summary>
        /// Cria um novo seguro calculando automaticamente os valores
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SeguroResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarSeguroAsync([FromBody] CriarSeguroRequest request)
        {
            try
            {
                var response = await _criarSeguroUseCase.ExecuteAsync(request);
                return CreatedAtAction(nameof(ObterSeguroPorIdAsync), new { id = response.Id }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Lista todos os seguros
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SeguroResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarSegurosAsync()
        {
            var seguros = await _listarSegurosUseCase.ExecuteAsync();
            return Ok(seguros);
        }

        /// <summary>
        /// Obtém um seguro específico por ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(SeguroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterSeguroPorIdAsync(Guid id)
        {
            var seguro = await _obterSeguroUseCase.ExecuteAsync(id);

            if (seguro == null)
                return NotFound(new { error = "Seguro não encontrado" });

            return Ok(seguro);
        }

        /// <summary>
        /// Obtém relatório com as médias aritméticas dos seguros
        /// </summary>
        [HttpGet("relatorio/medias")]
        [ProducesResponseType(typeof(RelatorioMediasResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterRelatorioMediasAsync()
        {
            var relatorio = await _obterRelatorioMediasUseCase.ExecuteAsync();
            return Ok(relatorio);
        }
    }
}