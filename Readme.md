# HR Digital - Importador ECD/SPED Contábil

## Descrição

Projeto desenvolvido em .NET 8 para importação e processamento de arquivos ECD/SPED Contábil.

O sistema realiza:

* Upload de arquivos `.txt`
* Leitura linha por linha do ECD
* Identificação dos blocos do SPED
* Conversão e tratamento dos dados
* Persistência das informações em banco MySQL

---

# Tecnologias Utilizadas

## Backend

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* Pomelo EntityFrameworkCore MySQL
* MySqlConnector

## Banco de Dados

* MySQL

## Manipulação de Arquivos

* IFormFile
* StreamReader

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

# Funcionamento da Importação

O sistema recebe um arquivo ECD via upload HTTP utilizando `IFormFile`.

Após o upload:

1. O arquivo é aberto utilizando `OpenReadStream()`
2. A leitura é feita linha por linha com `StreamReader`
3. Cada linha é separada utilizando `Split('|')`
4. O bloco SPED é identificado
5. Os dados são convertidos e mapeados
6. Os registros são persistidos no banco

Fluxo:

```txt
Upload HTTP
   ↓
IFormFile
   ↓
StreamReader
   ↓
ReadLineAsync()
   ↓
Split('|')
   ↓
Mapeamento dos blocos SPED
   ↓
Entity Framework Core
   ↓
MySQL
```

---

# Principais Tecnologias Explicadas

## IFormFile

Utilizado para receber arquivos enviados via HTTP.

Exemplo:

```csharp
public async Task ImportarArquivoAsync(IFormFile arquivo)
```

---

## StreamReader

Responsável pela leitura do arquivo texto.

Exemplo:

```csharp
using var stream = new StreamReader(arquivo.OpenReadStream());
```

---

## Entity Framework Core

ORM utilizado para persistência de dados.

Exemplo:

```csharp
await _context.ControleTributos.AddAsync(controle);
await _context.SaveChangesAsync();
```

---

## MySQL

Banco de dados utilizado para armazenamento das informações importadas.

---

# Conversões Implementadas

## Conversão de Datas

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

## Conversão Decimal

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

## Conversão Boolean

Conversão dos valores:

* 0 / 1
* S / N

para:

* true / false

---

# Tratamento de Erros

O sistema possui tratamento de exceções para:

* Linhas inválidas
* Conversões incorretas
* Campos inconsistentes
* Blocos não mapeados

Os erros são exibidos no console contendo:

* Bloco processado
* Linha completa
* Mensagem da exceção
* StackTrace

---

# Como Rodar o Projeto

## Pré-requisitos

* .NET 8 SDK
* MySQL
* Visual Studio 2022 ou VS Code

---

## 1. Clonar o projeto

```bash
git "LUGAR DO REPOSITÓRIO"
```

---

## 2. Entrar na pasta do projeto

```bash
cd webApi
```

---

## 3. Restaurar dependências

```bash
dotnet restore
```

---

## 4. Configurar Connection String

No arquivo `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=SEU_DB;user=SEU_USER;password=SUA_SENHA"
}
```

---

## 5. Executar migrations

```bash
dotnet ef database update
```

---

## 6. Executar o projeto

```bash
dotnet run
```

---

# Swagger

Após executar o projeto:

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

O projeto foi desenvolvido para automatizar a leitura e importação de arquivos ECD/SPED Contábil, transformando os registros do SPED em entidades persistidas em banco de dados relacional utilizando .NET 8 e Entity Framework Core.
