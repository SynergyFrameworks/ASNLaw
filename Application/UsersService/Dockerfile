#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["UsersService.csproj", ""]
COPY ["../../Domain/Scion.Domain.Users/Scion.Domain.Users.csproj", "../../Domain/Scion.Domain.Users/"]
COPY ["../Events/Scion.Events.csproj", "../Events/"]
COPY ["../../Infrastructure/Scion.Infrastructure.csproj", "../../Infrastructure/"]
COPY ["../../Persistence/Scion.Persistence.Users/Scion.Persistence.Users.csproj", "../../Persistence/Scion.Persistence.Users/"]
RUN dotnet restore "./UsersService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "UsersService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UsersService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UsersService.dll"]