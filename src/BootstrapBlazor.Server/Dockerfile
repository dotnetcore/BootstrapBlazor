#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

RUN apt-get update && apt-get install -y wget
RUN wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
RUN apt install -y ./google-chrome-stable_current_amd64.deb
RUN rm ./google-chrome-stable_current_amd64.deb
RUN apt install -y fonts-wqy-microhei

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /
COPY . .

# Generate LLM documentation
WORKDIR /tools/LlmsDocsGenerator
RUN dotnet run

WORKDIR "/src/BootstrapBlazor.Server"
FROM build AS publish
RUN dotnet build
RUN dotnet publish -c Release -o /app

FROM base AS final
ENV LANG=zh_CN.utf8
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BootstrapBlazor.Server.dll"]
