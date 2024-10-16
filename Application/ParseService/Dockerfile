#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ParseService.csproj", "."]
COPY ["../../Domain/Domain.Parse/Domain.Parse.csproj", "../../Domain/Domain.Parse/"]
COPY ["../../Domain/ASNLikeOperator/ASNLikeOperator.csproj", "../../Domain/ASNLikeOperator/"]
COPY ["../../Infrastructure.Common/Infrastructure.Common.csproj", "../../Infrastructure.Common/"]
COPY ["../../Excel/ASNExcel/ASNExcel.csproj", "../../Excel/ASNExcel/"]
COPY ["../../ASNWord/ASNWord.csproj", "../../ASNWord/"]
COPY ["../../Datalayer/Datalayer/Datalayer.csproj", "../../Datalayer/Datalayer/"]
COPY ["../../ASNDomain/ASNDomain.Parse/DataModel.csproj", "../../ASNDomain/ASNDomain.Parse/"]
COPY ["../../ASNEvents/ASNEvents.csproj", "../../ASNEvents/"]
RUN dotnet restore "./ParseService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ParseService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ParseService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ParseService.dll"]