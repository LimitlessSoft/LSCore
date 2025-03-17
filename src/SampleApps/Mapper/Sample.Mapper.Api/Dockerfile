FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SampleApps/Mapper/Sample.Mapper.Api/Sample.Mapper.Api.csproj", "src/SampleApps/Mapper/Sample.Mapper.Api/"]
RUN dotnet restore "src/SampleApps/Mapper/Sample.Mapper.Api/Sample.Mapper.Api.csproj"
COPY . .
WORKDIR "/src/src/SampleApps/Mapper/Sample.Mapper.Api"
RUN dotnet build "Sample.Mapper.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sample.Mapper.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.Mapper.Api.dll"]
