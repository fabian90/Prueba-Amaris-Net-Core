# Proyecto Prueba-Amaris-Net-Core
Este es un proyecto desarrollado en .NET 6, estructurado en **Arquitectura Limpia** y dividido en tres capas: API, Core e Infrastructure. Cada una de estas capas tiene una responsabilidad específica en la arquitectura del software.

## Estructura del Proyecto

- **Amaris.API**: 
  - Esta capa expone la interfaz RESTful del sistema. Maneja las solicitudes HTTP y coordina las interacciones entre la capa de presentación y la lógica de negocio.
  - **Tecnologías**: ASP.NET Core, Swagger, JWT para autenticación.
  
- **Amaris.Core**: 
  - Esta es la capa central de la aplicación que contiene las entidades y la lógica de negocio. Aquí se encuentran las definiciones de los modelos y las reglas de negocio.
  - **Tecnologías**: C#, AutoMapper para la proyección de modelos.

- **Amaris.Infrastructure**: 
 -Implementa la persistencia de datos y las integraciones externas (por ejemplo DynamoDB, etc.).
-También implementa los repositorios concretos y el patrón Unit of Work.
- **Tecnologías**: Entity Framework Core, Amazon DynamoDB SDK.
  
- **Amaris.Commons**:
-Contiene clases comunes y utilitarios compartidos por las otras capas, como configuraciones, constantes, helpers y estructuras base de respuesta (por ejemplo, paginación, validaciones genéricas, etc.).

## Patrones de Diseño

- **Patrones Creacionales**: 
  - **Singleton**: Utilizado para garantizar que una clase tenga una única instancia y proporcionar un punto de acceso global a ella.
  - **Factory Method**: Permite la creación de objetos sin especificar la clase concreta. Se utiliza para crear instancias de repositorios u otros servicios.
  - **Abstract Factory**: Proporciona una interfaz para crear familias de objetos relacionados o dependientes sin especificar sus clases concretas.

- **Patrones Estructurales**: 
  - **Repository**: Abstrae la lógica de acceso a datos y permite trabajar con diferentes fuentes de datos.
  - **Adapter**: Permite que interfaces incompatibles trabajen juntas, útil en la integración de servicios externos.
  - **Bridge**: Separa una interfaz de su implementación, permitiendo que ambas evolucionen de forma independiente.

- **Patrones Comportamentales**: 
  - **Mediator**: Permite la comunicación entre diferentes componentes sin que estos necesiten referirse directamente entre sí, útil en el manejo de eventos y comandos.
  - **Unit of Work**: Mantiene un registro de las operaciones en una transacción y asegura que todas las operaciones se completen exitosamente.

## Librerías y Versiones

Este proyecto utiliza las siguientes librerías:

- **Amaris.API**:
  - `AutoMapper.Extensions.Microsoft.DependencyInjection` - **Version**: 12.0.0
  - `FluentValidation.AspNetCore` - **Version**: 11.2.2
  - `Microsoft.AspNetCore.Authentication.JwtBearer` - **Version**: 6.0.33
  - `Microsoft.EntityFrameworkCore.Design` - **Version**: 6.0.33
  - `Microsoft.VisualStudio.Azure.Containers.Tools.Targets` - **Version**: 1.20.1
  - `Swashbuckle.AspNetCore` - **Version**: 6.2.3

- **Amaris.Core**:
  - `AutoMapper` - **Version**: 12.0.0
  - `FluentValidation` - **Version**: 11.3.0
  - `Microsoft.Extensions.Options` - **Version**: 6.0.0
  - `System.IdentityModel.Tokens.Jwt` - **Version**: 6.36.0

- **Amaris.Infrastructure**:
  - `AutoMapper.Extensions.Microsoft.DependencyInjection` - **Version**: 12.0.0
  - `Microsoft.EntityFrameworkCore` - **Version**: 6.0.10
  - `Microsoft.EntityFrameworkCore.SqlServer` - **Version**: 6.0.10
  - `Microsoft.EntityFrameworkCore.Tools` - **Version**: 6.0.10

## Requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Instalación

1. Clona este repositorio:
   ```bash
   git clone [https://github.com/fabian90/PruebaAmaris.git](https://github.com/fabian90/Prueba-Amaris-Net-Core.git)
   cd Amaris

