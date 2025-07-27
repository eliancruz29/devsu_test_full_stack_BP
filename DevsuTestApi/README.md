# DevsuTestApi

A Minimal API built with .NET 9, ready to run, test, and deploy with Docker.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (Preview/RC)
- [Docker](https://www.docker.com/get-started)

## Entity Framework Core Setup

1. **Install EF Core CLI tools (global):**

   ```sh
   dotnet tool install --global dotnet-ef --version 9.0.7
   ```

2. **Add EF Core packages (already included in `.csproj`):**
   - `Microsoft.EntityFrameworkCore`
   - `Microsoft.EntityFrameworkCore.SqlServer`
   - `Microsoft.EntityFrameworkCore.Tools`

3. **Create and apply migrations:**

   ```sh
   dotnet ef migrations add InitialCreate --project src/DevsuTestApi.csproj
   dotnet ef database update --project src/DevsuTestApi.csproj
   ```

## Build & Run Locally

```sh
dotnet build src/DevsuTestApi.csproj
dotnet run --project src/DevsuTestApi.csproj
```

## Run Tests

```sh
dotnet test ../DevsuTestApi.Tests/DevsuTestApi.Tests.csproj
```

## Build & Run with Docker

**Build the Docker image:**

```sh
docker build -t devsu-test-api .
```

**Run the Docker container:**

```sh
docker run -p 8080:80 devsu-test-api
```

Visit [http://localhost:8080](http://localhost:8080) to access the API.

## API Endpoints

- `GET /`  
  Returns: `Hello from DevsuTestApi!`

## Swagger

Swagger UI is available at `/swagger` in development mode.

---

**Project Structure**

- `src/` - API source code
- `Dockerfile` - Docker build instructions
- `.dockerignore` - Docker ignore file
- `../DevsuTestApi.Tests/` - Test project

---

**Local Setup**

1. Clone the repository:  
   `git clone https://github.com/yourusername/devsu_test_full_stack_BP.git`
2. Navigate to the project directory:  
   `cd devsu_test_full_stack_BP/DevsuTestApi`
3. Restore dependencies:  
   `dotnet restore`
4. Build the project:  
   `dotnet build`
5. Run the project:  
   `dotnet run --project src/DevsuTestApi.csproj`

The API should now be running on [http://localhost:5000](http://localhost:5000).
