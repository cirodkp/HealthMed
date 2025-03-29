FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/HealthMed.Consumer/HealthMed.Consumer.csproj", "src/HealthMed.Consumer/"]
RUN dotnet restore "./src/HealthMed.Consumer/HealthMed.Consumer.csproj"
COPY . .
WORKDIR "/src/src/HealthMed.Consumer"
RUN dotnet build "./HealthMed.Consumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HealthMed.Consumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthMed.Consumer.dll"]