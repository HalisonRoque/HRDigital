using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Features.Models
{
    [Table("controletributos")]
    public class ControleTributo
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string? CodigoContabil { get; set; }

        public string? DescricaoCodigoContabil { get; set; }

        public int? MesCompetencia { get; set; }

        public int? AnoCompetencia { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ValorPRovisaoRecolher { get; set; }

        [MaxLength(50)]
        public string? TipoLancamentoPrevisao { get; set; }

        public string? Historico { get; set; }

        public DateTime? Vencimento { get; set; }

        public DateTime? DataRecolhimento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ValorRecolhimento { get; set; }

        [MaxLength(50)]
        public string? TipoLancamento { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public bool? Ativo { get; set; }

        public int? EsteiraID { get; set; }

        public int? MensagemID { get; set; }

        [MaxLength(50)]
        public string? ChaveVinculo { get; set; }

        public bool? Origem { get; set; }

        public DateTime? DataLancamento { get; set; }

        [MaxLength(150)]
        public string? Regra { get; set; }

        [MaxLength(250)]
        public string? TipoRegra { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SaldoInicialConta { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SaldoFinalConta { get; set; }

        [MaxLength(250)]
        public string? Lancamento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Creditos { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Debitos { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ValorPagamento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ValorDesconto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SaldoFinalReal { get; set; }

        [MaxLength(250)]
        public string? LancamentoInicial { get; set; }

        [MaxLength(255)]
        public string? ChaveVinculoMatch { get; set; }

        public string? NovoHistorico { get; set; }

        public bool? Manteve { get; set; }

        public string? HistoricoOriginal { get; set; }

        [MaxLength(255)]
        public string? CodigoContabilOriginal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ValorAnterior { get; set; }

        [MaxLength(100)]
        public string? ContaContraPartida { get; set; }

        public int? UsuarioConciliou { get; set; }

        public int? Pontos { get; set; }

        [MaxLength(255)]
        public string? LancamentosRelacionados { get; set; }

        public int? Arquivo_Id { get; set; }

        public bool? IsSaldoInicial { get; set; }

        public int EmpresaId { get; set; }

        public int? RelatorioId { get; set; }
    }
}