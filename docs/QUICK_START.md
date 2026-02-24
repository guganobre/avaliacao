# Guia de Início Rápido

## 🚀 Como Rodar o Projeto

### Opção 1: Docker Compose (Recomendado)

A forma mais rápida de executar o projeto completo:

```bash
# Clone o repositório
git clone https://github.com/guganobre/avaliacao.git
cd avaliacao

# Inicie os containers
docker-compose up -d

# Aguarde alguns segundos para o SQL Server inicializar
# A API estará disponível em: http://localhost:5000
```

**Swagger UI:** http://localhost:5000/swagger

### Opção 2: .NET CLI (Desenvolvimento Local)

#### Pré-requisitos:
- .NET 9 SDK
- SQL Server (local ou Docker)

#### Passos:

1. **Clone o repositório:**
```bash
git clone https://github.com/guganobre/avaliacao.git
cd avaliacao
```

2. **Configure a connection string:**

Edite `src/Avaliacao.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "SQLConnection": "Server=localhost;Database=AvaliacaoDB;User Id=sa;Password=SuaSenha;TrustServerCertificate=True;"
  }
}
```

3. **Restaure os pacotes:**
```bash
dotnet restore
```

4. **Execute as migrations:**
```bash
dotnet ef database update --project src/Avaliacao.Infrastructure --startup-project src/Avaliacao.API
```

5. **Execute a aplicação:**
```bash
dotnet run --project src/Avaliacao.API
```

6. **Acesse o Swagger:**
```
https://localhost:5001/swagger
```

### Opção 3: Visual Studio

1. Abra a solution `Avaliacao.sln`
2. Configure a connection string no `appsettings.json`
3. Defina `Avaliacao.API` como projeto de inicialização
4. Pressione F5 para executar

## 🧪 Executar Testes

### Via .NET CLI:
```bash
dotnet test
```

### Via Visual Studio:
- Abra o Test Explorer (Test > Test Explorer)
- Clique em "Run All Tests"

### Testes com Cobertura:
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## 📝 Testando a API

### 1. Via Swagger (Mais Fácil)

1. Execute a aplicação
2. Acesse: https://localhost:5001/swagger
3. Clique em "POST /api/seguro"
4. Clique em "Try it out"
5. Cole o JSON de exemplo:

```json
{
  "veiculo": {
    "marcaModelo": "Toyota Corolla 2024",
    "valor": 120000.00
  },
  "segurado": {
    "nome": "Maria Santos",
    "cpf": "987.654.321-00",
    "idade": 35
  }
}
```

6. Clique em "Execute"

### 2. Via cURL

```bash
curl -X POST "https://localhost:5001/api/seguro" \
  -H "Content-Type: application/json" \
  -d '{
    "veiculo": {
      "marcaModelo": "Honda Civic 2024",
      "valor": 100000.00
    },
    "segurado": {
      "nome": "João Silva",
      "cpf": "123.456.789-00",
      "idade": 30
    }
  }'
```

### 3. Via PowerShell

```powershell
$body = @{
    veiculo = @{
        marcaModelo = "Chevrolet Onix 2024"
        valor = 75000.00
    }
    segurado = @{
        nome = "Roberto Silva"
        cpf = "444.555.666-77"
        idade = 32
    }
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/seguro" `
  -Method Post `
  -Body $body `
  -ContentType "application/json"
