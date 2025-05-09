# Arquitectura Vertical Slice

Este repositorio proporciona una plantilla para crear aplicaciones utilizando la arquitectura Vertical Slice con .NET 9. Incluye implementaciones para CQRS, MediatR, manejo de FluentValidation, EF Core y el patrón de resultados.

## Tabla de contenido

- [Como Iniciar](#como-iniciar)
- [Descripción general de la arquitectura](#descripción-general-de-la-arquitectura)
- [Características](#características)
- [Tecnologias Usadas](#tecnologias-usadas)
<!-- - [Folder Structure](#folder-structure) -->

## Como Iniciar

siga estos sencillos pasos.

### Requisitos

- [Docker Desktop](https://www.docker.com)

### Instalación

1. Clonar Repositorio
   ```sh
   git clone https://github.com/king-beard/payment-api.git
   ```
2. Navegar al directorio del proyecto
   ```sh
   cd payment-api
   ```
3. Ejecutar comando
   ```sh
   docker compose up -d
   ```

## Descripción general de la arquitectura

Esta plantilla sigue la arquitectura de corte vertical, que organiza el código por características en lugar de por cuestiones técnicas. Cada característica es independiente, lo que promueve una alta cohesión y un bajo acoplamiento.

## Características

- **CQRS**: Command and Query Responsibility Segregation (Segregación de Responsabilidades de Comandos y Consultas).
- **MediatR Library**: Implementación del patrón mediador para manejar solicitudes y notificaciones.
- **Carter Library**: Biblioteca ligera para crear API HTTP.
- **FluentValidation Library**: Biblioteca de validación para .NET.
- **Entity Framework Core**: Object-Relational Mapper (ORM) que facilita la interacción con bases de datos relacionales desde aplicaciones .NET
- **Result Pattern**: Forma estandarizada de manejar los resultados de las operaciones.
- **Health Checks**: Enfoque estandarizado para monitorear y evaluar el estado operativo de los sistemas.

## Tecnologias Usadas

- **.NET 9**
- **CQRS**
- **MediatR**
- **Carter Library**
- **FluentValidation**
- **EF Core**
- **HealthChecks Library**

<!-- ## Folder Structure

- **/src**: Contains the main application code.
  - **/Features**: Each feature is organized into its own folder, promoting encapsulation.
    - **/Products**: Contains all product related files for the feature.
       - **/CreateProduct**:  Logic for creating a product.
       - **/DeleteProduct**: Logic for deleting a product.
       - **/GetProductById**: Logic for retrieving a product by its Id.
       - **/GetProducts**: Logic for retrieving a list of products.
       - **/UpdateProduct**: Logic for updating product details.
       - **ProductErrors**: Contains all product-related error handling.
    - **FeatureXController.cs**: Entry point for HTTP requests related to the feature.
  - **/Abstractions**: Contains shared interfaces and contracts.
     - **/CQRS**: Contains all CQRS abstraction interfaces.
     - **/Errors**: Define Error class.
     - **/ResultResponse**: Standardized response structures for API results.
  - **/Behaviors**: Contains middleware and behaviors that apply to requests and responses.
  - **/Database**: Contains database-related code, including DB Context.
  - **/Entities**: Defines the core data models used throughout the application.
  - **/Exceptions**: Contains the global exception handler for the application.
  - **/Extensions**: Contains extension methods for various classes and services.
  - **/Migrations**: Database migration files for schema updates.
  - **Program.cs**: Application entry point. -->