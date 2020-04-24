function runCmd ($cmd) {
    write-host $cmd -ForegroundColor Cyan
    cmd.exe /c $cmd
}
runCmd "dotnet build src\BootstrapBlazor.WebConsole --configuration Release"
runCmd "dotnet pack src\BootstrapBlazor --configuration Release"
