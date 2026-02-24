# Sistema de Cálculo de Seguro de Veículos

## 📋 Descrição

Sistema completo para cálculo e gerenciamento de seguros de veículos, composto por:
- **Backend:** API REST em .NET 9 com Clean Architecture
- **Frontend:** Aplicação Angular para interface do usuário
- **Banco de Dados:** SQL Server com Entity Framework Core

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
├── angular-app/                    # Frontend Angular
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/        # Componentes da aplicação
│   │   │   ├── services/          # Serviços de comunicação com API
│   │   │   └── models/            # Interfaces TypeScript
│   │   └── assets/                # Recursos estáticos
│   └── package.json               # Dependências do Angular
└── docs/                           # Documentação
```

## 💡 Funcionalidades

### Endpoints da API (Backend)

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

### Funcionalidades da Aplicação Angular (Frontend)

- **Relatório de Médias:** Apresenta as médias dos cálculos dos seguros.

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
- Node.js 18+ e npm (para o frontend Angular)

### Executar o Projeto Completo

#### 1. Backend (API .NET)

**Via Docker Compose:**

```bash
docker-compose up -d
```

**Via .NET CLI:**

1. Configure a connection string no `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "SQLConnection": "Server=localhost;Database=AvaliacaoDB;User Id=sa;Password=SuaSenha;TrustServerCertificate=True;"
  }
}
```

2. Execute as migrations (automático no startup ou manual):
```bash
dotnet ef database update --project src/Avaliacao.Infrastructure --startup-project src/Avaliacao.API
```

3. Execute a aplicação:
```bash
cd src/Avaliacao.API
dotnet restore
dotnet run
```

4. Acesse o Swagger:
```
http://localhost:5000/swagger
```

#### 2. Frontend (Angular)

1. Instale as dependências:
```bash
cd angular-app
npm install
```

2. Execute a aplicação:
```bash
ng serve
```

3. Acesse a aplicação:
```
http://localhost:4200
```

### URLs Disponíveis

- **API Backend:** `http://localhost:5000`
- **Swagger (Documentação API):** `http://localhost:5000/swagger`
- **Frontend Angular:** `http://localhost:4200`

## 🎨 Frontend Angular

### Páginas Disponíveis

#### 1. Lista de Seguros (`/seguros`)
- Visualização de todos os seguros cadastrados em tabela
- Formatação de valores monetários (R$)
- Formatação de datas
- Navegação para criação de novo seguro

#### 2. Criar Novo Seguro (`/seguros/novo`)
- Formulário para cadastrar novo seguro
- Validação de campos obrigatórios
- Campos do veículo (Marca/Modelo, Valor)
- Campos do segurado (Nome, CPF, Idade)
- Cálculo automático pelo backend
- Redirecionamento após criação

#### 3. Relatório de Médias (`/relatorio/medias`)
- Exibição de estatísticas consolidadas
- Médias de todos os valores calculados
- Total de seguros cadastrados
- Cards informativos com formatação

### Componentes Principais

- **SeguroListComponent:** Listagem de seguros
- **SeguroFormComponent:** Formulário de criação
- **RelatorioMediasComponent:** Relatório estatístico
- **AppComponent:** Navegação e layout

### Serviços

- **SeguroService:** Comunicação com a API REST
  - `listarSeguros()` - GET /api/seguro
  - `criarSeguro()` - POST /api/seguro
  - `obterSeguroPorId()` - GET /api/seguro/{id}
  - `obterRelatorioMedias()` - GET /api/seguro/relatorio/medias

### Tecnologias Frontend

- **Angular 19+**
- **TypeScript 5.7+**
- **RxJS** (Observables)
- **HttpClient** (Requisições HTTP)
- **Angular Router** (Navegação)
- **FormsModule** (Formulários)

### Integração Backend-Frontend

O backend está configurado com CORS para aceitar requisições do Angular:

