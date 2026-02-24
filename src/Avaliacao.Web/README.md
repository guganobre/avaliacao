# Seguro Veículos - Frontend Angular

Frontend moderno desenvolvido em Angular 21 para exibição de relatório de médias aritméticas de seguros de veículos.

## 📋 Descrição

Este projeto é uma aplicação frontend que exibe um relatório com as médias aritméticas dos cálculos de seguros de veículos. Os dados são mockados e incluem informações sobre seguros, veículos e segurados.

## 🚀 Tecnologias

- **Angular 21** - Framework principal
- **TypeScript** - Linguagem de programação
- **SCSS** - Pré-processador CSS
- **Standalone Components** - Arquitetura moderna do Angular
- **Signals** - Gerenciamento de estado reativo

## 🏗️ Estrutura do Projeto

```
src/app/
├── components/
│   └── relatorio-medias/     # Componente de relatório
├── models/                    # Interfaces e modelos
│   ├── seguro.model.ts
│   ├── veiculo.model.ts
│   ├── segurado.model.ts
│   └── relatorio-medias.model.ts
└── services/
    └── seguro.service.ts     # Serviço com dados mockados
```

## 🎯 Funcionalidades

- ✅ Exibição de relatório com médias aritméticas dos seguros
- ✅ Visualização em formato JSON
- ✅ Interface moderna e responsiva
- ✅ Dados mockados para demonstração

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

## 🛠️ Instalação e Execução

### Pré-requisitos

- Node.js (versão 22 ou superior)
- npm (versão 11 ou superior)

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

### Build para produção

```bash
ng build
```

Os arquivos compilados estarão em `dist/`.

## Development server

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests with the [Vitest](https://vitest.dev/) test runner, use the following command:

```bash
ng test
```

## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```

Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.

## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.
