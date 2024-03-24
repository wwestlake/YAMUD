#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LagDaemon.YAMUD.WebAPI/LagDaemon.YAMUD.WebAPI.csproj", "LagDaemon.YAMUD.WebAPI/"]
COPY ["LagDaemon.YAMUD.API/LagDaemon.YAMUD.API.csproj", "LagDaemon.YAMUD.API/"]
COPY ["LagDaemon.YAMUD.Model/LagDaemon.YAMUD.Model.csproj", "LagDaemon.YAMUD.Model/"]
RUN dotnet restore "./LagDaemon.YAMUD.WebAPI/./LagDaemon.YAMUD.WebAPI.csproj"
COPY . .
WORKDIR "/src/LagDaemon.YAMUD.WebAPI"
RUN dotnet build "./LagDaemon.YAMUD.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LagDaemon.YAMUD.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Specify the network mode
# This line tells Docker to use the host's network stack
# allowing the container to access services on the host
CMD ["dotnet", "LagDaemon.YAMUD.WebAPI.dll", "--network", "host"]
ENTRYPOINT ["dotnet", "LagDaemon.YAMUD.WebAPI.dll"]