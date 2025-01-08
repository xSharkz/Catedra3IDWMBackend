# Catedra3IDWMBackend

## Descripción

Catedra3IDWMBackend es un proyecto backend web desarrollado con ASP.NET Core, diseñado para gestionar la autenticación de usuarios, publicaciones y cargas de imágenes utilizando Cloudinary. Este proyecto implementa autenticación basada en roles, JWT para autorización segura y proporciona puntos finales para gestionar usuarios, publicaciones y archivos multimedia.

## Características

- **Registro de Usuario**: Registro seguro con validación de contraseñas.
- **Autenticación**: Autenticación basada en JWT para un inicio de sesión seguro.
- **Creación de Publicaciones**: Los usuarios pueden subir publicaciones con imágenes.
- **Integración con Cloudinary**: Las imágenes subidas se almacenan y sirven a través de Cloudinary.
- **Autorización basada en Roles**: Restringir el acceso a ciertos puntos finales según roles (por ejemplo, "Usuario").

## Configuración

1. Clona este repositorio:
    ```bash
    git clone https://github.com/xSharkz/Catedra3IDWMBackend
    ```

2. Instala las dependencias:
    ```bash
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
    dotnet add package Microsoft.IdentityModel.Tokens
    dotnet add package System.IdentityModel.Tokens.Jwt
    dotnet add package CloudinaryDotNet
    dotnet add package Microsoft.EntityFrameworkCore.Design
    ```

3. Actualiza el archivo `appsettings.json` con la conexión a la base de datos y la configuración de JWT.
    ```bash
    "Cloudinary": {
        "CloudName":"dt1g9zgsw",
        "ApiKey":"737464834781119",
        "ApiSecret":"pvSRjM0IHJdm3GFIWObi7DPM8pI"
    },
    "Jwt": {
        "Issuer": "http://localhost:5100",
        "Audience": "http://localhost:5100",
        "Secret": "Catedra3IDWMBackendClaveSecretaOMGLOLTieneQueSerLarga"
    },
    "ConnectionStrings": {
        "DefaultConnection": "Data Source=Catedra3IDWM.db"
    }
    ```
4. Aplica las migraciones:
    ```bash
    dotnet ef database update
    ```

5. Ejecuta la aplicación:
    ```bash
    dotnet run
    ```

## Autenticación

El proyecto utiliza JWT para la autenticación. Puedes obtener un token iniciando sesión en el punto final `/api/Auth/login`, luego úsalo para acceder a rutas protegidas incluyendo el token en el encabezado `Authorization` como `Bearer <tu_token>`.

## Endpoints

- **POST /api/Auth/login**: Iniciar sesión y obtener un token JWT.
- **POST /api/Posts**: Crear una nueva publicación (requiere el rol "Usuario").
- **GET /api/Posts**: Obtener todas las publicaciones.
- **POST /api/Upload**: Subir una imagen a Cloudinary.

## Tecnologías Usadas

- ASP.NET Core
- Entity Framework Core
- API de Cloudinary
- Autenticación JWT
