FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SampleApps/Sample.ValidationWithRepository.Api/Sample.ValidationWithRepository.Api.csproj", "src/SampleApps/Sample.ValidationWithRepository.Api/"]
RUN dotnet restore "src/SampleApps/Sample.ValidationWithRepository.Api/Sample.ValidationWithRepository.Api.csproj"
COPY . .
WORKDIR "/src/src/SampleApps/Sample.ValidationWithRepository.Api"
RUN dotnet build "Sample.ValidationWithRepository.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sample.ValidationWithRepository.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.ValidationWithRepository.Api.dll"]
