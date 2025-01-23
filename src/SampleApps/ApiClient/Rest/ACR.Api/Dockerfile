FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SampleApps/ApiClient/Rest/ACR.Api/ACR.Api.csproj", "src/SampleApps/ApiClient/Rest/ACR.Api/"]
RUN dotnet restore "src/SampleApps/ApiClient/Rest/ACR.Api/ACR.Api.csproj"
COPY . .
WORKDIR "/src/src/SampleApps/ApiClient/Rest/ACR.Api"
RUN dotnet build "ACR.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ACR.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ACR.Api.dll"]
