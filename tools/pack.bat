@echo off
setlocal enabledelayedexpansion

cd ../src/BootstrapBlazor 
dotnet pack -c Release
 
endlocal
