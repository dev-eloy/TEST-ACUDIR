<div align="center">
  <img src="Assets/logo-acudir.png" alt="Logo Acudir" width="200"/>
</div>

# Acudir.Test - API

## Arquitectura

El proyecto realizado se encuentra estructurado usando los principios de Clean Architecture, separando las responsabilidades en capas:

```
Acudir.Test.sln
├── Acudir.Test.Apis/           # Capa de Presentación (Web API)
├── Acudir.Test.Domain/         # Capa de Dominio (Lógica de Negocio)
├── Acudir.Test.Infrastructure/ # Capa de Infraestructura (Datos)
└── Acudir.Test.Tests/          # Capa de Testing
```

### Patrones de Diseño Implementados

- **Repository Pattern**: Para abstracción de acceso a datos
- **Service Layer Pattern**: Para lógica de negocio
- **Dependency Injection**: Para inyección de dependencias
- **CQRS Light**: Separación de operaciones de lectura y escritura

## Funcionalidades

### Endpoints Disponibles

#### GET `/Persona/GetAll`
- **Descripción**: Obtiene todas las personas con filtros opcionales
- **Parámetros de Query**:
  - `nombre`: Filtro por nombre completo
  - `edad`: Filtro por edad exacta
  - `domicilio`: Filtro por domicilio
  - `telefono`: Filtro por teléfono
  - `profesion`: Filtro por profesión

**Ejemplo de uso:**
```
GET /Persona/GetAll?nombre=Juan&edad=30
```

#### POST `/Persona`
- **Descripción**: Agrega una nueva persona al sistema
- **Body**: Objeto Persona en formato JSON

#### PUT `/Persona`
- **Descripción**: Actualiza una persona existente
- **Body**: Objeto Persona en formato JSON

### Modelo de Datos

```json
{
  "NombreCompleto": "Juan Pérez",
  "Edad": 30,
  "Domicilio": "Av. Principal 123",
  "Telefono": "12345678",
  "Profesion": "Desarrollador"
}
```

## Estructura del Proyecto

### Acudir.Test.Domain
```
Domain/
├── Models/
│   └── Persona.cs                    # Modelo de dominio
├── Interfaces/
│   ├── IPersonaRepository.cs         # Interfaz del repositorio
│   ├── IPersonaService.cs            # Interfaz del servicio
│   └── IValidatorService.cs          # Interfaz del validador
├── Constants/
│   └── ValidationMessages.cs         # Mensajes de validación
├── Validators/
│   └── PersonaValidator.cs           # Validaciones de dominio
└── Models/DTOs/
    └── ServiceResult.cs              # DTOs para respuestas de servicio
```

### Acudir.Test.Infrastructure
```
Infrastructure/
├── Repositories/
│   └── PersonaRepository.cs          # Implementación del repositorio
├── Services/
│   ├── PersonaService.cs             # Implementación del servicio
│   └── ValidatorService.cs           # Implementación del validador
└── Data/
    └── Test.json                     # Archivo de datos
```

### Acudir.Test.Apis
```
APIs/
├── Controllers/
│   └── PersonaController.cs          # Controlador REST
├── Program.cs                         # Configuración de la aplicación
├── appsettings.json                  # Configuración general
└── appsettings.Development.json      # Configuración de desarrollo
```

### Acudir.Test.Tests
```
Tests/
├── UnitTests/
│   └── PersonaServiceTests.cs        # Tests unitarios del servicio
└── IntegrationTests/                 # Tests de integración (futuro)
```

## Compilar proyecto

### Prerrequisitos
- .NET 6
- Visual Studio 2022
- Docker (opcional)

### Opción 1: Visual Studio
1. Abrir `Acudir.Test.sln`
2. Establecer `Acudir.Test.Apis` como proyecto de inicio
3. Presionar F5 o Ctrl+F5

### Opción 2: Docker
```bash
# Construir la imagen
docker build -t acudir-test-api .

# Ejecutar el contenedor
docker run -d -p 8080:80 --name acudir-api acudir-test-api
```

### Opción 3: Docker Compose
```bash
# Ejecutar con docker-compose
docker-compose up -d

# Detener los servicios
docker-compose down
```

## Testing

### Ejecutar Tests Unitarios

1. En Visual Studio, Abrir **Ver** → **Explorador de pruebas**
2. Seleccionar los tests que desees ejecutar
3. Click derecho → **Ejecutar** o presionar **Ctrl+R, T**