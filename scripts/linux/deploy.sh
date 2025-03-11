#! /bin/bash
# wget https://raw.githubusercontent.com/dotnetcore/BootstrapBlazor/refs/heads/main/scripts/linux/deploy.sh

echo "*********************** clean env ***********************"
sudo rm -fr BootstrapBlazor
sudo rm -fr /usr/local/ba/blazor

echo "*********************** apt update ***********************"
sudo apt update

echo "*********************** install git ***********************"
yes|sudo apt install git

echo "*********************** install BootstrapBlazor ***********************"
sudo git clone https://gitee.com/LongbowEnterprise/BootstrapBlazor.git

echo "*********************** make directory BA/Blazor ***********************"
sudo mkdir /usr/local/ba
sudo mkdir /usr/local/ba/blazor

echo "*********************** copy scripts ***********************"
sudo cp BootstrapBlazor/scripts/linux/deploy-blazor.sh deploy-blazor.sh
sudo cp BootstrapBlazor/scripts/linux/ba.blazor.service /usr/lib/systemd/system/ba.blazor.service

echo "*********************** install ba.blazor.service ***********************"
sudo systemctl enable ba.blazor

echo "*********************** install nginx ***********************"
yes|sudo apt install nginx

echo "*********************** copy nginx config ***********************"
sudo cp BootstrapBlazor/scripts/linux/nginx.conf /etc/nginx/

echo "*********************** copy cert ***********************"
sudo mkdir /etc/nginx/cert
sudo cp BootstrapBlazor/scripts/linux/cert/* /etc/nginx/cert/

echo "*********************** install chrome ***********************"
wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
yes|sudo apt install ./google-chrome-stable_current_amd64.deb

echo "*********************** install support font ***********************"
sudo apt install fonts-wqy-microhei

echo "*********************** install DOTNET ***********************"
wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-9.0
