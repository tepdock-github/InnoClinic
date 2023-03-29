#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ServicesService/ServicesService.csproj", "ServicesService/"]
COPY ["ServicesService.Data/ServicesService.Data.csproj", "ServicesService.Data/"]
COPY ["ServicesService.Domain/ServicesService.Domain.csproj", "ServicesService.Domain/"]
RUN dotnet restore "ServicesService/ServicesService.csproj"
COPY . .
WORKDIR "/src/ServicesService"
RUN dotnet build "ServicesService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServicesService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServicesService.dll"]