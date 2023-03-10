FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
RUN apt update
RUN apt install -y libgdiplus
RUN ln -s /usr/lib/libgdiplus.so /lib/x86_64-linux-gnu/libgdiplus.so
RUN apt-get install -y --no-install-recommends zlib1g fontconfig libfreetype6 libx11-6 libxext6 libxrender1 wget gdebi
RUN wget https://github.com/wkhtmltopdf/wkhtmltopdf/releases/download/0.12.5/wkhtmltox_0.12.5-1.stretch_amd64.deb
RUN gdebi --n wkhtmltox_0.12.5-1.stretch_amd64.deb
RUN ln -s /usr/local/lib/libwkhtmltox.so /usr/lib/libwkhtmltox.so
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["Appointments.Read.API/Appointments.Read.API.csproj", "Appointments.Read.API/"]
COPY ["Appointments.Read.Application/Appointments.Read.Application.csproj", "Appointments.Read.Application/"]
COPY ["Appointments.Read.Persistence/Appointments.Read.Persistence.csproj", "Appointments.Read.Persistence/"]
COPY ["Appointments.Read.Domain/Appointments.Read.Domain.csproj", "Appointments.Read.Domain/"]
COPY ["Shared/Shared.Core/Shared.Core.csproj", "Shared/Shared.Core/"]
COPY ["Shared/Shared.Exceptions/Shared.Exceptions.csproj", "Shared/Shared.Exceptions/"]
COPY ["Shared/Shared.Messages/Shared.Messages.csproj", "Shared/Shared.Messages/"]
COPY ["Shared/Shared.Models/Shared.Models.csproj", "Shared/Shared.Models/"]
RUN dotnet restore "Appointments.Read.API/Appointments.Read.API.csproj"
COPY ["Appointments.Read.API/.", "./Appointments.Read.API/"]
COPY ["Appointments.Read.Application/.", "./Appointments.Read.Application/"]
COPY ["Appointments.Read.Persistence/.", "./Appointments.Read.Persistence/"]
COPY ["Appointments.Read.Domain/.", "./Appointments.Read.Domain/"]
COPY ["Shared/.", "./Shared/"]
COPY ["Appointments.Read.Application/FileTemplates/PdfTemplate.html", "/PdfTemplate.html"]
WORKDIR "/src/Appointments.Read.API"
RUN dotnet build "Appointments.Read.API.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Appointments.Read.API.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["Appointments.Read.Application/FileTemplates/", "./Appointments.Read.Application/FileTemplates/"]
ENTRYPOINT ["dotnet", "Appointments.Read.API.dll"]