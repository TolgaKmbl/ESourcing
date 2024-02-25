#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ESourcing.Order/ESourcing.Order.csproj", "ESourcing.Order/"]
COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
COPY ["Ordering.Application/Ordering.Application.csproj", "Ordering.Application/"]
COPY ["Ordering.Domain/Ordering.Domain.csproj", "Ordering.Domain/"]
COPY ["Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Ordering.Infrastructure/"]
RUN dotnet restore "./ESourcing.Order/./ESourcing.Order.csproj"
COPY . .
WORKDIR "/src/ESourcing.Order"
RUN dotnet build "./ESourcing.Order.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ESourcing.Order.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESourcing.Order.dll"]