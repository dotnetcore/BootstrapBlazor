#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /
COPY . .

WORKDIR "src/BootstrapBlazor.Server"
FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
ENV LANG=zh_CN.utf8
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BootstrapBlazor.Server.dll"]
