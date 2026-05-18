# HR Digital - Importador ECD/SPED Contábil

## Descrição

Projeto desenvolvido em .NET 8 para importação e processamento de arquivos ECD/SPED Contábil de alta volumetria.

O sistema realiza:

* Upload de arquivos `.txt`
* Leitura otimizada de arquivos gigantes
* Processamento linha por linha do ECD
* Identificação dos blocos SPED
* Conversão e tratamento de dados
* Persistência massiva em MySQL
* Importação otimizada utilizando Bulk Insert

O projeto foi estruturado para suportar milhões de registros com foco em:

* Alta performance
* Baixo consumo de memória
* Processamento em lote
* Escalabilidade
* Redução de overhead do Entity Framework Core

---

# Tecnologias Utilizadas

## Backend

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* Pomelo EntityFrameworkCore MySQL
* MySqlConnector
* EFCore.BulkExtensions

## Banco de Dados

* MySQL

## Processamento de Arquivos

* IFormFile
* StreamReader
* Processamento em lote (Batch Processing)
* Bulk Insert

---

# Estrutura do Projeto

```txt
Features
 ├── Controllers
 ├── DTOs
 ├── Services
 ├── Models

Context
 ├── AppDbContext
```

---

# Arquitetura de Processamento

O sistema foi desenvolvido utilizando uma arquitetura de importação massiva baseada em batches.

Fluxo do processamento:

```txt
Upload HTTP
   ↓
IFormFile
   ↓
OpenReadStream()
   ↓
StreamReader
   ↓
ReadLineAsync()
   ↓
Split('|')
   ↓
Mapeamento dos blocos SPED
   ↓
List<T> (Batch)
   ↓
BulkInsertAsync()
   ↓
MySQL
```

---

# Estratégias de Performance Implementadas

## Bulk Insert

O sistema utiliza:

```txt
EFCore.BulkExtensions
```

para realizar inserções em massa no MySQL.

Ao invés de executar:

```txt
1 INSERT por registro
```

o sistema envia milhares de registros em lote diretamente ao banco.

Exemplo:

```csharp
await _context.BulkInsertAsync(lote);
```

### Benefícios

* Redução extrema do tempo de importação
* Menor uso de memória
* Menor overhead do Entity Framework
* Melhor throughput de escrita

---

# Processamento em Lote

Os registros são agrupados em memória utilizando:

```csharp
private readonly List<ControleTributo> _lote = new(10000);
```

Quando o lote atinge:

```txt
10.000 registros
```

o sistema realiza o Bulk Insert.

---

# Desativação de Tracking

Para evitar overhead do Entity Framework Core, o tracking foi desativado globalmente:

```csharp
options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
```

E também:

```csharp
_context.ChangeTracker.AutoDetectChangesEnabled = false;
```

### Benefícios

* Menor consumo de RAM
* Menor uso de CPU
* Redução do Garbage Collector
* Melhor performance em processamento massivo

---

# Limpeza de Memória

Após cada lote processado:

```csharp
_context.ChangeTracker.Clear();
GC.Collect();
```

### Objetivo

Evitar crescimento excessivo de memória durante importações gigantes.

---

# Timeout de Banco

O timeout do banco foi configurado para suportar operações massivas:

```csharp
mysqlOptions.CommandTimeout(3600);
```

e:

```csharp
_context.Database.SetCommandTimeout(0);
```

---

# Configuração MySQL para Bulk Load

O projeto utiliza:

```txt
LOAD DATA LOCAL INFILE
```

internamente através do BulkExtensions.

Por isso é necessário habilitar:

## Connection String

```txt
AllowLoadLocalInfile=true
```

## MySQL

```sql
SET GLOBAL local_infile = true;
```

Verificação:

```sql
SHOW GLOBAL VARIABLES LIKE 'local_infile';
```

Resultado esperado:

```txt
ON
```

---

# Principais Tecnologias Explicadas

