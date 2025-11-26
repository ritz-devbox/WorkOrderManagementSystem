@echo off
REM Quick restart script
echo Restarting Work Order Management System...
echo.

REM Stop
echo [1/2] Stopping...
taskkill /F /IM WorkOrderManagementSystem.exe 2>nul
timeout /t 2 /nobreak >nul

REM Start
echo [2/2] Starting...
echo.
dotnet run
