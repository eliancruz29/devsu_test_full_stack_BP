# Minimal API Project

This project is a simple implementation of a Minimal API using .NET. It demonstrates how to set up a web application with minimal overhead and define API endpoints.

## Project Structure

```
DevsuTestApi
├── src
│   ├── Program.cs
│   └── Models
│       └── ExampleModel.cs
├── DevsuTestApi.sln
└── README.md
```

## Getting Started

### Prerequisites

- .NET SDK (latest version)

### Running the Project

1. Clone the repository or download the project files.
2. Navigate to the project directory in your terminal.
3. Run the following command to restore dependencies:

   ```
   dotnet restore
   ```

4. Start the application using:

   ```
   dotnet run --project src/DevsuTestApi.csproj
   ```

5. The API will be available at `http://localhost:5000` (or the port specified in your configuration).

### API Endpoints

- **GET /example**: Retrieves a list of ExampleModel items.
- **POST /example**: Creates a new ExampleModel item.

### Contributing

Feel free to submit issues or pull requests for improvements or bug fixes.

### License

This project is licensed under the MIT License.