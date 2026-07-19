# Stage 1: Build and Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copy the solution file first
COPY ["TodoApp.sln", "./"]

# 2. Copy the project file into its correct subfolder
COPY ["TodoApp/TodoApp.csproj", "TodoApp/"]

# 3. Now restore dependencies
RUN dotnet restore "TodoApp.sln"

# 4. Copy the rest of the source code
COPY . .

# 5. Move into the project folder to publish
WORKDIR "/src/TodoApp"
RUN dotnet publish "TodoApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Tell Docker how to start the API
ENTRYPOINT ["dotnet", "TodoApp.dll"]
