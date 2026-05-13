# Todo Application - Full Stack Assessment
A full-stack Todo application built with a C# .NET 8 Web API and a React (Vite) frontend.

## Prerequisites
Before running the application, ensure you have the following installed on your machine:
* **.NET 8.0 SDK** (or later)
* **Node.js** (v18.0 or later)

## How to Run the Backend
The backend uses a lightweight SQLite database that will automatically create itself upon startup (zero configuration required).
1. Open a terminal and navigate to the backend API folder (e.g., `cd TodoApp.API`).
2. Run the application:
   ```bash
   dotnet run
   The API will start locally on http://localhost:5000.

## How to Run the Frontend
Open a new terminal window and navigate to the React frontend folder (e.g., cd todo-frontend).
Install the necessary dependencies:
`npm install`

Start the development server:
`npm run dev`

*The UI will be accessible at `http://localhost:5173`. Ensure the backend is running simultaneously.*

## Design Choices
The backend is structured using Clean Architecture principles, separating HTTP routing in the Controllers from the core business logic in the Service layer to ensure maintainability. Additionally, a custom global exception-handling middleware was implemented to guarantee that all API errors are caught and returned to the client gracefully and consistently.