# IFormFile

Responsável pelo recebimento do arquivo via HTTP.

Exemplo:

```csharp
public async Task ImportarArquivoAsync(IFormFile arquivo)
```

---

# StreamReader

Utilizado para leitura eficiente do arquivo texto.

Exemplo:

```csharp
using var stream = new StreamReader(
    arquivo.OpenReadStream(),
    bufferSize: 1024 * 1024
);
```

O buffer aumentado melhora a performance de leitura para arquivos grandes.

---

# Entity Framework Core

Utilizado como ORM principal da aplicação.

Porém, o processamento massivo evita operações tradicionais como:

```csharp
AddAsync()
SaveChanges()
```

em milhões de registros.

---

# EFCore.BulkExtensions

Biblioteca utilizada para inserção massiva.

Exemplo:

```csharp
await _context.BulkInsertAsync(
    lote,
    new BulkConfig
    {
        BatchSize = 10000,
        TrackingEntities = false
    }
);
```

---

# MySqlConnector

Driver utilizado para comunicação de alta performance com o MySQL.

---

# Conversões Implementadas

# Conversão de Datas

Formato ECD:

```txt
01042019
```

Convertido para:

```txt
01/04/2019
```

Utilizando:

```csharp
DateTime.TryParseExact()
```

---

# Conversão Decimal

Valores do ECD:

```txt
10,00
```

Convertidos para:

```txt
10.00
```

Utilizando:

```csharp
decimal.Parse()
```

---

# Conversão Boolean

Conversão dos valores:

* 0 / 1
* S / N

para:

* true / false

---

# Tratamento de Erros

O sistema possui tratamento para:

* Linhas inválidas
* Conversões incorretas
* Campos inconsistentes
* Blocos não mapeados
* Falhas de importação

Os erros são exibidos no console contendo:

* Linha processada
* Conteúdo da linha
* Mensagem da exceção

---

# Performance Obtida

Teste realizado:

```txt
20.837.133 registros importados
```

Tempo aproximado:

```txt
47 minutos
```

Após otimizações utilizando Bulk Insert e desativação de tracking, houve melhoria significativa de throughput e redução de overhead do Entity Framework Core.

---

# Como Rodar o Projeto

## Pré-requisitos

* .NET 8 SDK
* MySQL
* Visual Studio 2022 ou VS Code

---

# 1. Clonar o projeto

```bash
git clone "https://github.com/HalisonRoque/HRDigital.git"
```

---

# 2. Entrar na pasta do projeto

```bash
cd webApi
```

---

# 3. Restaurar dependências

```bash
dotnet restore
```

---

# 4. Instalar dependências adicionais

```bash
dotnet add package EFCore.BulkExtensions
```

---

# 5. Configurar Connection String

No arquivo:

```txt
appsettings.json
```

Configure:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SEU_DB;Uid=SEU_USUARIO;Pwd=SUA_SENHA;AllowLoadLocalInfile=true;DefaultCommandTimeout=300;"
}
```

---

# 6. Habilitar local_infile no MySQL

Execute:

```sql
SET GLOBAL local_infile = true;
```

Verifique:

```sql
SHOW GLOBAL VARIABLES LIKE 'local_infile';
```

---

# 7. Executar migrations

```bash
dotnet ef database update
```

---

# 8. Executar o projeto

```bash
dotnet run
```

---

# Swagger

Após executar:

```txt
https://localhost:5284/swagger
```

---

# Exemplo de Upload

Endpoint:

```http
POST /api/ecd/upload
```

Content-Type:

```txt
multipart/form-data
```

Campo esperado:

```txt
arquivo
```

---

# Objetivo do Projeto

O projeto foi desenvolvido para automatizar o processamento de arquivos ECD/SPED Contábil em larga escala, permitindo importações massivas de milhões de registros utilizando .NET 8, Entity Framework Core, MySQL e técnicas avançadas de otimização de performance.
