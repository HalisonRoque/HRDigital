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
        public string CodigoContabil { get; set; } = string.Empty;

        public string DescricaoCodigoContabil { get; set; } = string.Empty;

        public int MesCompetencia { get; set; }

        public int AnoCompetencia { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorPRovisaoRecolher { get; set; }

        [MaxLength(50)]
        public string TipoLancamentoPrevisao { get; set; } = string.Empty;

        public string Historico { get; set; } = string.Empty;

        public DateTime? Vencimento { get; set; }

        public DateTime? DataRecolhimento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorRecolhimento { get; set; }

        [MaxLength(50)]
        public string TipoLancamento { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public bool? Ativo { get; set; }

        public int? EsteiraID { get; set; }

        public int? MensagemID { get; set; }

        [MaxLength(50)]
        public string ChaveVinculo { get; set; } = string.Empty;

        public bool? Origem { get; set; }

        public DateTime? DataLancamento { get; set; }

        [MaxLength(150)]
        public string Regra { get; set; } = string.Empty;

        [MaxLength(250)]
        public string TipoRegra { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal SaldoInicialConta { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SaldoFinalConta { get; set; }

        [MaxLength(250)]
        public string Lancamento { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Creditos { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Debitos { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorPagamento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorDesconto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SaldoFinalReal { get; set; }

        [MaxLength(250)]
        public string LancamentoInicial { get; set; } = string.Empty;

        [MaxLength(255)]
        public string ChaveVinculoMatch { get; set; } = string.Empty;

        public string NovoHistorico { get; set; } = string.Empty;

        public bool? Manteve { get; set; } = false;

        public string HistoricoOriginal { get; set; } = string.Empty;

        [MaxLength(255)]
        public string CodigoContabilOriginal { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorAnterior { get; set; }

        [MaxLength(100)]
        public string ContaContraPartida { get; set; } = string.Empty;

        public int UsuarioConciliou { get; set; }

        public int Pontos { get; set; } = 0;

        [MaxLength(255)]
        public string LancamentosRelacionados { get; set; } = string.Empty;

        public int? Arquivo_Id { get; set; }

        public bool? IsSaldoInicial { get; set; } = false;

        public int EmpresaId { get; set; }

        public int? RelatorioId { get; set; }
    }
}