using Microsoft.AspNetCore.Mvc;
using webApi.Features.DTOs;
using webApi.Features.Services;

namespace webApi.Features.Controllers
{
    [ApiController]
    [Route("api/ecd")]
    public class ECDImportController : ControllerBase
    {
        private readonly ECDImportService _ecdImportService;

        public ECDImportController(ECDImportService service)
        {
            _ecdImportService = service;
        }

        [HttpPost("upload")]
        [RequestSizeLimit(2147483648)]
        public async Task<IActionResult> Upload([FromForm] UploadArquivoDto dto)
        {
            if (dto.Arquivo == null || dto.Arquivo.Length == 0)
                return BadRequest("Arquivo inválido.");

            var total = await _ecdImportService.ImportarArquivoAsync(dto.Arquivo);

            return Ok(new
            {
                mensagem = "Arquivo importado com sucesso.",
                registros = total
            });
        }
    }
}