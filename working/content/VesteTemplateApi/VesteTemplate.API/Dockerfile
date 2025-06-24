#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM vesteacr.azurecr.io/veste/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["VesteTemplate.Extensions/VesteTemplate.Extensions.csproj", "VesteTemplate.Extensions/"]
COPY ["VesteTemplate.Shared/VesteTemplate.Shared.csproj", "VesteTemplate.Shared/"]
COPY ["VesteTemplate.Tests/VesteTemplate.Tests.csproj", "VesteTemplate.Tests/"]
COPY ["VesteTemplate.Domain/VesteTemplate.Domain.csproj", "VesteTemplate.Domain/"]
COPY ["VesteTemplate.Application/VesteTemplate.Application.csproj", "VesteTemplate.Application/"]
COPY ["VesteTemplate.Infra.Data/VesteTemplate.Infra.Data.csproj", "VesteTemplate.Infra.Data/"]
COPY ["VesteTemplate.API/VesteTemplate.API.csproj", "VesteTemplate.API/"]
RUN dotnet restore "VesteTemplate.API/VesteTemplate.API.csproj"
COPY . .
WORKDIR "/src/VesteTemplate.API"
RUN dotnet build "VesteTemplate.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VesteTemplate.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VesteTemplate.API.dll"]

