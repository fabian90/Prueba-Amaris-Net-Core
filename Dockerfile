# Etapa de construcción: Usa el SDK de .NET Core para compilar el proyecto
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Establece el directorio de trabajo dentro del contenedor
WORKDIR /app

# Copia el archivo .csproj del proyecto y restaura las dependencias
COPY amaris.Api/*.csproj ./amaris.Api/
COPY amaris.Core/*.csproj ./amaris.Core/
COPY amaris.Infrastructure/*.csproj ./amaris.Infrastructure/
COPY Commos/*.csproj ./Commos/

RUN dotnet restore amaris.Api/amaris.Api.csproj

# Copia todo el resto del código fuente al contenedor
COPY . ./

# Compila y publica la aplicación en modo Release
RUN dotnet publish amaris.Api/amaris.Api.csproj -c Release -o out

# Etapa de ejecución: Usa el runtime de .NET Core para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Establece el directorio de trabajo en la imagen final
WORKDIR /app

# Copia los archivos compilados de la etapa de construcción
COPY --from=build /app/out .

# Expone el puerto 80 donde la API estará disponible
EXPOSE 80

# Comando para iniciar la API cuando el contenedor se ejecute
ENTRYPOINT ["dotnet", "amaris.Api.dll"]
