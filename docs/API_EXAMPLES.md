# Exemplos de Uso da API

## Endpoints Disponíveis

### 1. Criar um Novo Seguro

**Endpoint:** `POST /api/seguro`

**Exemplo de Request:**

```bash
curl -X POST "https://localhost:5000/api/seguro" \
  -H "Content-Type: application/json" \
  -d '{
    "veiculo": {
      "marcaModelo": "Toyota Corolla 2024",
      "valor": 120000.00
    },
    "segurado": {
      "nome": "Maria Santos",
      "cpf": "987.654.321-00",
      "idade": 35
    }
  }'
```

**Exemplo de Response (201 Created):**

```json
{
  "id": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
  "criadoEmUtc": "2026-02-10T15:30:00Z",
  "veiculo": {
    "marcaModelo": "Toyota Corolla 2024",
    "valor": 120000.00
  },
  "segurado": {
    "nome": "Maria Santos",
    "cpf": "987.654.321-00",
    "idade": 35
  },
  "taxaRisco": 2.5,
  "premioRisco": 3000.00,
  "premioPuro": 3090.00,
  "premioComercial": 154.50,
  "valorSeguro": 154.50
}
```

---

### 2. Listar Todos os Seguros

**Endpoint:** `GET /api/seguro`

**Exemplo de Request:**

```bash
curl -X GET "https://localhost:5000/api/seguro"
```

**Exemplo de Response (200 OK):**

```json
[
  {
    "id": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
    "criadoEmUtc": "2026-02-10T15:30:00Z",
    "veiculo": {
      "marcaModelo": "Toyota Corolla 2024",
      "valor": 120000.00
    },
    "segurado": {
      "nome": "Maria Santos",
      "cpf": "987.654.321-00",
      "idade": 35
    },
    "taxaRisco": 2.5,
    "premioRisco": 3000.00,
    "premioPuro": 3090.00,
    "premioComercial": 154.50,
    "valorSeguro": 154.50
  },
  {
    "id": "b2c3d4e5-f6a7-8901-bcde-f12345678901",
    "criadoEmUtc": "2026-02-10T16:00:00Z",
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
]
```

---

### 3. Obter Seguro por ID

**Endpoint:** `GET /api/seguro/{id}`

**Exemplo de Request:**

```bash
curl -X GET "https://localhost:5000/api/seguro/a1b2c3d4-e5f6-7890-abcd-ef1234567890"
```

**Exemplo de Response (200 OK):**

```json
{
  "id": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
  "criadoEmUtc": "2026-02-10T15:30:00Z",
  "veiculo": {
    "marcaModelo": "Toyota Corolla 2024",
    "valor": 120000.00
  },
  "segurado": {
    "nome": "Maria Santos",
    "cpf": "987.654.321-00",
    "idade": 35
  },
  "taxaRisco": 2.5,
  "premioRisco": 3000.00,
  "premioPuro": 3090.00,
  "premioComercial": 154.50,
  "valorSeguro": 154.50
}
```

**Response quando não encontrado (404 Not Found):**

```json
{
  "error": "Seguro não encontrado"
}
```

---

### 4. Relatório de Médias

**Endpoint:** `GET /api/seguro/relatorio/medias`

**Exemplo de Request:**

```bash
curl -X GET "https://localhost:5000/api/seguro/relatorio/medias"
```

**Exemplo de Response (200 OK):**

```json
{
  "mediaValorVeiculo": 110000.00,
  "mediaTaxaRisco": 2.5,
  "mediaPremioRisco": 2750.00,
  "mediaPremioPuro": 2832.50,
  "mediaPremioComercial": 141.63,
  "mediaValorSeguro": 141.63,
  "totalSeguros": 2
}
```

---

## Testes com Diferentes Valores

### Exemplo 1: Veículo de R$ 50.000,00

```json
{
  "veiculo": {
    "marcaModelo": "Volkswagen Gol 2023",
    "valor": 50000.00
  },
  "segurado": {
    "nome": "Pedro Costa",
    "cpf": "111.222.333-44",
    "idade": 25
  }
}
```

**Valores Calculados:**
- Taxa de Risco: 2,5%
- Prêmio de Risco: R$ 1.250,00
- Prêmio Puro: R$ 1.287,50
- Prêmio Comercial: R$ 64,38
- **Valor do Seguro: R$ 64,38**

---

### Exemplo 2: Veículo de R$ 200.000,00

```json
{
  "veiculo": {
    "marcaModelo": "BMW X5 2024",
    "valor": 200000.00
  },
  "segurado": {
    "nome": "Ana Paula",
    "cpf": "555.666.777-88",
    "idade": 40
  }
}
```

**Valores Calculados:**
- Taxa de Risco: 2,5%
- Prêmio de Risco: R$ 5.000,00
- Prêmio Puro: R$ 5.150,00
- Prêmio Comercial: R$ 257,50
- **Valor do Seguro: R$ 257,50**

---

### Exemplo 3: Veículo de R$ 10.000,00 (Conforme exemplo do requisito)

```json
{
  "veiculo": {
    "marcaModelo": "Fiat Uno 2010",
    "valor": 10000.00
  },
  "segurado": {
    "nome": "Carlos Eduardo",
    "cpf": "999.888.777-66",
    "idade": 28
  }
}
```

**Valores Calculados:**
- Taxa de Risco: 2,5%
- Prêmio de Risco: R$ 250,00
- Prêmio Puro: R$ 257,50
- Prêmio Comercial: R$ 12,88
- **Valor do Seguro: R$ 12,88**

---

## Validações

### Erro: Valor do Veículo Inválido

**Request com valor zero ou negativo:**

