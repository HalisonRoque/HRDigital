using System.Globalization;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using webApi.Context;
using webApi.Features.Models;

namespace webApi.Features.Services;

public class ECDImportService
{
    private readonly AppDbContext _context;

    private Empresa? _empresaAtual;

    private readonly List<ControleTributo> _lote = new(10000);

    public ECDImportService(AppDbContext context)
    {
        _context = context;

        _context.ChangeTracker.AutoDetectChangesEnabled = false;

        _context.Database.SetCommandTimeout(0);
    }

    public async Task<int> ImportarArquivoAsync(IFormFile arquivo)
    {
        try
        {
            using var stream = new StreamReader(
                arquivo.OpenReadStream(),
                bufferSize: 1024 * 1024
            );

            int contador = 0;

            while (!stream.EndOfStream)
            {
                var linha = await stream.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(linha))
                    continue;

                try
                {
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
                            ProcessarI050(colunas);
                            break;

                        case "I051":
                            ProcessarI051(colunas);
                            break;

                        case "I150":
                            ProcessarI150(colunas);
                            break;

                        case "I155":
                            ProcessarI155(colunas);
                            break;

                        case "I200":
                            ProcessarI200(colunas);
                            break;

                        case "I250":
                            ProcessarI250(colunas);
                            break;
                    }

                    contador++;

                    if (_lote.Count >= 10000)
                    {
                        await SalvarLoteAsync();

                        Console.WriteLine(
                            $"PROCESSADOS: {contador:N0}"
                        );
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERRO NA LINHA:");
                    Console.WriteLine(linha);
                    Console.WriteLine(ex.Message);
                }
            }

            if (_lote.Count > 0)
            {
                await SalvarLoteAsync();
            }

            Console.WriteLine(
                $"FINALIZADO: {contador:N0}"
            );

            return contador;
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERRO GERAL");
            Console.WriteLine(ex.Message);

            throw;
        }
    }

    private async Task SalvarLoteAsync()
    {
        await _context.BulkInsertAsync(
            _lote,
            new BulkConfig
            {
                BatchSize = 10000,
                PreserveInsertOrder = false,
                SetOutputIdentity = false,
                TrackingEntities = false
            }
        );

        _lote.Clear();

        _context.ChangeTracker.Clear();

        GC.Collect();
    }

    private async Task Processar0000(string[] colunas)
    {
        var empresa = new Empresa
        {
            CNPJ = GetValue(colunas, 6),
            DataAbertura = ParseDate(GetValue(colunas, 3)),
            Bairro = GetValue(colunas, 8),
            Municipio = GetValue(colunas, 7),
            Estado = GetValue(colunas, 7),
            RazaoSocial = GetValue(colunas, 5),
            Email = GetValue(colunas, 8),
            NomeFantasia = GetValue(colunas, 11),
            Telefone = GetValue(colunas, 10),
            TelefoneAdicional = GetValue(colunas, 11),
            SituacaoCadastral = GetValue(colunas, 13),
            MotivoSituacaoCadastral = GetValue(colunas, 13),
            SituacaoEspecial = GetValue(colunas, 14),
            AtividadePrincipal = GetValue(colunas, 2),
            Endereco = GetValue(colunas, 17),
            Numero = GetValue(colunas, 17),
            Complemento = GetValue(colunas, 19),
            Ativo = ParseBool(GetValue(colunas, 13)),
        };

        await _context.Empresas.AddAsync(empresa);

        await _context.SaveChangesAsync();

        _empresaAtual = empresa;

        _context.ChangeTracker.Clear();
    }

    private void ProcessarI050(string[] colunas)
    {
        _lote.Add(new ControleTributo
        {
            CodigoContabil = GetValue(colunas, 1),
            EmpresaId = _empresaAtual!.Id
        });
    }

    private void ProcessarI051(string[] colunas)
    {
        _lote.Add(new ControleTributo
        {
            CodigoContabil = GetValue(colunas, 1),
            CodigoContabilOriginal = GetValue(colunas, 2),
            EmpresaId = _empresaAtual!.Id
        });
    }

    private void ProcessarI150(string[] colunas)
    {
        _lote.Add(new ControleTributo
        {
            CodigoContabil = GetValue(colunas, 1),
            DataRecolhimento = ParseDate(GetValue(colunas, 2)),
            Vencimento = ParseDate(GetValue(colunas, 3)),
            EmpresaId = _empresaAtual!.Id
        });
    }

    private void ProcessarI155(string[] colunas)
    {
        _lote.Add(new ControleTributo
        {
            CodigoContabil = GetValue(colunas, 1),

            ContaContraPartida = GetValue(colunas, 2),

            Debitos = GetValue(colunas, 5) == "D"
                ? ParseDecimal(GetValue(colunas, 4))
                : 0,

            Creditos = GetValue(colunas, 5) == "C"
                ? ParseDecimal(GetValue(colunas, 4))
                : 0,

            SaldoInicialConta =
                ParseDecimal(GetValue(colunas, 6)),

            SaldoFinalConta =
                ParseDecimal(GetValue(colunas, 8)),

            SaldoFinalReal =
                ParseDecimal(GetValue(colunas, 8)),

            TipoLancamento = GetValue(colunas, 9),

            EmpresaId = _empresaAtual!.Id
        });
    }

    private void ProcessarI200(string[] colunas)
    {
        _lote.Add(new ControleTributo
        {
            CodigoContabil = GetValue(colunas, 1),

            DataLancamento =
                ParseDate(GetValue(colunas, 2)),

            EmpresaId = _empresaAtual!.Id
        });
    }

    private void ProcessarI250(string[] colunas)
    {
        _lote.Add(new ControleTributo
        {
            CodigoContabil = GetValue(colunas, 1),

            ValorPRovisaoRecolher =
                ParseDecimal(GetValue(colunas, 3)),

            Debitos = GetValue(colunas, 5) == "D"
                ? ParseDecimal(GetValue(colunas, 4))
                : 0,

            Creditos = GetValue(colunas, 5) == "C"
                ? ParseDecimal(GetValue(colunas, 4))
                : 0,

            TipoLancamento = GetValue(colunas, 5),

            Historico = GetValue(colunas, 8),

            EmpresaId = _empresaAtual!.Id
        });
    }

    private string GetValue(string[] colunas, int index)
    {
        if (index >= colunas.Length)
            return string.Empty;

        return colunas[index];
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
