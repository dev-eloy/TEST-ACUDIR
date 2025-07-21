# Usar la imagen oficial de .NET 6 SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY ["Acudir.Test.Apis/Acudir.Test.Apis.csproj", "Acudir.Test.Apis/"]
COPY ["Acudir.Test.Domain/Acudir.Test.Domain.csproj", "Acudir.Test.Domain/"]
COPY ["Acudir.Test.Infrastructure/Acudir.Test.Infrastructure.csproj", "Acudir.Test.Infrastructure/"]
RUN dotnet restore "Acudir.Test.Apis/Acudir.Test.Apis.csproj"

# Copiar todo el código fuente
COPY . .
WORKDIR "/src/Acudir.Test.Apis"

# Compilar la aplicación
RUN dotnet build "Acudir.Test.Apis.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "Acudir.Test.Apis.csproj" -c Release -o /app/publish

# Imagen final más pequeña usando runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

# Crear directorio para datos
RUN mkdir -p /app/Data

# Copiar la aplicación publicada
COPY --from=publish /app/publish .

# Exponer el puerto 80
EXPOSE 80

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Acudir.Test.Apis.dll"]