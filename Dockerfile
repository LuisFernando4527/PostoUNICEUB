# Use a imagem base oficial do .NET
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copie e publique a aplicação
COPY . .
RUN dotnet publish -c Release -o out

# Crie a imagem final de execução
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "PostoUNICEUB.dll"]