```json
{
  "veiculo": {
    "marcaModelo": "Teste",
    "valor": 0
  },
  "segurado": {
    "nome": "Teste",
    "cpf": "000.000.000-00",
    "idade": 30
  }
}
```

**Response (400 Bad Request):**

```json
{
  "error": "Valor do veículo deve ser maior que zero."
}
```

---

## Testando via Swagger

1. Execute a aplicação:
   ```bash
   dotnet run --project src/Avaliacao.API
   ```

2. Acesse: `https://localhost:5000/swagger`

3. Explore os endpoints disponíveis interativamente

---

## Testando via Postman

1. Importe a coleção usando a URL do Swagger: `https://localhost:5000/swagger/v1/swagger.json`

2. Configure a variável de ambiente `baseUrl` para `https://localhost:5000`

3. Execute os requests da coleção

---

## Scripts de Teste Automatizado

### PowerShell Script

```powershell
# Criar um seguro
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

$response = Invoke-RestMethod -Uri "https://localhost:5000/api/seguro" -Method Post -Body $body -ContentType "application/json"

Write-Host "Seguro criado com ID: $($response.id)"
Write-Host "Valor do Seguro: R$ $($response.valorSeguro)"

# Obter relatório de médias
$relatorio = Invoke-RestMethod -Uri "https://localhost:5000/api/seguro/relatorio/medias" -Method Get

Write-Host "`nRelatório de Médias:"
Write-Host "Total de Seguros: $($relatorio.totalSeguros)"
Write-Host "Média Valor do Seguro: R$ $($relatorio.mediaValorSeguro)"
```

---

## Troubleshooting

### Erro de Conexão com Banco de Dados

Se encontrar erro de conexão, verifique:

1. SQL Server está rodando
2. Connection string está correta no `appsettings.json`
3. As migrations foram executadas:
   ```bash
   dotnet ef database update --project src/Avaliacao.Infrastructure --startup-project src/Avaliacao.API
   ```

### Erro de CORS

Se estiver consumindo a API de um frontend, certifique-se que o CORS está configurado no `Program.cs`.

---

## Integração com Aplicação Angular

### Configuração CORS

A API está configurada para aceitar requisições da aplicação Angular rodando em `http://localhost:4200`. A configuração de CORS no `Program.cs` permite:

- **Origem permitida:** `http://localhost:4200`
- **Métodos HTTP:** Todos (GET, POST, PUT, DELETE, etc.)
- **Cabeçalhos:** Todos

```csharp
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

### Executando a Aplicação Angular

1. **Navegue até o diretório da aplicação Angular:**
   ```bash
   cd angular-app
   ```

2. **Instale as dependências:**
   ```bash
   npm install
   ```

3. **Execute a aplicação:**
   ```bash
   ng serve
   ```

4. **Acesse no navegador:**
   ```
   http://localhost:4200
   ```

### URLs e Funcionalidades Disponíveis

#### Relatório de Médias
- **URL:** `http://localhost:4200/`
- Exibe estatísticas consolidadas:
  - Média do Valor do Veículo
  - Média da Taxa de Risco
  - Média do Prêmio de Risco
  - Média do Prêmio Puro
  - Média do Prêmio Comercial
  - Média do Valor do Seguro
  - Total de Seguros Cadastrados

### Exemplo de Consumo da API no Angular

#### Service de Seguros (TypeScript)

```typescript
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SeguroService {
  private apiUrl = 'http://localhost:5000/api/seguro';

  constructor(private http: HttpClient) { }

  // Listar todos os seguros
  listarSeguros(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // Criar novo seguro
  criarSeguro(seguro: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, seguro);
  }

  // Obter relatório de médias
  obterRelatorioMedias(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/relatorio/medias`);
  }

  // Obter seguro por ID
  obterSeguroPorId(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }
}
```

### Executando API e Angular Simultaneamente

Para testar a integração completa:

1. **Terminal 1 - Execute a API:**
   ```bash
   cd src/Avaliacao.API
   dotnet run
   ```
   A API estará disponível em: `http://localhost:5000`

2. **Terminal 2 - Execute o Angular:**
   ```bash
   cd angular-app
   ng serve
   ```
   A aplicação Angular estará disponível em: `http://localhost:4200`

3. **Acesse o navegador:**
   ```
   http://localhost:4200
   ```

### Fluxo de Trabalho Típico

1. **Analisar Médias:**
   - Acesse `http://localhost:4200/`
   - Visualize estatísticas consolidadas de todos os seguros

### Tratamento de Erros no Angular

A aplicação Angular está preparada para lidar com erros da API:

- **Erro de Conexão (CORS):** Verifica se a API está rodando e CORS configurado
- **Erro 400 (Bad Request):** Validações de formulário no frontend
- **Erro 404 (Not Found):** Mensagem quando seguro não é encontrado
- **Erro 500 (Server Error):** Mensagem genérica de erro do servidor

### Troubleshooting Angular

#### Erro de CORS
```
Access to XMLHttpRequest at 'http://localhost:5000/api/seguro' from origin 'http://localhost:4200' 
has been blocked by CORS policy
```

**Solução:**
- Verifique se a API está rodando em `http://localhost:5000`
- Confirme que o CORS está configurado no `Program.cs`
- Reinicie a API após configurar o CORS

#### Erro de Conexão
```
HttpErrorResponse: Failed to load resource: net::ERR_CONNECTION_REFUSED
```

**Solução:**
- Certifique-se que a API está rodando
- Verifique se a URL da API no Angular está correta (`http://localhost:5000/api/seguro`)

#### Erro de Build do Angular
```
Error: Cannot find module '@angular/core'
```

**Solução:**
```bash
npm install
