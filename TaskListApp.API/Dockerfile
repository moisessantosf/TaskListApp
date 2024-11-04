#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TaskListApp.API/TaskListApp.API.csproj", "TaskListApp.API/"]
COPY ["TaskListApp.Domain/TaskListApp.Domain.csproj", "TaskListApp.Domain/"]
COPY ["TaskListApp.Infrastructure/TaskListApp.Infrastructure.csproj", "TaskListApp.Infrastructure/"]
RUN dotnet restore "./TaskListApp.API/TaskListApp.API.csproj"
COPY . .
WORKDIR "/src/TaskListApp.API"
RUN dotnet build "./TaskListApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TaskListApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskListApp.API.dll"]