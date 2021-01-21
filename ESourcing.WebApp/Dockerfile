#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ESourcing.WebApp/ESourcing.WebApp.csproj", "ESourcing.WebApp/"]
COPY ["../WebApp.Core/WebApp.Core.csproj", "../WebApp.Core/"]
COPY ["../WebApp.Infrastructure/WebApp.Infrastructure/WebApp.Infrastructure.csproj", "../WebApp.Infrastructure/WebApp.Infrastructure/"]
RUN dotnet restore "ESourcing.WebApp/ESourcing.WebApp.csproj"
COPY . .
WORKDIR "/src/ESourcing.WebApp"
RUN dotnet build "ESourcing.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESourcing.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESourcing.WebApp.dll"]
