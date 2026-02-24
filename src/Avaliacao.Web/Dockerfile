# Stage 1: Build dependencies
FROM node:20-alpine AS dependencies

WORKDIR /app

COPY package*.json ./

RUN npm ci

# Stage 2: Development server
FROM node:20-alpine

WORKDIR /app

# Copiar node_modules do stage anterior
COPY --from=dependencies /app/node_modules ./node_modules

# Copiar código fonte
COPY . .

# Instalar Angular CLI globalmente
RUN npm install -g @angular/cli

# Expor porta
EXPOSE 4200

# Comando para iniciar o servidor de desenvolvimento
CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "4200", "--poll", "2000"]
