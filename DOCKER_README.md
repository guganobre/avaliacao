# Instruções para Docker

## Pré-requisitos
- Docker Desktop instalado
- Docker Compose instalado

## Como executar o projeto

### 1. Build e iniciar os containers
```bash
docker-compose up --build
```

### 2. Apenas iniciar os containers (sem rebuild)
```bash
docker-compose up
```

### 3. Executar em background (detached mode)
```bash
docker-compose up -d
```

### 4. Parar os containers
```bash
docker-compose down
```

### 5. Parar e remover volumes (limpar banco de dados)
```bash
docker-compose down -v
```

## Acessar a aplicação

- **API**: http://localhost:5000
- **Swagger**: http://localhost:5000/swagger
- **SQL Server**: localhost:1433
  - Usuário: `sa`
  - Senha: `Avaliacao@123`
  - Database: `AvaliacaoDB`

## Observações

- O SQL Server demora alguns segundos para inicializar. O healthcheck garante que a API só inicie quando o banco estiver pronto.
- As migrations são executadas automaticamente ao iniciar a API.
- Os dados do SQL Server são persistidos no volume `sqlserver-data`.

## Troubleshooting

### Se a API não conectar ao banco:
```bash
# Ver logs da API
docker logs avaliacao-api

# Ver logs do SQL Server
docker logs avaliacao-sqlserver

# Reiniciar apenas a API
docker-compose restart api
```

### Limpar tudo e recomeçar:
```bash
docker-compose down -v
docker system prune -f
docker-compose up --build
```
