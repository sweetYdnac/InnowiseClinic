FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["Appointments.Write.API/Appointments.Write.API.csproj", "Appointments.Write.API/"]
COPY ["Appointments.Write.Application/Appointments.Write.Application.csproj", "Appointments.Write.Application/"]
COPY ["Appointments.Write.Persistence/Appointments.Write.Persistence.csproj", "Appointments.Write.Persistence/"]
COPY ["Appointments.Write.Domain/Appointments.Write.Domain.csproj", "Appointments.Write.Domain/"]
COPY ["Shared/Shared.Core/Shared.Core.csproj", "Shared/Shared.Core/"]
COPY ["Shared/Shared.Exceptions/Shared.Exceptions.csproj", "Shared/Shared.Exceptions/"]
COPY ["Shared/Shared.Messages/Shared.Messages.csproj", "Shared/Shared.Messages/"]
COPY ["Shared/Shared.Models/Shared.Models.csproj", "Shared/Shared.Models/"]
RUN dotnet restore "Appointments.Write.API/Appointments.Write.API.csproj"
COPY ["Appointments.Write.API/.", "./Appointments.Write.API/"]
COPY ["Appointments.Write.Application/.", "./Appointments.Write.Application/"]
COPY ["Appointments.Write.Persistence/.", "./Appointments.Write.Persistence/"]
COPY ["Appointments.Write.Domain/.", "./Appointments.Write.Domain/"]
COPY ["Shared/.", "./Shared/"]
WORKDIR "/src/Appointments.Write.API"
RUN dotnet build "Appointments.Write.API.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Appointments.Write.API.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Appointments.Write.API.dll"]