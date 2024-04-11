#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GardenHub.Api/src/Presentations/WebApi/WebApi.csproj", "GardenHub.Api/src/Presentations/WebApi/"]
COPY ["GardenHub.Api/src/Libraries/Core/Core.csproj", "GardenHub.Api/src/Libraries/Core/"]
COPY ["GardenHub.Api/src/Libraries/Models/Models.csproj", "GardenHub.Api/src/Libraries/Models/"]
COPY ["GardenHub.Api/src/Libraries/Services/Services.csproj", "GardenHub.Api/src/Libraries/Services/"]
COPY ["GardenHub.Api/src/Libraries/Data/Data.csproj", "GardenHub.Api/src/Libraries/Data/"]
RUN dotnet restore "./GardenHub.Api/src/Presentations/WebApi/./WebApi.csproj"
COPY . .
WORKDIR "/src/GardenHub.Api/src/Presentations/WebApi"
RUN dotnet build "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]