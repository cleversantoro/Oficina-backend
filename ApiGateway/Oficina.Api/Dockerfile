# ====== build ======
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia csproj e restaura
COPY *.sln ./
COPY Oficina.Domain/Oficina.Domain.csproj ./Oficina.Domain/
COPY Oficina.Application/Oficina.Application.csproj ./Oficina.Application/
COPY Oficina.Infrastructure/Oficina.Infrastructure.csproj ./Oficina.Infrastructure/
COPY Oficina.Api.csproj ./Oficina.Api/
RUN dotnet restore ./Oficina.Api/Oficina.Api.csproj

# Copia o resto e compila
COPY Oficina.Domain ./Oficina.Domain
COPY Oficina.Application ./Oficina.Application
COPY Oficina.Infrastructure ./Oficina.Infrastructure
COPY . ./Oficina.Api
WORKDIR /src/Oficina.Api
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ====== runtime ======
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Oficina.Api.dll"]

# Imagem base para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
# Substitua pelo caminho do seu projeto principal
RUN dotnet publish Modules/Cadastro/Oficina.Cadastro.API/Oficina.Cadastro.API.csproj -c Release -o /app/publish

# Imagem base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Oficina.Cadastro.API.dll"]
