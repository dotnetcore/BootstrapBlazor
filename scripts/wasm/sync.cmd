@echo off
if "%1" == "" (
    echo Please provider SolutionDir like: "$(MSBuildThisFileDirectory)"
    exit /B
)
if "%2" == "" (
    echo Please provider TargetDir like: "$(TargetDir)publish"
    exit /B
)

set sourceDir=%2\wwwroot
set targetDir=%1..\..\dist

echo "Ready to copy files to dist

echo xcopy %sourceDir%\*.* %targetDir% /y

xcopy %sourceDir%\*.* %targetDir% /E /R /Y

xcopy %targetDir%\404.html %targetDir%\index.html /R /Y

echo Sync success!
