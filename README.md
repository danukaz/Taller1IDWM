# Proyecto API REST - E-Commerce

## Descripción del proyecto

Este sistema es una API REST que gestiona un catálogo de productos para un sistema de comercio electrónico. La API permite crear, leer, actualizar y eliminar productos en la base de datos. Utiliza SQLite como motor de base de datos y Entity Framework Core para la gestión de las migraciones y el acceso a datos.

## Requisitos previos

Para ejecutar este sistema, necesitas tener instaladas las siguientes herramientas:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet)
- [SQLite](https://www.sqlite.org/download.html) (Opcional: si quieres utilizar SQLite de manera local)
- [Postman](https://www.postman.com/downloads/) (Opcional: Para probar la API)
- [Visual Studio Code](https://code.visualstudio.com/) (o cualquier editor de código preferido)

## Instrucciones de instalación

Sigue estos pasos para clonar el repositorio y preparar tu entorno de desarrollo:
s
1. **Clonar el repositorio**:
   En tu terminal o consola, navega a la carpeta en la que quieras instalar el proyecto y ejecuta el siguiente comando para clonar el repositorio en tu máquina local:
   ```bash
   git clone https://github.com/danukaz/Taller1IDWM
   ```
2. **Restaurar las dependencias**:
   Una vez clonado el repositorio, ejecuta los siguiente comandos para restaurar las dependencias del proyecto:
   ```bash
   dir Taller1IDWM          # Para cambiar a la carpeta raiz del proyecto
   dotnet restore           # Restaura dependencias NuGet
   dotnet tool restore      # Restaura herramientras dotnet (como husky)
   ```
3. **Configurar appsettings.json**
   En la carpeta raíz del proyecto, verás un archivo llamado `appsettings.example.json`. Por razones de seguridad, no se puede compartir el archivo original, pero se provee este archivo de ejemplo para que el usuario no pueda ver información sensible que se pueda colocar en este archivo. Deberás rellenar los siguientes campos en este archivo:
   ```json
   "JWT": {
     "SignInKey": "<tu-clave-secreta>",
     "Issuer": "http://localhost:7088",
     "Audience": "http://localhost:7088"
   },
   "CorsSettings": {
     "AllowedOrigins": [ "http://localhost:3000" ],
     "AllowedMethods": ["GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS"],
     "AllowedHeaders": ["Content-Type", "Authorization"]
   },
   "Cloudinary": {
     "CloudName": "<nombre-de-tu-nube>",
     "ApiKey": "<api-key>",
     "ApiSecret": "<api-secret>"
   },
   ```
5. **Configurar la base de datos** (opcional):
   La base de datos se inicializa automáticamente al ejecutar la aplicación, pero si por algún motivo se produce un error o deseas inicializarla manualmente, puedes eliminar los archivos de la carpeta Src/Data/Migrations, y ejecutar el siguiente comando:
   ```bash
   dotnet ef migrations add FinalMigration -o Src/Data/Migrations
   dotnet ef database update
   ```

## Ejecutar y probar la API

Sigue estos pasos para ejecutar la API en tu entorno local:

1. **Iniciar la aplicación**:
   Ejecuta este comando en la raíz del proyecto:
   ```bash
   dotnet run
   ```
   En la terminal verás una URL, se verá algo así: `https://localhost:7279`, ésta será necesaria para probar la API en Postman, como se explica en la siguiente sección.

---

## **Endpoints de la API BLACKCAT E-Commerce**

**Base URL:** `https://localhost:7279/api`

---

#### **1. Autenticación**
| Método | Endpoint               | Descripción                                  | Requiere Auth |
|--------|------------------------|----------------------------------------------|---------------|
| POST   | `/auth/register`       | Registra un nuevo usuario (rol "User" por defecto) | No            |
| POST   | `/auth/login`          | Inicia sesión y devuelve JWT                | No            |

---

#### **2. Usuarios**
| Método | Endpoint                     | Descripción                                  | Requiere Auth | Roles Permitidos |
|--------|------------------------------|----------------------------------------------|---------------|------------------|
| GET    | `/user`                      | Lista usuarios (paginado/filtrado)           | Sí            | Admin            |
| GET    | `/user/{email}`              | Obtiene usuario por email                    | Sí            | Admin            |
| PATCH  | `/user/{email}/status`       | Activa/desactiva usuario (excepto admins)    | Sí            | Admin            |
| POST   | `/user/address`              | Crea dirección de envío para el usuario actual | Sí            | User             |
| PUT    | `/user/address`              | Actualiza dirección de envío existente       | Sí            | User             |
| PATCH  | `/user/profile`              | Actualiza perfil (nombre, email, teléfono)   | Sí            | User             |
| PATCH  | `/user/profile/password`     | Cambia contraseña del usuario                | Sí            | User             |
| GET    | `/user/profile`              | Obtiene perfil del usuario autenticado       | Sí            | User             |

---

#### **3. Productos**
| Método | Endpoint                     | Descripción                                  | Requiere Auth | Roles Permitidos |
|--------|------------------------------|----------------------------------------------|---------------|------------------|
| GET    | `/product`                   | Lista productos (paginado/filtrado)          | No            | -                |
| GET    | `/product/{id}`              | Obtiene detalles de un producto              | No            | -                |
| POST   | `/product/create`            | Crea un nuevo producto (con imágenes)       | Sí            | Admin            |
| PUT    | `/product/{id}`              | Actualiza un producto existente              | Sí            | Admin            |
| DELETE | `/product/{id}`              | Elimina un producto (soft delete)            | Sí            | Admin            |

---

#### **4. Carrito**
| Método | Endpoint                     | Descripción                                  | Requiere Auth | Roles Permitidos |
|--------|------------------------------|----------------------------------------------|---------------|------------------|
| GET    | `/basket`                    | Obtiene el carrito del usuario               | Sí            | User             |
| POST   | `/basket?productId=X&quantity=Y` | Agrega producto al carrito               | Sí            | User             |
| DELETE | `/basket?productId=X&quantity=Y` | Elimina/Reduce cantidad en carrito       | Sí            | User             |

---

#### **5. Pedidos**
| Método | Endpoint                     | Descripción                                  | Requiere Auth | Roles Permitidos |
|--------|------------------------------|----------------------------------------------|---------------|------------------|
| POST   | `/order`                     | Crea un pedido desde el carrito              | Sí            | User             |
| GET    | `/order`                     | Obtiene historial de pedidos del usuario     | Sí            | User             |
| GET    | `/order/{id}`                | Obtiene detalles de un pedido específica     | Sí            | User             |
| PATCH  | `/order/{id}/status`         | Actualiza estado del pedido (Admin only)     | Sí            | Admin            |

---

### **Ejemplo de Uso (en Postman)**
**1. Registrar usuario:**
```http
POST https://localhost:7279/api/auth/register
Body: {
  "firstName": "Juan",
  "lastName": "Pérez",
  "email": "juan.perez@mail.com",
  "telephone": "987654321",
  "password": "Pa$$word2025",
  "confirmPassword": "Pa$$word2025"
}
```

**2. Obtener productos filtrados:**
```http
GET https://localhost:7279/api/product?brand=Nike&condition=New&pageSize=5
```

**3. Agregar al carrito (requiere JWT):**
```http
POST https://localhost:7279/api/basket?productId=5&quantity=2
Headers: Authorization: Bearer {token}
```

---


## Información de los autores
| Nombre completo                  | Correo                                | RUT           |
|----------------------------------|----------------------------------------|----------------|
| Daniel Alexis Tomigo Contreras  | daniel.tomigo@alumnos.ucn.cl          | 21.564.036-1   |
| Mayckol Enrique Olivares Donoso | mayckol.olivares@alumnos.ucn.cl       | 21.153.340-4   |
