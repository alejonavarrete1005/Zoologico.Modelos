FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar proyectos
COPY Zoologico.API/Zoologico.API.csproj Zoologico.API/
COPY Zoologico.Modelo/Zoologico.Modelo.csproj Zoologico.Modelo/

# Restaurar dependencias
RUN dotnet restore Zoologico.API/Zoologico.API.csproj

# Copiar todo
COPY . .

# Build
RUN dotnet build Zoologico.API/Zoologico.API.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Zoologico.API/Zoologico.API.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zoologico.API.dll"]