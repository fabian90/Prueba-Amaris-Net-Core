# Establecer la imagen base de .NET
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Usar imagen base de construcción de .NET
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar el archivo de solución
COPY ["amaris.Api/amaris.Api.csproj", "amaris.Api/"]

# Restaurar las dependencias de NuGet
RUN dotnet restore "amaris.Api/amaris.Api.csproj"

# Copiar el resto del código
COPY . .

# Publicar la aplicación
WORKDIR "/src/amaris.Api"
RUN dotnet publish "amaris.Api.csproj" -c Release -o /app/publish

# Establecer la imagen final para ejecutar la aplicación
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "amaris.Api.dll"]