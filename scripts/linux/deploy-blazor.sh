#! /bin/bash

cd ~/BootstrapBlazor
git pull

curl https://www.blazor.zone/api/dispatch?token=BootstrapBlazor-Publish
dotnet build src/BootstrapBlazor.Server

curl https://www.blazor.zone/api/dispatch?token=BootstrapBlazor-Reboot
dotnet publish src/BootstrapBlazor.Server -c Release

systemctl stop ba.blazor
\cp -fr ~/BootstrapBlazor/src/BootstrapBlazor.Server/bin/Release/net10.0/publish/* /usr/local/ba/blazor
systemctl start ba.blazor
systemctl status ba.blazor -l --no-pager
