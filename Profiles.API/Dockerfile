FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["Profiles.API/Profiles.API.csproj", "Profiles.API/"]
COPY ["Profiles.Business/Profiles.Business.csproj", "Profiles.Business/"]
COPY ["Profiles.Data/Profiles.Data.csproj", "Profiles.Data/"]
COPY ["Shared/Shared.Core/Shared.Core.csproj", "Shared/Shared.Core/"]
COPY ["Shared/Shared.Exceptions/Shared.Exceptions.csproj", "Shared/Shared.Exceptions/"]
COPY ["Shared/Shared.Models/Shared.Models.csproj", "Shared/Shared.Models/"]
RUN dotnet restore "Profiles.API/Profiles.API.csproj"
COPY ["Profiles.API/.", "./Profiles.API/"]
COPY ["Profiles.Business/.", "./Profiles.Business/"]
COPY ["Profiles.Data/.", "./Profiles.Data/"]
COPY ["Shared/.", "./Shared/"]
WORKDIR "/src/Profiles.API"
RUN dotnet build "Profiles.API.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Profiles.API.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Profiles.API.dll"]