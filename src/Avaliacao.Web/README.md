# Seguro Veículos - Frontend Angular

Frontend moderno desenvolvido em Angular 21 para exibição de relatório de médias aritméticas de seguros de veículos.

## 📋 Descrição

Este projeto é uma aplicação frontend que exibe um relatório com as médias aritméticas dos cálculos de seguros de veículos. A aplicação consome dados de uma API REST e apresenta visualizações gráficas interativas dos dados estatísticos.

## 🚀 Tecnologias

- **Angular 21** - Framework principal
- **TypeScript** - Linguagem de programação
- **SCSS** - Pré-processador CSS
- **Standalone Components** - Arquitetura moderna do Angular
- **Signals** - Gerenciamento de estado reativo
- **RxJS** - Programação reativa
- **OnPush Change Detection** - Otimização de performance

## 🏗️ Estrutura do Projeto

```
src/app/
├── components/
│   └── relatorio-medias/          # Componente principal de relatório
│       ├── relatorio-medias.component.ts
│       ├── relatorio-medias.component.html
│       └── relatorio-medias.component.scss
├── models/                         # Interfaces e modelos TypeScript
│   ├── seguro.model.ts
│   ├── veiculo.model.ts
│   ├── segurado.model.ts
│   └── relatorio-medias.model.ts
├── services/
│   └── seguro.service.ts           # Serviço de comunicação com API
├── app.config.ts                   # Configuração da aplicação
├── app.routes.ts                   # Rotas da aplicação
└── app.ts                          # Componente raiz
```

## 🎯 Funcionalidades

- ✅ Exibição de relatório com médias aritméticas dos seguros
- ✅ Interface moderna e responsiva
- ✅ Integração com API REST
- ✅ Formatação de valores monetários e percentuais
- ✅ Change Detection otimizado com OnPush

## 📊 Cálculo do Seguro

O cálculo do seguro segue as seguintes fórmulas:

- **Taxa de Risco** = (Valor do Veículo × 5) / (2 × Valor do Veículo)
- **Prêmio de Risco** = Taxa de Risco × Valor do Veículo
- **Prêmio Puro** = Prêmio de Risco × (1 + MARGEM_SEGURANÇA)
- **Prêmio Comercial** = Prêmio Puro × (1 + LUCRO)
- **Valor do Seguro** = Prêmio Comercial

Onde:
- MARGEM_SEGURANÇA = 3%
- LUCRO = 5%

## 🔌 Integração com API

A aplicação consome dados de uma API REST localizada em:

```
http://localhost:5000/api/seguro
```

### Endpoints Utilizados

- `GET /api/seguro` - Retorna lista de seguros com todos os cálculos realizados

### Modelo de Dados

O serviço espera receber um array de objetos `Seguro` com a seguinte estrutura:

```typescript
{
  id: string;
  criadoEmUtc: string;
  veiculo: {
    valor: number;
    marcaModelo: string;
  };
  segurado: {
    nome: string;
    cpf: string;
    idade: number;
  };
  taxaRisco: number;
  premioRisco: number;
  premioPuro: number;
  premioComercial: number;
  valorSeguro: number;
}
```

## 🛠️ Instalação e Execução

### Pré-requisitos

- Node.js (versão 22 ou superior)
- npm (versão 11 ou superior)
- API backend rodando em `http://localhost:5000`

### Instalação

```bash
npm install
```

### Executar em desenvolvimento

```bash
ng serve
# ou
npm start
```

Acesse `http://localhost:4200/` no navegador.

A aplicação irá recarregar automaticamente quando você modificar os arquivos de código.

### Build para produção

```bash
ng build
```

Os arquivos compilados estarão em `dist/`. Por padrão, o build de produção otimiza a aplicação para performance e velocidade.

### Executar em modo watch

```bash
npm run watch
```

Compila o projeto e observa mudanças nos arquivos, recompilando automaticamente.

## 🧪 Testes

### Testes unitários

Para executar os testes unitários com [Vitest](https://vitest.dev/):

```bash
ng test
```

### Testes end-to-end

Para testes end-to-end (e2e):

```bash
ng e2e
```

O Angular CLI não vem com um framework de testes e2e por padrão. Você pode escolher um que atenda às suas necessidades.

## 📦 Scripts Disponíveis

- `npm start` - Inicia o servidor de desenvolvimento
- `npm run build` - Compila o projeto para produção
- `npm run watch` - Compila e observa mudanças
- `npm test` - Executa os testes unitários
- `ng generate component component-name` - Gera um novo componente

Para uma lista completa de schematics disponíveis (como `components`, `directives`, ou `pipes`), execute:

```bash
ng generate --help
```

## 🎨 Características da Interface

- Design moderno com gradientes e animações suaves
- Layout responsivo para diferentes tamanhos de tela
- Cards interativos com efeitos hover
- Gráficos interativos com tooltips formatados
- Formatação automática de valores monetários (BRL)
- Formatação de percentuais com 2 casas decimais

## 📚 Recursos Adicionais

Para mais informações sobre o uso do Angular CLI, incluindo referências detalhadas de comandos, visite a [página de Visão Geral e Referência de Comandos do Angular CLI](https://angular.dev/tools/cli).

## 🔧 Configuração

### Prettier

O projeto está configurado com Prettier para formatação de código:

- Largura máxima de linha: 100 caracteres
- Aspas simples habilitadas
- Parser Angular para arquivos HTML

### TypeScript

Configurações TypeScript estão em:
- `tsconfig.json` - Configuração base
- `tsconfig.app.json` - Configuração da aplicação
- `tsconfig.spec.json` - Configuração dos testes
