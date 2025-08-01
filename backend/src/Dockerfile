FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /tempSrc
COPY ["backend/src/.", "tempSrc/"]
RUN dotnet restore "tempSrc/DevsuApi.Api/DevsuApi.Api.csproj"

COPY backend/src/. .
WORKDIR "/tempSrc"
RUN dotnet publish "DevsuApi.Api/DevsuApi.Api.csproj" -c Release -o /app/publish

# Runtime stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DevsuApi.Api.dll"]