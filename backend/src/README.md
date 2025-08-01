# DevsuApi

A Minimal API built with .NET 9, ready to run, test, and deploy with Docker.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (Preview/RC)
- [Docker](https://www.docker.com/get-started)

**Local Setup:**

1. Clone the repository:  
   `git clone https://github.com/yourusername/devsu_test_full_stack_BP.git`
2. Navigate to the project directory:  
   `cd devsu_test_full_stack_BP/src/DevsuApi.Api`
3. Restore dependencies:  
   `dotnet restore`
4. Navigate to the ROOT directory:  
   `cd devsu_test_full_stack_BP`
5. Build & Run with Docker

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

**Create and apply migrations:**

   Navigate to DevsuApi.Api project and from there execute the commands below

   ```sh
   dotnet ef migrations add InitialCreate --project ./../DevsuApi.Infrastructure/DevsuApi.Infrastructure.csproj -o ./../DevsuApi.Infrastructure/Migrations
   dotnet ef database update --project ./../DevsuApi.Infrastructure/DevsuApi.Infrastructure.csproj -o ./../DevsuApi.Infrastructure/Migrations --verbose
   ```

**Run the Docker container:**

Visit [http://localhost:5002](http://localhost:5002) to access the API.

## Swagger

Swagger UI is available at `/swagger` in development mode.

---

**Project Structure:**

`` src/
      DevsuApi.Api/
         Middlewares/
         Program.cs
      DevsuApi.Domain/
         Entities/
         Enums/
         Interfaces/
         Repositories/
         Shared/
      DevsuApi.Features/
         [Feature]/
            Create/
            Get/
            GetListOf/
            PatchUpdate/
            Update/
            Delete/
      DevsuApi.Infrastructure/
         Extensions/
         Migrations/
         Persistence/
         Repositories/
         DependencyInjection.cs
      DevsuApi.SharedKernel/
         BaseEntity.cs
         IDomainEvent.cs
      tests/
         DevsuApi.UnitTests/
         DevsuApi.IntegrationTests/
   ``
