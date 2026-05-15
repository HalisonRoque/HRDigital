using System.Globalization;
using webApi.Context;
using webApi.Features.Models;

namespace webApi.Features.Services
{
    public class ECDImportService
    {
        private readonly AppDbContext _context;
        private Empresa? _empresaAtual;

        public ECDImportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task ImportarArquivoAsync(IFormFile arquivo)
        {
            using var stream = new StreamReader(arquivo.OpenReadStream());

            while (!stream.EndOfStream)
            {
                var linha = await stream.ReadLineAsync();

                try
                {
                    if (string.IsNullOrWhiteSpace(linha))
                        continue;

                    var colunas = linha.Split('|');

                    if (colunas.Length < 2)
                        continue;

                    var bloco = colunas[1];

                    switch (bloco)
                    {
                        case "0000":
                            await Processar0000(colunas);
                            break;

                        case "I050":
                            await ProcessarI050(colunas);
                            break;

                        case "I051":
                            await ProcessarI051(colunas);
                            break;

                        case "I150":
                            await ProcessarI150(colunas);
                            break;

                        case "I155":
                            await ProcessarI155(colunas);
                            break;

                        case "I200":
                            await ProcessarI200(colunas);
                            break;

                        case "I250":
                            await ProcessarI250(colunas);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERRO NA LINHA:");
                    Console.WriteLine(linha);
                    Console.WriteLine(ex.Message);
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task Processar0000(string[] colunas)
        {
            var empresa = new Empresa
            {
                CNPJ = colunas[6],
                DataAbertura = ParseDate(colunas[3]),
                Bairro = colunas[8],
                Municipio = colunas[7],
                Estado = colunas[7],
                RazaoSocial = colunas[5],
                Email = colunas[8],
                NomeFantasia = colunas[11],
                Telefone = colunas[10],
                TelefoneAdicional = colunas[11],
                SituacaoCadastral = colunas[13],
                MotivoSituacaoCadastral = colunas[13],
                SituacaoEspecial = colunas[14],
                AtividadePrincipal = colunas[2],
                Endereco = colunas[17],
                Numero = colunas[17],
                Complemento = colunas[19],
                Ativo = ParseBool(colunas[13]),
            };

            await _context.Empresas.AddAsync(empresa);

            await _context.SaveChangesAsync();

            _empresaAtual = empresa;
        }
        private async Task ProcessarI050(string[] colunas)
        {
            var conta = new ControleTributo
            {
                CodigoContabil = colunas[1],
                EmpresaId = _empresaAtual!.Id
            };

            await _context.ControleTributos.AddAsync(conta);
        }

        private async Task ProcessarI051(string[] colunas)
        {
            var conta = new ControleTributo
            {
               CodigoContabil = colunas[1],
               CodigoContabilOriginal = colunas[2],
               EmpresaId = _empresaAtual!.Id
            };

            await _context.ControleTributos.AddAsync(conta);
        }

        private async Task ProcessarI150(string[] colunas)
        {
            
            var conta = new ControleTributo
            {
                CodigoContabil = colunas[1],
                DataRecolhimento = ParseDate(colunas[2]),
                Vencimento = ParseDate(colunas[3]),
                EmpresaId = _empresaAtual!.Id
            };

            await _context.ControleTributos.AddAsync(conta);
        }

        private async Task ProcessarI155(string[] colunas)
        {
            var controle = new ControleTributo
            {
                CodigoContabil = colunas[1],
                ContaContraPartida = colunas[2],
                Debitos = colunas[5] == "D"
                    ? ParseDecimal(colunas[4])
                    : 0,

                Creditos = colunas[5] == "C"
                    ? ParseDecimal(colunas[4])
                    : 0,
                SaldoInicialConta = ParseDecimal(colunas[6]),
                SaldoFinalConta = ParseDecimal(colunas[8]),
                SaldoFinalReal = ParseDecimal(colunas[8]),
                TipoLancamento = colunas[9],
                EmpresaId = _empresaAtual!.Id
            };

            await _context.ControleTributos.AddAsync(controle);
        }

        private async Task ProcessarI200(string[] colunas)
        {
            var lancamento = new ControleTributo
            {
                CodigoContabil = colunas[1],
                DataLancamento = ParseDate(colunas[2]),
                EmpresaId = _empresaAtual!.Id
            };

            await _context.ControleTributos.AddAsync(lancamento);
        }

        private async Task ProcessarI250(string[] colunas)
        {
            var lancamento = new ControleTributo
            {
                CodigoContabil = colunas[1],
                ValorPRovisaoRecolher = ParseDecimal(colunas[3]),
                Historico = colunas[8],
                EmpresaId = _empresaAtual!.Id
            };

            await _context.ControleTributos.AddAsync(lancamento);
        }

        private decimal ParseDecimal(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return 0;

            return decimal.Parse(
                valor.Replace(",", "."),
                CultureInfo.InvariantCulture
            );
        }

        private DateTime? ParseDate(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return null;

            if (DateTime.TryParseExact(
                valor,
                "ddMMyyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var data))
            {
                return data;
            }

            return null;
        }
    
        private bool? ParseBool(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return null;

            valor = valor.Trim().ToUpper();

            return valor switch
            {
                "1" => true,
                "0" => false,
                "S" => true,
                "N" => false,
                _ => null
            };
        }
    }
}