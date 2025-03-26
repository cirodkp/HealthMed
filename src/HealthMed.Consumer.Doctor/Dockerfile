FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/HealthMed.Consumer.Doctor/HealthMed.Consumer.Doctor.csproj", "src/HealthMed.Consumer.Doctor/"]
RUN dotnet restore "./src/HealthMed.Consumer.Doctor/HealthMed.Consumer.Doctor.csproj"
COPY . .
WORKDIR "/src/src/HealthMed.Consumer.Doctor"
RUN dotnet build "./HealthMed.Consumer.Doctor.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HealthMed.Consumer.Doctor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthMed.Consumer.Doctor.dll"]