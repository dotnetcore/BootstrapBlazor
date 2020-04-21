@echo off
if "%1" == "" (
    echo Please provider ProjectName like: "BootstrapBlazor Debug|Release"
    exit /B
)
set config=%2
if "%ProjectFolder%" == "" (
    set "ProjectFolder=%BB%"
)
if "%ProjectFolder%" == "" (
    echo Please set ProjectFolder evniroment Variables
    exit /B
)
if "%config%" == "" (
    set "config=Debug"
)
cd %ProjectFolder%\%1
dotnet pack -c %config% %ProjectFolder%\%1\
copy %ProjectFolder%\%1\bin\%config%\%1*.nupkg %NugetLib% /y
del %ProjectFolder%\%1\bin\%config%\%1*.nupkg
cd %NugetLib%
set config=
echo Ready to DELETE %USERPROFILE%\.nuget\packages\%1 /S /F /Q
pause

del %USERPROFILE%\.nuget\packages\%1 /S /F /Q
dir %NugetLib%\%1*.nupkg
