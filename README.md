# StopWatch Application

A simple Windows Forms stopwatch application written in C# (.NET 7).

What it does
- Provides a graphical stopwatch UI with Start, Pause, Resume, Reset and Stop controls.
- Keeps elapsed time using a small `StopwatchEngine` class that exposes elapsed seconds, a formatted HH:MM:SS string, and controls to Start/Pause/Resume/Reset/Stop.
- Includes unit tests (MSTest) that verify the engine's logic.

Prerequisites
- Windows OS (the app targets `net7.0-windows` and uses WinForms).
- .NET 7 SDK installed. Download from https://dotnet.microsoft.com if needed.
- (Optional) Visual Studio 2022/2023 or newer with .NET desktop workload for opening the solution and debugging.

Project structure (relevant files)
- `StopWatch.sln` — solution file.
- `StopWatchApplication/StopWatchApplication.csproj` — WinForms app project.
- `StopWatchApplication/StopWatchEngine.cs` — core timing logic.
- `StopWatchApplication/StopWatchForm.cs` — main UI form and event handlers.
- `StopWatchApplication/StopWatchApplication.cs` (Program entry point) — starts the WinForms app.
- `StopWatchApplication.Tests/StopWatchApplication.Tests.cs` — MSTest unit tests for the engine.

Build instructions (cmd.exe)
1. Open a Command Prompt in the repository root (where `StopWatch.sln` is located):

   cd C:\Users\ADMIN\StopWatch

2. Restore and build the solution:

   dotnet build StopWatch.sln

This will compile both the app and the tests. The GUI project's output will be placed under:

   StopWatchApplication\bin\Debug\net7.0-windows\

Run the application (two ways)
- Using dotnet run (quick):

  dotnet run --project StopWatchApplication\StopWatchApplication.csproj

  Note: `dotnet run` will launch the WinForms app directly. Ensure you run this from a Windows environment.

- Running the built executable (after `dotnet build`):

  C:\Users\ADMIN\StopWatch\StopWatchApplication\bin\Debug\net7.0-windows\StopWatchApplication.exe

  Double-clicking the exe in File Explorer will also run it.

Run the tests
- From the repository root (cmd.exe):

  dotnet test StopWatchApplication.Tests\StopWatchApplication.Tests.csproj

What to expect when running the app
- A window titled "Stopwatch Application - C# Assignment" opens.
- Large time display in HH:MM:SS format starting at 00:00:00.
- Buttons available: Start, Pause, Resume, Reset, Stop. Their enabled/disabled states change depending on stopwatch state.
- Status label shows current state (Ready, Running, Paused at XX:XX:XX, Stopped at XX:XX:XX).

Notes about implementation
- `StopwatchEngine` uses DateTime and TimeSpan to compute elapsed time, with logic to Start, Pause, Resume, Reset, and Stop.
- `GetFormattedTime()` computes hours/minutes/seconds using loops and returns a two-digit colon-separated string.
- `StopwatchForm` uses a System.Windows.Forms.Timer with a 100ms interval to update the UI while the engine runs.
- Tests use MSTest and `Thread.Sleep` to simulate passing time; the tests assert reasonable tolerances around elapsed times.

Troubleshooting
- If `dotnet build` fails: make sure the .NET 7 SDK is installed and that you are on Windows for the WinForms target.
- If the UI won't start using `dotnet run`, try building first then run the exe in the `bin\Debug\net7.0-windows` folder.
- Open `StopWatch.sln` in Visual Studio and press F5 for the easiest debugging experience.

Possible next steps (optional)
- Add a Release build section and packaging (MSIX/installer or single-file publish) if you want a distributable executable.
- Improve time formatting to include fractional seconds or add lap/split functionality.

License & attribution
- This README describes the local project; check project headers for author/license information if you plan to redistribute.

---
Generated on 2025-11-04 by a project assistant summarizing the repository contents and run steps.