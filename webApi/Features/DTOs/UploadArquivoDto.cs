using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace webApi.Features.DTOs
{
    public class UploadArquivoDto
    {
        [Required]
        public IFormFile Arquivo { get; set; } = null!;
    }
}