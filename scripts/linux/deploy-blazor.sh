#! /bin/bash

cd ~/BootstrapBlazor
git pull
dotnet publish src/BootstrapBlazor.WebConsole -c Release

systemctl stop ba.blazor
\cp -fr ~/BootstrapBlazor/src/BootstrapBlazor.WebConsole/bin/Release/net5.0/publish/* /usr/local/ba/blazor
systemctl start ba.blazor
systemctl status ba.blazor -l
