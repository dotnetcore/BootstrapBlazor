#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/BootstrapBlazor.Server/BootstrapBlazor.Server.csproj", "src/BootstrapBlazor.Server/"]
COPY ["src/BootstrapBlazor.Shared/BootstrapBlazor.Shared.csproj", "src/BootstrapBlazor.Shared/"]
COPY ["src/BootstrapBlazor/BootstrapBlazor.csproj", "src/BootstrapBlazor/"]
RUN dotnet restore "src/BootstrapBlazor.Server/BootstrapBlazor.Server.csproj"
COPY . .
WORKDIR "/src/src/BootstrapBlazor.Server"
RUN dotnet build "BootstrapBlazor.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BootstrapBlazor.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BootstrapBlazor.Server.dll"]