```

## 🗄️ Gerenciamento do Banco de Dados

### Criar Nova Migration

```bash
dotnet ef migrations add NomeDaMigration --project src/Avaliacao.Infrastructure --startup-project src/Avaliacao.API
```

### Aplicar Migrations

```bash
dotnet ef database update --project src/Avaliacao.Infrastructure --startup-project src/Avaliacao.API
```

### Reverter Migration

```bash
dotnet ef database update NomeMigrationAnterior --project src/Avaliacao.Infrastructure --startup-project src/Avaliacao.API
```

### Remover Última Migration (se não aplicada)

```bash
dotnet ef migrations remove --project src/Avaliacao.Infrastructure --startup-project src/Avaliacao.API
```

## 🐛 Troubleshooting

### Problema: Erro de conexão com SQL Server

**Solução:**
1. Verifique se o SQL Server está rodando
2. Confirme a connection string
3. Teste a conexão:

```bash
sqlcmd -S localhost -U sa -P SuaSenha
```

### Problema: Migrations não aplicadas

**Solução:**
```bash
dotnet ef database update --project src/Avaliacao.Infrastructure --startup-project src/Avaliacao.API
```

### Problema: Porta já em uso

**Solução:**
Edite `src/Avaliacao.API/Properties/launchSettings.json` e altere as portas.

### Problema: Certificado SSL

**Solução:**
```bash
dotnet dev-certs https --trust
```

## 📚 Estrutura do Projeto

```
Avaliacao/
├── src/
│   ├── Avaliacao.API/
│   │   ├── Controllers/
│   │   │   └── SeguroController.cs
│   │   ├── Program.cs
│   │   └── appsettings.json
│   ├── Avaliacao.Application/
│   │   ├── Services/
│   │   │   └── SeguroCalculadoraService.cs
│   │   └── UseCases/
│   │       ├── CriarSeguroUseCase.cs
│   │       ├── ObterSeguroUseCase.cs
│   │       ├── ListarSegurosUseCase.cs
│   │       └── ObterRelatorioMediasUseCase.cs
│   ├── Avaliacao.Domain/
│   │   ├── DTOs/
│   │   │   ├── CriarSeguroRequest.cs
│   │   │   ├── SeguroResponse.cs
│   │   │   ├── RelatorioMediasResponse.cs
│   │   │   ├── VeiculoDto.cs
│   │   │   └── SeguradoDto.cs
│   │   ├── Entities/
│   │   │   ├── Seguro.cs
│   │   │   ├── Veiculo.cs
│   │   │   └── Segurado.cs
│   │   └── Interfaces/
│   │       └── Infrastructure/
│   │           ├── ISeguroRepository.cs
│   │           ├── IVeiculoRepository.cs
│   │           ├── ISeguradoRepository.cs
│   │           ├── IBaseRepository.cs
│   │           └── IUnitOfWork.cs
│   ├── Avaliacao.Infrastructure/
│   │   ├── Context/
│   │   │   ├── DbContextAvaliacao.cs
│   │   │   └── Configurations/
│   │   ├── Repositories/
│   │   │   ├── BaseRepository.cs
│   │   │   ├── SeguroRepository.cs
│   │   │   ├── VeiculoRepository.cs
│   │   │   └── SeguradoRepository.cs
│   │   └── Migrations/
│   ├── Avaliacao.Infrastructure.IoC/
│   │   └── InfrastructureConfiguration.cs
│   └── Avaliacao.Tests/
│       └── Application/
│           └── Services/
│               └── SeguroCalculadoraServiceTests.cs
├── docs/
│   ├── API_EXAMPLES.md
│   ├── IMPLEMENTATION_SUMMARY.md
│   └── QUICK_START.md
├── docker-compose.yml
└── README.md
```

## 🔧 Comandos Úteis

### Build
```bash
dotnet build
```

### Limpar
```bash
dotnet clean
```

### Restaurar Pacotes
```bash
dotnet restore
```

### Executar em Watch Mode
```bash
dotnet watch run --project src/Avaliacao.API
```

### Publicar
```bash
dotnet publish -c Release -o ./publish
```

## 🌐 Endpoints Disponíveis

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | /api/seguro | Criar novo seguro |
| GET | /api/seguro | Listar todos os seguros |
| GET | /api/seguro/{id} | Obter seguro por ID |
| GET | /api/seguro/relatorio/medias | Relatório de médias |

## 📖 Mais Informações

- [README.md](../README.md) - Visão geral do projeto
- [API_EXAMPLES.md](API_EXAMPLES.md) - Exemplos detalhados de uso da API
- [IMPLEMENTATION_SUMMARY.md](IMPLEMENTATION_SUMMARY.md) - Detalhes da implementação

## 💬 Suporte

Para dúvidas ou problemas:
1. Verifique a documentação
2. Consulte os exemplos
3. Abra uma issue no GitHub

---

**Desenvolvido com ❤️ usando .NET 9 e Clean Architecture**
