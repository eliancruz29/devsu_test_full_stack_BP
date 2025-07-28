FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["DevsuTestApi/src/DevsuTestApi.csproj", "src/"]
RUN dotnet restore "src/DevsuTestApi.csproj"

COPY DevsuTestApi/src/. .
WORKDIR "/src"
RUN dotnet publish "DevsuTestApi.csproj" -c Release -o /app/publish

# Runtime stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DevsuTestApi.dll"]