FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# criar uma pasta dentro do container e copiar os arquivos
RUN mkdir /app
WORKDIR /app
COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o out

# build da aplicação
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# copiando o binario gerado para o container
COPY --from=build-env /app/out .

# setar as variaveis de ambiente
ENV tokem=tokem_bot
ENV opemAItokem=tokem_opem_ai

# iniciar a aplicação
ENTRYPOINT ["dotnet", "opemainet.dll"]
