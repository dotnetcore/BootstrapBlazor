#! /bin/bash

echo "clean env"

sudo rm -fr BootstrapBlazor

sudo rm -fr /usr/local/ba

echo "apt update"
sudo apt update

echo "install git"
echo yes|sudo apt install git

echo "install BootstrapBlazor"
sudo git clone https://gitee.com/LongbowEnterprise/BootstrapBlazor.git

echo "make directory BA/Blazor"
sudo mkdir /usr/local/ba
sudo mkdir /usr/local/ba/blazor

echo "copy scripts"
sudo cp BootstrapBlazor/scripts/linux/deploy-blazor.sh deploy-blazor.sh
sudo cp BootstrapBlazor/scripts/linux/ba.blazor.service /usr/lib/systemd/system/ba.blazor.service

echo "install ba.blazor.service"
sudo systemctl enable ba.blazor

echo "install nginx"
echo yes|sudo apt install nginx

echo "copy nginx config"
sudo cp BootstrapBlazor/scripts/linux/nginx.conf /etc/nginx/

echo "copy cert"
sudo mkdir /etc/nginx/cert
sudo cp BootstrapBlazor/scripts/linux/cert/* /etc/nginx/cert/
