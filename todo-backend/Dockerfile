# Stage 1: Build and Publish
# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["TodoApp.sln", "./"]
RUN dotnet restore "TodoApp.sln"

# Copy the rest of the code and build the release
COPY . .
RUN dotnet publish "TodoApp.sln" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
# Use the much smaller ASP.NET runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Tell Docker how to start the API
ENTRYPOINT ["dotnet", "TodoApp.dll"]
