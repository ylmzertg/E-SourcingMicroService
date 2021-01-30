#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["ESourcing.Product/ESourcing.Product.csproj", "ESourcing.Product/"]
RUN dotnet restore "ESourcing.Product/ESourcing.Product.csproj"
COPY . .
WORKDIR "/src/ESourcing.Product"
RUN dotnet build "ESourcing.Product.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESourcing.Product.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESourcing.Product.dll"]