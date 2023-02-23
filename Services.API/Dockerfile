FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["Services.API/Services.API.csproj", "Services.API/"]
COPY ["Services.Business/Services.Business.csproj", "Services.Business/"]
COPY ["Services.Data/Services.Data.csproj", "Services.Data/"]
COPY ["Shared/Shared.Core/Shared.Core.csproj", "Shared/Shared.Core/"]
COPY ["Shared/Shared.Exceptions/Shared.Exceptions.csproj", "Shared/Shared.Exceptions/"]
COPY ["Shared/Shared.Messages/Shared.Messages.csproj", "Shared/Shared.Messages/"]
COPY ["Shared/Shared.Models/Shared.Models.csproj", "Shared/Shared.Models/"]
RUN dotnet restore "Services.API/Services.API.csproj"
COPY ["Services.API/.", "./Services.API/"]
COPY ["Services.Business/.", "./Services.Business/"]
COPY ["Services.Data/.", "./Services.Data/"]
COPY ["Shared/.", "./Shared/"]
WORKDIR "/src/Services.API"
RUN dotnet build "Services.API.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Services.API.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.API.dll"]