FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Authorization.API/Authorization.API.csproj", "Authorization.API/"]
COPY ["Authorization.Core/Authorization.Business.csproj", "Authorization.Business/"]
COPY ["Authorization.Data/Authorization.Data.csproj", "Authorization.Data/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "Authorization.API/Authorization.API.csproj"
COPY . .
WORKDIR "/src/Authorization.API"
RUN dotnet build "Authorization.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authorization.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authorization.API.dll"]