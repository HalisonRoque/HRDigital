using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Features.Models
{
    [Table("cliente")]
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome Fantasia é obrigatório.")]
        [MaxLength(250)]
        public string NomeFantasia { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [MaxLength(20)]
        public string CNPJ { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Razão Social é obrigatório.")]
        [MaxLength(350)]
        public string RazaoSocial { get; set; } = string.Empty;

        public DateTime? DataAbertura { get; set; }

        [Required(ErrorMessage = "O Porte é obrigatório.")]
        [MaxLength(100)]
        public string Porte { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Atividade principal é obrigatória.")]
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

        [Required(ErrorMessage = "O Bairro é obrigatório.")]
        [MaxLength(150)]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Cidade é obrigatório.")]
        [MaxLength(150)]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Estado é obrigatório.")]
        [MaxLength(2)]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [MaxLength(10)]
        public string CEP { get; set; } = string.Empty;

        [Required(ErrorMessage = "O E-mail é obrigatório.")]
        [MaxLength(250)]
        public string Email { get; set; } = string.Empty;

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

        public DateTime? ClienteDesde { get; set; }

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public bool? Ativo { get; set; }

        [MaxLength(20)]
        public string InscricaoEstadual { get; set; } = string.Empty;

        [MaxLength(20)]
        public string CodigoMunicipio { get; set; } = string.Empty;

        [MaxLength(20)]
        public string InscricaoMunicipal { get; set; } = string.Empty;

        public int MaximoCNPJImportacao { get; set; }

        public bool? ImportacaoEspecial { get; set; } = false;

        [MaxLength(255)]
        public string TipoAssinatura { get; set; } = string.Empty;

        public bool? ExcecaoImportacaoPlano { get; set; } = false;

        [MaxLength(255)]
        public string ChaveIntegracao { get; set; } = string.Empty;

        [MaxLength(255)]
        public string DominioIntegracao { get; set; } = string.Empty;

        public bool? PossuiIntegracao { get; set; } = false;
    }
}