# Librería Estudiante API

Este proyecto es una API desarrollada en **C#** con **ASP.NET Core**, que se conecta a una base de datos en **Microsoft SQL Server**. La aplicación y la base de datos están contenidas en **Docker** como servicios separados para facilitar su despliegue y gestión.

## Tecnologías Utilizadas

- **C#** (ASP.NET Core)
- **Microsoft SQL Server**
- **Docker & Docker Compose**
- **Entity Framework Core**

## Requisitos Previos

Asegúrate de tener instalado en tu máquina:

- [.NET SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/get-started)

## Instalación y Configuración

1. **Clona el repositorio**
   ```sh
   git clone https://github.com/oscarMolina1523/LibreriaEstudianteApi.git
   cd LibreriaEstudianteApi
   ```

2. **Configura el archivo `appsettings.json`** con los datos de conexión a la base de datos.

3. **Levanta los contenedores con Docker**
   ```sh
   docker-compose up -d
   ```

4. **Aplica las migraciones de la base de datos**
   ```sh
   dotnet ef database update
   ```

5. **Ejecuta la API**
   ```sh
   dotnet run
   ```

## Endpoints Principales

- `GET /api/libros` - Obtiene la lista de libros
- `POST /api/libros` - Agrega un nuevo libro
- `PUT /api/libros/{id}` - Actualiza un libro
- `DELETE /api/libros/{id}` - Elimina un libro

## Despliegue

Este proyecto puede desplegarse fácilmente utilizando Docker. Para detener los contenedores:
```sh
docker-compose down
```

## Autor

Desarrollado por **Oscar Danilo Molina**.

---

Si te resulta útil este proyecto, ¡no dudes en darle una estrella ⭐ en GitHub!

