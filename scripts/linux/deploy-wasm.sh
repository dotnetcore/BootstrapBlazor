#! /bin/bash

cd ~/BootstrapBlazor
git pull

dotnet publish ~/BootstrapBlazor/src/Wasm/BootstrapBlazor.WebAssembly.ClientHost -c Release -o /usr/local/ba/wasm/
