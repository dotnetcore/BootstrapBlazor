#! /bin/bash

cd ~/BootstrapBlazor
git pull
dotnet restore --no-cache
dotnet publish src/BootstrapBlazor.Server -c Release

systemctl stop ba.blazor
\cp -fr ~/BootstrapBlazor/src/BootstrapBlazor.Server/bin/Release/net6.0/publish/* /usr/local/ba/blazor
systemctl start ba.blazor
systemctl status ba.blazor -l
