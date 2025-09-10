# syntax=docker/dockerfile:1.9

#############################
#  Base (runtime)
#############################
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app

# Porta única para simplificar em cloud
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

# Globalization (necessário para pt-BR funcionar corretamente)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
RUN apk add --no-cache icu-libs

#############################
#  Build
#############################
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copia só os csprojs para cache de restore
COPY FiapCloudGames.sln ./
COPY Api/*.csproj Api/
COPY Domain/*.csproj Domain/
COPY Infrastructure/*.csproj Infrastructure/
COPY CrossCutting/*.csproj CrossCutting/

# Restore com cache de pacotes
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore Api/Api.csproj

# Agora copia o resto do código
COPY . .

# Publica otimizado p/ container
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish Api/Api.csproj -c Release -o /app/publish \
      /p:UseAppHost=false /p:PublishSingleFile=false /p:DebugType=None

#############################
#  Final (imagem pequena)
#############################
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Api.dll"]
