FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["Logs.API/Logs.API.csproj", "Logs.API/"]
COPY ["Logs.Business/Logs.Business.csproj", "Logs.Business/"]
COPY ["Logs.Data/Logs.Data.csproj", "Logs.Data/"]
COPY ["Shared/Shared.Core/Shared.Core.csproj", "Shared/Shared.Core/"]
COPY ["Shared/Shared.Exceptions/Shared.Exceptions.csproj", "Shared/Shared.Exceptions/"]
COPY ["Shared/Shared.Messages/Shared.Messages.csproj", "Shared/Shared.Messages/"]
COPY ["Shared/Shared.Models/Shared.Models.csproj", "Shared/Shared.Models/"]
RUN dotnet restore "Logs.API/Logs.API.csproj"
COPY . .
COPY ["Logs.API/.", "./Logs.API/"]
COPY ["Logs.Business/.", "./Logs.Business/"]
COPY ["Logs.Data/.", "./Logs.Data/"]
COPY ["Shared/.", "./Shared/"]
WORKDIR "/src/Logs.API"
RUN dotnet build "Logs.API.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Logs.API.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Logs.API.dll"]