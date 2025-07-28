# DevsuTestApi

A Minimal API built with .NET 9, ready to run, test, and deploy with Docker.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (Preview/RC)
- [Docker](https://www.docker.com/get-started)

## Build & Run with Docker

**Build the Docker image:**

   ```sh
   docker-compose build --no-cache
   docker-compose up -d
   ```

**If EF tools are missing:**

   ```sh
   dotnet tool install --global dotnet-ef
   export PATH="$PATH:/root/.dotnet/tools"
   ```

**Run the Docker container:**

Visit [http://localhost:5000](http://localhost:5000) to access the API.

**Create and apply migrations:**

   ```sh
   docker-compose exec api dotnet ef migrations add InitialCreate --project DevsuTestApi/src/DevsuTestApi.csproj
   docker-compose exec api dotnet ef database update --project DevsuTestApi/src/DevsuTestApi.csproj -v
   ```

## API Endpoints

- `GET /`  
  Returns: `Hello from DevsuTestApi!`

## Swagger

Swagger UI is available at `/swagger` in development mode.

---

**Project Structure:**

- `src/` - API source code
- `Dockerfile` - Docker build instructions
- `.dockerignore` - Docker ignore file
- `../DevsuTestApi.Tests/` - Test project

---

**Local Setup:**

1. Clone the repository:  
   `git clone https://github.com/yourusername/devsu_test_full_stack_BP.git`
2. Navigate to the project directory:  
   `cd devsu_test_full_stack_BP/DevsuTestApi`
3. Restore dependencies:  
   `dotnet restore`
4. Build & Run with Docker

The API should now be running on [http://localhost:5000](http://localhost:5000).
