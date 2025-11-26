@echo off
REM Kill any running instances of the Work Order Management System
echo Stopping Work Order Management System...
taskkill /F /IM WorkOrderManagementSystem.exe 2>nul
if %errorlevel% equ 0 (
    echo Application stopped successfully.
) else (
    echo No running instance found.
)
pause
