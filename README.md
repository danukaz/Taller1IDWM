# Proyecto API REST - E-Commerce

## Descripción del proyecto

Este sistema es una API REST que gestiona un catálogo de productos para un sistema de comercio electrónico. La API permite crear, leer, actualizar y eliminar productos en la base de datos. Utiliza SQLite como motor de base de datos y Entity Framework Core para la gestión de las migraciones y el acceso a datos.

## Requisitos previos

Para ejecutar este sistema, necesitas tener instaladas las siguientes herramientas:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet)
- [SQLite](https://www.sqlite.org/download.html) (si quieres utilizar SQLite de manera local)
- [Visual Studio Code](https://code.visualstudio.com/) (o cualquier editor de código preferido)

## Instrucciones de instalación

Sigue estos pasos para clonar el repositorio y preparar tu entorno de desarrollo:

1. **Clonar el repositorio**:
   En tu terminal o consola, navega a la carpeta en la que quieras instalar el proyecto y ejecuta el siguiente comando para clonar el repositorio en tu máquina local:
   ```bash
   git clone https://github.com/danukaz/Taller1IDWM
   ```
2. **Restaurar las dependencias**:
   Una vez clonado el repositorio, ejecuta los siguiente comandos para restaurar las dependencias del proyecto:
   ```bash
   dotnet restore           # Restaura dependencias NuGet
   dotnet tool restore      # Restaura herramientras dotnet (como husky)
   ```
3. **Configurar la base de datos** (opcional):
   La base de datos se inicializa automáticamente al ejecutar la aplicación, pero si por algún motivo se produce un error o deseas inicializarla manualmente, puedes eliminar los archivos de la carpeta Src/Data/Migrations, y ejecutar el siguiente comando:
   ```bash
   dotnet ef migrations add InitialMigration -o Src/Data/Migrations
   ```

## Ejecutar y probar la API

Sigue estos pasos para ejecutar la API en tu entorno local:

1. **Iniciar la aplicación**:
   Ejecuta este comando en la raíz del proyecto:
   ```bash
   dotnet run
   ```
   En la terminal verás una URL, que es la que deberás usar para utilizar para probar la API, se verá algo así: `https://localhost:7279`
2. **Probar la API**:
   Para verificar que la API esté funcionando, puedes acceder a Postman (desde la aplicación o desde `https://www.postman.com/`) y probar los siguientes endpoints:
   - `GET https://localhost:7279/Product`
   - `GET https://localhost:7279/Product/{id}`


## Información de los autores
| Nombre completo                  | Correo                                | RUT           |
|----------------------------------|----------------------------------------|----------------|
| Daniel Alexis Tomigo Contreras  | daniel.tomigo@alumnos.ucn.cl          | 21.564.036-1   |
| Mayckol Enrique Olivares Donoso | mayckol.olivares@alumnos.ucn.cl       | 21.153.340-4   |
