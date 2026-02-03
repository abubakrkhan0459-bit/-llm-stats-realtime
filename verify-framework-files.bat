@echo off
REM Pre-deployment verification script for LLM Stats Realtime (Windows)
REM This script ensures all framework files exist before deployment
REM Run this before: firebase deploy

echo.
echo 🔍 Pre-deployment Framework File Verification
echo ============================================
echo.

set "FRAMEWORK_DIR=publish\wwwroot\_framework"
set "ALL_GOOD=1"

REM Check if publish directory exists
if not exist "%FRAMEWORK_DIR%" (
    echo ❌ ERROR: Framework directory not found at %FRAMEWORK_DIR%
    echo Please run: dotnet publish first
    exit /b 1
)

echo ✓ Framework directory found

REM Check each required file
echo.
echo 🔧 Checking versioned framework files...

if exist "%FRAMEWORK_DIR%\blazor.webassembly.66stpp682q.js" (
    for %%F in ("%FRAMEWORK_DIR%\blazor.webassembly.66stpp682q.js") do (
        echo ✓ blazor.webassembly.66stpp682q.js exists (%%~zF bytes)
    )
) else (
    echo ❌ ERROR: blazor.webassembly.66stpp682q.js not found!
    set "ALL_GOOD=0"
)

if exist "%FRAMEWORK_DIR%\dotnet.runtime.2tx45g8lli.js" (
    for %%F in ("%FRAMEWORK_DIR%\dotnet.runtime.2tx45g8lli.js") do (
        echo ✓ dotnet.runtime.2tx45g8lli.js exists (%%~zF bytes)
    )
) else (
    echo ❌ ERROR: dotnet.runtime.2tx45g8lli.js not found!
    set "ALL_GOOD=0"
)

REM Check for non-versioned files (legacy support)
echo.
echo 🔧 Checking non-versioned file copies...

if exist "%FRAMEWORK_DIR%\blazor.webassembly.js" (
    echo ✓ blazor.webassembly.js exists (for legacy support)
) else (
    if exist "%FRAMEWORK_DIR%\blazor.webassembly.66stpp682q.js" (
        copy "%FRAMEWORK_DIR%\blazor.webassembly.66stpp682q.js" "%FRAMEWORK_DIR%\blazor.webassembly.js" >nul
        echo ✓ Created blazor.webassembly.js backup
    )
)

if exist "%FRAMEWORK_DIR%\dotnet.runtime.js" (
    echo ✓ dotnet.runtime.js exists (for legacy support)
) else (
    if exist "%FRAMEWORK_DIR%\dotnet.runtime.2tx45g8lli.js" (
        copy "%FRAMEWORK_DIR%\dotnet.runtime.2tx45g8lli.js" "%FRAMEWORK_DIR%\dotnet.runtime.js" >nul
        echo ✓ Created dotnet.runtime.js backup
    )
)

if exist "%FRAMEWORK_DIR%\dotnet.js" (
    echo ✓ dotnet.js exists (for legacy support)
) else (
    REM Find any dotnet.*.js file (excluding runtime and native)
    for %%F in ("%FRAMEWORK_DIR%\dotnet.*.js") do (
        echo %%~nF | findstr /V "runtime native" >nul && (
            copy "%%F" "%FRAMEWORK_DIR%\dotnet.js" >nul
            echo ✓ Created dotnet.js backup
            goto :dotnet_done
        )
    )
    :dotnet_done
)

echo.
if "%ALL_GOOD%"=="1" (
    echo ✅ All checks passed! Ready for deployment.
    echo Run: firebase deploy
    exit /b 0
) else (
    echo ❌ Some checks failed. Please fix the issues above.
    exit /b 1
)
