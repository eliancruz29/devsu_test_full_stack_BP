FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/DevsuTestApi.csproj", "src/"]
RUN dotnet restore "src/DevsuTestApi.csproj"
COPY src/. .
WORKDIR "/src"
RUN dotnet publish "DevsuTestApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DevsuTestApi.dll"]