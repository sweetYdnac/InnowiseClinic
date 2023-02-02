FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Offices.API/Offices.API.csproj", "Offices.API/"]
COPY ["Offices.Application/Offices.Application.csproj", "Offices.Application/"]
COPY ["Offices.Persistence/Offices.Persistence.csproj", "Offices.Persistence/"]
COPY ["Offices.Domain/Offices.Domain.csproj", "Offices.Domain/"]
COPY ["Shared/Shared.Core/Shared.Core.csproj", "Shared/Shared.Core/"]
COPY ["Shared/Shared.Exceptions/Shared.Exceptions.csproj", "Shared/Shared.Exceptions/"]
COPY ["Shared/Shared.Models/Shared.Models.csproj", "Shared/Shared.Models/"]
RUN dotnet restore "Offices.API/Offices.API.csproj"
COPY ["Offices.API/.", "./Offices.API/"]
COPY ["Offices.Application/.", "./Offices.Application/"]
COPY ["Offices.Persistence/.", "./Offices.Persistence/"]
COPY ["Offices.Domain/.", "./Offices.Domain/"]
COPY ["Shared/.", "./Shared/"]
WORKDIR "/src/Offices.API"
RUN dotnet build "Offices.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Offices.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Offices.API.dll"]