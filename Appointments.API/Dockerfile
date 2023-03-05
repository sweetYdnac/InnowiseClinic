FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["Appointments.API/Appointments.API.csproj", "Appointments.API/"]
COPY ["Appointments.Application/Appointments.Application.csproj", "Appointments.Application/"]
COPY ["Appointments.Persistence/Appointments.Persistence.csproj", "Appointments.Persistence/"]
COPY ["Appointments.Domain/Appointments.Domain.csproj", "Appointments.Domain/"]
COPY ["Shared/Shared.Core/Shared.Core.csproj", "Shared/Shared.Core/"]
COPY ["Shared/Shared.Exceptions/Shared.Exceptions.csproj", "Shared/Shared.Exceptions/"]
COPY ["Shared/Shared.Messages/Shared.Messages.csproj", "Shared/Shared.Messages/"]
COPY ["Shared/Shared.Models/Shared.Models.csproj", "Shared/Shared.Models/"]
RUN dotnet restore "Appointments.API/Appointments.API.csproj"
COPY ["Appointments.API/.", "./Appointments.API/"]
COPY ["Appointments.Application/.", "./Appointments.Application/"]
COPY ["Appointments.Persistence/.", "./Appointments.Persistence/"]
COPY ["Appointments.Domain/.", "./Appointments.Domain/"]
COPY ["Shared/.", "./Shared/"]
WORKDIR "/src/Appointments.API"
RUN dotnet build "Appointments.API.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Appointments.API.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Appointments.API.dll"]