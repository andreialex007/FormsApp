# FormsApp

FormsApp is a sample form submission dashboard that combines an ASP.NET Core minimal API with a Vue 3 front end. The backend stores submissions in an in-memory Entity Framework Core database and exposes search/filter endpoints, while the frontend (built with Vite and Tailwind) lets you browse and manage those submissions.

## Tech Stack
- ASP.NET Core 8 minimal API + EF Core InMemory for the backend
- Vue 3, Vite, TailwindCSS, and MobX for the client
- PowerShell orchestration script to start both layers together

## Prerequisites
- .NET 8 SDK
- Node.js 20+ and npm
- PowerShell (Windows PowerShell 5+ or PowerShell 7)

## Running the app
1. Open a PowerShell window at the repository root (`C:\proj\FormsApp`).
2. Execute `.\run-all.ps1`.
   - The script stops anything already listening on ports 5173/5174, runs `dotnet run` for the backend, and starts `npm run dev` for the Vite frontend.
   - After a couple of seconds it launches your browser at http://localhost:5173/. The ASP.NET Core API (with Swagger UI) is served separately from `http://localhost:5026`.
3. Press Ctrl+C in the PowerShell window (or close either process window) to stop both apps when you're done.

> Tip: To run only one side, you can still use `dotnet run --project FormsApp/FormsApp.csproj` or `npm run dev --prefix ClientApp`, but the script keeps everything in sync.
