#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ESourcing.Order/ESourcing.Order.csproj", "ESourcing.Order/"]
COPY ["../Order.Infrastructure/Ordering.Infrastructure.csproj", "../Order.Infrastructure/"]
COPY ["Order.Core/Ordering.Core.csproj", "Order.Core/"]
COPY ["Order.Application/Ordering.Application.csproj", "Order.Application/"]
COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
RUN dotnet restore "ESourcing.Order/ESourcing.Order.csproj"
COPY . .
WORKDIR "/src/ESourcing.Order"
RUN dotnet build "ESourcing.Order.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESourcing.Order.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESourcing.Order.dll"]
