# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

# Restore the solution
WORKDIR /
COPY ["InstantCodeLab.Backend.sln", "."]
COPY ["src/", "src/"]
RUN dotnet restore "./InstantCodeLab.Backend.sln"

WORKDIR /src/InstantCodeLab.Api

RUN dotnet build "./InstantCodeLab.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InstantCodeLab.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV ASPNETCORE_URLS=http://*:8080/
WORKDIR /app
COPY --from=publish /app/publish .

COPY ["src/InstantCodeLab.Api/entrypoint.sh", "."]

RUN chmod +x entrypoint.sh
CMD ["./entrypoint.sh"]