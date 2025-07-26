# DevsuTestApi

A Minimal API built with .NET 9, ready to run, test, and deploy with Docker.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (Preview/RC)
- [Docker](https://www.docker.com/get-started)

## Build & Run Locally

```sh
dotnet build src/DevsuTestApi.csproj
dotnet run --project src/DevsuTestApi.csproj
```

Visit [http://localhost:5000](http://localhost:5000) or [http://localhost:8080](http://localhost:8080) (Docker).

## Run Tests

```sh
dotnet test ../DevsuTestApi.Tests/DevsuTestApi.Tests.csproj
```

## Build & Run with Docker

```sh
docker build -t devsu-test-api .
docker run -p 8080:80 devsu-test-api
```

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
   `dotnet run`

The API should now be running on [http://localhost:5000](http://localhost:5000).
