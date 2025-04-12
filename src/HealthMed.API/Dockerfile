FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/HealthMed.API/HealthMed.API.csproj", "src/HealthMed.API/"]
COPY ["src/HealthMed.API.IoC/HealthMed.API.IoC.csproj", "src/HealthMed.API.IoC/"]
COPY ["src/HealthMed.Application/HealthMed.Application.csproj", "src/HealthMed.Application/"]
COPY ["src/HealthMed.Domain/HealthMed.Domain.csproj", "src/HealthMed.Domain/"]

RUN dotnet restore "./src/HealthMed.API/HealthMed.API.csproj"
COPY . .

WORKDIR "/src/src/HealthMed.API"
RUN dotnet build "./HealthMed.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HealthMed.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthMed.API.dll"]