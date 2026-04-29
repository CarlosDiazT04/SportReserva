# Usa la imagen oficial de ASP.NET Core como base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Usa la imagen del SDK de .NET para construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SportReserva.csproj", "."]
RUN dotnet restore "./SportReserva.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SportReserva.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SportReserva.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportReserva.dll"]