FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SampleApps/AuthRole/Sample.AuthRole.Api/Sample.AuthRole.Api.csproj", "src/SampleApps/AuthRole/Sample.AuthRole.Api/"]
RUN dotnet restore "src/SampleApps/AuthRole/Sample.AuthRole.Api/Sample.AuthRole.Api.csproj"
COPY . .
WORKDIR "/src/src/SampleApps/AuthRole/Sample.AuthRole.Api"
RUN dotnet build "Sample.AuthRole.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sample.AuthRole.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.AuthRole.Api.dll"]
