using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Features.Models
{
    [Table("empresa")]
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [MaxLength(20)]
        public string CNPJ { get; set; } = string.Empty;

        public DateTime? DataAbertura { get; set; }

        [Required(ErrorMessage = "O Bairro é obrigatório.")]
        [MaxLength(150)]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Município é obrigatório.")]
        [MaxLength(150)]
        public string Municipio { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Estado deve ser informado")]
        [MaxLength(2)]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "Razão Social deve ser informado!")]
        [MaxLength(250)]
        public string RazaoSocial { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail deve ser informado!")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nome Fantasia deve ser informado!")]
        [MaxLength(250)]
        public string NomeFantasia { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Porte é obrigatório.")]
        [MaxLength(100)]
        public string Porte { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Telefone { get; set; } = string.Empty;

        [MaxLength(20)]
        public string TelefoneAdicional { get; set; } = string.Empty;

        [MaxLength(250)]
        public string SituacaoCadastral { get; set; } = string.Empty;

        public DateTime? DataSituacaoCadastral { get; set; }

        [MaxLength(500)]
        public string MotivoSituacaoCadastral { get; set; } = string.Empty;

        [MaxLength(250)]
        public string SituacaoEspecial { get; set; } = string.Empty;

        public DateTime? DataSituacaoEspecial { get; set; }

        [Required(ErrorMessage = "Atividade Principal deve ser Informada")]
        [MaxLength(250)]
        public string AtividadePrincipal { get; set; } = string.Empty;

        [MaxLength(250)]
        public string AtividadeSecundaria { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Endereço é obrigatório.")]
        [MaxLength(250)]
        public string Endereco { get; set; } = string.Empty;

        [MaxLength(10)]
        public string Numero { get; set; } = string.Empty;

        [MaxLength(150)]
        public string Complemento { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Cep é obrigatório.")]
        [MaxLength(10)]
        public string Cep { get; set; } = string.Empty;

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public bool? Ativo { get; set; }

        public int? ClienteID { get; set; }

        public bool? UmaContaPorFornecedor { get; set; }

        public bool? AgruparContas { get; set; }

        [MaxLength(100)]
        public string TipoSistema { get; set; } = string.Empty;

        public int? IntegracaoID { get; set; }

        public bool? PermitirCaracterConta { get; set; }

        public bool? ConciliacaoPremium { get; set; }

        public bool? ConciliacaoV2 { get; set; }

        public bool? TransferirSaldo { get; set; }

        public bool? ManterProgresso { get; set; }

        [ForeignKey(nameof(ClienteID))]
        public Cliente? Cliente { get; set; }
    }
}