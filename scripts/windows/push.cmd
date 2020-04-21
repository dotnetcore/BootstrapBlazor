@echo off
call pack %1 Release
dotnet nuget push -k %NugetKey% -s https://api.nuget.org/v3/index.json %NugetLib%\%1.*.nupkg --skip-duplicate --no-symbols %NugetLib%\%1.*.snupkg
del %NugetLib%\%1*.nupkg /F /Q