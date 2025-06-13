# Usa a imagem base oficial do .NET 8.0 SDK para construir o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos do projeto e restaura as dependências
COPY . .
RUN dotnet restore

# Publica a aplicação para a pasta 'out'
RUN dotnet publish -c Release -o out

# Usa a imagem base oficial do ASP.NET 8.0 para executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "PostoUNICEUB.dll"]