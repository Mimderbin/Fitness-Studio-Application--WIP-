ARG BUILD_CONFIGURATION=Release
ARG APP_UID=1000

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION
WORKDIR /src
COPY ["FitUI/FitUI.csproj", "FitUI/"]
RUN dotnet restore "FitUI/FitUI.csproj"
COPY . .
WORKDIR "/src/FitUI"
RUN dotnet build "FitUI.csproj" -c $BUILD_CONFIGURATION -o /app/build --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION
WORKDIR "/src/FitUI"
RUN dotnet publish "FitUI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore

FROM base AS final
ARG APP_UID
RUN groupadd -g $APP_UID appgroup || true && useradd -u $APP_UID -g appgroup -m appuser || true
WORKDIR /app
COPY --from=publish /app/publish .
RUN chown -R appuser:appgroup /app
USER appuser
ENTRYPOINT ["dotnet", "FitUI.dll"]