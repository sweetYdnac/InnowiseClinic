FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["Authorization.API/Authorization.API.csproj", "Authorization.API/"]
COPY ["Authorization.Business/Authorization.Business.csproj", "Authorization.Business/"]
COPY ["Authorization.Data/Authorization.Data.csproj", "Authorization.Data/"]
COPY ["Shared/Shared.Core/Shared.Core.csproj", "Shared/Shared.Core/"]
COPY ["Shared/Shared.Exceptions/Shared.Exceptions.csproj", "Shared/Shared.Exceptions/"]
COPY ["Shared/Shared.Models/Shared.Models.csproj", "Shared/Shared.Models/"]
RUN dotnet restore "Authorization.API/Authorization.API.csproj"
COPY ["Authorization.API/.", "./Authorization.API/"]
COPY ["Authorization.Business/.", "./Authorization.Business/"]
COPY ["Authorization.Data/.", "./Authorization.Data/"]
COPY ["Shared/.", "./Shared/"]
WORKDIR "/src/Authorization.API"
RUN dotnet build "Authorization.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authorization.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authorization.API.dll"]