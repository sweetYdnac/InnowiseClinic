FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["Documents.API/Documents.API.csproj", "Documents.API/"]
COPY ["Documents.Business/Documents.Business.csproj", "Documents.Business/"]
COPY ["Shared/Shared.Core/Shared.Core.csproj", "Shared/Shared.Core/"]
COPY ["Shared/Shared.Exceptions/Shared.Exceptions.csproj", "Shared/Shared.Exceptions/"]
COPY ["Shared/Shared.Messages/Shared.Messages.csproj", "Shared/Shared.Messages/"]
COPY ["Shared/Shared.Models/Shared.Models.csproj", "Shared/Shared.Models/"]
RUN dotnet restore "Documents.API/Documents.API.csproj"
COPY ["Documents.API/.", "./Documents.API/"]
COPY ["Documents.Business/.", "./Documents.Business/"]
COPY ["Shared/.", "./Shared/"]
WORKDIR "/src/Documents.API"
RUN dotnet build "Documents.API.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Documents.API.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Documents.API.dll"]