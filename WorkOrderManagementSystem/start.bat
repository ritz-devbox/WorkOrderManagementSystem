@echo off
REM Clean start script - kills any running instance and starts fresh
echo ========================================
echo Work Order Management System
echo Clean Start Script
echo ========================================
echo.

REM Kill any existing instances
echo [1/3] Stopping any running instances...
taskkill /F /IM WorkOrderManagementSystem.exe 2>nul
if %errorlevel% equ 0 (
    echo       Previous instance stopped.
    timeout /t 2 /nobreak >nul
) else (
    echo       No previous instance found.
)

REM Build the application
echo.
echo [2/3] Building application...
dotnet build --nologo --verbosity quiet
if %errorlevel% neq 0 (
    echo       Build failed! Check errors above.
    pause
    exit /b 1
)
echo       Build successful.

REM Run the application
echo.
echo [3/3] Starting application...
echo.
echo ========================================
echo Application is starting...
echo Press Ctrl+C to stop the server
echo ========================================
echo.
dotnet run --no-build
