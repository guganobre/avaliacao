# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["src/Avaliacao.API/Avaliacao.API.csproj", "Avaliacao.API/"]
COPY ["src/Avaliacao.Infrastructure.IoC/Avaliacao.Infrastructure.IoC.csproj", "Avaliacao.Infrastructure.IoC/"]
COPY ["src/Avaliacao.Infrastructure/Avaliacao.Infrastructure.csproj", "Avaliacao.Infrastructure/"]
COPY ["src/Avaliacao.Domain/Avaliacao.Domain.csproj", "Avaliacao.Domain/"]
COPY ["src/Avalicao.Application/Avaliacao.Application.csproj", "Avalicao.Application/"]

# Restore dependencies
RUN dotnet restore "Avaliacao.API/Avaliacao.API.csproj"

# Copy all source files
COPY src/Avaliacao.API/ Avaliacao.API/
COPY src/Avaliacao.Infrastructure.IoC/ Avaliacao.Infrastructure.IoC/
COPY src/Avaliacao.Infrastructure/ Avaliacao.Infrastructure/
COPY src/Avaliacao.Domain/ Avaliacao.Domain/
COPY src/Avalicao.Application/ Avalicao.Application/

# Build the application
WORKDIR "/src/Avaliacao.API"
RUN dotnet build "Avaliacao.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Avaliacao.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Avaliacao.API.dll"]