```csharp
// Program.cs
services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

**Fluxo de Dados:**
1. Usuário acessa `http://localhost:4200`
2. Angular faz requisição HTTP para `http://localhost:5000/api/seguro`
3. Backend processa, valida e calcula
4. Backend retorna JSON
5. Angular exibe na interface

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

### Backend:
- **.NET 9**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server**
- **Swagger/OpenAPI**
- **xUnit** (Testes)
- **Docker**

### Frontend:
- **Angular 19+**
- **TypeScript**
- **RxJS**
- **HttpClient**
- **Angular Router**
- **FormsModule**

## 📦 Padrões e Princípios

- ✅ **Clean Architecture**
- ✅ **SOLID Principles**
- ✅ **Repository Pattern**
- ✅ **Unit of Work Pattern**
- ✅ **Dependency Injection**
- ✅ **DTOs (Data Transfer Objects)** - Localizados na camada Domain
- ✅ **Use Cases Pattern**

## 📝 Modelo de Deployment

### Backend (API):

1. **Azure App Service**
   - Recomendado para produção
   - Suporte nativo a .NET 9
   - Escalabilidade automática

2. **Containers (Docker)**
   - Portabilidade
   - Facilidade de deploy
   - Kubernetes/Azure Container Apps

3. **IIS (Windows Server)**
   - Ambiente tradicional
   - Integração com infraestrutura Windows

### Frontend (Angular):

1. **Azure Static Web Apps**
   - Hospedagem otimizada para SPAs
   - CI/CD integrado com GitHub
   - CDN global

2. **Build de Produção**
   ```bash
   cd angular-app
   ng build --configuration production
   # Arquivos gerados em dist/
   ```

3. **Nginx/Apache**
   - Servidor web tradicional
   - Arquivos estáticos
   - Configuração de rotas SPA

## 🔧 Troubleshooting

### Erro de CORS no Frontend

Se aparecer erro de CORS no console do navegador:

```
Access to XMLHttpRequest has been blocked by CORS policy
```

**Solução:**
1. Verifique se a API está rodando em `http://localhost:5000`
2. Confirme que o CORS está configurado no `Program.cs`
3. Reinicie a API após configurar o CORS

### Erro de Conexão com API

**Sintomas:** Frontend não consegue conectar com backend

**Solução:**
1. Certifique-se que a API está rodando
2. Verifique se a URL no `SeguroService` está correta: `http://localhost:5000/api/seguro`
3. Teste a API diretamente no Swagger

### Banco de Dados não Inicializa

**Solução:**
1. Verifique se o SQL Server está rodando
2. Confirme a connection string no `appsettings.json`
3. Execute manualmente: `dotnet ef database update`

## 📚 Documentação Adicional

- **[API_EXAMPLES.md](docs/API_EXAMPLES.md)** - Exemplos de uso da API e integração Angular
- **[IMPLEMENTATION_SUMMARY.md](docs/IMPLEMENTATION_SUMMARY.md)** - Resumo da implementação backend
- **[ANGULAR_INTEGRATION.md](docs/ANGULAR_INTEGRATION.md)** - Documentação completa do frontend Angular

## 🎯 Checklist de Execução Rápida

### Para executar o sistema completo:

- [ ] 1. **SQL Server rodando**
- [ ] 2. **Backend (.NET):**
  ```bash
  cd src/Avaliacao.API
  dotnet run
  ```
  ✅ API em: `http://localhost:5000`

- [ ] 3. **Frontend (Angular):**
  ```bash
  cd angular-app
  npm install
  ng serve
  ```
  ✅ App em: `http://localhost:4200`

- [ ] 4. **Testar:**
  - Acesse `http://localhost:4200`
  - Acesse o relatório de médias

**Sistema pronto! 🚀**

## 👤 Autor

GitHub: [guganobre](https://github.com/guganobre/avaliacao)

## 📄 Licença

Este projeto foi desenvolvido como parte de uma avaliação técnica.
