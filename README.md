# Sistema de Cálculo de Seguro de Veículos

## 📋 Descrição

API REST desenvolvida em .NET 9 seguindo princípios de Clean Architecture para cálculo e gerenciamento de seguros de veículos.

## 🏗️ Arquitetura

O projeto está organizado em camadas seguindo Clean Architecture:

```
Avaliacao/
├── src/
│   ├── Avaliacao.API/              # Camada de Apresentação (Controllers)
│   ├── Avaliacao.Application/      # Camada de Aplicação (Use Cases, Services)
│   ├── Avaliacao.Domain/           # Camada de Domínio (Entidades, DTOs, Interfaces)
│   ├── Avaliacao.Infrastructure/   # Camada de Infraestrutura (Repositórios, DbContext)
│   ├── Avaliacao.Infrastructure.IoC/ # Injeção de Dependências
│   └── Avaliacao.Tests/            # Testes Unitários
```

## 💡 Funcionalidades

### Endpoints da API

#### 1. Criar Seguro
**POST** `/api/seguro`

Cria um novo seguro calculando automaticamente todos os valores.

**Request Body:**
```json
{
  "veiculo": {
    "marcaModelo": "Honda Civic 2024",
    "valor": 100000.00
  },
  "segurado": {
    "nome": "João Silva",
    "cpf": "123.456.789-00",
    "idade": 30
  }
}
```

**Response:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "criadoEmUtc": "2026-02-10T14:30:00Z",
  "veiculo": {
    "marcaModelo": "Honda Civic 2024",
    "valor": 100000.00
  },
  "segurado": {
    "nome": "João Silva",
    "cpf": "123.456.789-00",
    "idade": 30
  },
  "taxaRisco": 2.5,
  "premioRisco": 2500.00,
  "premioPuro": 2575.00,
  "premioComercial": 128.75,
  "valorSeguro": 128.75
}
```

#### 2. Listar Todos os Seguros
**GET** `/api/seguro`

Retorna todos os seguros cadastrados com seus dados completos.

#### 3. Obter Seguro por ID
**GET** `/api/seguro/{id}`

Retorna os dados de um seguro específico.

#### 4. Relatório de Médias
**GET** `/api/seguro/relatorio/medias`

Retorna um relatório com as médias aritméticas de todos os seguros.

**Response:**
```json
{
  "mediaValorVeiculo": 75000.00,
  "mediaTaxaRisco": 2.5,
  "mediaPremioRisco": 1875.00,
  "mediaPremioPuro": 1931.25,
  "mediaPremioComercial": 96.56,
  "mediaValorSeguro": 96.56,
  "totalSeguros": 10
}
```

## 🧮 Fórmulas de Cálculo

O sistema utiliza as seguintes fórmulas para calcular o seguro:

**Constantes:**
- MARGEM_SEGURANÇA = 3%
- LUCRO = 5%

**Cálculos:**
1. **Taxa de Risco** = (Valor do Veículo × 5) / (2 × Valor do Veículo) = 2,5%
2. **Prêmio de Risco** = Taxa de Risco × Valor do Veículo
3. **Prêmio Puro** = Prêmio de Risco × (1 + MARGEM_SEGURANÇA)
4. **Prêmio Comercial** = LUCRO × Prêmio Puro
5. **Valor do Seguro** = Prêmio Comercial

**Exemplo com Valor do Veículo = R$ 10.000,00:**
- Taxa de Risco = 2,5%
- Prêmio de Risco = R$ 250,00
- Prêmio Puro = R$ 257,50
- Prêmio Comercial = R$ 12,88
- **Valor do Seguro = R$ 12,88**

## 🗄️ Banco de Dados

O projeto utiliza SQL Server com Entity Framework Core e Code-First Migrations.

**Entidades:**
- **Seguro**: Armazena o cálculo do seguro
- **Veículo**: Dados do veículo (Marca/Modelo, Valor)
- **Segurado**: Dados do segurado (Nome, CPF, Idade)

## 🚀 Como Executar

### Pré-requisitos
- .NET 9 SDK
- SQL Server ou Docker

### Via Docker Compose

```bash
docker-compose up -d
```

### Via .NET CLI

1. Configure a connection string no `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "SQLConnection": "Server=localhost;Database=AvaliacaoDB;User Id=sa;Password=SuaSenha;TrustServerCertificate=True;"
  }
}
```

2. Execute as migrations:
```bash
dotnet ef database update --project src/Avaliacao.Infrastructure --startup-project src/Avaliacao.API
```

3. Execute a aplicação:
```bash
dotnet run --project src/Avaliacao.API
```

4. Acesse o Swagger:
```
https://localhost:5001/swagger
```

## 🧪 Testes

O projeto inclui testes unitários automatizados para o serviço de cálculo de seguro.

### Executar os testes:

```bash
dotnet test src/Avaliacao.Tests/Avaliacao.Tests.csproj
```

### Cobertura de Testes:
- ✅ Cálculo de Taxa de Risco
- ✅ Cálculo de Prêmio de Risco
- ✅ Cálculo de Prêmio Puro
- ✅ Cálculo de Prêmio Comercial
- ✅ Cálculo completo do seguro
- ✅ Validações de entrada (valores zero e negativos)

## 🛠️ Tecnologias Utilizadas

- **.NET 9**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server**
- **Swagger/OpenAPI**
- **xUnit** (Testes)
- **Docker**

## 📦 Padrões e Princípios

- ✅ **Clean Architecture**
- ✅ **SOLID Principles**
- ✅ **Repository Pattern**
- ✅ **Unit of Work Pattern**
- ✅ **Dependency Injection**
- ✅ **DTOs (Data Transfer Objects)** - Localizados na camada Domain
- ✅ **Use Cases Pattern**

## 📝 Modelo de Deployment

### Opções de Hospedagem:

1. **Azure App Service**
   - Recomendado para produção
   - Suporte nativo a .NET
   - Escalabilidade automática

2. **Containers (Docker)**
   - Portabilidade
   - Facilidade de deploy
   - Kubernetes/Azure Container Apps

3. **IIS (Windows Server)**
   - Ambiente tradicional
   - Integração com infraestrutura Windows

## 👤 Autor

GitHub: [guganobre](https://github.com/guganobre/avaliacao)

## 📄 Licença

Este projeto foi desenvolvido como parte de uma avaliação técnica.
