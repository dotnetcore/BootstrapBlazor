#! /bin/bash

cd ~/BootstrapBlazor
curl https://www.blazor.zone/api/dispatch?token=BootstrapBlazor-Publish

git pull
dotnet build src/BootstrapBlazor.Server
dotnet publish src/BootstrapBlazor.Server -c Release
curl https://www.blazor.zone/api/dispatch?token=BootstrapBlazor-Reboot

systemctl stop ba.blazor
\cp -fr ~/BootstrapBlazor/src/BootstrapBlazor.Server/bin/Release/net8.0/publish/* /usr/local/ba/blazor
systemctl start ba.blazor
systemctl status ba.blazor -l --no-pager
