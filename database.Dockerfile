# Inicia a partir da imagem oficial da Microsoft
FROM mcr.microsoft.com/mssql/server:2022-latest

# Força a execução de todos os comandos subsequentes como o usuário root
USER root