FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime-base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS sdk-build
WORKDIR /src

# Копіюємо саме Api.csproj, але папка залишається EventsAPI
COPY EventsAPI/Api.csproj EventsAPI/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
COPY Contracts/Contracts.csproj Contracts/

# Тут теж міняємо на Api.csproj
RUN dotnet restore EventsAPI/Api.csproj

COPY . .
WORKDIR /src/EventsAPI
# І тут вказуємо Api.csproj для публікації
RUN dotnet publish Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM runtime-base AS final
WORKDIR /app
COPY --from=sdk-build /app/publish .
# Зверни увагу: компілятор .NET згенерує dll за назвою проекту, тобто Api.dll
ENTRYPOINT ["dotnet", "Api.dll"]